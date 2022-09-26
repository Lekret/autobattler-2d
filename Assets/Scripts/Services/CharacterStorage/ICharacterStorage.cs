using System.Collections.Generic;
using Logic;
using Logic.Characters;

namespace Services.CharacterStorage
{
    public interface ICharacterStorage
    {
        void AddRange(IEnumerable<Character> characters);
        void Remove(Character character);
        IReadOnlyList<Character> GetAll();
        IReadOnlyList<Character> GetAll(Team team);
        Character GetRandom(Team team);
        void GetRandom(Team team, int count, List<Character> buffer);
        void GetRandomUnique(Team team, int maxCount, List<Character> buffer);
    }
}