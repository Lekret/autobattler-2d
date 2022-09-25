using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Services.AssetsManagement
{
    public interface IAssetProvider
    {
        void Initialize();
        Task<T> LoadAsync<T>(string address) where T : class;
        Task<T> LoadAsync<T>(AssetReference assetReference) where T : class;
        Task<SceneInstance> LoadSceneAsync(string address);
        void Cleanup();
    }
}