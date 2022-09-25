using UnityEngine;

namespace Logic
{
    public class AnimatorEnterTrigger : StateMachineBehaviour
    {
        private IAnimatorEnterListener[] _listeners;
        
        public void SetListeners(IAnimatorEnterListener[] listeners)
        {
            _listeners = listeners;
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var listener in _listeners)
            {
                listener.OnStateEntered(stateInfo.shortNameHash);
            }
        }
    }
}