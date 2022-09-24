using System.Collections.Generic;
using Logic;
using Logic.Characters;
using Services.Randomizer;

namespace Services.CharacterSelector
{
    public class CharacterSelector : ICharacterSelector
    {
        private readonly IAliveCharacters _characters;
        private readonly IRandomizer _randomizer;

        public CharacterSelector(IAliveCharacters characters, IRandomizer randomizer)
        {
            _characters = characters;
            _randomizer = randomizer;
        }
        
        public Character GetSingle(Team team)
        {
            var characters = _characters.GetByTeam(team);
            return _randomizer.GetRandom(characters);
        }

        public void GetMany(Team team, int count, List<Character> buffer)
        {
            buffer.Clear();
            var characters = _characters.GetByTeam(team);
            for (var i = 0; i < count; i++)
            {
                var randomCharacter = _randomizer.GetRandom(characters);
                buffer.Add(randomCharacter);
            }
        }

        public void GetManyDifferent(Team team, int count, List<Character> buffer)
        {
            buffer.Clear();
            var characters = _characters.GetByTeam(team);
            for (var i = 0; i < count && i < characters.Count; i++)
            {
                buffer.Add(characters[i]);
            }
            _randomizer.Shuffle(buffer);
        }
    }
}