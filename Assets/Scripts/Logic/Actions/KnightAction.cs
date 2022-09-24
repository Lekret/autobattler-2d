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
        
        private Character _opponent;
        
        public override IEnumerator Execute()
        {
            var initialPosition = _character.transform.position;
            var oppositeCharacters = _characters.GetByTeam(_character.Team.Opposite());
            _opponent = _randomizer.GetRandom(oppositeCharacters);
            yield return _movement.MoveTo(_opponent.transform.position);
            yield return _meleeAttack.AttackOpponent(_opponent);
            yield return _movement.MoveTo(initialPosition);
            _sprite.ResetFlip(_character.Team);
            _animator.Play(AnimHashes.Idle);
            _opponent = null;
        }
    }
}