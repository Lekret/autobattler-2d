using System.Collections.Generic;
using StaticData;

namespace Services.BattleSetup
{
    public interface IBattleSetupService
    {
        public IReadOnlyList<CharacterStaticData> GetLeftTeamData();
        public IReadOnlyList<CharacterStaticData> GetRightTeamData();
    }
}