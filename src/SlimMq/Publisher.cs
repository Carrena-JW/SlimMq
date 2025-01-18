namespace SlimMq;

public class Publisher : IPublisher
{
    private readonly string _tempFolderPath;
    private readonly string _rootFolderPath;
    private readonly string _channelPath;
    private readonly string _routeKey;

    
    public Publisher(string tempFolderPath, string rootFolderPath, string routeKey, string channelPath = "")
    {
        _tempFolderPath = tempFolderPath;
        _rootFolderPath = rootFolderPath;
        _channelPath = channelPath;
        _routeKey = routeKey;
    }

    //namming
    public async Task PublishAsync<T>(string typeName, T body, [CallerMemberName] string caller = "")
    {
        var messageTypeName = typeof(T).Name;

        var jsonBody = ConvertBodyToJson(body!);

        var fileName = $"{Ulid.NewUlid()}_{_routeKey}.{Const.QUEUE_FILE_EXTENTION}";

        var fileNameWithTempPath = Path.Join(_tempFolderPath, fileName);

        await File.WriteAllTextAsync(fileNameWithTempPath,jsonBody);

        AlternateDataStreamUtility.WriteFileIdToADS(fileNameWithTempPath, new QueueFileMeta
        {
            From = caller,
            RouteKey = _routeKey,
            TypeName = typeName,
        });

        File.Move(fileNameWithTempPath, Path.Join(_channelPath, fileName));
    }

    private string ConvertBodyToJson(object body, bool isIndented = true, bool isEncrypt = false)
    {
        //TO-DO.#01 encryted body
        var jsonFormat = isIndented ? Formatting.Indented : Formatting.None;

        return JsonConvert.SerializeObject(body, jsonFormat);
    }


}
