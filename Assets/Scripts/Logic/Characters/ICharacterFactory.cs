using UnityEngine;

namespace Logic.Characters
{
    public interface ICharacterFactory
    {
        Character Create(string id, Team team, Vector3 position);
    }
}