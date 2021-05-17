using System;

namespace CadastroProdutos.WebApi
{
    public static class SentryEnvInfoProvider
    {
        public static string Dsn => Environment.GetEnvironmentVariable("SENTRY_DSN");
        public static string SERVICE_VERSION => Environment.GetEnvironmentVariable("SERVICE_VERSION");
    }
}