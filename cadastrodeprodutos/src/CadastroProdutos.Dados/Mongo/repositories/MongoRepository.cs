
using CadastroProdutos.Dados.Entidades;
using  MongoDB.Driver;

namespace CadastroProdutos.Dados.Mongo.repositories
{
    public abstract class MongoRepository<TDocument>
        where TDocument : IEntidade

    {
        protected IMongoDatabase MongoDatabase {get;}
        public string CollectionName => Colecoes.ObterNomeColecao<TDocument>();

        protected MongoRepository(IMongoDatabase mongoDatabase)
        {
            MongoDatabase = mongoDatabase;
        }

        protected IMongoCollection<TDocument> Collection => MongoDatabase.GetCollection<TDocument>(CollectionName);
    
    }
}