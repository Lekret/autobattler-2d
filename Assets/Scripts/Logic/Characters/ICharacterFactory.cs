using StaticData;
using UnityEngine;

namespace Logic.Characters
{
    public interface ICharacterFactory
    {
        Character Create(CharacterStaticData data, Team team, Vector3 position);
    }
}