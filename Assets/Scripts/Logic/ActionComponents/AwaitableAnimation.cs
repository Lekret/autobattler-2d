using System;
using System.Collections;
using UnityEngine;

namespace Logic.ActionComponents
{
    public class AwaitableAnimation : MonoBehaviour, IAnimatorEnterListener
    {
        [SerializeField] private Animator _animator;

        private Action _onTrigger;
        private bool _triggered;
        private int _hash;

        public IEnumerator Play(int hash, Action onTrigger)
        {
            _triggered = false;
            _hash = hash;
            _onTrigger = onTrigger;
            _animator.Play(_hash);
            yield return new WaitUntil(() =>
            {
                if (!_triggered) return false;
                var state = _animator.GetCurrentAnimatorStateInfo(0);
                return state.shortNameHash == _hash &&
                       state.normalizedTime >= 1;
            });
            _onTrigger = null;
        }

        public void OnStateEntered(int hash)
        {
            if (hash == _hash)
            {
                _onTrigger?.Invoke();
                _triggered = true;
            }
        }
    }
}