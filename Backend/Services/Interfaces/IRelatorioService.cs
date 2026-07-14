using ControleGastos.DTO.Relatorios;

namespace ControleGastos.Services.Interfaces {
    public interface IRelatorioService {
        Task<RelatorioTotaisDto> GerarRelatorioAsync();
    }
}
