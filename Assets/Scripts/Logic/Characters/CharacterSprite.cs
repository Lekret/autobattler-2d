using UnityEngine;

namespace Logic.Characters
{
    public class CharacterSprite : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetFlipX(bool flipX)
        {
            _spriteRenderer.flipX = flipX;
        }
    }
}