using CadastroProdutos.Dados.Entidades;
using CadastroProdutos.Dados.Mongo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Messaging;
using Br.Com.Hbsis.Rpc.Produtos;


namespace CadastroProdutos.WebApi.Controllers
{
    [Route("/api/produtos")]
    public class ProdutosController : Controller
    {
        //interface privada para acesso ao bd
        //tem contrato com IprodutoRepository para acessar os metodos
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEventManager _eventManager; //linha nova

        public ProdutosController(IProdutoRepository produtoRepository, IEventManager eventManager)
        {
            _produtoRepository = produtoRepository;
            _eventManager = eventManager;
        }

        //Metodos rest para Listar produtos acessados por meio da interface
        //IProdutoRepository
        //GET

        [HttpGet]
        //IAresult classe base para listagem do status de cada serviço, exemplio, 200, 404.
        public IActionResult Listar()
        {
            return Ok(_produtoRepository.Listar());
        }

        [HttpGet("{codigo}")]
        //IAresult classe base para listagem do status de cada serviço, exemplio, 200, 404.
        public IActionResult ObterUm(int codigo)
        {
            //persiste no bd com metodo ObterUm
            var produto = _produtoRepository.ObterUm(codigo);

            if (produto == null)
            {
                return NotFound("produto com código " + codigo + "não encontrado");
            }
            return Ok(produto);
        }

        [HttpPost]
        public IActionResult Incluir([FromBody] Produto produto)
        {
            _produtoRepository.InserirOuAtualizar(produto);
            ProdutoIncluido produtoIncluido = new ProdutoIncluido
            {
                Codigo = produto.Codigo,
                Nome = produto.Nome,
                Valor = produto.Valor,
                UberTraceId = "12314124-1231-1231-1231-123123123"
            };
            _eventManager.Publish(produtoIncluido);

            return Ok(produto);
        }

        [HttpPut("{codigo}")]
        public IActionResult Atualizar(int codigo, [FromBody] Produto produto)
        {
            var produtosTemp = _produtoRepository.ObterUm(codigo);
            if (produtosTemp == null)
            {
                return NotFound("produto" + codigo + "Não encontrado");
            }

            _produtoRepository.InserirOuAtualizar(produto);
            return Ok(produto);
        }

        [HttpDelete("{codigo}")]
        public IActionResult Excluir(int codigo)
        {
            var produto = _produtoRepository.ObterUm(codigo);
            if (produto == null)
            {
                return NotFound("produto" + codigo + "Não encontrado");
            }

            _produtoRepository.Excluir(codigo);
            return NoContent();
        }
    }
}