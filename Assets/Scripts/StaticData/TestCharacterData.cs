using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "Static Data/TestCharacterData", fileName = "TestCharacterData")]
    public class TestCharacterData : ScriptableObject
    {
        public CharacterStaticData[] Left;
        public CharacterStaticData[] Right;
    }
}