using ControleGastos.DTO.Transacoes;

namespace ControleGastos.Services.Interfaces {
    public interface ITransacaoService {
        Task<List<ResponseTransacaoDto>> ListarTransacoesAsync();
        Task<ResponseTransacaoDto> CadastrarTransacaoAsync(RequestTransacaoDto dto);
    }
}

////Interface para utilizar no service que ja passa o 