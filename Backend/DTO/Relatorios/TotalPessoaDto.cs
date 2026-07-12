namespace ControleGastos.DTO.Relatorios {
    public class TotalPessoaDto {
        public int PessoaId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Saldo => TotalReceitas - TotalDespesas;
    }
}

//Esse DTO representa o total de receitas, despesas e saldo de uma pessoa. 