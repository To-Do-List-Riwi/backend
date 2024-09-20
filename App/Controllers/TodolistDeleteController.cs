using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace backend.App.Controllers
{
    public class TodolistDeleteController : Controller
    {
        private readonly IToDoListRepository _toDoListRepository;
        public TodolistDeleteController(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }

        [HttpDelete]
        [Route("todolist/delete/{name}/task")]
        public async Task<IActionResult> DeleteTask(string name)
        {
            try
            {
                var task = await _toDoListRepository.DeleteTask(name);
                if (name == null || !name.Any())
                {
                    return NotFound("Tarea no encontrada.");
                }
                return Ok("Se ha eliminado la tarea correctamente.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en el proceso de la tarea", ex);
            }
        }
    }
}