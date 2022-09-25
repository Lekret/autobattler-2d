using System.Collections;
using Logic.Characters;
using UnityEngine;
using Utils;

namespace Logic.ActionComponents
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _approachSpeed = 4;
        [SerializeField] private Character _character;
        [SerializeField] private Animator _animator;
        
        public IEnumerator MoveTo(Vector2 targetPosition)
        {
            var characterTransform = _character.transform;
            _animator.Play(AnimHashes.Move);
            while ((characterTransform.position.ToVec2() - targetPosition).sqrMagnitude > Vector2.kEpsilon)
            {
                var characterPosition = characterTransform.position;
                var diff = targetPosition.x - characterPosition.x;
                _character.SetSpriteFlipX(diff < 0);
                characterTransform.position = Vector2.MoveTowards(
                    characterPosition,
                    targetPosition,
                    _approachSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}