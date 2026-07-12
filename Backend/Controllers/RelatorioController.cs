using ControleGastos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ControleGastos.DTO.Relatorios;


namespace ControleGastos.Controllers {
    [ApiController]
    [Route("api/relatorio")]
    public class RelatorioController :ControllerBase {

        //Injeção de depêndecia da camada de Service
        private readonly IRelatorioService _relatorioService;

        public RelatorioController(IRelatorioService relatorioService) {
            _relatorioService = relatorioService;
        }

        // aqui ele ja monta a lista toda (lista de pessoas com totais e os totais gerais) e devolve como resposta
        [HttpGet]
        public async Task<ActionResult<RelatorioTotaisDto>> Obter() {
            var relatorio = await _relatorioService.GerarRelatorioAsync();
            return Ok(relatorio);
        }
    }
}
