using SlimMq.Abstracts;

namespace SlimMq;

public class Consumer : IConsumer
{
    private readonly string _routeKey;
    private readonly string _channelPath;
    private readonly FileSystemWatcher _watcher;
    private dynamic _action;

    public Consumer(string channelPath, string routeKey)
    {
        _routeKey = routeKey;
        _channelPath = channelPath;

        //var watcherFilter = $"*_{routeKey}.{Const.QUEUE_FILE_EXTENTION}";
        var watcherFilter = $"*_{routeKey}.{Const.QUEUE_FILE_EXTENTION}";
        _watcher = new FileSystemWatcher(_channelPath, watcherFilter);
    }

    public Task ConsumeAsync<T>(Func<T,Task> action)
    {
        _watcher.Created += EventHandler<T>;
        _watcher.Error += ErrorEventHandler;
        _watcher.InternalBufferSize = 65536;
        _watcher.EnableRaisingEvents = true;
        _action = action;

        FileSystemWatchers.AddWatcher(_watcher);

        return Task.CompletedTask;
    }

    private void ErrorEventHandler(object sender, ErrorEventArgs e)
    {
        Console.WriteLine(e.ToString());
    }

    private async void EventHandler<T>(object sender, FileSystemEventArgs e)
    {
        await Task.Delay(100);

        var meta = await AlternateDataStreamUtility.ReadFileIdFromADSAsync(e.FullPath);

        var body = await File.ReadAllTextAsync(e.FullPath);

        var obj = JsonConvert.DeserializeObject<T>(body);

        await _action.Invoke(obj);

       // Console.WriteLine("EvenrHandler !!");

        // 큐제거
        if (false)
        {
            await FileUtility.DeleteWithRetry(e.FullPath);

        }
        else
        {
            try
            {
                File.Delete(e.FullPath);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
