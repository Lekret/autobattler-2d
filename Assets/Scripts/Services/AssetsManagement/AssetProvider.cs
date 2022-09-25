using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Services.AssetsManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _cache = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new();

        public void Initialize()
        {
            Addressables.InitializeAsync();
        }

        public async Task<T> Load<T>(string address) where T : class
        {
            if (_cache.TryGetValue(address, out var completedHandle))
            {
                return (T) completedHandle.Result;
            }
            var handle = Addressables.LoadAssetAsync<T>(address);
            return await RunWithCacheOnComplete(handle, address);
        }

        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (_cache.TryGetValue(assetReference.AssetGUID, out var completedHandle))
            {
                return (T) completedHandle.Result;
            }
            var handle = Addressables.LoadAssetAsync<T>(assetReference);
            return await RunWithCacheOnComplete(handle, assetReference.AssetGUID);
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
            _cache.Clear();
            _handles.Clear();
        }
        
        private Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += completeHandle =>
            {
                _cache[cacheKey] = completeHandle;
            };
            AddHandle(cacheKey, handle);
            return handle.Task;
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