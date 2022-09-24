using System;
using System.Collections;
using Logic.ActionComponents;
using Logic.Characters;
using Services.Randomizer;
using UnityEngine;
using Zenject;

namespace Logic.Actions
{
    public class KnightAction : CharacterAction
    {
        [SerializeField] private Character _character;
        [SerializeField] private Movement _movement;
        [SerializeField] private AnimatorListener _animatorListener;
        [SerializeField] private DamageDealer _damageDealer;
        [SerializeField] private Animator _animator;

        [Inject] private IAliveCharacters _characters;
        [Inject] private IRandomizer _randomizer;
        
        private Character _target;

        public override IEnumerator Execute()
        {
            _target = GetTarget();
            var initialPosition = _character.transform.position;
            var targetPosition = _target.transform.position;
            var offset = (initialPosition - targetPosition).normalized;
            yield return _movement.MoveTo(targetPosition + offset);
            yield return _animatorListener.WaitFor(AnimHashes.Attack, ApplyDamage);
            yield return _movement.MoveTo(initialPosition);
            _character.ResetSpriteFlip();
            _animator.Play(AnimHashes.Idle);
            _target = null;
        }

        private void ApplyDamage()
        {
            _damageDealer.ApplyDamage(_target);
        }

        private Character GetTarget()
        {
            var targetCharacters = _characters.GetByTeam(_character.Team.Opposite());
            return _randomizer.GetRandom(targetCharacters);
        }
    }
}