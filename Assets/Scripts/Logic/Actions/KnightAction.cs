using System.Collections;
using Logic.Actions.Components;
using Logic.Characters;
using Services.CharacterStorage;
using UnityEngine;
using Zenject;

namespace Logic.Actions
{
    public class KnightAction : CharacterAction
    {
        [SerializeField] private float _attackDelay;
        [SerializeField] private Character _character;
        [SerializeField] private Movement _movement;
        [SerializeField] private AwaitableAnimation _awaitableAnimation;
        [SerializeField] private DamageDealer _damageDealer;
        [SerializeField] private Animator _animator;

        [Inject] private ICharacterStorage _characterStorage;
        
        private Character _target;
        private Coroutine _attackRoutine;

        public override IEnumerator Execute()
        {
            _target = _characterStorage.GetRandom(_character.Team.Opposite());
            var initialPosition = _character.transform.position;
            var targetPosition = _target.transform.position;
            var offset = (initialPosition - targetPosition).normalized;
            yield return _movement.MoveTo(targetPosition + offset);
            yield return _awaitableAnimation.Play(AnimHashes.Attack, Attack);
            yield return _attackRoutine;
            yield return _movement.MoveTo(initialPosition);
            _character.ResetSpriteFlip();
            _animator.Play(AnimHashes.Idle);
            _attackRoutine = null;
            _target = null;
        }

        private void Attack()
        {
            _attackRoutine = StartCoroutine(AttackWithDelay());
        }

        private IEnumerator AttackWithDelay()
        {
            yield return new WaitForSeconds(_attackDelay);
            _damageDealer.ApplyDamage(_target);
        }
    }
}