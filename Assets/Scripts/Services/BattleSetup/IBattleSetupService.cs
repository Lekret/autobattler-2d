using System.Collections.Generic;
using StaticData;

namespace Services.BattleSetup
{
    public interface IBattleSetupService
    {
        public IEnumerable<CharacterStaticData> GetLeftTeamData();
        public IEnumerable<CharacterStaticData> GetRightTeamData();
    }
}