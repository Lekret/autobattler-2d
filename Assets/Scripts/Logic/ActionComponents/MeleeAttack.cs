using System.Collections;
using Logic.Characters;
using UnityEngine;

namespace Logic.ActionComponents
{
    public class MeleeAttack : MonoBehaviour, IAnimatorListener
    {
        [SerializeField] private int _damage;
        [SerializeField] private Animator _animator;

        private Character _opponent;
        
        public IEnumerator AttackOpponent(Character opponent)
        {
            _opponent = opponent;
            _animator.Play(AnimHashes.Attack);
            yield return new WaitUntil(() =>
            {
                var state = _animator.GetCurrentAnimatorStateInfo(0);
                return state.shortNameHash == AnimHashes.Attack && state.normalizedTime >= 1;
            });
            _opponent = null;
        }

        public void OnStateTriggered(int hash)
        {
            if (hash == AnimHashes.Attack && _opponent != null)
            {
                _opponent.Health.ApplyDamage(_damage);
            }
        }
    }
}