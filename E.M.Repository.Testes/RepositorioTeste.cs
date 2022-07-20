using EM.Domain;
using EM.Repository;
using System.Linq;


namespace EM.Repository.Testes
{
    
    public class RepositorioTeste
    {
        RepositorioAluno repositorioAluno = new();

        [Fact]
        public void ListarTodos()
        {
            Assert.NotNull(repositorioAluno.ListarTodos());
        }

        [Fact]
        public void Adicionar()
        {
            Aluno aluno = new Aluno
            {
                Matricula = 52,
                Nome = "Samara",
                Sexo = (EnumeradorSexo)1,
                Nascimento = Convert.ToDateTime("27/05/1985"),
                CPF = "56215816743"
            };

            repositorioAluno.Adicionar(aluno);
            Assert.NotNull(repositorioAluno.GetByMatricula(aluno.Matricula));

        }

        [Fact]
        public void Atualizar() 
        {
            Aluno aluno = new Aluno
            {
                Matricula = 52,
                Nome = "Samara",
                Sexo = (EnumeradorSexo)1,
                Nascimento = Convert.ToDateTime("27/05/1985"),
                CPF = "56215816743"
            };

            Aluno alunoAtt = repositorioAluno.Atualizar(aluno);

            if (alunoAtt.Nome != aluno.Nome || alunoAtt.Sexo != aluno.Sexo
                || alunoAtt.Nascimento != aluno.Nascimento || alunoAtt.CPF != aluno.CPF)
            {
                Assert.False(false);
            }

            else
                Assert.False(true);
        }

        [Fact]
        public void Excluir()
        {
            Aluno aluno = new Aluno
            {
                Matricula = 52,
                Nome = "Samara",
                Sexo = (EnumeradorSexo)1,
                Nascimento = Convert.ToDateTime("27/05/1985"),
                CPF = "56215816743"
            };

            repositorioAluno.Excluir(aluno.Matricula);
            Assert.Null(repositorioAluno.GetByMatricula(aluno.Matricula));
            
        }

       
    }
}