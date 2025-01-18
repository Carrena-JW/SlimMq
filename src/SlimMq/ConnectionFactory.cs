using SlimMq.Utilities;

namespace SlimMq
{
    public class ConnectionFactory
    {
        private readonly string _FileQueueRootPath;
        private readonly string _tempFolderPath;

        public ConnectionFactory(string fileQueueRootPath, bool allowAnonymusQueueFile = false)
        {
            _FileQueueRootPath = fileQueueRootPath;
            _tempFolderPath = Path.Combine(_FileQueueRootPath, "$temp");
            //_channelIdentifier = channelIdentifier;

            InitializeAsync();
        }

        private Task InitializeAsync()
        {
            // Queue Path 유효성
            if(File.Exists(_FileQueueRootPath) is false)
            {
                throw new DirectoryNotFoundException($"Directory not fountd, intput value: {_FileQueueRootPath}");
            }

            // tempFolder (숨김) 생성
            DirectoryUtility.CreateFolder(_tempFolderPath, true);

            return Task.CompletedTask;
        }

        public void CreateConnection()
        {
        }

        public void CreateChannel(string channel)
        {
            // channel folder 생성
            DirectoryUtility.CreateFolder(Path.Combine(_FileQueueRootPath, channel));

        }

        public Task PublishAsync(string routeKey, string ModelName, object message)
        {
            return Task.FromResult(0);
        }

        public Task ConsumeAsync(Func<Action> action)
        {

            return Task.CompletedTask;
        }
    }
}
