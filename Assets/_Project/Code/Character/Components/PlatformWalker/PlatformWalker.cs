using Project.Services;
using UnityEngine;
using VContainer;

namespace Project.Gameplay
{
    public class PlatformWalker : MonoBehaviour, IComponent
    {
        [SerializeField] private Platform _platform;
        [SerializeField] private Transform _transform;
        [SerializeField] private PlatformWalkerConfig _config;

        private IInputService _inputService;
        private PlatformWalkerModel _model;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Start()
        {
            _model = new PlatformWalkerModel(_platform, _config, _inputService);
            _model.SnapToSurface();
            
            ApplyTransform();
        }

        public void Tick()
        {
            _model.Tick(Time.deltaTime);
            ApplyTransform();
        }

        private void ApplyTransform()
        {
            _transform.position = _model.Position;

            var newAngle = Mathf.MoveTowardsAngle(
                _transform.eulerAngles.z,
                _model.TargetAngle,
                _config.RotationSpeed * Time.deltaTime);

            _transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
        }
    }
}
