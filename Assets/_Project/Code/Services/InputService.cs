using UnityEngine;
using VContainer.Unity;

namespace Project.Gameplay
{
    public interface IInputService : ITickable
    {
        float MoveInput { get; }
        bool JumpInput { get; }
    }
    
    public class InputService : IInputService
    {
        public float MoveInput { get; private set; }
        public bool JumpInput { get; private set; }

        public void Tick()
        {
            MoveInput = 0f;
            JumpInput = false;

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
    }
}
