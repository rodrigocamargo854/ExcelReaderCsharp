using MongoDB.Driver;
using CadastroProdutos.Dados.Entidades;

namespace CadastroProdutos.Dados.Mongo.ClassMapping
{
    public interface IMapping<out TEntidade>
        where TEntidade : IEntidade
    {
        void RegisterClassMap();
        void EnsureIndices(IMongoDatabase mongoDatabase);
    }
}