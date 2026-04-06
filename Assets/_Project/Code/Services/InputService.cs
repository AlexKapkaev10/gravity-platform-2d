using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Project.Services
{
    public interface IInputService : IInitializable, IDisposable
    {
        float MoveInput { get; }
        bool JumpInput { get; }
    }
    
    public class InputService : IInputService
    {
        private readonly InputActions _inputActions = new ();

        public float MoveInput { get; private set; }
        public bool JumpInput { get; private set; }

        public void Initialize()
        {
            _inputActions.Enable();
            
            PlayerMapEnable();
        }

        private void PlayerMapEnable()
        {
            _inputActions.Player.Enable();

            _inputActions.Player.Move.performed += OnMovePerformed;
            _inputActions.Player.Move.canceled += OnMoveCanceled;
            _inputActions.Player.Jump.performed += OnJumpPerformed;
            _inputActions.Player.Jump.canceled += OnJumpCanceled;
        }

        private void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            MoveInput = ctx.ReadValue<Vector2>().x;
        }

        private void OnMoveCanceled(InputAction.CallbackContext ctx)
        {
            MoveInput = 0f;
        }

        private void OnJumpPerformed(InputAction.CallbackContext ctx)
        {
            JumpInput = true;
        }

        private void OnJumpCanceled(InputAction.CallbackContext ctx)
        {
            JumpInput = false;
        }

        public void Dispose()
        {
            _inputActions.Player.Move.performed -= OnMovePerformed;
            _inputActions.Player.Move.canceled -= OnMoveCanceled;
            _inputActions.Player.Jump.performed -= OnJumpPerformed;
            _inputActions.Player.Jump.canceled -= OnJumpCanceled;

            _inputActions?.Dispose();
        }
    }
}
