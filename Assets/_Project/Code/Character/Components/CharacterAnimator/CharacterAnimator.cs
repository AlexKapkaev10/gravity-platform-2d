using Project.Services;
using UnityEngine;
using VContainer;

namespace Project.Gameplay
{
    public class CharacterAnimator : MonoBehaviour, IComponent
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CharacterAnimatorConfig _config;

        private IInputService _inputService;
        private bool _isRunning;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Tick()
        {
            UpdateDirection();
            UpdateAnimation();
        }

        private void UpdateDirection()
        {
            if (_inputService.MoveInput != 0f)
            {
                _spriteRenderer.flipX = _inputService.MoveInput > 0f;
            }
        }

        private void UpdateAnimation()
        {
            var shouldRun = _inputService.MoveInput != 0f;

            if (shouldRun == _isRunning)
            {
                return;
            }

            _isRunning = shouldRun;
            _animator.SetTrigger(_isRunning ? _config.RunTrigger : _config.IdleTrigger);
        }
    }
}
