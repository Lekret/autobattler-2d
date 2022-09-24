using System.Collections.Generic;
using System.Linq;
using StaticData;

namespace Services.StaticData
{
    public class CharacterDataService : ICharacterDataService
    {
        private readonly Dictionary<string, CharacterStaticData> _characterStaticData;

        public CharacterDataService(IEnumerable<CharacterStaticData> data)
        {
            _characterStaticData = data.ToDictionary(x => x.Id, x => x);
        }
        
        public IEnumerable<CharacterStaticData> GetAll()
        {
            return _characterStaticData.Values;
        }

        // TODO DELETE MAYBE???
        public CharacterStaticData GetById(string characterId)
        {
            return _characterStaticData[characterId];
        }
    }
}