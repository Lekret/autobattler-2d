using Services.NextAction;
using UnityEngine;
using Zenject;

namespace Logic.Characters
{
    public class CharacterHit : MonoBehaviour, IAnimatorExitListener
    {
        [SerializeField] private Character _character;
        [SerializeField] private Animator _animator;

        [Inject] private ICharacterActionService _characterActionService;
        
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
            _characterActionService.AddBlocker(this);
            _animator.Play(AnimHashes.Hit);
            _hitPlayed = true;
        }

        public void OnStateExited(int hash)
        {
            if (_hitPlayed && hash == AnimHashes.Hit)
            {
                _characterActionService.RemoveBlocker(this);
                _hitPlayed = false;
            }
        }
    }
}