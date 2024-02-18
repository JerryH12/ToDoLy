// Requirements
// Model a task with a task title, due date, status and project
// Display a collection of tasks that can be sorted both by date and project
//Support the ability to add, edit, mark as done, and remove tasks
// Support a text-based user interface
// Load and save task lists to file. The solution may also include other creative features at your discretion in case you wish to show some flair.

using System.Xml.Linq;
using ToDoLy;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System;

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

ToDoList todoly = new ToDoList();
//string projectName = todoly.SelectedProject.projectName;
bool go = true;
bool skipMenu = false;
string input="";

while (go)
{
    // check whether to continue with the previous input or select another one.
    if (!skipMenu) 
    {
        Console.WriteLine($"{CYAN}>>{NORMAL} {BOLD}{RED}Welcome to ToDoLy{NORMAL}{NOBOLD}");
        Console.WriteLine($"{CYAN}>>{NORMAL} You have {YELLOW}{todoly.CountUnfinishedTasks()} tasks todo{NORMAL} and {GREEN}{todoly.CountFinishedTasks()} tasks are done!{GREEN}{NORMAL}");
        Console.WriteLine($"{CYAN}>>{NORMAL} Pick an option:");
        Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}1{NORMAL}) Show Task List (by date or project)");
        Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}2{NORMAL}) Add New Task");
        Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}3{NORMAL}) Edit Task (update, mark as done, remove)");
        Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}4{NORMAL}) Save and Quit");

        input = Console.ReadLine();
    }
    skipMenu = false;
    MainOptions(input); 
}

// perform actions from the main menu.
void MainOptions(string options)
{
    string projectName = todoly.SelectedProject.projectName;

    switch (input)
    {
        case "1":
            // Show a complete list of projects and tasks.
            todoly.ShowToDoList();
            break;
        case "2":
            // Add a new task.
            Console.Write("Enter a name for the new task: ");
            string taskName = Console.ReadLine();
            Console.Write("Enter the date for the deadline: ");
            string dueDate = Console.ReadLine();
            todoly.AddTask(projectName, taskName, Convert.ToDateTime(dueDate));
            Console.WriteLine("Task added successfully!");
            //TODO: Select a project. Also add due date. Show message Done! when successful. Able to quit.
            break;
        case "3":
            Console.WriteLine("---------------------------------------------------------------------------------------");       
            Console.WriteLine($"Tasks to be done in {projectName}:");

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
 
                    TaskOptions(currentTask, userInput);            
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
                    else if(userInput.ToLower() !="q")
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
        case "4":
            todoly.SaveToFile();
            go = false;
            break;
        default:
            break;
    }
    return;
}

// task operations
void TaskOptions(ToDoLy.Task task1, string options)
{
    string userInput = "";

    switch (options.ToLower())
    {
        case "d":
            Console.Write("\nEnter \"D\" again to delete this task: | \"Q\" to quit: ");
            userInput = Console.ReadLine();

            if (userInput.ToLower() == "d")
            {
                todoly.RemoveTask(task1); // to be tested.
                Console.WriteLine("Task removed successfully!"); // TODO: is it?
            }
            break;
        case "n":
            // alter the name.
            Console.Write("\nEnter a new name for this task: | \"Q\" to quit: ");
            userInput = Console.ReadLine();

            if (userInput.ToLower() != "q")
            {
                task1.Title = userInput;
            }
            break;
        case "s":
            // update status
            Console.Write("\nIf the task is finished - type \"Y\". Otherwise \"N\": | \"Q\" to quit: ");
            userInput = Console.ReadLine().ToLower();

            if (userInput == "y")
            {
                task1.Status = 1;
            }
            else if (userInput == "n")
            {
                task1.Status = 0;
            }
            break;
        case "q": // quit
            break;
        default:
            break;
    }
    return;
}




