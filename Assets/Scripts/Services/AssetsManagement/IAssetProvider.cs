using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Services.AssetsManagement
{
    public interface IAssetProvider
    {
        void Initialize();
        Task<T> Load<T>(string address) where T : class;
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        void Cleanup();
    }
}