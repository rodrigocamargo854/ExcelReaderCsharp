using MongoDB.Bson.Serialization;
using CadastroProdutos.Dados.Mongo.ClassMapping;

// mapeamento da classe produto
//################################################################//

namespace CadastroProdutos.Dados.Entidades
{
    public class ProdutosMapping : MongoBsonClassMap<Produto>
    {
        protected override void RegisterBsonClassMap(BsonClassMap<Produto> classMap)
        {
            classMap.MapMember(x => x.Nome).SetElementName("Nome").SetIsRequired(true);
            classMap.MapMember(x => x.Valor).SetElementName("Valor").SetIsRequired(true);
            classMap.MapMember(x => x.Codigo).SetElementName("Codigo").SetIsRequired(true);

        }

        protected override void SetupIndices(IndicesManager indicesManager)
        {
            indicesManager.AddIndex(IndexBuilder.Ascending(x => x.Codigo), unique: true);
        }
    }
}