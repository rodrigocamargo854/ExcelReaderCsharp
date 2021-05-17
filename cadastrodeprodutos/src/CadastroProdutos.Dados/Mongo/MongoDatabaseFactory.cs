using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using OpenTracing.Contrib.Mongo;

namespace CadastroProdutos.Dados.Mongo
{
    public class MongoDatabaseFactory : IMongoDatabaseFactory
    {
        private readonly IMongoInfoProvider _mongoInfoProvider;
        private readonly ILogger<MongoDatabaseFactory> _logger;

        public MongoDatabaseFactory(IMongoInfoProvider mongoInfoProvider, ILogger<MongoDatabaseFactory> logger)
        {
            _mongoInfoProvider = mongoInfoProvider;
            _logger = logger;
        }

        public IMongoDatabase GetDatabase()
        {
            var host = _mongoInfoProvider.Host;
            var username = _mongoInfoProvider.UserName;
            var password = _mongoInfoProvider.Password;
            var port = _mongoInfoProvider.Port;
            var args = _mongoInfoProvider.Args;

            var temCredenciais = !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password);
            var stringConexao = temCredenciais
                ? $"mongodb://{username}:{password}@{host}:{port}/{args}"
                : $"mongodb://{host}:{port}/{args}";
            _logger.LogInformation($"Conectando no mongo em '{stringConexao}'");
            var client = _mongoInfoProvider.TraceEnabled
                ? new TracingMongoClient(stringConexao)
                : new MongoClient(stringConexao);

            var mongoDatabase = client.GetDatabase("cadastro_produtos");
            MappingLoader.LoadAllClassMappings();
            MappingLoader.EnsureAllIndices(mongoDatabase);

            return mongoDatabase;
        }
    }
}