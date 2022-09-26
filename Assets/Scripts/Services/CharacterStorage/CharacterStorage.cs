using System.Collections.Generic;
using Logic;
using Logic.Characters;
using Services.Randomizer;

namespace Services.CharacterStorage
{
    public class CharacterStorage : ICharacterStorage
    {
        private readonly IRandomizer _randomizer;
        private readonly List<Character> _shuffleBuffer = new List<Character>();
        private readonly List<Character> _characters = new List<Character>();
        private readonly Dictionary<Team, List<Character>> _charactersByTeam = new Dictionary<Team, List<Character>>()
        {
            {Team.Left, new List<Character>()},
            {Team.Right, new List<Character>()}
        };

        public CharacterStorage(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public void AddRange(IEnumerable<Character> characters)
        {
            foreach (var character in characters)
            {
                _characters.Add(character);
                _charactersByTeam[character.Team].Add(character);
            }
        }
        
        public void Remove(Character character)
        {
            _characters.Remove(character);
            _charactersByTeam[character.Team].Remove(character);
        }
        
        public IReadOnlyList<Character> GetAll()
        {
            return _characters;
        }

        public IReadOnlyList<Character> GetAll(Team team)
        {
            return _charactersByTeam[team];
        }

        public Character GetRandom(Team team)
        {
            return _randomizer.GetRandom(_charactersByTeam[team]);
        }

        public void GetRandom(Team team, int count, List<Character> buffer)
        {
            buffer.Clear();
            var characters = _charactersByTeam[team];
            for (var i = 0; i < count; i++)
            {
                var randomCharacter = _randomizer.GetRandom(characters);
                buffer.Add(randomCharacter);
            }
        }

        public void GetRandomUnique(Team team, int maxCount, List<Character> buffer)
        {
            buffer.Clear();
            _shuffleBuffer.Clear();
            _shuffleBuffer.AddRange(GetAll(team));
            _randomizer.Shuffle(_shuffleBuffer);
            for (var i = 0; i < maxCount && i < _shuffleBuffer.Count; i++)
            {
                buffer.Add(_shuffleBuffer[i]);
            }
        }
    }
}