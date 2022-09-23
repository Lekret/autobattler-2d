using System.Collections;
using Logic.Characters;
using UnityEngine;
using Zenject;

namespace Logic.Abilities
{
    public class MeleeAttack : CharacterAbility
    {
        [SerializeField] private Animator _animator;

        [Inject] private ICharacterCollection _characters;
        
        public override IEnumerator Execute()
        {
            yield break;
        }
    }
}