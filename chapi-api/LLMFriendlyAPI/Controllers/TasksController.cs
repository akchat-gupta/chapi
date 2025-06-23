// This file represents the final enhanced backend controller with all LLM-friendly improvements
// including: example integration, response enrichment, semantic annotations, and best practices.

using System.ComponentModel.DataAnnotations;
using LLMFriendlyAPI.DTOs;
using LLMFriendlyAPI.Examples;
using LLMFriendlyAPI.Models;
using LLMFriendlyAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace LLMFriendlyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TasksController(ITaskService service) : ControllerBase
    {
        private readonly ITaskService _service = service;

        /// <summary>Retrieve all tasks in the system.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<TaskDto>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get all tasks", OperationId = "GetAllTasks")]
        public IActionResult GetAllTasks() => Ok(_service.GetAll());

        /// <summary>Fetch a specific task by its unique identifier.</summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Get task by ID", OperationId = "GetTaskById")]
        public IActionResult GetTaskById(
            [FromRoute, Required, Range(1, int.MaxValue)] int id)
        {
            var task = _service.GetById(id);
            return task == null
                ? NotFound(new ProblemDetails { Title = "Task not found", Detail = $"Task with ID {id} was not found." })
                : Ok(task);
        }

        /// <summary>Create a new task with title, description, and due date.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(TaskDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Create a new task", OperationId = "CreateTask")]
        [SwaggerRequestExample(typeof(CreateTaskRequest), typeof(CreateTaskRequestExample))]
        public IActionResult CreateTask([FromBody, Required] CreateTaskRequest request)
        {
            var created = _service.Create(request);
            return CreatedAtAction(nameof(GetTaskById), new { id = created.Id }, created);
        }

        /// <summary>Update the fields of an existing task by ID.</summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Update an existing task", OperationId = "UpdateTask")]
        [SwaggerRequestExample(typeof(TaskDto), typeof(TaskDtoExample))]
        public IActionResult UpdateTask(
            [FromRoute, Required] int id,
            [FromBody, Required] TaskDto dto)
        {
            var success = _service.Update(id, dto);
            return success
                ? NoContent()
                : NotFound(new ProblemDetails { Title = "Task not found", Detail = $"Task with ID {id} does not exist." });
        }

        /// <summary>Remove a task by its identifier.</summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Delete a task", OperationId = "DeleteTask")]
        public IActionResult DeleteTask([FromRoute, Required] int id)
        {
            var deleted = _service.Delete(id);
            return deleted
                ? NoContent()
                : NotFound(new ProblemDetails { Title = "Task not found", Detail = $"Task with ID {id} was not found." });
        }

        /// <summary>Add a comment to a specific task.</summary>
        [HttpPost("{id}/comments")]
        [ProducesResponseType(typeof(TaskComment), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Add comment to task", OperationId = "AddCommentToTask")]
        [SwaggerRequestExample(typeof(AddCommentRequest), typeof(AddCommentRequestExample))]
        public IActionResult AddCommentToTask([FromRoute, Required] int id, [FromBody, Required] AddCommentRequest request)
        {
            if (_service.GetById(id) == null)
                return NotFound(new ProblemDetails { Title = "Task not found", Detail = $"Task with ID {id} was not found." });

            var comment = _service.AddComment(id, request);
            return Ok(comment);
        }

        /// <summary>Update the status of a task (e.g., mark as Completed or In Progress).</summary>
        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Update task status", OperationId = "UpdateTaskStatus")]
        [SwaggerRequestExample(typeof(TaskStatusUpdateRequest), typeof(TaskStatusUpdateRequestExample))]
        public IActionResult UpdateTaskStatus([FromRoute, Required] int id, [FromBody, Required] TaskStatusUpdateRequest request)
        {
            var updated = _service.UpdateStatus(id, request);
            return updated
                ? NoContent()
                : NotFound(new ProblemDetails { Title = "Task not found", Detail = $"Task with ID {id} was not found." });
        }

        /// <summary>Search for tasks based on optional filters like status and due date.</summary>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<Models.Task>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Search tasks by filters", OperationId = "SearchTasks")]
        public IActionResult SearchTasks(
            [FromQuery] string? status,
            [FromQuery] string? assignedTo,
            [FromQuery] DateTime? dueBefore)
        {
            var results = _service.Search(status, assignedTo, dueBefore);
            return Ok(results);
        }
    }
}
