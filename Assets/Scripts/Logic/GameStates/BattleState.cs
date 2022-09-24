using System.Collections;
using Infrastructure.States;
using Logic.Characters;
using Services.CoroutineRunner;
using Services.Randomizer;
using UnityEngine;

namespace Logic.GameStates
{
    public class BattleState : IEnterState, IExitState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IAliveCharacters _aliveCharacters;
        private readonly IRandomizer _randomizer;
        private readonly ICoroutineRunner _coroutineRunner;
        private Team _currentTeam = Team.Left;
        private bool _battleIsRunning;

        public BattleState(
            IGameStateMachine stateMachine, 
            IAliveCharacters aliveCharacters, 
            IRandomizer randomizer, 
            ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _aliveCharacters = aliveCharacters;
            _randomizer = randomizer;
            _coroutineRunner = coroutineRunner;
        }

        public void Enter()
        {
            foreach (var character in _aliveCharacters.GetAll())
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
                var characters = _aliveCharacters.GetByTeam(_currentTeam);
                var character = _randomizer.GetRandom(characters);
                yield return character.ExecuteAbility();
                _currentTeam = _currentTeam.Opposite();
            }
        }

        public void Exit()
        {
            foreach (var character in _aliveCharacters.GetAll())
            {
                character.Died -= OnCharacterDied;
            }
        }
        
        private void OnCharacterDied(Character deadCharacter)
        {
            deadCharacter.Died -= OnCharacterDied;
            var winnersTeam = GetWinnersTeam();
            if (winnersTeam.HasValue)
            {
                _battleIsRunning = false;
                _stateMachine.Enter<ResultState, Team>(winnersTeam.Value);
            }
        }

        private Team? GetWinnersTeam()
        {
            var leftAlive = _aliveCharacters.GetByTeam(Team.Left);
            var rightAlive = _aliveCharacters.GetByTeam(Team.Right);
            if (leftAlive.Count > 0 && rightAlive.Count > 0)
            {
                return null;
            }
            return leftAlive.Count > 0 ? Team.Left : Team.Right;
        }
    }
}