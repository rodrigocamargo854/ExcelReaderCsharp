namespace CadastroProdutos.Dados.Mongo
{
    public interface IMongoInfoProvider
    {
        string Host { get; }
        string UserName { get; }
        string Password { get; }
        int Port { get; }
        string Args { get; }
        bool TraceEnabled { get; }
    }
}