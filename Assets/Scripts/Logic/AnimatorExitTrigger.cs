using UnityEngine;

namespace Logic
{
    public class AnimatorExitTrigger : StateMachineBehaviour
    {
        private IAnimatorExitListener[] _listeners;
        
        public void SetListeners(IAnimatorExitListener[] listeners)
        {
            _listeners = listeners;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var listener in _listeners)
            {
                listener.OnStateExited(stateInfo.shortNameHash);
            }
        }
    }
}