namespace ExemploBlazorADOFuncionario.Entidades
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public List<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();

    }
}
