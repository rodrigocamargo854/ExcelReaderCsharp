
//Sample entity class

namespace CadastroProdutos.Dados.Entidades
{
    public class Produto : IEntidade
    {
        public string Nome { get; set; }
        public int Codigo { get; set; }
        public double Valor { get; set; }
    }
}