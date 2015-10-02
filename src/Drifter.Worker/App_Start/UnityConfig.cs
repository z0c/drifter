
using Microsoft.Practices.Unity;

namespace Drifter.Worker
{
    internal static class UnityConfig
    {
        internal static IUnityContainer RegisterComponents()
        {
            var container = new UnityContainer();

            return container;
        }
    }
}
