using System;
using System.Collections.Generic;
using CadastroProdutos.Dados.Entidades;

namespace CadastroProdutos.Dados
{
    public class Colecoes
    {
        // TODO criar um atributo para as entidades, identificando o nome da coleção, e carregar de lá
        private static readonly Dictionary<Type, string> Mapeamento = new Dictionary<Type, string>
        {
            { typeof(Entidades.Produto), "cadastro_produtos_rodrigo.godoy" },
        };

        public static string ObterNomeColecao<TDocument>()
            where TDocument : IEntidade
        {
            var documentType = typeof(TDocument);

            if (Mapeamento.ContainsKey(documentType))
                return Mapeamento[documentType];

            throw new InvalidOperationException($"Coleção não possui nome especificado {typeof(TDocument).Name}");
        }
    }
}