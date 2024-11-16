using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GerenciadordeTarefas.Models.Task;

namespace GerenciadordeTarefas.Controllers.Task
{
    public class TaskController : Controller
    {
        // Lista estatica (armazena tarefas)
        private static List<TaskViewModel> tasks = new List<TaskViewModel>
        {
            new TaskViewModel { Id = 1, Title = "Vai ao Show", Description = "Comprado", IsCompleted = true},
            new TaskViewModel { Id = 2, Title = "Não vai ao Show", Description = "Recusado", IsCompleted = false}
        };

        //Acao que lista tarefas
        public ActionResult Index()
        {
            return View(tasks);
        }
        
    
        // Acao GET (exibir a pag de criacao de tarefas)
        public ActionResult Create()
        {
            return View(new TaskViewModel()); //Cria uma nova tarefa
        }

        // Acao POST criar nova tarefa
        [HttpPost]
        public ActionResult Create(TaskViewModel task) 
        {
            if(ModelState.IsValid) // Adicionando validacao
            {
                task.Id = tasks.Max(tarefa => tarefa.Id) + 1;
                tasks.Add(task);
                return RedirectToAction("Index");
            }
            return View(task); //Retorna a view com o modelo se a validacao nao for concluida
        }

        //Acao GET para exibir o formulao de editar

        public ActionResult Edit(int id)
        {
            var task = tasks.FirstOrDefault(tarefa => tarefa.Id == id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //Acao POST para salvar o form com a tarefa editada
        [HttpPost]
        public ActionResult Edit(TaskViewModel updatedTask)
        {

            if (ModelState.IsValid) //Valida
            {
                var task = tasks.FirstOrDefault(tarefa => tarefa.Id == updatedTask.Id);
                if(task != null)
                {
                    //Insiro os dados na tarefa antiga
                    task.Title = updatedTask.Title;
                    task.Description = updatedTask.Description;
                    task.IsCompleted = updatedTask.IsCompleted;
                }
                return RedirectToAction("Index");
            }
            return View(updatedTask); //Retorna a view com o modelo se a validacao falhar
        }
        

        //Acao para visualizar detalhes de uma tarefa
        public ActionResult Details(int id)
        {
            var task = tasks.FirstOrDefault(tarefa => tarefa.Id == id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }
    
    
        //Acao GET para exibir a pagina de confirmacao de exclusao
        public ActionResult Delete (int id)
        {
            var task = tasks.FirstOrDefault(tarefa => tarefa.Id == id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //Acao POST para salvar a exclusao da tarefa
        [HttpPost]
        public ActionResult DeleteConfirmed (int id)
        {
            var task = tasks.FirstOrDefault(tarefa => tarefa.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
            }
            return RedirectToAction("Index");
        }
    }
}