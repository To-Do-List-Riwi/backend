using backend.Models;
using backend.Models.Enum;

namespace backend.Services.Interfaces
{
    public interface IToDoListRepository
    {
        Task<IEnumerable<ToDoList>> GetAllList();
        Task<IEnumerable<ToDoList>> GetAllNotCompleted();
        Task<IEnumerable<ToDoList>> GetAllCompleted();
        Task<ToDoList> GetTaskByName(string name);
        void add(ToDoList toDoList);
        Task<bool> CompletedTask(string name);
        Task<bool> ProcesTask(string name);
        Task<bool> DeleteTask(string name);


    }
}