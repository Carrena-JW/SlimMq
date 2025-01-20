var builder = Host.CreateApplicationBuilder(args);

// Windows ���񽺷� ������ �� �ֵ��� ����
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "DSAD SWFA BatchService";
});

builder.Services.AddSlimMq(config =>
{
    config.SetStoragePath("dddddddd");
    config.AddConsumers(AssemblyReference.Assembly);
});


builder.Services.AddSingleton<TestSubscriber>();

LoggerProviderOptions.RegisterProviderOptions<
    EventLogSettings, EventLogLoggerProvider>(builder.Services);

// Hosted Service ���
builder.Services.AddHostedService<SubscribeWorker>();
builder.Services.AddHostedService<TestWorker>();

var host = builder.Build();
host.Run();