using System;
using UnityEngine;

namespace Project.Gameplay
{
    public class PlatformModel
    {
        private const float Half = 0.5f;
        private const int One = 1;

        private readonly PlatformSide[] _sideOrder =
        {
            PlatformSide.Top,
            PlatformSide.Right,
            PlatformSide.Bottom,
            PlatformSide.Left
        };

        public float GetHalfLength(PlatformSide side, Vector2 size)
        {
            switch (side)
            {
                case PlatformSide.Top:
                case PlatformSide.Bottom:
                    return size.x * Half;
                default:
                    return size.y * Half;
            }
        }

        public Vector2 GetWorldPosition(PlatformSide side, float offset, float height, Vector2 position, Vector2 size)
        {
            Vector2 sideCenter = GetSideCenter(side, position, size);
            Vector2 normal = GetNormal(side);
            Vector2 moveAxis = GetMoveAxis(side);

            return sideCenter + normal * height + moveAxis * offset;
        }

        public PlatformSide GetNextSide(PlatformSide side)
        {
            return _sideOrder[(Array.IndexOf(_sideOrder, side) + One) % _sideOrder.Length];
        }

        public PlatformSide GetPrevSide(PlatformSide side)
        {
            return _sideOrder[(Array.IndexOf(_sideOrder, side) + _sideOrder.Length - One) % _sideOrder.Length];
        }

        private Vector2 GetSideCenter(PlatformSide side, Vector2 position, Vector2 size)
        {
            switch (side)
            {
                case PlatformSide.Top:
                    return position + Vector2.up * (size.y * Half);
                case PlatformSide.Bottom:
                    return position + Vector2.down * (size.y * Half);
                case PlatformSide.Right:
                    return position + Vector2.right * (size.x * Half);
                case PlatformSide.Left:
                    return position + Vector2.left * (size.x * Half);
                default:
                    return position;
            }
        }

        private Vector2 GetNormal(PlatformSide side)
        {
            switch (side)
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

        private Vector2 GetMoveAxis(PlatformSide side)
        {
            switch (side)
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
    }
}
