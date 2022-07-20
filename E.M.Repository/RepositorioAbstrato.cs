using EM.Domain;
namespace EM.Repository
{

    public abstract class RepositorioAbstrato<T>
        where T : IEntidade
    {
        public abstract List<Aluno> GetByContendoNoNome(string parteDoNome);
        public abstract Aluno GetByMatricula(int matricula);
        public abstract List<Aluno> ListarTodos();
        public abstract void Adicionar(Aluno aluno);
        public abstract Aluno Atualizar(Aluno aluno);
        public abstract bool Excluir(int matricula);
    }

}