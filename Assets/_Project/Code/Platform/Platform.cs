using System;
using UnityEngine;

namespace Project.Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Platform : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        
        private readonly PlatformSide[] _sideOrder =
        {
            PlatformSide.Top,
            PlatformSide.Right,
            PlatformSide.Bottom,
            PlatformSide.Left
        };

        private Vector2 Size => _spriteRenderer.bounds.size;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public float GetHalfLength(PlatformSide platformSide)
        {
            switch (platformSide)
            {
                case PlatformSide.Top:
                case PlatformSide.Bottom:
                    return Size.x * 0.5f;
                default:
                    return Size.y * 0.5f;
            }
        }

        public Vector2 GetWorldPosition(PlatformSide platformSide, float offset, float height)
        {
            Vector2 sideCenter = GetSideCenter(platformSide);
            Vector2 normal = GetNormal(platformSide);
            Vector2 moveAxis = GetMoveAxis(platformSide);

            return sideCenter + normal * height + moveAxis * offset;
        }

        private Vector2 GetNormal(PlatformSide platformSide)
        {
            switch (platformSide)
            {
                case PlatformSide.Top:
                    return Vector2.up;
                case PlatformSide.Bottom:
                    return Vector2.down;
                case PlatformSide.Right:
                    return Vector2.right;
                case PlatformSide.Left:
                    return Vector2.left;
                default:
                    return Vector2.up;
            }
        }

        private Vector2 GetMoveAxis(PlatformSide platformSide)
        {
            switch (platformSide)
            {
                case PlatformSide.Top:
                    return Vector2.right;
                case PlatformSide.Bottom:
                    return Vector2.left;
                case PlatformSide.Right:
                    return Vector2.down;
                case PlatformSide.Left:
                    return Vector2.up;
                default:
                    return Vector2.right;
            }
        }

        private Vector2 GetSideCenter(PlatformSide platformSide)
        {
            Vector2 c = transform.position;

            switch (platformSide)
            {
                case PlatformSide.Top:
                    return c + Vector2.up * (Size.y * 0.5f);
                case PlatformSide.Bottom:
                    return c + Vector2.down * (Size.y * 0.5f);
                case PlatformSide.Right:
                    return c + Vector2.right * (Size.x * 0.5f);
                case PlatformSide.Left:
                    return c + Vector2.left * (Size.x * 0.5f);
                default:
                    return c;
            }
        }

        public PlatformSide GetNextSide(PlatformSide platformSide)
        {
            return _sideOrder[(Array.IndexOf(_sideOrder, platformSide) + 1) % _sideOrder.Length];
        }

        public PlatformSide GetPrevSide(PlatformSide platformSide)
        {
            return _sideOrder[(Array.IndexOf(_sideOrder, platformSide) 
                + _sideOrder.Length - 1) % _sideOrder.Length];
        }

        private void OnDrawGizmos()
        {
            if (_spriteRenderer == null)
                return;

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, new Vector3(Size.x, Size.y, 0.1f));

            foreach (PlatformSide side in _sideOrder)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawRay(GetSideCenter(side), GetNormal(side) * 0.4f);
            }
        }
    }
}