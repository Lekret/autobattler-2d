using System.Collections;
using Logic.ActionComponents;
using Logic.Characters;
using Services.CharacterSelector;
using UnityEngine;
using Zenject;

namespace Logic.Actions
{
    public class ArcherAction : CharacterAction
    {
        [SerializeField] private Character _character;
        [SerializeField] private ProjectileLauncher _projectileLauncher;
        [SerializeField] private AwaitableAnimation _awaitableAnimation;
        [SerializeField] private DamageDealer _damageDealer;
        [SerializeField] private Animator _animator;

        [Inject] private ICharacterSelector _characterSelector;
        
        private Character _target;
        private Coroutine _projectileRoutine;

        public override IEnumerator Execute()
        {
            _target = _characterSelector.GetSingle(_character.Team.Opposite());
            yield return _awaitableAnimation.Play(AnimHashes.Attack, ShootProjectile);
            yield return _projectileRoutine;
            _damageDealer.ApplyDamage(_target);
            _animator.Play(AnimHashes.Idle);
            _projectileRoutine = null;
            _target = null;
        }

        private void ShootProjectile()
        {
            _projectileRoutine = StartCoroutine(_projectileLauncher.LaunchAt(_target.transform.position));
        }
    }
}