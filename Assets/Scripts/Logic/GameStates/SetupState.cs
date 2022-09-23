using System.Linq;
using Infrastructure.States;
using Logic.Characters;

namespace Logic.GameStates
{
    public class SetupState : IEnterState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ICharacterFactory _characterFactory;
        private readonly SpawnPoints _spawnPoints;

        public SetupState(
            IGameStateMachine stateMachine, 
            ICharacterFactory characterFactory, 
            SpawnPoints spawnPoints)
        {
            _stateMachine = stateMachine;
            _characterFactory = characterFactory;
            _spawnPoints = spawnPoints;
        }

        public void Enter()
        {
            _characterFactory.Create("knight", Team.Left, _spawnPoints.LeftSpawnPoints.First().position);
            _characterFactory.Create("knight", Team.Right, _spawnPoints.RightSpawnPoints.First().position);
            _stateMachine.Enter<BattleState>();
        }
    }
}