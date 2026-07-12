using ControleGastos.Models;

namespace ControleGastos.DTO.Transacoes {
    public class RequestTransacaoDto {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public int PessoaId { get; set; }
    }
}

//Segue a mesma lógica do RequestPessoaDto, mas dessa vez temos o PessoaId, já que a transação precisa estar vinculado a uma pessoa
//dessa forma, sempre que um request da transação foi enviado também precisa do id da pessoa que ta solicitando.