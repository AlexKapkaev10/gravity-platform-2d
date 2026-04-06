using Project.Gameplay;
using VContainer;
using VContainer.Unity;

namespace Project.Core
{
    public class GameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .Register<InputService>(Lifetime.Scoped)
                .As<IInputService>()
                .As<ITickable>();
        }
    }
}