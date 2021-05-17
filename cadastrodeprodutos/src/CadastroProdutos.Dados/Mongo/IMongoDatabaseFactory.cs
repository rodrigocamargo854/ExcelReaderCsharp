using MongoDB.Driver;

namespace CadastroProdutos.Dados.Mongo
{
    public interface IMongoDatabaseFactory
    {
        IMongoDatabase GetDatabase();
    }
}