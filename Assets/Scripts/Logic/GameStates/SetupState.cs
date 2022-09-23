using System.Linq;
using Infrastructure.States;
using Logic.Characters;

namespace Logic.GameStates
{
    public class SetupState : IEnterState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ICharacterFactory _characterFactory;
        private readonly IAliveCharacters _aliveCharacters;
        private readonly SpawnPoints _spawnPoints;

        public SetupState(
            IGameStateMachine stateMachine, 
            ICharacterFactory characterFactory, 
            IAliveCharacters aliveCharacters,
            SpawnPoints spawnPoints)
        {
            _stateMachine = stateMachine;
            _characterFactory = characterFactory;
            _aliveCharacters = aliveCharacters;
            _spawnPoints = spawnPoints;
        }

        public void Enter()
        {
            var char1 = _characterFactory.Create("knight", Team.Left, _spawnPoints.LeftSpawnPoints.First().position);
            var char2 = _characterFactory.Create("knight", Team.Right, _spawnPoints.RightSpawnPoints.First().position);
            _aliveCharacters.Add(char1);
            _aliveCharacters.Add(char2);
            _stateMachine.Enter<BattleState>();
        }
    }
}