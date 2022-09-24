using System.Collections.Generic;
using Logic;
using Logic.Characters;

namespace Services.CharacterSelector
{
    public interface ICharacterSelector
    {
        Character GetSingle(Team team);
        void GetMany(Team team, int count, List<Character> buffer);
        void GetManyDifferent(Team team, int count, List<Character> buffer);
    }
}