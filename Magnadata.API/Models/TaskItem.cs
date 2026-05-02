using System.ComponentModel.DataAnnotations;

namespace Magnadata.API.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public TaskStatus Status { get; set; } = TaskStatus.Pendente;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public enum TaskStatus
    {
        Pendente,
        EmAndamento,
        Concluido
    }
}
