using System.Collections.Generic;

namespace Logic.Characters
{
    public class AliveCharacters : IAliveCharacters
    {
        private readonly List<Character> _characters = new List<Character>();
        private readonly Dictionary<Team, List<Character>> _charactersByTeam = new Dictionary<Team, List<Character>>()
        {
            {Team.Left, new List<Character>()},
            {Team.Right, new List<Character>()}
        };
        
        public IReadOnlyList<Character> GetAll()
        {
            return _characters;
        }

        public IReadOnlyList<Character> GetByTeam(Team team)
        {
            return _charactersByTeam[team];
        }

        public void Add(Character character)
        {
            _characters.Add(character);
            _charactersByTeam[character.Team].Add(character);
            character.Died += OnDied;
        }

        private void OnDied(Character character)
        {
            character.Died -= OnDied;
            _characters.Remove(character);
            _charactersByTeam[character.Team].Remove(character);
        }
    }
}