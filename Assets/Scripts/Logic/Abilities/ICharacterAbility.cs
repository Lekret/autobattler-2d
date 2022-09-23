using System.Collections;
using UnityEngine;

namespace Logic.Abilities
{
    public abstract class CharacterAbility : MonoBehaviour
    {
        public abstract IEnumerator Execute();
    }
}