# ToDoApp - Task Management Application

**ToDoApp** is a simple task management application designed to help organize and manage your daily tasks. The app allows users to add, edit, and view tasks, as well as automatically generate descriptions for tasks using the OpenAI API.

## Features

- **Add Tasks**: Add new tasks with a title, description, due date, due time, and priority.
- **Automatic Description Generation**: Use the OpenAI API to generate detailed descriptions for your tasks based on the title.
- **Edit Tasks**: Edit the title, description, due date, due time, and priority of your tasks.
- **Task Storage**: Tasks are stored in a local SQLite database, ensuring persistence even after application restarts.
- **Priority**: Set the priority of tasks as "High", "Medium", or "Low".
- **Task Validation**: Ensure all fields are completed before adding or saving tasks.
- **Time and Date Picker**: The app includes a date and time picker for managing deadlines.
- **User Interface (UI)**: Simple and clean UI designed with WPF (Windows Presentation Foundation).
- **Responsive Layout**: The app adapts to different screen sizes and resolution settings, providing a user-friendly experience.

## Technologies

- **.NET 8.0**: The framework used to develop the application.
- **WPF (Windows Presentation Foundation)**: Used for the graphical user interface (GUI).
- **SQLite**: Local database to store tasks.
- **OpenAI API**: Used to generate task descriptions automatically.
- **Newtonsoft.Json**: Used for JSON serialization/deserialization when interacting with the OpenAI API.

## How it Works

- **Generate Descriptions**: Once you add a task title, you can click on the "Generate Description" button to automatically generate a description for your task using OpenAI's GPT-3.5 or GPT-4 model. The generated description is based on the task title and provides a detailed explanation.
  
- **Task Management**: Users can add, edit, and view tasks with relevant information, such as title, description, due date, time, and priority.

- **Task Persistence**: All tasks are saved locally in a SQLite database, so they remain available even if the application is closed and reopened.

## Resources

- **OpenAI API**: To use the description generation feature, you need an OpenAI API key. You can get an API key [here](https://platform.openai.com/signup).
  
- **SQLite**: The database used for task storage. No additional setup required, as the app automatically creates and manages the database file.

- **WPF UI**: The application uses WPF to display all user interface components. It is responsive and adjusts based on user actions.

- **.NET 8.0 SDK**: This project is built using .NET 8.0, and you can explore its features for better performance and cross-platform support.

## Project Structure

- **AddTaskWindow.xaml**: Contains the UI for adding new tasks.
- **AddTaskWindow.xaml.cs**: Code-behind for the add task window logic.
- **ToDoApp.db**: SQLite database file where tasks are stored.
- **OpenAI-DotNet.dll**: Dependency for accessing the OpenAI API and generating task descriptions.
- **MainWindow.xaml**: Contains the main UI window where tasks are listed and managed.
- **MainWindow.xaml.cs**: Code-behind for the main window logic.

## Additional Features

- **Error Handling**: The application provides helpful error messages in case of missing input or invalid actions, making it user-friendly.
  
- **Placeholder Text**: The app uses placeholder text in various text fields to guide the user, such as "Enter task title", and automatically formats time inputs like "00:00".

- **Task Sorting and Filtering**: Future releases may include features for sorting tasks by due date or priority and filtering tasks by status.

- **Customizable Interface**: The WPF-based design allows for easy customization of the user interface, including changes to color schemes and font styles.

## Contributing

Contributions are welcome! If you find a bug or want to add a new feature, feel free to open an issue or submit a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
