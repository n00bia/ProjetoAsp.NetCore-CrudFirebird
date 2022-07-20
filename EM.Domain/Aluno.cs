using System.ComponentModel.DataAnnotations;

namespace EM.Domain
{
    public class Aluno : IEntidade
    {
        public int Matricula { get; set; }
        [Required(ErrorMessage = "Digite o nome do aluno")]
        public String Nome { get; set; }
        [Required(ErrorMessage = "Digite o CPF")]
        public String CPF { get; set; }
        [Required(ErrorMessage = "Digite a data de nascimento")]
        public DateTime? Nascimento { get; set; }

        public EnumeradorSexo Sexo { get; set; }

        //List<Aluno> Alunos = new List<Aluno>();
        public Aluno() { }


        public Aluno(int matricula, string nome, string cpf, DateTime nascimento, EnumeradorSexo sexo)
        {
            Matricula = matricula;
            Nome = nome;
            CPF = cpf;
            Nascimento = nascimento;
            Sexo = sexo;
        }
    }
}
