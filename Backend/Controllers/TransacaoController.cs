using ControleGastos.DTO.Transacoes;
using ControleGastos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Controllers {

    //Camada responsável por receber as requisições HTTP relacionadas a Transação. O construtor é responsável pelo
    //acesso as regras de negócio através de Injeção de Dependência. Ele só encaminha os dados e traduz os resultados
    //em respostas HTTP
    [ApiController]
    [Route("api/transacoes")]
    public class TransacaoController : ControllerBase {

        private readonly ITransacaoService _transacaoService;

        public TransacaoController(ITransacaoService transacaoService) {
            _transacaoService = transacaoService;
        }

        //Lista transações cadastradas
        [HttpGet]
        public async Task<ActionResult<List<ResponseTransacaoDto>>> Listar() {
            var transacoes = await _transacaoService.ListarTransacoesAsync();
            return Ok(transacoes);
        }

        //Registra uma nova transação. Captura as exceções lançadas pelo service e devolve um erro. 
        [HttpPost]
        public async Task<ActionResult<ResponseTransacaoDto>> Cadastrar(RequestTransacaoDto dto) {
            try {
                var transacao = await _transacaoService.CadastrarTransacaoAsync(dto);
                return Ok("Transação cadastrada com sucesso.");
            }
            catch (Exception ex) {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}


