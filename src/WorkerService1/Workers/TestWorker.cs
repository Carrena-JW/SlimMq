using SlimMq;
using System.Diagnostics;

namespace Swfa.Host.WindowsService.Workers
{
    public class TestWorker : BackgroundService
    {
        private readonly ILogger<TestWorker> _logger;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(100);

        public TestWorker(ILogger<TestWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                var sw = new Stopwatch();
                sw.Start();

                var numbers = Enumerable.Range(1, 10000);

                var publisher = new ConnectionFactory("E:\\SlimMq_Storage")
                    .CreatePublisher("TestBusiness1", "TestTask1");

                var tasks = new List<Task>();

                foreach (var number in numbers)
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        await Task.Delay(1000); // 임의 지연시간 knox 결재건 상태체크 (1초)

                        await publisher.PublishAsync("TestModel", new
                        {
                            MyProperty1 = number,
                            MyProperty2 = "TestModel",
                            //MyProperty3 = DateTime.Now
                        });

                        _semaphore.Release();
                    }));
                }

                await Task.WhenAll(tasks);

                sw.Stop();

                _logger.LogInformation($"Total seconds: {sw.Elapsed.TotalSeconds}");

                await Task.Delay((60 * 1000), stoppingToken);
            }
        }
    }
}
