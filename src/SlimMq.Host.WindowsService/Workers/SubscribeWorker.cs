using Swfa.Host.WindowsService.Subscribers;

namespace Swfa.Host.WindowsService.Workers;

internal class SubscribeWorker : BackgroundService
{
    private readonly TestConsumer _testConsumer;

    public SubscribeWorker(TestConsumer testConsumer)
    {
        _testConsumer = testConsumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.WhenAll(new List<Task>
        {
           // _testConsumer.Subscribe(default,stoppingToken)
        });
    }
}
