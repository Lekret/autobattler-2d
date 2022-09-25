using System.Collections.Generic;
using StaticData;
using UnityEngine;

namespace Services.BattleSetup
{
    public class BattleSetupService : IBattleSetupService
    {
        private static TestCharacterData TestData => 
            Resources.Load<TestCharacterData>("StaticData/TestCharacterData");

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