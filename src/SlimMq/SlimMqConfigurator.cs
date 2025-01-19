using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SlimMq.Abstracts;
using System.Reflection;

namespace SlimMq;


public class SlimMqConfigurator : ISlimMqConfigurator
{
    private readonly IOptionsMonitor<SlimMqOptions> _options;

    public SlimMqConfigurator(IServiceCollection collection)
    {
        var options = collection.BuildServiceProvider()
                .GetRequiredService<IOptionsMonitor<SlimMqOptions>>();
        
        _options = options;
    }

    public void SetStoragePath(string rootPath)
    {
        _options.CurrentValue.StorageRootPath = rootPath;
        
    }

    public void AddConsumers(params Assembly[] assemblies)
    {
        return;

        foreach (var assembly in assemblies)
        {
            // 모든 타입을 가져와서 ISubscriber<T>를 구현하는 타입을 찾습니다.
            var subscriberTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscriber<>)));

            foreach (var type in subscriberTypes)
            {
                // ISubscriber<T>의 T 타입을 가져옵니다.
                var subscriberInterface = type.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscriber<>));
                var messageType = subscriberInterface.GenericTypeArguments[0];

                // 인스턴스를 생성합니다.
                var instance = Activator.CreateInstance(type);

                // 메시지와 CancellationToken을 준비합니다.
                var message = Activator.CreateInstance(messageType); // 기본 생성자를 통한 메시지 인스턴스 생성
                var token = new CancellationToken(); // 예시 토큰

                // Subscribe 메서드를 호출합니다.
                var subscribeMethod = subscriberInterface.GetMethod("Subscribe");
                if (subscribeMethod != null)
                {
                    // Subscribe 메서드를 비동기로 호출합니다.
                    _ = subscribeMethod.Invoke(instance, new object[] { message, token });
                }
            }
        }
    }

    public void SetSlimMqOptions()
    {

    }
}
