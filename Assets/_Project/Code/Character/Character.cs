using UnityEngine;

namespace Project.Gameplay
{
    public class Character : MonoBehaviour
    {
        private IComponent[]  _components;
        
        private void Awake()
        {
            _components = GetComponentsInChildren<IComponent>();
        }

        private void Update()
        {
            foreach (var component in _components)
            {
                component.Tick();
            }
        }
    }
}