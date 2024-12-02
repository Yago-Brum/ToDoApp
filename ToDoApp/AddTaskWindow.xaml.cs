using OpenAI;
using OpenAI.Chat;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace ToDoApp
{
    public partial class AddTaskWindow : Window
    {
        private OpenAIClient _openAIClient;

        public string TaskTitle { get; private set; } = string.Empty;
        public string TaskDescription { get; private set; } = string.Empty;
        public string TaskDueDateTime { get; private set; } = string.Empty;
        public string TaskPriority { get; private set; } = string.Empty;

        private readonly TaskDatabase _db = new TaskDatabase();

        public AddTaskWindow()
        {
            InitializeComponent();
            _openAIClient = new OpenAIClient(new OpenAIAuthentication(""));
        }

        private async void GenerateDescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleTextBox.Text;

            // Verifique se o título não está vazio
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a task title before generating a description.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Prompt inicial
            string prompt = $"Generate a detailed description for the task titled in 300 caracteres: {title}";

            try
            {
                // Criar mensagens para o chat
                var chatMessages = new[]
                {
                    new Message(Role.System, "You are a helpful assistant."),
                    new Message(Role.User, prompt)
                };

                // Requisição para geração de resposta
                var chatRequest = new ChatRequest(chatMessages, model: "gpt-3.5-turbo");

                // Obter a resposta do modelo
                var chatResponse = await _openAIClient.ChatEndpoint.GetCompletionAsync(chatRequest);

                // Preencher a descrição com o resultado
                string responseContent = chatResponse.FirstChoice.Message.Content.ToString().Trim();
                TaskDescriptionTextBox.Text = responseContent;
            }
            catch (Exception ex)
            {
                // Tratamento de erro
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskTitleTextBox.Text))
            {
                MessageBox.Show("Please enter a task title.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
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
                await Task.Run(() => db.AddTask(newTask));

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
            if (sender is not TextBox textBox)
            {
                e.Handled = true; // Cancela a entrada se o sender não for um TextBox
                return;
            }

            // Garante que o texto atual do TextBox não seja nulo
            string currentText = textBox.Text ?? string.Empty;

            // Insere o texto digitado no ponto atual do cursor
            string newText = currentText.Insert(textBox.SelectionStart, e.Text);

            // Valida o formato do texto após a inserção
            if (!IsValidTimeFormat(newText))
            {
                e.Handled = true; // Cancela a entrada se não for válida
            }
        }

        private void AddPlaceholder(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox == null) return; // Verifica se o TextBox é nulo

            // Verifica se o texto do TextBox está vazio ou contém apenas espaços em branco
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                // Define o valor de placeholder (00:00) e ajusta a cor do texto
                textBox.Text = "00:00";
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
            else
            {
                // Quando o texto não está vazio, formata o texto no formato correto
                textBox.Text = FormatTime(textBox.Text);

                // Ajusta a cor do texto para preto
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }


        private void RemovePlaceholder(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox == null) return; // Verifica se o sender é um TextBox válido

            // Se o texto for "00:00", ele será limpo
            if (textBox.Text == "00:00")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = System.Windows.Media.Brushes.Black; // Define a cor preta ao remover o placeholder
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
