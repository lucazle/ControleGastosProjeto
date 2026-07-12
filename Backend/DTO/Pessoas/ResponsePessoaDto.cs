namespace ControleGastos.DTO.Pessoas {
    public class ResponsePessoaDto {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
    }
}

//segue a mesma lógica do ResponseTransacaoDto, retorna os dados solicitados pelo request, incluindo o ID do usuário.