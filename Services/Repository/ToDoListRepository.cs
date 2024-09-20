using backend.Infrastructure.Data;
using backend.Models;
using backend.Models.Enum;
using backend.Services.Interfaces;
using MongoDB.Driver;

namespace backend.Services.Repository
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly IMongoCollection<ToDoList> _mongoCollection;
        public ToDoListRepository(MongoDbContext context)
        {
            _mongoCollection = context.ToDoLists;
        }

        public async Task<IEnumerable<ToDoList>> GetAllCompleted()
        {
            try
            {
                var filter = Builders<ToDoList>.Filter.In(c => c.State, new[] { TaskState.Completada.ToString() });

                var list = await _mongoCollection.Find(filter).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No hay tareas completadas",ex);
            }
           
        }

        public async Task<IEnumerable<ToDoList>> GetAllNotCompleted()
        {
            try
            {
                var filter = Builders<ToDoList>.Filter.In(c => c.State, new[] { TaskState.pendiente.ToString(), TaskState.Proceso.ToString() });
                
                var list = await _mongoCollection.Find(filter).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No tienes tareas pendientes.",ex);
            }
        }

        //We get all tasks
        public async Task<IEnumerable<ToDoList>> GetAllList()
        {
            try
            {
                var list = await _mongoCollection.Find(_ => true).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No existen tareas.", ex);
            }
        }

        public async Task<ToDoList> GetTaskByName(string name)
        {
            try
            {
                var task = await _mongoCollection.Find(c => c.Name == name).FirstOrDefaultAsync();
                return task;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener tarea por nombre.", ex);
            }
        }

        public void add(ToDoList toDoList)
        {
            try
            {
                toDoList.Date_created = DateTime.UtcNow;
                toDoList.State = TaskState.pendiente.ToString();
                toDoList.Date_finish = null;

                _mongoCollection.InsertOne(toDoList);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("No se pudo crear la tarea.",ex);
            }
        }

        public async Task<bool> CompletedTask(string name)
        {
            try
            {
                var filter = Builders<ToDoList>.Filter.And(
                    Builders<ToDoList>.Filter.Eq(t => t.Name, name),
                    Builders<ToDoList>.Filter.In(t => t.State, new[] { TaskState.pendiente.ToString(), TaskState.Proceso.ToString() })
                );
 

                var update = Builders<ToDoList>.Update
                    .Set(t => t.State, TaskState.Completada.ToString())
                    .Set(t => t.Date_finish, DateTime.UtcNow);

                var result = await _mongoCollection.UpdateOneAsync(filter, update);

                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al completar tarea", ex);
            }
            

        }

        public async Task<bool> ProcesTask(string name)
        {
            try
            {
                var filter = Builders<ToDoList>.Filter.And(
                    Builders<ToDoList>.Filter.Eq(t => t.Name, name),
                    Builders<ToDoList>.Filter.In(t => t.State, new[] {TaskState.pendiente.ToString()}));

                var update = Builders<ToDoList>.Update
                    .Set(t => t.State, TaskState.Proceso.ToString());

                var result = await _mongoCollection.UpdateOneAsync(filter, update);

                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en el proceso de la tarea", ex);
            }
        }

        public async Task<bool> DeleteTask(string name)
        {
            try
            {
                var filter = Builders<ToDoList>.Filter.Eq(t => t.Name, name);

                var update = Builders<ToDoList>.Update
                    .Set(t => t.State, TaskState.Eliminada.ToString());

                var result = await _mongoCollection.UpdateOneAsync(filter, update);

                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en la eliminacion de la tarea", ex);
            }
        }
    }
}