using System.Collections.Generic;
using StaticData;

namespace Services.BattleSetupService
{
    public interface IBattleSetupService
    {
        public IReadOnlyList<CharacterStaticData> GetLeftTeamData();
        public IReadOnlyList<CharacterStaticData> GetRightTeamData();
    }
}