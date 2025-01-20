namespace SlimMq.Abstracts;

public interface ISubscriber<T>
{
    string Channel { get; init; }
    Task Subscribe(T message, CancellationToken token = default);
}
