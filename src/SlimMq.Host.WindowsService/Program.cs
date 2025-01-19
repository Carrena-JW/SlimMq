using SlimMq;
using Swfa.Host.WindowsService.Models;
using Swfa.Host.WindowsService.Workers;

var builder = Host.CreateApplicationBuilder(args);

// Windows 서비스로 실행할 수 있도록 설정
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "DSAD SWFA BatchService";
});

if (true)
{
    var consumer = new ConnectionFactory("E:\\SlimMq_Storage")
        .CreateConsumer("TestBusiness1", "TestTask1");

    await consumer.ConsumeAsync<TestModel1>(async () =>
    {
        await Task.Delay(1000);
        Console.WriteLine("Completed sendding email");
    });
}



LoggerProviderOptions.RegisterProviderOptions<
    EventLogSettings, EventLogLoggerProvider>(builder.Services);

// Hosted Service 등록
builder.Services.AddHostedService<TestWorker>();

var host = builder.Build();
host.Run();