using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    #region TodoController
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemsService _todoItemsService;
        public TodoItemsController(ITodoItemsService todoItemsService)
        {
            _todoItemsService = todoItemsService;
        }
        #endregion

        #region snippet_GetAll
        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            try
            {
                return Ok(await _todoItemsService.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(599, ex.Message);
            }
        }
        #endregion

        #region snippet_GetByID
        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            if (id == 0) return BadRequest("Id must not be 0!");

            try
            {
                var todoItem = await _todoItemsService.GetById(id);
                if (todoItem == null)
                    return NotFound("Todo item not found!");

                return Ok(todoItem);
            }
            catch (Exception ex)
            {
                return StatusCode(599, ex.Message);
            }
        }
        #endregion

        #region snippet_Update
        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (todoItem == null) return BadRequest("Provide todo item!");
            if (id != todoItem.Id) return BadRequest("Ids don't match!");
            if (id == 0) return BadRequest("Id must not be 0!");

            try
            {
                TodoItem todo = await _todoItemsService.Update(id, todoItem);
                return AcceptedAtAction(nameof(GetTodoItem), new { todoItem.Id }, todoItem);
            }
            catch (Exception ex)
            {
                return StatusCode(599, ex.Message);
            }
        }
        #endregion

        #region snippet_Create
        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            if (todoItem == null) return BadRequest("Provide todo item!");

            try
            {
                await _todoItemsService.Create(todoItem);
                return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
            }
            catch (Exception ex)
            {
                return StatusCode(599, ex.Message);
            }
        }
        #endregion

        #region snippet_Delete
        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            if (id == 0) return BadRequest("Id must not be 0!");

            try
            {
                await _todoItemsService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(599, ex.Message);
            }
        }
        #endregion
    }
}