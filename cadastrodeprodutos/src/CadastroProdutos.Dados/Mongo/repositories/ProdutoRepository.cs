using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CadastroProdutos.Dados.Entidades;
using CadastroProdutos.Dados.Mongo.repositories;
using MongoDB.Driver;

namespace CadastroProdutos.Dados.Mongo.Repositories
{
    //classe criada para executar persistencia no banco de dados
    //Acessa o Produto do Mongo Repository e associa aos metodos da interface
    //IProdutoRepository
    public class ProdutoRepository : MongoRepository<Produto>, IProdutoRepository
    {

        public ProdutoRepository(IMongoDatabase database) : base(database)
        {

        }

        public void InserirOuAtualizar(Produto produto)
        {
            Expression<Func<Produto, bool>> filtro = x =>
                x.Codigo == produto.Codigo;

            var updateDefinition = Builders<Produto>.Update
                .SetOnInsert(x => x.Codigo, produto.Codigo)
                .Set(x => x.Nome, produto.Nome)
                .Set(x => x.Valor, produto.Valor);
            Collection.UpdateOne(filtro, updateDefinition, new UpdateOptions
            {
                IsUpsert = true
            });
        }

        public Produto ObterUm(int codigo)
        {
            return Collection.Find(x => x.Codigo == codigo).FirstOrDefault();
        }

        public List<Produto> Listar()
        {

            var filter = new FilterDefinitionBuilder<Produto>().Empty;

            var result = Collection.Find(filter);

            return result.ToList();

        }

        public void Excluir(int codigo)
        {
            Collection.DeleteOne(x => x.Codigo == codigo);
        }
    }
}