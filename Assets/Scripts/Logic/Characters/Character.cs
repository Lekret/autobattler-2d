﻿using System;
using System.Collections;
using Logic.Abilities;
using UnityEngine;

namespace Logic.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterAbility _ability;
        
        public Team Team { get; private set; }
        public int Hp { get; private set; }
        public int MaxHp { get; private set; }

        public void SetTeam(Team team)
        {
            Team = team;
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