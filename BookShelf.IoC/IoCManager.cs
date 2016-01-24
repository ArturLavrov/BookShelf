using Ninject;

namespace BookShelf.IoC
{
    public static class IoCManager
    {
        public static IKernel Kernel { get; set; }
        public static void Start()
        {
            Kernel = new StandardKernel(new NinjectConfiguration());
        }

        public static void Stop()
        {
            Kernel.Dispose();
        }
    }
}
