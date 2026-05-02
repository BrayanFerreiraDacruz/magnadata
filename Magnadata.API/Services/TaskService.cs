using Magnadata.API.DTOs;
using Magnadata.API.Models;
using Magnadata.API.Repositories;

namespace Magnadata.API.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllAsync(string? status, string? search);
        Task<TaskDto?> GetByIdAsync(int id);
        Task<TaskDto> CreateAsync(CreateTaskDto createTaskDto);
        Task UpdateAsync(int id, UpdateTaskDto updateTaskDto);
        Task DeleteAsync(int id);
    }

    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskDto>> GetAllAsync(string? status, string? search)
        {
            var tasks = await _repository.GetAllAsync(status, search);
            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status.ToString(),
                CreatedAt = t.CreatedAt
            });
        }

        public async Task<TaskDto?> GetByIdAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null) return null;

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                CreatedAt = task.CreatedAt
            };
        }

        public async Task<TaskDto> CreateAsync(CreateTaskDto createTaskDto)
        {
            if (string.IsNullOrWhiteSpace(createTaskDto.Title))
            {
                throw new ArgumentException("Título é obrigatório.");
            }

            var task = new TaskItem
            {
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                Status = Models.TaskStatus.Pendente,
                CreatedAt = DateTime.Now
            };

            var createdTask = await _repository.CreateAsync(task);

            return new TaskDto
            {
                Id = createdTask.Id,
                Title = createdTask.Title,
                Description = createdTask.Description,
                Status = createdTask.Status.ToString(),
                CreatedAt = createdTask.CreatedAt
            };
        }

        public async Task UpdateAsync(int id, UpdateTaskDto updateTaskDto)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null) throw new KeyNotFoundException("Tarefa não encontrada.");

            // Regra de Negócio: Uma tarefa não pode ser concluída sem título válido
            if (updateTaskDto.Status == Models.TaskStatus.Concluido && string.IsNullOrWhiteSpace(updateTaskDto.Title))
            {
                throw new ArgumentException("Uma tarefa não pode ser concluída sem um título válido.");
            }

            task.Title = updateTaskDto.Title;
            task.Description = updateTaskDto.Description;
            task.Status = updateTaskDto.Status;

            await _repository.UpdateAsync(task);
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null) throw new KeyNotFoundException("Tarefa não encontrada.");

            // Regra de Negócio: Tarefas concluídas não podem ser excluídas (exigir confirmação/flag especial)
            // Aqui vamos apenas lançar exceção para simplificar, ou poderíamos passar um flag 'force'.
            if (task.Status == Models.TaskStatus.Concluido)
            {
                throw new InvalidOperationException("Tarefas concluídas não podem ser excluídas.");
            }

            await _repository.DeleteAsync(id);
        }
    }
}
