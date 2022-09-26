using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.States;
using Logic.Characters;
using Services.AssetsManagement;
using Services.BattleSetup;
using Services.CharacterStorage;
using StaticData;
using UnityEngine;

namespace Logic.GameStates
{
    public class SetupState : IEnterState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ICharacterFactory _characterFactory;
        private readonly ICharacterStorage _characterStorage;
        private readonly IBattleSetupService _battleSetupService;
        private readonly IAssetProvider _assetProvider;
        private readonly SpawnPoints _spawnPoints;

        public SetupState(
            IGameStateMachine stateMachine, 
            ICharacterFactory characterFactory, 
            ICharacterStorage characterStorage,
            IBattleSetupService battleSetupService,
            IAssetProvider assetProvider,
            SpawnPoints spawnPoints)
        {
            _stateMachine = stateMachine;
            _characterFactory = characterFactory;
            _characterStorage = characterStorage;
            _battleSetupService = battleSetupService;
            _assetProvider = assetProvider;
            _spawnPoints = spawnPoints;
        }

        public async void Enter()
        {
            var characterTasks = new List<Task<Character>>();
            SpawnCharacters(
                characterTasks, 
                Team.Left, 
                _battleSetupService.GetLeftTeamData(), 
                _spawnPoints.LeftSpawnPoints);
            SpawnCharacters(
                characterTasks,
                Team.Right, 
                _battleSetupService.GetRightTeamData(),
                _spawnPoints.RightSpawnPoints);
            var characters = await Task.WhenAll(characterTasks);
            _characterStorage.AddRange(characters);
            _assetProvider.Cleanup();
            _stateMachine.Enter<BattleState>();
        }

        private void SpawnCharacters(ICollection<Task<Character>> characterTasks, Team team,
            IReadOnlyList<CharacterStaticData> characterData,
            IReadOnlyList<Transform> spawnPoints)
        {
            if (characterData.Count > spawnPoints.Count)
            {
                Debug.LogError("Not enough spawn points!");
            }

            for (var i = 0; i < characterData.Count && i < spawnPoints.Count; i++)
            {
                var characterTask = _characterFactory.CreateAsync(characterData[i], team, spawnPoints[i].position);
                characterTasks.Add(characterTask);
            }
        }
    }
}