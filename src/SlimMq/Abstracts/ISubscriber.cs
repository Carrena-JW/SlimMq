namespace SlimMq.Abstracts;

public interface ISubscriber<T>
{
    Task Subscribe(T message, CancellationToken token = default);
}
