namespace SlimMq;

public class Consumer : IConsumer
{
    private readonly string _routeKey;
    private readonly string _channelPath;
    private readonly FileSystemWatcher _watcher;
    private Action _action;

    public Consumer(string routeKey, string channelPath)
    {
        _routeKey = routeKey;
        _channelPath = channelPath;

        var watcherFilter = $"*{_routeKey}.{Const.QUEUE_FILE_EXTENTION}";
        _watcher = new FileSystemWatcher(_channelPath, watcherFilter);

    }

    public Task ConsumeAsync<T>(Action action)
    {
        _watcher.Created += EventHandler;
        _watcher.EnableRaisingEvents = true;
        _action = action;

        return Task.CompletedTask;
    }

    private void EventHandler(object sender, FileSystemEventArgs e)
    {
        _action.Invoke();
        Console.WriteLine("EvenrHandler !!");
    }
}
