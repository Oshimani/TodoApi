using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ITodoItemsService
    {
        Task<IEnumerable<TodoItem>> GetAll();
        Task<TodoItem> GetById(long id);
        Task<TodoItem> Create(TodoItem todoItem);
        Task<TodoItem> Update(long id, TodoItem todoItem);
        Task<TodoItem> Delete(long id);
    }
}
