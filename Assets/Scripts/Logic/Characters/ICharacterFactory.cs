using System.Threading.Tasks;
using StaticData;
using UnityEngine;

namespace Logic.Characters
{
    public interface ICharacterFactory
    {
        Task<Character> CreateAsync(CharacterStaticData data, Team team, Vector2 position);
    }
}