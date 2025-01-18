using SlimMq.Host.BatchModels;
using System.Diagnostics;

namespace SlimMq.Host.BatchBusinessService
{
    internal class TestBusiness1
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(100);

        internal async Task StartAync()
        {
            var sw = new Stopwatch();
            sw.Start();

            var numbers = Enumerable.Range(1, 10000);

            var connection = new ConnectionFactory("E:\\SlimMq_Storage")
                .CreateConnection("TestBusiness1");

            var tasks = new List<Task>();

            foreach (var number in numbers)
            {
                tasks.Add(Task.Run(async () =>
                {
                    await Task.Delay(1000); // 임의 지연시간 knox 결재건 상태체크 (1초)

                    await connection.PublishAsync("TestModel", new TestModel1
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

            Console.WriteLine($"Total seconds: {sw.Elapsed.TotalSeconds}");
        }
    }
}
