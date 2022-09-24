using System.Collections.Generic;
using Infrastructure.States;
using Logic.Characters;
using Services.BattleSetup;
using StaticData;
using UnityEngine;
using Utils;

namespace Logic.GameStates
{
    public class SetupState : IEnterState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ICharacterFactory _characterFactory;
        private readonly IAliveCharacters _aliveCharacters;
        private readonly IBattleSetupService _battleSetupService;
        private readonly SpawnPoints _spawnPoints;

        public SetupState(
            IGameStateMachine stateMachine, 
            ICharacterFactory characterFactory, 
            IAliveCharacters aliveCharacters,
            IBattleSetupService battleSetupService,
            SpawnPoints spawnPoints)
        {
            _stateMachine = stateMachine;
            _characterFactory = characterFactory;
            _aliveCharacters = aliveCharacters;
            _spawnPoints = spawnPoints;
            _battleSetupService = battleSetupService;
        }

        public void Enter()
        {
            SpawnCharacters(
                Team.Left,
                _battleSetupService.GetLeftTeamData().ToQueue(),
                _spawnPoints.LeftSpawnPoints.ToQueue());
            SpawnCharacters(
                Team.Right,
                _battleSetupService.GetRightTeamData().ToQueue(), 
                _spawnPoints.RightSpawnPoints.ToQueue());
            
            _stateMachine.Enter<BattleState>();
        }

        private void SpawnCharacters(
            Team team,
            Queue<CharacterStaticData> characterData, 
            Queue<Transform> spawnPoints)
        {
            if (characterData.Count > spawnPoints.Count)
            {
                Debug.LogError("Not enough spawn points!");
            }

            while (characterData.Count > 0 && spawnPoints.Count > 0)
            {
                var data = characterData.Dequeue();
                var spawnPoint = spawnPoints.Dequeue();
                var character = _characterFactory.Create(data, team, spawnPoint.position);
                _aliveCharacters.Add(character);
            }
        }
    }
}