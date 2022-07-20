using Microsoft.AspNetCore.Mvc;
using ProjectManager.Models;
using EM.Domain;
using EM.Repository;

namespace ProjectManager.Controllers
{
    public class AlunoController : Controller
    {
        private readonly RepositorioAbstrato<Aluno> _alunoRepositorio;
        public AlunoController(RepositorioAbstrato<Aluno> alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int matricula)
        {
            Aluno aluno = _alunoRepositorio.GetByMatricula(matricula);
            return View(aluno);
        }


        public IActionResult Excluir(int matricula)
        {
            _alunoRepositorio.Excluir(matricula);
            return RedirectToAction("Index");
        }

        public IActionResult ExcluirConfirmacao(int matricula)
        {
            Aluno aluno = _alunoRepositorio.GetByMatricula(matricula);
            return View(aluno);
        }

        [HttpPost]
        public IActionResult Criar(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _alunoRepositorio.Adicionar(aluno);
                return RedirectToAction("Index");
            }
            return View(aluno);

        }

        [HttpPost]
        public IActionResult Alterar(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _alunoRepositorio.Atualizar(aluno);
                return RedirectToAction("Index");
            }
            return View("Editar", aluno);
        }

        public IActionResult ListaDeAlunos(string parteDoNome = null, string opcao = "nome")
        {
            List<Aluno> alunos = new List<Aluno>();

            if (string.IsNullOrEmpty(parteDoNome))
            {
                alunos = _alunoRepositorio.ListarTodos();
            }
            else if (opcao == "nome")
            {
                alunos = _alunoRepositorio.GetByContendoNoNome(parteDoNome);
            }
            else if (opcao == "matricula")
            {
                var aluno = _alunoRepositorio.GetByMatricula(int.Parse(parteDoNome));
                if (aluno != null)
                {
                    alunos.Add(aluno);
                }
            }
            return PartialView("_Alunos", alunos);
        }
    }
}
