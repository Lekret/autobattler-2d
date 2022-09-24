using System;
using System.Collections;
using UnityEngine;

namespace Logic.ActionComponents
{
    public class MeleeAttack : MonoBehaviour, IAnimatorListener
    {
        [SerializeField] private Animator _animator;

        private Action _applyDamage;
        
        public IEnumerator Attack(Action applyDamage)
        {
            _applyDamage = applyDamage;
            _animator.Play(AnimHashes.Attack);
            yield return new WaitUntil(() =>
            {
                var state = _animator.GetCurrentAnimatorStateInfo(0);
                return state.shortNameHash == AnimHashes.Attack && state.normalizedTime >= 1;
            });
            _applyDamage = null;
        }

        public void OnStateTriggered(int hash)
        {
            if (hash == AnimHashes.Attack)
            {
                _applyDamage?.Invoke();
            }
        }
    }
}