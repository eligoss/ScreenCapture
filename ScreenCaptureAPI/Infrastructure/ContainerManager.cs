using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace ScreenCaptureAPI
{
    public static class ContainerManager
    {
        private static IUnityContainer container = new UnityContainer();

        public static IUnityContainer Container
        {
            get { return container; }
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public static IEnumerable<T> ResolveAll<T>()
        {
            return container.ResolveAll<T>();
        }

        public static T Resolve<T>(string name)
        {
            return container.Resolve<T>(name);
        }
    }
}
