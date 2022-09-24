using System.Collections;
using Logic.Characters;
using UnityEngine;

namespace Logic.Abilities
{
    public abstract class CharacterAbility : MonoBehaviour
    {
        public abstract IEnumerator Execute(Character character);
    }
}