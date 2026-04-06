using UnityEngine;

namespace Project.Gameplay
{
    [CreateAssetMenu(fileName = nameof(CharacterAnimatorConfig), menuName = "Config/Character Animator")]
    public class CharacterAnimatorConfig : ScriptableObject
    {
        public int IdleTrigger = Animator.StringToHash("Idle");
        public int RunTrigger = Animator.StringToHash("Run");
    }
}