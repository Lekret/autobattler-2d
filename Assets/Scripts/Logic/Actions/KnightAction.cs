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
        [SerializeField] private CharacterSprite _sprite;
        [SerializeField] private Movement _movement;
        [SerializeField] private MeleeAttack _meleeAttack;
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
            yield return _meleeAttack.AttackTarget(_target);
            yield return _movement.MoveTo(initialPosition);
            _sprite.ResetFlip(_character.Team);
            _animator.Play(AnimHashes.Idle);
            _target = null;
        }

        private Character GetTarget()
        {
            var targetCharacters = _characters.GetByTeam(_character.Team.Opposite());
            return _randomizer.GetRandom(targetCharacters);
        }
    }
}