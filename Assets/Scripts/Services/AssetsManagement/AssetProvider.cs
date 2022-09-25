using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Services.AssetsManagement
{
    public class AssetProvider : IAssets
    {
        private readonly Dictionary<string, AsyncOperationHandle> _cache = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new();

        public async Task<T> Load<T>(AssetReference assetReference)
        {
            if (_cache.TryGetValue(assetReference.AssetGUID, out var completedHandle))
            {
                return (T) completedHandle.Result;
            }
            
            var handle = Addressables.LoadAssetAsync<T>(assetReference);
            handle.Completed += h =>
            {
                _cache[assetReference.AssetGUID] = h;
            };

            AddHandle(assetReference.AssetGUID, handle);
            return await handle.Task;
        }

        public void Cleanup()
        {
            foreach (var resourceHandles in _handles.Values)
            {
                foreach (var handle in resourceHandles)
                {
                    Addressables.Release(handle);
                }
            }
        }

        private void AddHandle<T>(string guid, AsyncOperationHandle<T> handle)
        {
            if (!_handles.TryGetValue(guid, out var resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[guid] = resourceHandles;
            }
            resourceHandles.Add(handle);
        }
    }
}