using System.Collections.Generic;
using System.Linq;
using Infrastructure.States;
using Logic.Characters;
using Services.Randomizer;
using UnityEngine;

namespace Logic.GameStates
{
    public class SetupState : IEnterState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ICharacterFactory _characterFactory;
        private readonly IAliveCharacters _aliveCharacters;
        private readonly IRandomizer _randomizer;
        private readonly SpawnPoints _spawnPoints;

        public SetupState(
            IGameStateMachine stateMachine, 
            ICharacterFactory characterFactory, 
            IAliveCharacters aliveCharacters,
            IRandomizer randomizer,
            SpawnPoints spawnPoints)
        {
            _stateMachine = stateMachine;
            _characterFactory = characterFactory;
            _aliveCharacters = aliveCharacters;
            _spawnPoints = spawnPoints;
            _randomizer = randomizer;
        }

        public void Enter()
        {
            var leftSpawnPoints = _spawnPoints.LeftSpawnPoints.ToList();
            var rightSpawnPoints = _spawnPoints.RightSpawnPoints.ToList();
            var leftPos = OccupyRandomPosition(leftSpawnPoints);
            var rightPos = OccupyRandomPosition(rightSpawnPoints);
            var char1 = _characterFactory.Create("knight", Team.Left, leftPos);
            var char2 = _characterFactory.Create("knight", Team.Right, rightPos);
            _aliveCharacters.Add(char1);
            _aliveCharacters.Add(char2);
            _stateMachine.Enter<BattleState>();
        }

        private Vector3 OccupyRandomPosition(List<Transform> spawnPoints)
        {
            var spawnPoint = _randomizer.GetRandom(spawnPoints);
            spawnPoints.Remove(spawnPoint);
            return spawnPoint.position;
        }
    }
}