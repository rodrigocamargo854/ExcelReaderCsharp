using MongoDB.Bson.Serialization;
using CadastroProdutos.Dados.Mongo.ClassMapping;

namespace CadastroProdutos.Dados.Entidades
{
    public class CadastroProdutosMapping : MongoBsonClassMap<CadastroProdutos>
    {
        protected override void RegisterBsonClassMap(BsonClassMap<CadastroProdutos> classMap)
        {
            classMap.MapMember(x => x.Nome).SetElementName("Nome").SetIsRequired(true);
        }

        protected override void SetupIndices(IndicesManager indicesManager)
        {
            indicesManager.AddIndex(IndexBuilder.Ascending(x => x.Nome), unique: true);
        }
    }
}