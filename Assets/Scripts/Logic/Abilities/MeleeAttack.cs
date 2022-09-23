using System.Collections;
using Logic.Characters;
using UnityEngine;
using Zenject;

namespace Logic.Abilities
{
    public class MeleeAttack : MonoBehaviour, ICharacterAbility
    {
        [SerializeField] private Animator _animator;

        [Inject] private ICharacterCollection _characters;
        
        public IEnumerator Execute()
        {
            yield break;
        }
    }
}