using SlimMq;
using SlimMq.Abstracts;

namespace Swfa.Host.WindowsService.Consumers;

internal class TestConsumer : ISubscriber<TestModel1>
{
    private readonly ConnectionFactory _connectionFactory;
    public TestConsumer(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task Subscribe(TestModel1 message, CancellationToken token)
    {
        //await Task.Delay(100);

        //Console.WriteLine(message.MyProperty1);

        var consumer = _connectionFactory
               .CreateConsumer("TestBusiness1", "TestTask1");

        await consumer.ConsumeAsync<TestModel1>(async () =>
        {
            await Task.Delay(1000);
            //Console.WriteLine("Completed sendding email");
        });
    }
}
