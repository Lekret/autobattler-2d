using System.Collections;
using Logic.Characters;
using UnityEngine;

namespace Logic.ActionComponents
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _approachSpeed = 4;
        [SerializeField] private CharacterSprite _sprite;
        [SerializeField] private Character _character;
        [SerializeField] private Animator _animator;
        
        public IEnumerator MoveTo(Vector3 targetPosition)
        {
            var characterTransform = _character.transform;
            _animator.Play(AnimHashes.Move);
            while ((characterTransform.position - targetPosition).sqrMagnitude > Vector3.kEpsilon)
            {
                var characterPosition = characterTransform.position;
                var diff = targetPosition.x - characterPosition.x;
                _sprite.SetFlipX(diff < 0);
                characterTransform.position = Vector3.MoveTowards(
                    characterPosition,
                    targetPosition,
                    _approachSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}