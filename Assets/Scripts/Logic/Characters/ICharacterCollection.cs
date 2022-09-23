using System.Collections.Generic;

namespace Logic.Characters
{
    public interface ICharacterCollection
    {
        IReadOnlyList<Character> Characters { get; }
        void Add(Character character);
        void Remove(Character character);
    }
}