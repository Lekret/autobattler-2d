using System.Collections.Generic;
using StaticData;
using UnityEngine;

namespace Services.BattleSetupService
{
    public class BattleSetupService : IBattleSetupService
    {
        private TestCharacterData TestData => Resources.Load<TestCharacterData>("StaticData/TestCharacterData");

        public IReadOnlyList<CharacterStaticData> GetLeftTeamData()
        {
            return TestData.Left;
        }

        public IReadOnlyList<CharacterStaticData> GetRightTeamData()
        {
            return TestData.Right;
        }
    }
}