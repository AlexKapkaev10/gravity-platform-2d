# Gravity Platform 2D

A character walks along all four sides of a rectangular platform — top, bottom, left, and right.
When reaching a corner, the character transitions to the next side with a smooth rotation.
Jumping works relative to the current surface.

## Architecture

- `Platform` — MonoBehaviour that provides platform geometry. All logic lives in `PlatformModel`.
- `PlatformWalker` — MonoBehaviour that applies position and rotation to the transform. All logic lives in `PlatformWalkerModel`.
- `InputService` — reads input via Unity Input System, exposes `MoveInput` and `JumpInput`.
- `CharacterAnimator` — MonoBehaviour that switches animations and sprite flip based on input.

## Dependencies

- VContainer — DI container
- Unity Input System

---

Unity 6000.3.10f1
