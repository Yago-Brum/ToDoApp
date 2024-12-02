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
        // Evento para ordenar por prioridade
        private void SortByPriority_Click(object sender, RoutedEventArgs e)
        {
            // Ordena as tarefas pela prioridade, da maior para a menor
            var sortedTasks = Tasks.OrderByDescending(task => GetPriorityValue(task.Priority)).ToList();

            // Atualiza a coleção ObservableCollection
            Tasks.Clear();
            foreach (var task in sortedTasks)
            {
                Tasks.Add(task);
            }

            // Força a atualização da lista de exibição
            TaskListView.Items.Refresh();
        }

        // Método para atribuir um valor numérico à prioridade
        public int GetPriorityValue(string? priority)
        {
            // Verifique se o valor de 'priority' é nulo ou vazio e trate isso
            if (string.IsNullOrEmpty(priority))
            {
                return 0; // Pode retornar um valor padrão, como 0, para tarefas com prioridade indefinida
            }

            // Retorna a prioridade numérica baseada no valor da string (exemplo de prioridades)
            switch (priority.ToLower())
            {
                case "high":
                    return 3; // Alta prioridade
                case "medium":
                    return 2; // Prioridade média
                case "low":
                    return 1; // Baixa prioridade
                default:
                    return 0; // Se não for um valor conhecido, define uma prioridade baixa por padrão
            }
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            // Verificar se há uma tarefa selecionada
            var selectedTask = TaskListView.SelectedItem as TaskModel;

            if (selectedTask == null)
            {
                MessageBox.Show("Por favor, selecione uma tarefa para editar.", "Editar Tarefa", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Criar uma janela/modal para editar a tarefa (exemplo básico de entrada)
            EditTaskWindow editWindow = new EditTaskWindow(selectedTask);

            if (editWindow.ShowDialog() == true)
            {
                // Atualizar a lista de tarefas
                var index = Tasks.IndexOf(selectedTask);
                if (index >= 0)
                {
                    Tasks[index] = editWindow.UpdatedTask;
                    TaskListView.Items.Refresh();
                }
            }
        }
    }
}
