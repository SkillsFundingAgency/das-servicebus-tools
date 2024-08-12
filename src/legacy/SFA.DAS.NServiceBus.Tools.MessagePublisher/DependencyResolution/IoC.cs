using StructureMap;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            return new Container(c =>
            {
                c.AddRegistry<DefaultRegistry>();
            });
        }
    }
}