using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Configuration;
using EM.Domain;

namespace EM.Repository
{
    public class RepositorioAluno : RepositorioAbstrato<Aluno> 
    {
        public RepositorioAluno() { }

        private IConfiguration _configuracoes;
        private string _conexao { get { return _configuracoes.GetConnectionString("firedb"); } }

        public RepositorioAluno(IConfiguration config)
        {
            _configuracoes = config;
        }
        public override void Adicionar(Aluno aluno)
        {
            using (var conexao = new FbConnection(_conexao))
            {
                string sql = "INSERT INTO ALUNOS (Nome, CPF, Nascimento, Sexo) VALUES (@Nome, @CPF, @Nascimento, @Sexo)";
                FbCommand cmd = new FbCommand(sql, conexao);
                cmd.Parameters.Add(new FbParameter("@Nome", aluno.Nome));
                cmd.Parameters.Add(new FbParameter("@CPF", aluno.CPF));
                cmd.Parameters.Add(new FbParameter("@Nascimento", aluno.Nascimento));
                cmd.Parameters.Add(new FbParameter("@Sexo", aluno.Sexo));

                try
                {
                    conexao.Open();
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override List<Aluno> ListarTodos()
        {
            string sql = "SELECT Matricula, Nome, CPF, Nascimento, Sexo FROM Alunos ORDER BY Matricula";
            using (var conexao = new FbConnection(_conexao))
            {
                var cmd = new FbCommand(sql, conexao);
                List<Aluno> list = new List<Aluno>();
                Aluno aluno = null;
                try
                {
                    conexao.Open();
                    using (var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            aluno = new Aluno();
                            aluno.Matricula = (int)reader["Matricula"];
                            aluno.Nome = reader["Nome"].ToString();
                            aluno.CPF = reader["CPF"].ToString();
                            aluno.Nascimento = (DateTime)reader["Nascimento"];
                            aluno.Sexo = (EnumeradorSexo)reader["Sexo"];
                            list.Add(aluno);

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return list;
            }
        }

        public override Aluno GetByMatricula(int matricula)
        {
            using (var conexao = new FbConnection(_conexao))
            {
                string sql = "SELECT Matricula, Nome, CPF, Nascimento, Sexo FROM Alunos WHERE Matricula=@Matricula";
                FbCommand cmd = new FbCommand(sql, conexao);
                cmd.Parameters.Add(new FbParameter("@Matricula", matricula));
                Aluno aluno = null;
                try
                {
                    conexao.Open();
                    using (var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                aluno = new Aluno();
                                aluno.Matricula = (int)reader["Matricula"];
                                aluno.Nome = reader["Nome"].ToString();
                                aluno.CPF = reader["CPF"].ToString();
                                aluno.Nascimento = (DateTime)reader["Nascimento"];
                                aluno.Sexo = (EnumeradorSexo)reader["Sexo"];
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return aluno;

            }


        }

        public override Aluno Atualizar(Aluno aluno)
        {
            Aluno alunoDB = GetByMatricula(aluno.Matricula);

            if (alunoDB == null) throw new System.Exception("Houve um erro na atualização do aluno!");

            using (var conexao = new FbConnection(_conexao))
            {
                string sql = "UPDATE Alunos SET Nome=@Nome, CPF=@CPF, Nascimento=@Nascimento, Sexo=@Sexo WHERE MAtricula=@Matricula";
                FbCommand cmd = new FbCommand(sql, conexao);

                cmd.Parameters.Add(new FbParameter("@Matricula", aluno.Matricula));
                cmd.Parameters.Add(new FbParameter("@Nome", aluno.Nome));
                cmd.Parameters.Add(new FbParameter("@CPF", aluno.CPF));
                cmd.Parameters.Add(new FbParameter("@Nascimento", aluno.Nascimento));
                cmd.Parameters.Add(new FbParameter("@Sexo", aluno.Sexo));

                try
                {
                    conexao.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return alunoDB;

            }

        }

        public override bool Excluir(int matricula)
        {
            Aluno alunoDB = GetByMatricula(matricula);

            if (alunoDB == null) throw new System.Exception("Houve um erro na exclusão do aluno!");

            using (var conexao = new FbConnection(_conexao))
            {
                string sql = "DELETE FROM Alunos WHERE Matricula=@Matricula";
                FbCommand cmd = new FbCommand(sql, conexao);
                cmd.Parameters.Add(new FbParameter("Matricula", matricula));
                try
                {
                    conexao.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return true;
            }

        }

        public override List<Aluno> GetByContendoNoNome(string parteDoNome)
        {
            using (var conexao = new FbConnection(_conexao))
            {
                string sql = "SELECT * FROM Alunos WHERE Upper(Nome) LIKE @parte";
                FbCommand cmd = new FbCommand(sql, conexao);
                cmd.Parameters.Add(new FbParameter("@parte", "%" + parteDoNome.ToUpper() + "%"));
                List<Aluno> alunos = new List<Aluno>();
                try
                {
                    conexao.Open();
                    using (var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var aluno = new Aluno();
                                aluno.Matricula = (int)reader["Matricula"];
                                aluno.Nome = reader["Nome"].ToString();
                                aluno.CPF = reader["CPF"].ToString();
                                aluno.Nascimento = (DateTime)reader["Nascimento"];
                                aluno.Sexo = (EnumeradorSexo)reader["Sexo"];
                                alunos.Add(aluno);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return alunos;

            }
        }
    }
}


