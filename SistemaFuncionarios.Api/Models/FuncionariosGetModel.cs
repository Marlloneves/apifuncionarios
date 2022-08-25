namespace SistemaFuncionarios.Api.Models
{
    public class FuncionariosGetModel
    {
        public Guid IdFuncionario { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Matricula { get; set; }
        public DateTime DataAdmissao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
    }
}
