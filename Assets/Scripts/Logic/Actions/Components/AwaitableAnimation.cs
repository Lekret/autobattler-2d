using System;
using System.Collections;
using UnityEngine;

namespace Logic.Actions.Components
{
    public class AwaitableAnimation : MonoBehaviour, IAnimatorEnterListener
    {
        [SerializeField] private Animator _animator;

        private Action _onEnter;
        private bool _triggered;
        private int _hash;

        public IEnumerator Play(int hash, Action onEnter = null)
        {
            _triggered = false;
            _hash = hash;
            _onEnter = onEnter;
            _animator.Play(_hash);
            yield return new WaitUntil(() =>
            {
                if (!_triggered) return false;
                var state = _animator.GetCurrentAnimatorStateInfo(0);
                return state.shortNameHash == _hash &&
                       state.normalizedTime >= 1;
            });
            _onEnter = null;
        }

        public void OnStateEntered(int hash)
        {
            if (hash == _hash)
            {
                _onEnter?.Invoke();
                _triggered = true;
            }
        }
    }
}