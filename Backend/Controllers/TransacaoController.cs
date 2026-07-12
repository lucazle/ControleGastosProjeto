using ControleGastos.DTO.Transacoes;
using ControleGastos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Controllers {

    [ApiController]
    [Route("api/transacoes")]
    public class TransacaoController : ControllerBase {

        //Injeção de depêndecia da camada de Service
        private readonly ITransacaoService _transacaoService;

        public TransacaoController(ITransacaoService transacaoService) {
            _transacaoService = transacaoService;
        }

        //Método GET para listar todas transações
        [HttpGet]
        public async Task<ActionResult<List<ResponseTransacaoDto>>> Listar() {
            var transacoes = await _transacaoService.ListarTransacoesAsync();
            return Ok(transacoes);
        }

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


