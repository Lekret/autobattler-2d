namespace Services.NextAction
{
    public interface ICharacterActionService
    {
        public bool CanPerform();
        public void AddBlocker(object reason);
        public void RemoveBlocker(object reason);
    }
}