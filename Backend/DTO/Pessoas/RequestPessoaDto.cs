namespace ControleGastos.DTO.Pessoas {
    public class RequestPessoaDto {
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
    }
}

//Criado para quando o frontend fizer uma requisição para a criação de uma nova pessoa, por exemplo.