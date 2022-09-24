using System.Collections;
using Logic.Characters;
using Services.Randomizer;
using UnityEngine;
using Zenject;

namespace Logic.Abilities
{
    public class MeleeAttack : CharacterAbility, IAnimatorListener
    {
        [SerializeField] private CharacterSprite _sprite;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _approachSpeed;
        [SerializeField] private int _damage;

        [Inject] private IAliveCharacters _characters;
        [Inject] private IRandomizer _randomizer;

        private static readonly int IdleHash = Animator.StringToHash("Idle");
        private static readonly int MoveHash = Animator.StringToHash("Move");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private Character _opponent;

        public void OnStateTriggered(int hash)
        {
            if (hash == AttackHash && _opponent != null)
            {
                _opponent.Health.ApplyDamage(_damage);
            }
        }

        public override IEnumerator Execute(Character character)
        {
            var initialPosition = character.transform.position;
            var oppositeCharacters = _characters.GetByTeam(character.Team.Opposite());
            _opponent = _randomizer.GetRandom(oppositeCharacters);
            yield return ApproachOpponent(character);
            yield return AttackOpponent();
            yield return ReturnBack(character, initialPosition);
            _opponent = null;
        }
        
        private IEnumerator ApproachOpponent(Character character)
        {
            yield return MoveTo(character, _opponent.transform.position);
        }

        private IEnumerator AttackOpponent()
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
            var characterTransform = character.transform;
            _animator.Play(MoveHash);
            while ((characterTransform.position - position).sqrMagnitude > Vector3.kEpsilon)
            {
                characterTransform.position = Vector3.MoveTowards(
                    characterTransform.position,
                    position,
                    _approachSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}