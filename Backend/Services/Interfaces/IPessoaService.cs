using ControleGastos.DTO.Pessoas;

namespace ControleGastos.Services.Interfaces {
    public interface IPessoaService {
        Task<List<ResponsePessoaDto>> ListarPessoasAsync();
        Task<ResponsePessoaDto> CadastrarPessoaAsync(RequestPessoaDto dto);
        Task RemoverPessoaAsync(int id);
    }
}