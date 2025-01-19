namespace SlimMq
{
    public interface IConsumer
    {
        Task ConsumeAsync<T>(Func<Task> action);
    }
}
