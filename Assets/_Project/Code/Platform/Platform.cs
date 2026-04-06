using UnityEngine;

namespace Project.Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Platform : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private PlatformModel _model;

        private Vector2 Size => _spriteRenderer.bounds.size;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _model = new PlatformModel();
        }

        public float GetHalfLength(PlatformSide side)
        {
            return _model.GetHalfLength(side, Size);
        }

        public Vector2 GetWorldPosition(PlatformSide side, float offset, float height)
        {
            return _model.GetWorldPosition(side, offset, height, transform.position, Size);
        }

        public PlatformSide GetNextSide(PlatformSide side)
        {
            return _model.GetNextSide(side);
        }

        public PlatformSide GetPrevSide(PlatformSide side)
        {
            return _model.GetPrevSide(side);
        }
    }
}
