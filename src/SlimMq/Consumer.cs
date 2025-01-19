namespace SlimMq;

public class Consumer : IConsumer
{
    private readonly string _routeKey;
    private readonly string _channelPath;
    private readonly FileSystemWatcher _watcher;
    private Func<Task> _action;

    public Consumer(string channelPath, string routeKey)
    {
        _routeKey = routeKey;
        _channelPath = channelPath;

        var watcherFilter = $"*{_routeKey}.{Const.QUEUE_FILE_EXTENTION}";
        _watcher = new FileSystemWatcher(_channelPath, watcherFilter);
    }

    public Task ConsumeAsync<T>(Func<Task> action)
    {
        _watcher.Created += EventHandler;
        _watcher.EnableRaisingEvents = true;
        _action = action;

        return Task.CompletedTask;
    }

    private async void EventHandler(object sender, FileSystemEventArgs e)
    {
        await Task.Delay(1);

       // var meta = AlternateDataStreamUtility.ReadFileIdFromADSAsync(e.FullPath);
            
        await _action.Invoke();

        Console.WriteLine("EvenrHandler !!");

        // 큐제거

        await FileUtility.DeleteWithRetry(e.FullPath);
    }
}
