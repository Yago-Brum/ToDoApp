using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        public TaskModel UpdatedTask { get; private set; }
        public string? TaskDueDateTime { get; private set; }

        public EditTaskWindow(TaskModel task)
        {
            InitializeComponent();

            // Preencher os campos com os valores da tarefa
            TaskTitleTextBox.Text = task.Title;
            TaskDescriptionTextBox.Text = task.Description;

            // Atribuindo a data formatada corretamente
            if (DateTime.TryParseExact(task.DueDate, "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime dueDateTime))
            {
                // Exibindo a data e a hora formatadas nos TextBoxes
                TaskDueDateTextBox.Text = dueDateTime.ToString("yyyy-MM-dd"); // Exibe apenas a data
                TaskDueTimeTextBox.Text = dueDateTime.ToString("HH:mm"); // Exibe apenas a hora
            }
            else
            {
                // Tratar erro de formatação se necessário
                MessageBox.Show("Invalid due date format.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            // Atribuindo a prioridade
            PriorityComboBox.SelectedItem = PriorityComboBox.Items.Cast<ComboBoxItem>()
                .FirstOrDefault(item => item.Content.ToString() == task.Priority);

            // Copiar os dados da tarefa selecionada
            UpdatedTask = new TaskModel
            {
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority
            };
        }



        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Atualizar os valores da tarefa
            UpdatedTask.Title = TaskTitleTextBox.Text;
            UpdatedTask.Description = TaskDescriptionTextBox.Text;
            UpdatedTask.Priority = (PriorityComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Low";

            DialogResult = true; // Fecha a janela com sucesso
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Fecha a janela sem salvar
            Close();
        }
    }

}
