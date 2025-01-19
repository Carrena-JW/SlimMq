using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SlimMq.Abstracts;

namespace SlimMq.Exetentions
{
    public static class DependencyInjectionExtentions
    {
        public static IServiceCollection AddSlimMq(this IServiceCollection collection, Action<ISlimMqConfigurator> configure = null)
        {
            collection.AddSingleton<ConnectionFactory>();

            collection.ConfigureOptions<SlimMqOptionSetup>();

            var cofigurator = new SlimMqConfigurator(collection);

            configure.Invoke(cofigurator);

            //configure.SetSlimMqOptions();

            return collection;
        }
    }
}
