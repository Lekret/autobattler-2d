using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly HashSet<object> _actionBlockers = new HashSet<object>();

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
        
        public void AddActionBlocker(object blocker) => _actionBlockers.Add(blocker);

        public void RemoveActionBlocker(object blocker) => _actionBlockers.Remove(blocker);

        public IEnumerator ExecuteAction()
        {
            yield return new WaitWhile(() => _actionBlockers.Count > 0);
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