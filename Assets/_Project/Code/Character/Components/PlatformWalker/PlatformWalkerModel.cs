using Project.Services;
using UnityEngine;

namespace Project.Gameplay
{
    public class PlatformWalkerModel
    {
        private readonly Platform _platform;
        private readonly PlatformWalkerConfig _config;
        private readonly IInputService _inputService;

        private PlatformSide _currentSide = PlatformSide.Top;
        private float _offset;
        private float _height;
        private float _verticalVelocity;
        private float _targetAngle;
        private bool _isGrounded = true;

        public Vector2 Position { get; private set; }
        public float TargetAngle => _targetAngle;

        public PlatformWalkerModel(Platform platform, PlatformWalkerConfig config, IInputService inputService)
        {
            _platform = platform;
            _config = config;
            _inputService = inputService;
        }

        public void Tick(float deltaTime)
        {
            HandleJump(deltaTime);
            HandleMovement(deltaTime);
            HandleSideSwitch();
            UpdatePosition();
        }

        public void SnapToSurface()
        {
            _height = 0f;
            _isGrounded = true;
            UpdateTargetAngle();
            UpdatePosition();
        }

        private void HandleJump(float deltaTime)
        {
            if (_isGrounded && _inputService.JumpInput)
            {
                _verticalVelocity = _config.JumpForce;
                _isGrounded = false;
            }

            if (!_isGrounded)
            {
                _verticalVelocity -= _config.Gravity * deltaTime;
                _height += _verticalVelocity * deltaTime;

                if (_height <= 0f)
                {
                    _height = 0f;
                    _verticalVelocity = 0f;
                    _isGrounded = true;
                }
            }
        }

        private void HandleMovement(float deltaTime)
        {
            _offset += _inputService.MoveInput * _config.MoveSpeed * deltaTime;
        }

        private void HandleSideSwitch()
        {
            if (!_isGrounded)
                return;

            float halfLength = _platform.GetHalfLength(_currentSide);

            if (_offset > halfLength)
            {
                float overflow = _offset - halfLength;
                _currentSide = _platform.GetNextSide(_currentSide);
                float newHalfLength = _platform.GetHalfLength(_currentSide);
                _offset = -newHalfLength + overflow;
                UpdateTargetAngle();
            }
            else if (_offset < -halfLength)
            {
                float overflow = _offset + halfLength;
                _currentSide = _platform.GetPrevSide(_currentSide);
                float newHalfLength = _platform.GetHalfLength(_currentSide);
                _offset = newHalfLength + overflow;
                UpdateTargetAngle();
            }
        }

        private void UpdateTargetAngle()
        {
            switch (_currentSide)
            {
                case PlatformSide.Top:
                    _targetAngle = 0f;
                    break;
                case PlatformSide.Right:
                    _targetAngle = -90f;
                    break;
                case PlatformSide.Bottom:
                    _targetAngle = 180f;
                    break;
                case PlatformSide.Left:
                    _targetAngle = 90f;
                    break;
            }
        }

        private void UpdatePosition()
        {
            Position = _platform.GetWorldPosition(_currentSide, _offset, _height);
        }
    }
}
