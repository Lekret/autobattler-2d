using System.Collections.Generic;

namespace Services.NextAction
{
    public class CharacterActionService : ICharacterActionService
    {
        private readonly HashSet<object> _blockers = new HashSet<object>();

        public bool CanPerform()
        {
            return _blockers.Count == 0;
        }

        public void AddBlocker(object reason)
        {
            _blockers.Add(reason);
        }

        public void RemoveBlocker(object reason)
        {
            _blockers.Remove(reason);
        }
    }
}