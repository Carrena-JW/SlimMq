namespace SlimMq.Abstracts;

public interface IPublisher
{
    Task PublishAsync<T>(string typeName, T body, [CallerMemberName] string caller = "");
}
