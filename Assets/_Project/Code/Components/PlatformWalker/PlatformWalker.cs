using Project.Services;
using UnityEngine;
using VContainer;

namespace Project.Gameplay
{
    public class PlatformWalker : MonoBehaviour
    {
        [SerializeField] private Platform _platform;
        [SerializeField] private Transform _transform;
        [SerializeField] private PlatformWalkerConfig _config;

        private PlatformSide _currentSide = PlatformSide.Top;
        private IInputService _inputService;
        
        private float _offset;
        private float _height;
        private float _verticalVelocity;
        private float _targetAngle;
        private bool _isGrounded = true;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Start()
        {
            SnapToSurface();
        }

        private void Update()
        {
            HandleJump();
            HandleMovement();
            HandleSideSwitch();
            ApplyTransform();
        }

        private void HandleJump()
        {
            if (_isGrounded && _inputService.JumpInput)
            {
                _verticalVelocity = _config.JumpForce;
                _isGrounded = false;
            }

            if (!_isGrounded)
            {
                _verticalVelocity -= _config.Gravity * Time.deltaTime;
                _height += _verticalVelocity * Time.deltaTime;

                if (_height <= 0f)
                {
                    _height = 0f;
                    _verticalVelocity = 0f;
                    _isGrounded = true;
                }
            }
        }

        private void HandleMovement()
        {
            _offset += _inputService.MoveInput * _config.MoveSpeed * Time.deltaTime;
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

        private void ApplyTransform()
        {
            _transform.position = _platform.GetWorldPosition(
                _currentSide,
                _offset,
                _height
            );
            
            var newAngle = Mathf
                .MoveTowardsAngle(_transform.eulerAngles.z, 
                    _targetAngle, 
                    _config.RotationSpeed * Time.deltaTime);
            
            _transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
        }

        private void SnapToSurface()
        {
            _height = 0f;
            _isGrounded = true;
            UpdateTargetAngle();
            ApplyTransform();
        }

    }
}