using System.Collections;
using Logic.Actions.Components;
using Logic.Characters;
using Services.CharacterStorage;
using UnityEngine;
using Zenject;

namespace Logic.Actions
{
    public class ArcherAction : CharacterAction
    {
        [SerializeField] private float _attackDelay;
        [SerializeField] private Character _character;
        [SerializeField] private ProjectileLauncher _projectileLauncher;
        [SerializeField] private AwaitableAnimation _awaitableAnimation;
        [SerializeField] private DamageDealer _damageDealer;
        [SerializeField] private Animator _animator;

        [Inject] private ICharacterStorage _characterStorage;
        
        private Character _target;

        public override IEnumerator Execute()
        {
            _target = _characterStorage.GetRandom(_character.Team.Opposite());
            var animCor = StartCoroutine(_awaitableAnimation.Play(AnimHashes.Attack));
            yield return new WaitForSeconds(_attackDelay);
            var projectileRoutine = StartCoroutine(_projectileLauncher.LaunchAt(_target.transform.position));
            yield return projectileRoutine;
            _damageDealer.ApplyDamage(_target);
            yield return animCor;
            _animator.Play(AnimHashes.Idle);
            _target = null;
        }
    }
}