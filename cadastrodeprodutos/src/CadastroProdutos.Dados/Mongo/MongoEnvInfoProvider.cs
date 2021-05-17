using System;

namespace CadastroProdutos.Dados.Mongo
{
    public class MongoEnvInfoProvider : IMongoInfoProvider
    {
        public string Host => Environment.GetEnvironmentVariable("MONGO_HOST") ?? "localhost";
        public string UserName => Environment.GetEnvironmentVariable("MONGO_USERNAME");
        public string Password => Environment.GetEnvironmentVariable("MONGO_PASSWORD");
        public int Port => int.Parse(Environment.GetEnvironmentVariable("MONGO_PORT") ?? "27017");
        public string Args => Environment.GetEnvironmentVariable("MONGO_ARGUMENTS") ?? "";
        public bool TraceEnabled => bool.Parse(Environment.GetEnvironmentVariable("MONGO_TRACE")?.ToLower() ?? "true");
    }
}