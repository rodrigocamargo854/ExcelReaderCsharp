using System.Collections.Generic;
using CadastroProdutos.Dados.Entidades;

namespace CadastroProdutos.Dados.Mongo.Repositories
{
    public interface IProdutoRepository
    {
        void InserirOuAtualizar(Produto produto);
        List<Produto> Listar();
        Produto ObterUm(int codigo);
        void Excluir(int codigo);
    }
}