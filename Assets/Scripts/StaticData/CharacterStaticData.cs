using Logic.Characters;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "Static Data/CharacterStaticData", fileName = "CharacterData")]
    public class CharacterStaticData : ScriptableObject
    {
        public string Id;
        public int Hp;
        public Character Prefab;
    }
}