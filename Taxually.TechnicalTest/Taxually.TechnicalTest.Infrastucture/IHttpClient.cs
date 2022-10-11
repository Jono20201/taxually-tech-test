namespace Taxually.TechnicalTest.Infrastucture;

public interface IHttpClient
{
    public Task PostAsync<TRequest>(string url, TRequest request);
}