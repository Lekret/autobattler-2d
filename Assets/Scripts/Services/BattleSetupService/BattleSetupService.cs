using System.Collections.Generic;
using System.Linq;
using StaticData;
using UnityEngine;

namespace Services.BattleSetupService
{
    public class BattleSetupService : IBattleSetupService
    {
        private static TestCharacterData TestData => 
            Resources.Load<TestCharacterData>("StaticData/TestCharacterData");

        public IEnumerable<CharacterStaticData> GetLeftTeamData()
        {
            return TestData.Left;
        }

        public IEnumerable<CharacterStaticData> GetRightTeamData()
        {
            return TestData.Right;
        }
    }
}