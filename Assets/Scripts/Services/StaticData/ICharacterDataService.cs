using System.Collections.Generic;
using StaticData;

namespace Services.StaticData
{
    public interface ICharacterDataService
    {
        IEnumerable<CharacterStaticData> GetAll();
    }
}