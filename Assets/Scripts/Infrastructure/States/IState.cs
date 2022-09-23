namespace Infrastructure.States
{
    public interface IState
    {
    }
    
    public interface IEnterState : IState
    {
        void Enter();
    }

    public interface IEnterState<TPayload> : IState
    {
        void Enter(TPayload payload);
    }
  
    public interface IExitState : IState
    {
        void Exit();
    }

    public interface ITickState : IState
    {
        void Tick();
    }
}