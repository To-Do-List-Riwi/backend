using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace backend.App.Controllers
{
    public class TodolistUpdateController : Controller
    {
        private readonly IToDoListRepository _toDoListRepository;
       public TodolistUpdateController(IToDoListRepository toDoListRepository)
       {
            _toDoListRepository = toDoListRepository;
       }

       [HttpPatch]
       [Route("todolist/completed/{name}/task")]
       public async Task<IActionResult> CompletedTask(string name)
       {
        try
        {
            var task = await _toDoListRepository.CompletedTask(name);
            if (name == null || !name.Any())
            {
                return NotFound("Tarea no encontrada.");
            }
            return Ok("Tarea Completada.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Hay un error ", ex);
        }
       }

       [HttpPatch]
       [Route("todolist/proces/{name}/task")]
       public async Task<IActionResult> ProcesTask(string name)
       {
        try
        {
            var task = await _toDoListRepository.ProcesTask(name);
            if (name == null || !name.Any())
            {
                return NotFound("Tarea no encontrada.");
            }
            return Ok("Tarea en proceso.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error en el proceso de la tarea", ex);
        }
       }
    }
}