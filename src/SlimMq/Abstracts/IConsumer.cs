namespace SlimMq.Abstracts
{
    public interface IConsumer
    {
        Task ConsumeAsync<T>(Func<T,Task> action);
    }
}
