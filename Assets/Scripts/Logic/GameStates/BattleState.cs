using Infrastructure.States;
using Logic.Characters;

namespace Logic.GameStates
{
    public class BattleState : ITickState
    {
        private readonly ICharacterCollection _characters;
        private Team _startTeam = Team.Left;

        public BattleState(ICharacterCollection characters)
        {
            _characters = characters;
        }

        public void Tick()
        {
        }
    }
}