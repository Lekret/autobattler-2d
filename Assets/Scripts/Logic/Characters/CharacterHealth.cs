using System;
using UnityEngine;

namespace Logic.Characters
{
    public class CharacterHealth : MonoBehaviour, IHealth
    {
        public int Hp { get; private set; }
        public int MaxHp { get; private set; }
        public event Action Changed;

        public void Init(int hp)
        {
            Hp = hp;
            MaxHp = hp;
        }
        
        public void ApplyDamage(int dmg)
        {
            Hp = Mathf.Max(Hp - dmg, 0);
            Changed?.Invoke();
        }
    }
}