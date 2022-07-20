using System.Linq;

namespace EM.Domain.Test
{
    public class DominioTeste
    {
        [Fact]
        public void Validacao()
        {
            Aluno aluno = new Aluno()
            {
                Matricula = 52,
                Nome = "Samara",
                Sexo = (EnumeradorSexo)1,
                Nascimento = Convert.ToDateTime("27/05/1985"),
                CPF = "56215816743"
            };

            if (aluno.CPF.Length != 11)
            {
                Assert.False(true);
            }

            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
            {
                numeros[i] = int.Parse(aluno.CPF[i].ToString());
            }

            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += (10 - i) * numeros[i];
            }

            int resultado = soma % 11;


            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    Assert.False(true);
            }

            else if (numeros[9] != 11 - resultado)
                Assert.False(true);


            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += (11 - i) * numeros[i];
            }


            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    Assert.False(true);
            }

            else
            {
                if (numeros[10] != 11 - resultado)
                    Assert.False(true);
            }

            Assert.True(true);
        }

        [Fact]

        public void Unicidade()
        {
            List<Aluno> list = new List<Aluno>();

            Aluno aluno = new Aluno()
            {
                Matricula = 52,
                Nome = "Samara",
                Sexo = (EnumeradorSexo)1,
                Nascimento = Convert.ToDateTime("27/05/1985"),
                CPF = "56215816743"
            };

            Aluno aluno1 = new Aluno()
            {
                Matricula = 25,
                Nome = "Manoel",
                Sexo = (EnumeradorSexo)0,
                Nascimento = Convert.ToDateTime("20/08/1989"),
                CPF = "27947998353"
            };

            Aluno aluno2 = new Aluno()
            {
                Matricula = 52,
                Nome = "Sofia",
                Sexo = (EnumeradorSexo)1,
                Nascimento = Convert.ToDateTime("15/03/2002"),
                CPF = "60273123351"
            };
            list.Add(aluno);
            list.Add(aluno1);

            if (list.Any(item => item.Matricula == aluno.Matricula || item.Matricula == aluno1.Matricula
            || item.Matricula == aluno2.Matricula))
            {
                Assert.False(true);
            }
        
        }
    }
}