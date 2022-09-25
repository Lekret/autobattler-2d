using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.States;
using Logic.Characters;
using Services.AssetsManagement;
using Services.BattleSetup;
using StaticData;
using UnityEngine;

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
                _battleSetupService.GetLeftTeamData(),
                _spawnPoints.LeftSpawnPoints);
            await SpawnCharactersAsync(
                Team.Right,
                _battleSetupService.GetRightTeamData(), 
                _spawnPoints.RightSpawnPoints);
            
            _assetProvider.Cleanup();
            _stateMachine.Enter<BattleState>();
        }

        private async Task SpawnCharactersAsync(
            Team team,
            IReadOnlyList<CharacterStaticData> characterData, 
            IReadOnlyList<Transform> spawnPoints)
        {
            if (characterData.Count > spawnPoints.Count)
            {
                Debug.LogError("Not enough spawn points!");
            }

            for (var i = 0; i < characterData.Count && i < spawnPoints.Count; i++)
            {
                var character = await _characterFactory.CreateAsync(characterData[i], team, spawnPoints[i].position);
                _aliveCharacters.Add(character);
            }
        }
    }
}