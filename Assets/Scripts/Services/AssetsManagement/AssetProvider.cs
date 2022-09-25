using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Services.AssetsManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _cache = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _usedResources = new();

        public void Initialize()
        {
            Addressables.InitializeAsync();
        }

        public async Task<T> LoadAsync<T>(string address) where T : class
        {
            if (_cache.TryGetValue(address, out var completedHandle))
            {
                return (T) completedHandle.Result;
            }
            var handle = Addressables.LoadAssetAsync<T>(address);
            CacheOnCompleteWithCleanup(address, handle);
            return await handle.Task;
        }
        
        public async Task<T> LoadAsync<T>(AssetReference assetReference) where T : class
        {
            if (_cache.TryGetValue(assetReference.AssetGUID, out var completedHandle))
            {
                return (T) completedHandle.Result;
            }
            var handle = Addressables.LoadAssetAsync<T>(assetReference);
            CacheOnCompleteWithCleanup(assetReference.AssetGUID, handle);
            return await handle.Task;
        }
        
        public async Task<SceneInstance> LoadSceneAsync(string address)
        {
            if (_cache.TryGetValue(address, out var completedHandle))
            {
                return (SceneInstance) completedHandle.Result;
            }
            var handle = Addressables.LoadSceneAsync(address);
            CacheOnCompleteWithCleanup(address, handle);
            return await handle.Task;
        }

        public void Cleanup()
        {
            foreach (var resourceHandles in _usedResources.Values)
            {
                foreach (var handle in resourceHandles)
                {
                    Addressables.Release(handle);
                }
            }
            _cache.Clear();
            _usedResources.Clear();
        }

        private void CacheOnCompleteWithCleanup<T>(string key, AsyncOperationHandle<T> handle)
        {
            handle.Completed += completeHandle =>
            {
                _cache[key] = completeHandle;
            };
            RegisterForCleanup(key, handle);
        }

        private void RegisterForCleanup<T>(string guid, AsyncOperationHandle<T> handle)
        {
            if (!_usedResources.TryGetValue(guid, out var resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _usedResources[guid] = resourceHandles;
            }
            resourceHandles.Add(handle);
        }
    }
}