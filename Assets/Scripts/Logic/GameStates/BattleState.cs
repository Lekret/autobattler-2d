using System.Collections.Generic;
using Infrastructure.States;
using Logic.Characters;
using StaticData;

namespace Logic.GameStates
{
    public class BattleState : IEnterState, ITickState, IExitState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IAliveCharacters _aliveCharacters;
        private Team _startTeam = Team.Left;

        public BattleState(IGameStateMachine stateMachine, IAliveCharacters aliveCharacters)
        {
            _stateMachine = stateMachine;
            _aliveCharacters = aliveCharacters;
        }

        public void Enter()
        {
            foreach (var character in _aliveCharacters.GetAll())
            {
                character.Died += OnCharacterDied;
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

        public void Tick()
        {
            
        }
    }
}