using UnityEngine;

namespace Logic.Characters
{
    public class CharacterHit : MonoBehaviour, IAnimatorListener
    {
        [SerializeField] private Character _character;
        [SerializeField] private Animator _animator;

        private bool _hitPlayed;
        
        private void Start()
        {
            _character.Hit += OnHit;
        }

        private void OnDestroy()
        {
            _character.Hit -= OnHit;
        }

        private void OnHit()
        {
            _character.AddActionBlocker(this);
            _animator.Play(AnimHashes.Hit);
            _hitPlayed = true;
        }

        public void OnStateTriggered(int hash)
        {
            if (_hitPlayed && hash == AnimHashes.Idle)
            {
                _character.RemoveActionBlocker(this);
                _hitPlayed = false;
            }
        }
    }
}