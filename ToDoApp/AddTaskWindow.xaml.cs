using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ToDoApp
{
    public partial class AddTaskWindow : Window
    {
        public string TaskTitle { get; private set; }
        public string TaskDescription { get; private set; }
        public string TaskDueDateTime { get; private set; }
        public string TaskPriority { get; private set; }

        private readonly TaskDatabase _db = new TaskDatabase();

        public AddTaskWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TaskTitle = TaskTitleTextBox.Text;
                TaskDescription = TaskDescriptionTextBox.Text;

                // Valida a data e hora
                if (!TaskDueDatePicker.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select a due date.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!DateTime.TryParseExact(TaskDueTimeTextBox.Text, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime time))
                {
                    MessageBox.Show("Please enter a valid time (HH:mm).", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Combina a data com a hora
                DateTime dueDateTime = TaskDueDatePicker.SelectedDate.Value.Add(time.TimeOfDay);

                TaskDueDateTime = dueDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                TaskPriority = PriorityComboBox.Text;

                // Adiciona tarefa no banco de dados
                var newTask = new TaskModel
                {
                    Title = TaskTitle,
                    Description = TaskDescription,
                    DueDate = TaskDueDateTime,
                    Priority = TaskPriority,
                    IsCompleted = false
                };

                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow?.Tasks.Add(newTask);

                // Salva no banco
                var db = new TaskDatabase();
                db.AddTask(newTask);

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Retorna falha
            Close();
        }
        private void ValidateTimeInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            string newText = textBox.Text.Insert(textBox.SelectionStart, e.Text);

            // Validar formato parcial (hora ou minuto)
            if (!IsValidTimeFormat(newText))
            {
                e.Handled = true; // Cancela a entrada se não for válida
            }
        }

        private void AddPlaceholder(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "00:00";
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
            else
            {
                // Ajustar para o formato correto ao sair do campo
                textBox.Text = FormatTime(textBox.Text);
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void RemovePlaceholder(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "00:00")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private bool IsValidTimeFormat(string input)
        {
            // Permitir formatos parciais como "9", "09", "09:0"
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"^([0-9]{1,2}(:[0-9]{0,2})?)?$");
        }

        private string FormatTime(string input)
        {
            if (DateTime.TryParse(input, out DateTime parsedTime))
            {
                return parsedTime.ToString("HH:mm"); // Formato correto de hora e minuto
            }

            // Caso o usuário digite apenas a hora (ex: "9"), completar com ":00"
            if (int.TryParse(input, out int hour) && hour >= 0 && hour <= 23)
            {
                return $"{hour:D2}:00"; // Completa com :00 se a hora estiver correta
            }

            return "00:00"; // Valor padrão em caso de erro
        }

    }
}
