using UnityEngine;

namespace Project.Gameplay
{
    [CreateAssetMenu(fileName = nameof(PlatformWalkerConfig), menuName = "Config/Platform Walker")]
    public class PlatformWalkerConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; } = 3f;
        [field: SerializeField] public float JumpForce { get; private set; } = 4f;
        [field: SerializeField] public float Gravity { get; private set; } = 15f;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 400;
    }
}