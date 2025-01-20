namespace Swfa.Host.WindowsService.Workers;

internal class SubscribeWorker : BackgroundService
{
    private readonly TestSubscriber _testConsumer;

    public SubscribeWorker(TestSubscriber testConsumer)
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
