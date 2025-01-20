using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SlimMq.Abstracts;
using System.Reflection;

namespace SlimMq;


public class SlimMqConfigurator : ISlimMqConfigurator
{
    private readonly IOptionsMonitor<SlimMqOptions> _options;
    private readonly ConnectionFactory _connectionFactory;

    public SlimMqConfigurator(IServiceCollection collection, ConnectionFactory connectionFactory)
    {
        var options = collection.BuildServiceProvider()
                .GetRequiredService<IOptionsMonitor<SlimMqOptions>>();

        _options = options;
        _connectionFactory = connectionFactory;
    }

    public void SetStoragePath(string rootPath)
    {
        _options.CurrentValue.StorageRootPath = rootPath;
        
    }

    public void AddConsumers(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var subscriberTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscriber<>)));

            foreach (var type in subscriberTypes)
            {
                var subscriberInterface = type.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscriber<>));
                var messageType = subscriberInterface.GenericTypeArguments[0];

                var instance = Activator.CreateInstance(type);

                var message = Activator.CreateInstance(messageType); 

                var propertyInfo = type.GetProperty("Channel");

                if (propertyInfo != null)
                {
                    string channelValue = (string)propertyInfo.GetValue(instance)!;

                    var consumer = _connectionFactory
                            .CreateConsumer(channelValue, messageType.Name);

                    var subscribeMethod = subscriberInterface.GetMethod("Subscribe");
                    if (subscribeMethod != null)
                    {
                        var consumeAsyncMethod = consumer.GetType().GetMethod("ConsumeAsync")
                            ?.MakeGenericMethod(messageType)!;

                        var taks = (Task)consumeAsyncMethod.Invoke(consumer, new object[]
                        {
                            new Func<object, Task>(async (msg) =>
                            {
                                await (Task) subscribeMethod.Invoke(instance, new object[] { msg, new CancellationToken() })!;
                            })
                        })!;
                    }
                }
            }
        }
    }

    public void SetSlimMqOptions()
    {

    }
}
