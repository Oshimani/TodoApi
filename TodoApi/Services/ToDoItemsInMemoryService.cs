using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class TodoItemsInMemoryService : ITodoItemsService
    {
        private readonly TodoContext _context;

        public TodoItemsInMemoryService(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            try
            {
                return await _context.TodoItems.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch!", ex);
            }
        }

        public async Task<TodoItem> GetById(long id)
        {
            try
            {
                var todoItem = await _context.TodoItems.FindAsync(id);
                if (todoItem == null) return null;
                return todoItem;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch!", ex);
            }

        }

        public async Task<TodoItem> Create(TodoItem todoItem)
        {
            try
            {
                _context.TodoItems.Add(todoItem);
                var res = await _context.SaveChangesAsync();
                return todoItem;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create new Todo!", ex);
            }

        }

        public async Task<TodoItem> Update(long id, TodoItem todoItem)
        {

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                var res = await _context.SaveChangesAsync();
                return todoItem;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!_TodoItemExists(id))
                    throw new Exception("Item does not exist!", ex);
                throw new Exception("Failed to update item!", ex);
            }
        }

        public async Task<TodoItem> Delete(long id)
        {
            try
            {
                var todoItem = await _context.TodoItems.FindAsync(id);
                if (todoItem == null)
                    throw new Exception("Item does not exist!");

                _context.TodoItems.Remove(todoItem);
                var res = await _context.SaveChangesAsync();
                return todoItem;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete item!", ex);
            }


        }

        private bool _TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
