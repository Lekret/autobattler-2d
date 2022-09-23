using System.Collections.Generic;

namespace Logic.Characters
{
    public class CharacterCollection : ICharacterCollection
    {
        private readonly List<Character> _characters = new List<Character>();
        
        public IReadOnlyList<Character> Characters => _characters;
        
        public void Add(Character character)
        {
            _characters.Add(character);
        }

        public void Remove(Character character)
        {
            _characters.Remove(character);
        }
    }
}