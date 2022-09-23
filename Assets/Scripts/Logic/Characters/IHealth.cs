using System;

namespace Logic.Characters
{
    public interface IHealth
    {
        int Hp { get; }
        int MaxHp { get; }
        event Action Changed;
        void ApplyDamage(int dmg);
    }
}