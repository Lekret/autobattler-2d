using System.Collections.Generic;

namespace Logic.Characters
{
    public interface IAliveCharacters
    {
        IReadOnlyList<Character> GetAll();
        IReadOnlyList<Character> GetByTeam(Team team);
        void AddRange(IEnumerable<Character> characters);
    }
}