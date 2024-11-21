namespace ToDoApp
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted { get; set; }
    }
}
