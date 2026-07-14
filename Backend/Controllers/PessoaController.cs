using ControleGastos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ControleGastos.DTO.Pessoas;


namespace ControleGastos.Controllers {

    //Camada responsável por receber as requisições HTTP relacionadas a Pessoa. O construtor é responsável pelo
    //acesso as regras de negócio através de Injeção de Dependência. Ele só encaminha os dados e traduz os resultados
    //em respostas HTTP
    [ApiController]
    [Route("api/pessoas")]
    public class PessoaController : ControllerBase {
        
        private readonly IPessoaService _pessoaService;
        
        public PessoaController(IPessoaService pessoaService) {             
            _pessoaService = pessoaService;
        }

        //Lista todas pessoas cadastradas
        [HttpGet]
        public async Task<ActionResult<List<ResponsePessoaDto>>> Listar() {       
            var pessoas = await _pessoaService.ListarPessoasAsync();
            return Ok(pessoas);
        }

        //Cadastra uma nova pessoa. Captura as exceções lançadas pelo service e devolve um erro.
        [HttpPost]
        public async Task<ActionResult<ResponsePessoaDto>> Cadastrar (RequestPessoaDto dto) {
            try {
                var pessoa = await _pessoaService.CadastrarPessoaAsync(dto);
                return Ok("Pessoa cadastrada com sucesso.");
            }catch (Exception ex) {
                return BadRequest(new { mensagem = ex.Message });
            }
            
        }

        //Deleta uma pessoa. As transações são apagadas junto, lança uma exceção se o ID não existir.
        [HttpDelete("{id}")]                             
        public async Task<IActionResult> Remover (int id) {  
            try {
                await _pessoaService.RemoverPessoaAsync(id);
                return Ok("Pessoa removida com sucesso.");
            }
            catch (Exception ex) {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}
