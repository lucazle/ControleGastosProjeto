using ControleGastos.Models;

namespace ControleGastos.DTO.Transacoes {
    public class ResponseTransacaoDto {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public int PessoaId { get; set; }
        public string PessoaNome { get; set; } = string.Empty;
    }
}