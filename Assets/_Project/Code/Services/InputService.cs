using UnityEngine;

namespace Project.Gameplay
{
    public class InputService : MonoBehaviour
    {
        public float MoveInput { get; private set; }
        public bool JumpInput { get; private set; }

        private void Update()
        {
            MoveInput = 0f;
            JumpInput = false;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                MoveInput = -1f;

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                MoveInput = 1f;

            if (Input.GetKeyDown(KeyCode.Space))
                JumpInput = true;
        }
    }
}
