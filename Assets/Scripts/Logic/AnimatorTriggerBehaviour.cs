using UnityEngine;

namespace Logic
{
    public class AnimatorTriggerBehaviour : StateMachineBehaviour
    {
        [SerializeField] [Range(0, 1)] private float _normalizedTime;

        private IAnimatorListener _listener;
        private bool _triggered;
        
        public void SetListener(IAnimatorListener listener)
        {
            _listener = listener;
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
            if (!_triggered)
            {
                Trigger(stateInfo);
            }
            _triggered = false;
        }

        private void Trigger(AnimatorStateInfo stateInfo)
        {
            if (_listener != null)
            {
                _listener.OnStateTriggered(stateInfo.shortNameHash);
            }
        }
    }
}