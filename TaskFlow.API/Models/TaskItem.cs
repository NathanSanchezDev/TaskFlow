namespace TaskFlow.API.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = "ToDo";
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
