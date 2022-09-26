using System.Collections;
using Logic.Actions.Components;
using Logic.Characters;
using Services.CharacterStorage;
using Services.Randomizer;
using UnityEngine;
using Zenject;

namespace Logic.Actions
{
    public class ReaperAction : CharacterAction
    {
        [SerializeField] private float _attackDamageDelay;
        [SerializeField] private float _spellDamageDelay;
        [Range(0, 1)] 
        [SerializeField] private float _spellProbability;
        [SerializeField] private Character _character;
        [SerializeField] private Movement _movement;
        [SerializeField] private AwaitableAnimation _awaitableAnimation;
        [SerializeField] private DamageDealer _meleeDamageDealer;
        [SerializeField] private DamageDealer _spellDamageDealer;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _spellPrefab;

        [Inject] private IRandomizer _randomizer;
        [Inject] private IInstantiator _instantiator;
        [Inject] private ICharacterStorage _characterStorage;
        
        private Character _target;

        public override IEnumerator Execute()
        {
            _target = _characterStorage.GetRandom(_character.Team.Opposite());

            if (_spellProbability > _randomizer.Range(0f, 1f))
            {
                yield return CastSpell();
            }
            else
            {
                yield return AttackMelee();
            }
            
            _animator.Play(AnimHashes.Idle);
            _target = null;
        }

        private IEnumerator CastSpell()
        {
            yield return _awaitableAnimation.Play(AnimHashes.Cast);
            _animator.Play(AnimHashes.Idle);
            var spell = _instantiator.InstantiatePrefab(_spellPrefab);
            spell.transform.position = _target.transform.position;
            yield return new WaitForSeconds(_spellDamageDelay);
            _spellDamageDealer.ApplyDamage(_target);
        }
        
        private IEnumerator AttackMelee()
        {
            var initialPosition = _character.transform.position;
            var targetPosition = _target.transform.position;
            var offset = (initialPosition - targetPosition).normalized;
            yield return _movement.MoveTo(targetPosition + offset);
            var animCor = StartCoroutine(_awaitableAnimation.Play(AnimHashes.Attack));
            yield return new WaitForSeconds(_attackDamageDelay);
            _meleeDamageDealer.ApplyDamage(_target);
            yield return animCor;
            yield return _movement.MoveTo(initialPosition);
            _character.ResetSpriteFlip();
        }
    }
}