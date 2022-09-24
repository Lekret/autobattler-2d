using UnityEngine;

namespace Logic
{
    public class AnimatorTrigger : StateMachineBehaviour
    {
        [SerializeField] [Range(0, 1)] private float _normalizedTime;

        private IAnimatorListener[] _listeners;
        private bool _triggered;
        
        public void SetListeners(IAnimatorListener[] listeners)
        {
            _listeners = listeners;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!_triggered && stateInfo.normalizedTime > _normalizedTime)
            { 
                Trigger(stateInfo);
                _triggered = true;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _triggered = false;
        }

        private void Trigger(AnimatorStateInfo stateInfo)
        {
            foreach (var listener in _listeners)
            {
                listener.OnStateTriggered(stateInfo.shortNameHash);
            }
        }
    }
}