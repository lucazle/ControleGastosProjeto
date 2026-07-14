using ControleGastos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ControleGastos.DTO.Relatorios;


namespace ControleGastos.Controllers {

    //Camada responsável por receber as requisições HTTP relacionadas aos Relátórios. O construtor é responsável pelo
    //acesso as regras de negócio através de Injeção de Dependência. Ele só encaminha os dados e traduz os resultados
    //em respostas HTTP.
    [ApiController]
    [Route("api/relatorio")]
    public class RelatorioController :ControllerBase {

        private readonly IRelatorioService _relatorioService;

        public RelatorioController(IRelatorioService relatorioService) {
            _relatorioService = relatorioService;
        }

        //Esse controller só tem a função de listar as informações que foram solicitadas no service.
        [HttpGet]
        public async Task<ActionResult<RelatorioTotaisDto>> Obter() {
            var relatorio = await _relatorioService.GerarRelatorioAsync();
            return Ok(relatorio);
        }
    }
}
