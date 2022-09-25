﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.States;
using Logic.Characters;
using Services.AssetsManagement;
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
        private readonly IAssetProvider _assetProvider;
        private readonly SpawnPoints _spawnPoints;

        public SetupState(
            IGameStateMachine stateMachine, 
            ICharacterFactory characterFactory, 
            IAliveCharacters aliveCharacters,
            IBattleSetupService battleSetupService,
            IAssetProvider assetProvider,
            SpawnPoints spawnPoints)
        {
            _stateMachine = stateMachine;
            _characterFactory = characterFactory;
            _aliveCharacters = aliveCharacters;
            _battleSetupService = battleSetupService;
            _assetProvider = assetProvider;
            _spawnPoints = spawnPoints;
        }

        public async void Enter()
        {
            await SpawnCharactersAsync(
                Team.Left,
                _battleSetupService.GetLeftTeamData().ToQueue(),
                _spawnPoints.LeftSpawnPoints.ToQueue());
            await SpawnCharactersAsync(
                Team.Right,
                _battleSetupService.GetRightTeamData().ToQueue(), 
                _spawnPoints.RightSpawnPoints.ToQueue());
            
            _assetProvider.Cleanup();
            _stateMachine.Enter<BattleState>();
        }

        private async Task SpawnCharactersAsync(
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
                var character = await _characterFactory.CreateAsync(data, team, spawnPoint.position);
                _aliveCharacters.Add(character);
            }
        }
    }
}