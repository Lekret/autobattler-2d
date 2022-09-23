using System;
using System.Collections;
using Logic.Abilities;
using UnityEngine;

namespace Logic.Characters
{
    [RequireComponent(typeof(CharacterHealth))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterAbility _ability;
        [SerializeField] private CharacterSprite _sprite;

        private CharacterHealth _health;
        private bool _isDead;

        public IHealth Health => _health;
        public Team Team { get; private set; }
        public event Action<Character> Died;

        public void Init(Team team, int currentHp)
        {
            Team = team;
            _health.Init(currentHp);
            _sprite.SetFlipX(team == Team.Right);
        }
        
        public void ExecuteAbility(Action onEnd)
        {
            StartCoroutine(InternalExecuteAbility(onEnd));
        }
        
        private void Awake()
        {
            _health = GetComponent<CharacterHealth>();
            Health.Changed += OnHealthChanged;
        }

        private void OnDestroy()
        {
            _health.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (!_isDead && _health.Hp <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;
        }

        private IEnumerator InternalExecuteAbility(Action onEnd)
        {
            yield return _ability.Execute();
            onEnd?.Invoke();
        }
    }
}