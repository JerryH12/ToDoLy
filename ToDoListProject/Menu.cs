using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoLy
{
    internal class Menu
    {
        /*
        string NL = Environment.NewLine; // shortcut
        string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
        string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
        string GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
        string YELLOW = Console.IsOutputRedirected ? "" : "\x1b[93m";
        string BLUE = Console.IsOutputRedirected ? "" : "\x1b[94m";
        string MAGENTA = Console.IsOutputRedirected ? "" : "\x1b[95m";
        string CYAN = Console.IsOutputRedirected ? "" : "\x1b[96m";
        string GREY = Console.IsOutputRedirected ? "" : "\x1b[97m";
        string BOLD = Console.IsOutputRedirected ? "" : "\x1b[1m";
        string NOBOLD = Console.IsOutputRedirected ? "" : "\x1b[22m";
        string UNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[4m";
        string NOUNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[24m";
        string REVERSE = Console.IsOutputRedirected ? "" : "\x1b[7m";
        string NOREVERSE = Console.IsOutputRedirected ? "" : "\x1b[27m";

        public ToDoList todoly;

        // perform actions from the main menu.
        public void MainMenu(string options)
        {
            string projectName = todoly.SelectedProject.projectName;

            switch (options)
            {
                case "1":
                    // Show a complete list of projects and tasks.
                    todoly.SortTasksByDateAscending();
                    todoly.ShowToDoList();
                    break;
                case "2":
                    todoly.SortTasksByProject();
                    todoly.ShowToDoList();
                    break;
                case "3":
                    todoly.SortTasksByDateDescending();
                    todoly.ShowToDoList();
                    break;
                case "4":
                    try
                    {
                        // Add a new task.
                        Console.Write("Enter a name for the new task: ");
                        string taskName = Console.ReadLine();
                        Console.Write("Enter the date for the deadline: ");
                        string dueDate = Console.ReadLine();
                        todoly.AddTask(projectName, taskName, Convert.ToDateTime(dueDate));
                        Console.WriteLine("Task added successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "5":
                    Console.WriteLine("---------------------------------------------------------------------------------------");
                    Console.WriteLine($"Tasks to be done in {CYAN}{projectName}{NORMAL}:");

                    // Show a list of tasks for this project.
                    int index = 1;
                    foreach (ToDoLy.Task item in todoly.SelectedProject.tasks)
                    {
                        string statusDescription = item.Status == 1 ? $"{GREEN}Finished{NORMAL}" : $"{YELLOW}Unfinished{NORMAL}";
                        Console.WriteLine($"\n({MAGENTA}{index}{NORMAL}) * " + item.Title.PadRight(20) + "Due date: " + item.DueDate.ToString("yy-MM-dd").PadRight(20) + "Status: " + statusDescription);
                        index++;
                    }

                    Console.WriteLine("\nEnter a number to edit a task: | To select another project - enter \"P\": | \"Q\" to quit: ");

                    try
                    {
                        // Select the task to be edited or deleted.
                        string userInput = Console.ReadLine().ToLower();

                        if (int.TryParse(userInput, out int value))
                        {
                            int taskID = value - 1;
                            ToDoLy.Task currentTask = todoly.SelectedProject.tasks.ElementAt(taskID);
                            string statusDescription = currentTask.Status == 1 ? $"{GREEN}Finished{NORMAL}" : $"{YELLOW}Unfinished{NORMAL}";

                            Console.WriteLine("---------------------------------------------------------------------------------------");
                            Console.WriteLine("Task: " + currentTask.Title.PadRight(20) + "Due date: " + currentTask.DueDate.ToString("yy-MM-dd").PadRight(20) + "Status: " + statusDescription);

                            // select options
                            Console.WriteLine("\nTo delete this task - enter \"D\" | To alter the name - enter \"N\" | To update the status - enter \"S\" | \"Q\" to quit: ");
                            userInput = Console.ReadLine();

                            TaskOperations(currentTask, userInput);
                        }
                        else if (userInput == "p")
                        {
                            foreach (KeyValuePair<string, ToDoLy.Project> projectItem in todoly.Projects)
                            {
                                Console.WriteLine($"{CYAN}{projectItem.Value.projectName}{NORMAL}");
                            }

                            Console.Write("Select one of these projects: ");
                            userInput = Console.ReadLine();

                            if (todoly.Projects.ContainsKey(userInput))
                            {
                                todoly.SetCurrentProject(userInput);
                                Console.WriteLine("Done!");
                            }
                            else if (userInput.ToLower() != "q")
                            {
                                Console.WriteLine("The selected project doesn't exist!"); // TODO: Ask if user wish to create it.
                            }
                            skipMenu = true; // jump to switch without menu.      
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "6":
                    todoly.SaveToFile();
                    go = false;
                    break;
                default:
                    break;
            }
            return;

        }

        public void TaskOperations(ToDoLy.Task task1, string options)
        {

        }*/
    }
}
