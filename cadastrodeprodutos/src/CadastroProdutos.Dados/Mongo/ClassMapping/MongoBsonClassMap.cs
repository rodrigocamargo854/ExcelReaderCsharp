using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using CadastroProdutos.Dados.Entidades;

namespace CadastroProdutos.Dados.Mongo.ClassMapping
{
    public abstract class MongoBsonClassMap<TDocument> : IMapping<TDocument>
        where TDocument : IEntidade
    {
        public void RegisterClassMap()
        {
            BsonClassMap.RegisterClassMap<TDocument>(RegisterBsonClassMap);
        }

        protected abstract void RegisterBsonClassMap(BsonClassMap<TDocument> classMap);

        protected IndexKeysDefinitionBuilder<TDocument> IndexBuilder => Builders<TDocument>.IndexKeys;

        public void EnsureIndices(IMongoDatabase mongoDatabase)
        {
            var indicesManager = new IndicesManager();
            SetupIndices(indicesManager);

            var indexModels = indicesManager
                .GetIndicesDefinitions()
                .Select(indexDefinition => new CreateIndexModel<TDocument>(indexDefinition.IndexModel,
                    new CreateIndexOptions<TDocument> { Unique = indexDefinition.IsUnique }
                ))
                .ToList();
            if (indexModels.Any())
            {
                mongoDatabase
                    .GetCollection<TDocument>(Colecoes.ObterNomeColecao<TDocument>())
                    .Indexes
                    .CreateMany(indexModels);
            }
        }

        protected virtual void SetupIndices(IndicesManager indicesManager)
        {
        }

        protected class IndicesManager
        {
            private readonly List<(IndexKeysDefinition<TDocument>, bool)> _indices = new List<(IndexKeysDefinition<TDocument>, bool)>();

            public IndicesManager AddIndex(IndexKeysDefinition<TDocument> index, bool unique = false)
            {
                _indices.Add((index, unique));
                return this;
            }

            public IEnumerable<(IndexKeysDefinition<TDocument> IndexModel, bool IsUnique)> GetIndicesDefinitions()
            {
                return _indices;
            }
        }
    }
}