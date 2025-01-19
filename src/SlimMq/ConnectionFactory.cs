using Microsoft.Extensions.Options;
using SlimMq.Abstracts;

namespace SlimMq;

public class ConnectionFactory
{
    private readonly string _fileQueueRootPath;
    private readonly string _tempFolderPath;

    public ConnectionFactory(IOptionsMonitor<SlimMqOptions> option)
    //public ConnectionFactory(string fileQueueRootPath, bool allowAnonymusQueueFile = false)
    {
        _fileQueueRootPath = option.CurrentValue.StorageRootPath;
        _tempFolderPath = Path.Combine(_fileQueueRootPath, "$temp");
        //_channelIdentifier = channelIdentifier;

        InitializeAsync();
    }

   

    private Task InitializeAsync()
    {
        // ADS 사용 가능 여부 확인
        if (AlternateDataStreamUtility.IsNTFS(_fileQueueRootPath) is false)
        {
            throw new NotSupportedException("Disk volume must be NTFS.");
        }

        // Queue Path 유효성
        if(Directory.Exists(_fileQueueRootPath) is false)
        {
            throw new DirectoryNotFoundException($"Directory not fountd, intput value: {_fileQueueRootPath}");
        }

        // tempFolder (숨김) 생성
        DirectoryUtility.CreateFolder(_tempFolderPath, true);

        return Task.CompletedTask;
    }

    public IPublisher CreatePublisher(string channel, string routeKey)
    {
        var rootPathWithChannel = Path.Combine(_fileQueueRootPath, channel);

        DirectoryUtility.CreateFolder(rootPathWithChannel);

        return new Publisher(_tempFolderPath, _fileQueueRootPath, routeKey, rootPathWithChannel);
    }

    public IConsumer CreateConsumer(string channel, string routeKey)
    {
        var rootPathWithChannel = Path.Combine(_fileQueueRootPath, channel);

        if(Directory.Exists(rootPathWithChannel) is false){
            throw new DirectoryNotFoundException($"Directory not fountd, intput value: {rootPathWithChannel}");
        }

        return new Consumer(rootPathWithChannel, routeKey);
    }

   
}
