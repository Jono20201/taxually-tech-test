namespace Taxually.TechnicalTest.Infrastucture;

public interface IQueueClient
{
    public Task EnqueueAsync<TPayload>(string queueName, TPayload payload);
}