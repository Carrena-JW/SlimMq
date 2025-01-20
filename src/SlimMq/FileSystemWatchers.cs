namespace SlimMq;

internal static class FileSystemWatchers
{
    private static List<FileSystemWatcher> _watchers = new List<FileSystemWatcher>();

    internal static void AddWatcher(FileSystemWatcher watcher)
    {
        _watchers.Add(watcher);
    }
}
