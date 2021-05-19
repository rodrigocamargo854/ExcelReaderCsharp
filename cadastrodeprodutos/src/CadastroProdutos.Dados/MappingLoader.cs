using System;
using System.Linq;
using MongoDB.Driver;
using CadastroProdutos.Dados.Entidades;
using CadastroProdutos.Dados.Mongo.ClassMapping;

namespace CadastroProdutos.Dados
{
    public static class MappingLoader
    {
        public static readonly Lazy<IMapping<IEntidade>[]> Mappings = new Lazy<IMapping<IEntidade>[]>(() =>
        {
            return typeof(ProdutosMapping)
                .Assembly.GetTypes()
                .Where(type => typeof(IMapping<IEntidade>).IsAssignableFrom(type))
                .Where(type => !type.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IMapping<IEntidade>>()
                .ToArray();
        });

        private static bool _didLoadClassMappings;

        public static void LoadAllClassMappings()
        {
            if (_didLoadClassMappings)
                return;

            foreach (var mapping in Mappings.Value)
            {
                mapping.RegisterClassMap();
            }

            _didLoadClassMappings = true;
        }

        public static void EnsureAllIndices(IMongoDatabase mongoDatabase)
        {
            foreach (var mapping in Mappings.Value)
            {
                mapping.EnsureIndices(mongoDatabase);
            }
        }
    }
}