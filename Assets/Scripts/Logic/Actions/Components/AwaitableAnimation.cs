using System.Collections;
using UnityEngine;

namespace Logic.Actions.Components
{
    public class AwaitableAnimation : MonoBehaviour, IAnimatorEnterListener
    {
        [SerializeField] private Animator _animator;

        private bool _triggered;
        private int _hash;

        public IEnumerator Play(int hash)
        {
            _triggered = false;
            _hash = hash;
            _animator.Play(_hash);
            yield return new WaitUntil(() =>
            {
                if (!_triggered) return false;
                var state = _animator.GetCurrentAnimatorStateInfo(0);
                return state.shortNameHash == _hash &&
                       state.normalizedTime >= 1;
            });
        }

        public void OnStateEntered(int hash)
        {
            if (hash == _hash)
                _triggered = true;
        }
    }
}