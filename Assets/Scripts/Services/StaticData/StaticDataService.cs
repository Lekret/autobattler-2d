using System.Collections.Generic;
using System.Linq;
using StaticData;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly Dictionary<string, CharacterStaticData> _characterStaticData;

        public StaticDataService(IEnumerable<CharacterStaticData> data)
        {
            _characterStaticData = data.ToDictionary(x => x.Id, x => x);
        }
        
        public IEnumerable<CharacterStaticData> GetAll()
        {
            return _characterStaticData.Values;
        }

        public CharacterStaticData GetById(string characterId)
        {
            return _characterStaticData[characterId];
        }
    }
}