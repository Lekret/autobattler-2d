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
        [SerializeField] private CharacterSprite _sprite;

        private bool _isDead;

        public IHealth Health => _health;
        public Team Team { get; private set; }
        public event Action<Character> Died;
        public event Action Hit;

        public void Init(Team team, int currentHp)
        {
            Team = team;
            _health.Init(currentHp);
            _sprite.SetFlipX(team == Team.Right);
        }
        
        public IEnumerator ExecuteAction()
        {
            return _action.Execute();
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