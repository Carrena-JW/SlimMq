using SlimMq;
using Swfa.Host.WindowsService.Models;
using Swfa.Host.WindowsService.Workers;

var builder = Host.CreateApplicationBuilder(args);

// Windows ���񽺷� ������ �� �ֵ��� ����
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "DSAD SWFA BatchService";
});

builder.Services.AddSingleton(async () =>
{
    var consumer = new ConnectionFactory("E:\\SlimMq_Storage")
    .CreateConsumer("TestBusiness1", "TestTask1");

    await consumer.ConsumeAsync<TestModel1>(() =>
    {
        Console.WriteLine("This is action");
    });
});



LoggerProviderOptions.RegisterProviderOptions<
    EventLogSettings, EventLogLoggerProvider>(builder.Services);

// Hosted Service ���
builder.Services.AddHostedService<TestWorker>();

var host = builder.Build();
host.Run();