using System.Collections;
using Infrastructure.States;
using Logic.Characters;
using Services.CharacterStorage;
using Services.CoroutineRunner;
using Services.NextAction;
using UnityEngine;

namespace Logic.GameStates
{
    public class BattleState : IEnterState, IExitState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ICharacterStorage _characterStorage;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ICharacterActionService _characterActionService;
        private Team _currentTeam = Team.Left;
        private bool _battleIsRunning;

        public BattleState(
            IGameStateMachine stateMachine, 
            ICharacterStorage characterStorage, 
            ICoroutineRunner coroutineRunner, 
            ICharacterActionService characterActionService)
        {
            _stateMachine = stateMachine;
            _characterStorage = characterStorage;
            _coroutineRunner = coroutineRunner;
            _characterActionService = characterActionService;
        }

        public void Enter()
        {
            foreach (var character in _characterStorage.GetAll())
            {
                character.Died += OnCharacterDied;
            }

            _battleIsRunning = true;
            _coroutineRunner.StartCoroutine(RunBattle());
        }

        private IEnumerator RunBattle()
        {
            while (_battleIsRunning)
            {
                var character = _characterStorage.GetRandom(_currentTeam);
                yield return new WaitUntil(() => _characterActionService.CanPerform());
                yield return character.ExecuteAction();
                _currentTeam = _currentTeam.Opposite();
            }
        }

        public void Exit()
        {
            foreach (var character in _characterStorage.GetAll())
            {
                character.Died -= OnCharacterDied;
            }
        }
        
        private void OnCharacterDied(Character deadCharacter)
        {
            deadCharacter.Died -= OnCharacterDied;
            _characterStorage.Remove(deadCharacter);
            var winnersTeam = GetWinnersTeam();
            if (winnersTeam.HasValue)
            {
                _battleIsRunning = false;
                _stateMachine.Enter<ResultState, Team>(winnersTeam.Value);
            }
        }

        private Team? GetWinnersTeam()
        {
            var leftAlive = _characterStorage.GetAll(Team.Left);
            var rightAlive = _characterStorage.GetAll(Team.Right);
            if (leftAlive.Count > 0 && rightAlive.Count > 0)
            {
                return null;
            }
            return leftAlive.Count > 0 ? Team.Left : Team.Right;
        }
    }
}