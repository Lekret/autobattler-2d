using System;
using System.Collections;
using Logic.Abilities;
using UnityEngine;

namespace Logic.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterAbility _ability;
        [SerializeField] private CharacterSprite _sprite;
        
        public Team Team { get; private set; }
        public int Hp { get; private set; }
        public int MaxHp { get; private set; }

        public void SetTeam(Team team)
        {
            Team = team;
            _sprite.SetFlipX(team == Team.Right);
        }
        
        public void SetHp(int hp)
        {
            Hp = hp;
            MaxHp = hp;
        }
        
        public void ExecuteAbility(Action onEnd)
        {
            StartCoroutine(InternalExecuteAbility(onEnd));
        }

        private IEnumerator InternalExecuteAbility(Action onEnd)
        {
            yield return _ability.Execute();
            onEnd?.Invoke();
        }
    }
}