using Zenject;

namespace Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine, ITickable
    {
        private readonly DiContainer _container;
        private IState _currentState;

        public GameStateMachine(DiContainer container)
        {
            _container = container;
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            if (state is IEnterState enterState)
                enterState.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IEnterState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            if (_currentState is IExitState exitState)
                exitState.Exit();
            var state = GetState<TState>();
            _currentState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IState
        {
            return _container.Resolve<TState>();
        }
        
        public void Tick()
        {
            if (_currentState is ITickState tickState)
            {
                tickState.Tick();
            }
        }
    }
}