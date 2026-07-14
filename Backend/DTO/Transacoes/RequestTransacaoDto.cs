using ControleGastos.Models;

namespace ControleGastos.DTO.Transacoes {
    public class RequestTransacaoDto {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public int PessoaId { get; set; }
    }
}