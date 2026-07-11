namespace ControleGastos.Models {

    public class Pessoa {

        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public List<Transacao> Transacoes { get; set; } = new List<Transacao>();
    }
}

//Criação da entidade com atributos solicitados e relação um para muitos. (Uma pessoa para muitas transações, também uma cascata para 
//caso uma pessoa seja deletada todas transações também sejam, como foi solicitado.
