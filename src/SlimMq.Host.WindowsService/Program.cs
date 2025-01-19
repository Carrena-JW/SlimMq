using SlimMq.Exetentions;
using Swfa.Host.WindowsService;
using Swfa.Host.WindowsService.Consumers;

var builder = Host.CreateApplicationBuilder(args);

// Windows 서비스로 실행할 수 있도록 설정
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "DSAD SWFA BatchService";
});

builder.Services.AddSlimMq(config =>
{
    config.SetStoragePath("dddddddd");
    config.AddConsumers(AssemblyReference.Assembly);
});


builder.Services.AddSingleton<TestConsumer>();

LoggerProviderOptions.RegisterProviderOptions<
    EventLogSettings, EventLogLoggerProvider>(builder.Services);

// Hosted Service 등록
builder.Services.AddHostedService<SubscribeWorker>();
builder.Services.AddHostedService<TestWorker>();

var host = builder.Build();
host.Run();