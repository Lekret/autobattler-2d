using System.Collections.Generic;
using StaticData;

namespace Services.StaticData
{
    public interface IStaticDataService
    {
        IEnumerable<CharacterStaticData> GetAll();
        CharacterStaticData GetById(string characterId);
    }
}