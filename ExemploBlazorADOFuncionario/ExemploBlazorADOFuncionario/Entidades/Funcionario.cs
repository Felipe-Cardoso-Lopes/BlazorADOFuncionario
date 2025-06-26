using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExemploBlazorADOFuncionario.Entidades
{
    [Table("tbFuncionario")]
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Departamento")]
        public int DepartamentoId { get; set; }

        [StringLength(200)]
        public string NomeDepartamento { get; set;}

        [Required]
        [StringLength(200)]
        public string Nome { get; set; }

        [StringLength(200)]
        public string NomeMae { get; set; }

        [StringLength(200)]
        public string NomePai { get; set; }

        public DateTime DataNascimento { get; set; }

        [Required]
        [StringLength(11)]
        public string CPF { get; set; }

        [StringLength(11)]
        public string RG { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public Departamento Departamento { get; set; }
    }
}
