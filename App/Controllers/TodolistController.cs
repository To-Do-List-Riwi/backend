using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.App.Controllers
{
    public class TodolistController : Controller
    {
        private readonly IToDoListRepository _todolistRepository;
        public TodolistController(IToDoListRepository toDoListRepository)
        {
            _todolistRepository = toDoListRepository;
        }

        [HttpGet]
        [Route("todolist/GetTasks")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var list = await _todolistRepository.GetAllList();
                if (list == null || !list.Any())
                {
                    return NotFound("No se pueden encontrar tareas.");
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
               throw new ApplicationException("No existen tareas.", ex);
            }
        }

        [HttpGet]
        [Route("todolist/getcompleted")]
        public async Task<IActionResult> GetCompleted()
        {
            try
            {
                var list = await _todolistRepository.GetAllCompleted();
                if (list == null || !list.Any())
                {
                    return NotFound("No tienes tareas pendientes.");
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                
                throw new ApplicationException("hay un error al obtener tareas completadas", ex);
            }
        }

        [HttpGet]
        [Route("todolist/getnotcompleted")]
        public async Task<IActionResult> GetAllNotCompleted()
        {
            try
            {
                var list = await _todolistRepository.GetAllNotCompleted();
                if (list == null || !list.Any())
                {
                    return NotFound("No hay tareas incompletas");
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Hay un problema al obtener tareas incompletas.",ex);
            }
        }

        [HttpGet]
        [Route("todolist/{name}/task")]
        public async Task<IActionResult> GetTaskByName(string name)
        {
            try
            {
                var task = await _todolistRepository.GetTaskByName(name);
                if (task == null)
                {
                    return NotFound("No hay tarea por ese nombre.");
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("", ex);
            }
        }
    }
}