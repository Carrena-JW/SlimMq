using SlimMq;
using SlimMq.Abstracts;

namespace Swfa.Host.WindowsService.Subscribers;

internal class TestConsumer : ISubscriber<TestModel1>
{
    public string Channel { get; init; } = "TestBusiness1";

    public async Task Subscribe(TestModel1 message, CancellationToken token)
    {
        await Task.Delay(1000);
        Console.WriteLine($"{message.MyProperty1} Completed sendding email");
    }
}
