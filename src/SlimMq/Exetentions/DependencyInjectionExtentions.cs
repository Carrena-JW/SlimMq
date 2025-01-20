using Microsoft.Extensions.DependencyInjection;
using SlimMq.Abstracts;

namespace SlimMq.Exetentions
{
    public static class DependencyInjectionExtentions
    {
        public static IServiceCollection AddSlimMq(this IServiceCollection collection, Action<ISlimMqConfigurator> configure = null)
        {
            collection.AddSingleton<ConnectionFactory>();

            collection.ConfigureOptions<SlimMqOptionSetup>();

            var connetionFactory = collection.BuildServiceProvider()
                .GetService<ConnectionFactory>()!;

            var cofigurator = new SlimMqConfigurator(collection, connetionFactory);

            configure.Invoke(cofigurator);

            //configure.SetSlimMqOptions();

            return collection;
        }
    }
}
