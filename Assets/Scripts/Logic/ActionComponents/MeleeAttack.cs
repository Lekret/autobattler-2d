﻿using System.Collections;
using Logic.Characters;
using UnityEngine;

namespace Logic.ActionComponents
{
    public class MeleeAttack : MonoBehaviour, IAnimatorListener
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private DamageDealer _damageDealer;

        private Character _target;
        
        public IEnumerator AttackTarget(Character opponent)
        {
            _target = opponent;
            _animator.Play(AnimHashes.Attack);
            yield return new WaitUntil(() =>
            {
                var state = _animator.GetCurrentAnimatorStateInfo(0);
                return state.shortNameHash == AnimHashes.Attack && state.normalizedTime >= 1;
            });
            _target = null;
        }

        public void OnStateTriggered(int hash)
        {
            if (hash == AnimHashes.Attack && _target != null)
            {
                _damageDealer.ApplyDamage(_target);
            }
        }
    }
}