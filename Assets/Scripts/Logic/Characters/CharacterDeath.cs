using UnityEngine;

namespace Logic.Characters
{
    public class CharacterDeath : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private Animator _animator;

        private void Start()
        {
            _character.Died += OnDied;
        }

        private void OnDestroy()
        {
            _character.Died -= OnDied;
        }

        private void OnDied(Character _)
        {
            _animator.Play("Dead");
        }
    }
}