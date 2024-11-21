using System.Collections.ObjectModel;
using System.Windows;

namespace ToDoApp
{
    public partial class MainWindow : Window
    {
        private TaskDatabase _db = new TaskDatabase();
        public ObservableCollection<TaskModel> Tasks { get; set; } = new ObservableCollection<TaskModel>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;  // Vincula a View ao ViewModel (ou neste caso, o código-behind)
            LoadTasks(); // Carrega as tarefas quando o aplicativo inicia
        }

        // Carregar as tarefas do banco de dados
        private void LoadTasks()
        {
            Tasks.Clear(); // Limpa a lista de tarefas existente
            foreach (var task in _db.GetAllTasks()) // Obtém as tarefas do banco
            {
                Tasks.Add(task); // Adiciona à ObservableCollection
            }
        }

        // Adicionar uma nova tarefa ao banco e recarregar a lista
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            var addTaskWindow = new AddTaskWindow();
            if (addTaskWindow.ShowDialog() == true)
            {
                // Criar a nova tarefa com os dados inseridos
                var newTask = new TaskModel
                {
                    Title = addTaskWindow.TaskTitle,
                    Description = addTaskWindow.TaskDescription,
                    DueDate = addTaskWindow.TaskDueDateTime,
                    Priority = addTaskWindow.TaskPriority,
                    IsCompleted = false
                };

                // Adicionar a tarefa ao banco de dados
                //_db.AddTask(newTask);
                //Tasks.Add(newTask); // Adiciona a tarefa diretamente na ObservableCollection
            }
        }

        // Editar uma tarefa selecionada (função a ser implementada)
        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit functionality not implemented yet.");
        }

        // Excluir uma tarefa selecionada
        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListView.SelectedItem is TaskModel selectedTask)
            {
                _db.DeleteTask(selectedTask.Id); // Excluir do banco de dados
                Tasks.Remove(selectedTask); // Remove da ObservableCollection
            }
            else
            {
                MessageBox.Show("Please select a task to delete.");
            }
        }
    }
}
