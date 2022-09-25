using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Services.AssetsManagement
{
    public interface IAssets
    {
        Task<T> Load<T>(AssetReference assetReference);
        void Cleanup();
    }
}