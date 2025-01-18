namespace SlimMq
{
    public interface IConsumer
    {
        Task ConsumeAsync<T>(Action action);
    }
}
