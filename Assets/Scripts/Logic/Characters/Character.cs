using System;
using System.Collections;
using Logic.Abilities;
using UnityEngine;

namespace Logic.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterAbility _ability;
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
        
        public IEnumerator ExecuteAbility()
        {
            return _ability.Execute(this);
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