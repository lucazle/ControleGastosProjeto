namespace ControleGastos.Models {

    public class Transacao {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}

//Criação da entidade Transação com o ID da Entidade Pessoa para referenciar dizendo que cada transação pertence a uma pessoa
