using UnityEngine;

namespace Logic.Characters
{
    public class CharacterHit : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private Animator _animator;

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
            _animator.Play("Hit");
        }
    }
}