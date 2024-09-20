using backend.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend.App.Controllers
{
    public class TodolistCreateController : Controller
    {
        private readonly IToDoListRepository _toDoListRepository;
        public TodolistCreateController(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }

        [HttpPost]
        [Route("todolist/create/task")]
        public IActionResult Post([FromBody] ToDoList toDoList)
        {
            try
            {
                _toDoListRepository.add(toDoList);
                if (toDoList is null)
                {
                    return NotFound("No puedes guardar un dato nulo.");
                }
                return Ok("la tarea se ha creado correctamente.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Hubo un error al crear una tarea.", ex);
            }
        }
    }
}