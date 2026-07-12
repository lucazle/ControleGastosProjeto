namespace ControleGastos.DTO.Relatorios {
    public class RelatorioTotaisDto {
        public List<TotalPessoaDto> Pessoas { get; set; } = new();
        public decimal TotalReceitasGeral { get; set; }
        public decimal TotalDespesasGeral { get; set; }
        public decimal SaldoGeral => TotalReceitasGeral - TotalDespesasGeral;
    }
}

//Esse DTO representa o total geral que é solicitado, ele vai exibir a soma do total de todas pessoas ao final da lista.