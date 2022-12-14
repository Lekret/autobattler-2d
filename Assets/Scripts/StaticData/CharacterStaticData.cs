using UnityEngine;
using UnityEngine.AddressableAssets;

namespace StaticData
{
    [CreateAssetMenu(menuName = "Static Data/CharacterStaticData", fileName = "CharacterData")]
    public class CharacterStaticData : ScriptableObject
    {
        public string Id;
        public string Name;
        public int Hp;
        public AssetReferenceGameObject PrefabReference;
    }
}