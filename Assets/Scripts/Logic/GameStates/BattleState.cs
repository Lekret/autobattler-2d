using Infrastructure.States;
using Logic.Battle;

namespace Logic.GameStates
{
    public class BattleState : ITickState
    {
        private IBattleSimulator _battleSimulator;

        public BattleState(IBattleSimulator battleSimulator)
        {
            _battleSimulator = battleSimulator;
        }
        
        public void Tick()
        {
            
        }
    }
}