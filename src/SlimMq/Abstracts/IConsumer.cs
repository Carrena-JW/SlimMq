namespace SlimMq.Abstracts
{
    public interface IConsumer
    {
        Task ConsumeAsync<T>(Func<Task> action);
    }
}
