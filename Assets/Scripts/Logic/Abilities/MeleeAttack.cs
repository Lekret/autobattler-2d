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
            Debug.LogError("ATTACK");
            yield break;
        }

        private IEnumerator ReturnBack(Character character, Vector3 initialPosition)
        {
            _sprite.SetFlipX(character.Team == Team.Left);
            yield return MoveTo(character, initialPosition);
            _sprite.SetFlipX(character.Team == Team.Right);
        }

        private IEnumerator MoveTo(Character character, Vector3 position)
        {
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