using ControleGastos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ControleGastos.DTO.Pessoas;


namespace ControleGastos.Controllers {

    [ApiController]
    [Route("api/pessoas")]
    public class PessoaController : ControllerBase {

        //Injeção de depêndecia da camada de Service
        private readonly IPessoaService _pessoaService;
        
        public PessoaController(IPessoaService pessoaService) {             
            _pessoaService = pessoaService;
        }

        //Método GET para listar todas pessoas
        [HttpGet]
        public async Task<ActionResult<List<ResponsePessoaDto>>> Listar() {       
            var pessoas = await _pessoaService.ListarPessoasAsync();
            return Ok(pessoas);
        }

        //Método POST para registrar novos usuários no banco
        [HttpPost]
        public async Task<ActionResult<ResponsePessoaDto>> Cadastrar (RequestPessoaDto dto) {  
            var pessoa = await _pessoaService.CadastrarPessoaAsync(dto);
            return Ok("Pessoa cadastrada com sucesso.");
        }

        //Método DELETE com para remover dados do banco, como usei uma exception no service, é necessário utilizar o método com try catch ele captura o erro e lança uma resposta HTTP, tipo 404 
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
