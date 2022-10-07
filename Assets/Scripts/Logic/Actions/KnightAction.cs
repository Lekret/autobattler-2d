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

        public override IEnumerator Execute()
        {
            _target = _characterStorage.GetRandom(_character.Team.Opposite());
            var initialPosition = _character.transform.position;
            var targetPosition = _target.Center.position;
            var offset = (initialPosition - targetPosition).normalized;
            yield return _movement.MoveTo(targetPosition + offset);
            var animCor = StartCoroutine(_awaitableAnimation.Play(AnimHashes.Attack));
            yield return new WaitForSeconds(_attackDelay);
            _damageDealer.ApplyDamage(_target);
            yield return animCor;
            yield return _movement.MoveTo(initialPosition);
            _character.ResetSpriteFlip();
            _animator.Play(AnimHashes.Idle);
            _target = null;
        }
    }
}