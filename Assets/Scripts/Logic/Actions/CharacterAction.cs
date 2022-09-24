using System.Collections;
using UnityEngine;

namespace Logic.Actions
{
    public abstract class CharacterAction : MonoBehaviour
    {
        public abstract IEnumerator Execute();
    }
}