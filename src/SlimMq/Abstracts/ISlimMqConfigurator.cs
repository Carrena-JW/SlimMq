using System.Reflection;

namespace SlimMq.Abstracts
{
    public interface ISlimMqConfigurator
    {
        void SetStoragePath(string rootPath);
        void AddConsumers(params Assembly[] assemblies);
    }
}
