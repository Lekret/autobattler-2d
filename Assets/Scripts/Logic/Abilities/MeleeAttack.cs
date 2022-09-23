using System;
using System.Collections;
using Logic.Characters;
using Services.Randomizer;
using UnityEngine;
using Zenject;

namespace Logic.Abilities
{
    public class MeleeAttack : CharacterAbility
    {
        [SerializeField] private CharacterSprite _sprite;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _approachSpeed;

        [Inject] private IAliveCharacters _characters;
        [Inject] private IRandomizer _randomizer;

        private static readonly int IdleHash = Animator.StringToHash("Idle");
        private static readonly int MoveHash = Animator.StringToHash("Move");
        private static readonly int AttackHash = Animator.StringToHash("Attack");

        private void Awake()
        {
            _animator.Play(IdleHash);
        }

        public override IEnumerator Execute(Character character)
        {
            var initialPosition = character.transform.position;
            var oppositeCharacters = _characters.GetByTeam(character.Team.Opposite());
            var opponent = _randomizer.GetRandom(oppositeCharacters);
            yield return ApproachOpponent(character, opponent);
            yield return AttackOpponent(opponent);
            yield return ReturnBack(character, initialPosition);
        }
        
        private IEnumerator ApproachOpponent(Character character, Character opponent)
        {
            yield return MoveTo(character, opponent.transform.position);
        }

        private IEnumerator AttackOpponent(Character opponent)
        {
            _animator.Play(AttackHash);
            yield return new WaitUntil(() =>
            {
                var state = _animator.GetCurrentAnimatorStateInfo(0);
                return state.shortNameHash == AttackHash && state.normalizedTime >= 1;
            });
        }

        private IEnumerator ReturnBack(Character character, Vector3 initialPosition)
        {
            _sprite.SetFlipX(character.Team == Team.Left);
            yield return MoveTo(character, initialPosition);
            _sprite.SetFlipX(character.Team == Team.Right);
            _animator.Play(IdleHash);
        }

        private IEnumerator MoveTo(Character character, Vector3 position)
        {
            _animator.Play(MoveHash);
            while ((character.transform.position - position).sqrMagnitude > Vector3.kEpsilon)
            {
                character.transform.position = Vector3.MoveTowards(
                    character.transform.position,
                    position,
                    _approachSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}