using UnityEngine;

namespace Project.Gameplay
{
    public class PlatformWalker : MonoBehaviour
    {
        [SerializeField] private Platform _platform;
        [SerializeField] private Transform _transform;
        
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private float _jumpForce = 7f;
        [SerializeField] private float _gravity = 15f;
        [SerializeField] private float _rotationSpeed = 720f;

        private PlatformSide _currentSide = PlatformSide.Top;
        private float _offset;
        private float _height;
        private float _verticalVelocity;
        private float _targetAngle;

        private bool _isGrounded = true;

        public float MoveInput = 0f;
        public bool JumpInput = false;

        private void Start()
        {
            SnapToSurface();
        }

        private void Update()
        {
            ReadInput();
            HandleJump();
            HandleMovement();
            HandleSideSwitch();
            ApplyTransform();
            ResetInput();
        }

        private void ReadInput()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                MoveInput = -1f;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                MoveInput = 1f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpInput = true;
            }
        }

        private void HandleJump()
        {
            if (_isGrounded && JumpInput)
            {
                _verticalVelocity = _jumpForce;
                _isGrounded = false;
            }

            if (!_isGrounded)
            {
                _verticalVelocity -= _gravity * Time.deltaTime;
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
            _offset += MoveInput * _moveSpeed * Time.deltaTime;
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

            float currentAngle = _transform.eulerAngles.z;
            float newAngle = Mathf.MoveTowardsAngle(currentAngle, _targetAngle, _rotationSpeed * Time.deltaTime);
            _transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
        }

        private void SnapToSurface()
        {
            _height = 0f;
            _isGrounded = true;
            UpdateTargetAngle();
            ApplyTransform();
        }

        private void ResetInput()
        {
            MoveInput = 0f;
            JumpInput = false;
        }
    }
}