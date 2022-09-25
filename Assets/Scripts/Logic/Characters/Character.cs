using System.Collections;
using UnityEngine;
using System;
using Logic.Actions;

namespace Logic.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterAction _action;
        [SerializeField] private CharacterHealth _health;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private Animator _animator;

        private bool _isDead;

        public IHealth Health => _health;
        public Team Team { get; private set; }
        public event Action<Character> Died;
        public event Action Hit;

        public void Init(Team team, int currentHp)
        {
            Team = team;
            _health.Init(currentHp);
            SetSpriteFlipX(team == Team.Right);
        }
        
        public IEnumerator ExecuteAction()
        {
            yield return null;
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).shortNameHash == AnimHashes.Idle);
            yield return _action.Execute();
        }
        
        public void SetSpriteFlipX(bool flipX)
        {
            _sprite.flipX = flipX;
        }

        public void ResetSpriteFlip()
        {
            SetSpriteFlipX(Team == Team.Right);
        }
        
        private void Awake()
        {
            _health.Changed += OnHealthChanged;
        }

        private void OnDestroy()
        {
            _health.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (_isDead)
                return;
            
            if (_health.Hp > 0)
            {
                Hit?.Invoke();
            }
            else
            {
                Died?.Invoke(this);
                _isDead = true;
            }
        }
    }
}