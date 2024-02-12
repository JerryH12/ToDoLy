// Requirements
// Model a task with a task title, due date, status and project
// Display a collection of tasks that can be sorted both by date and project
//Support the ability to add, edit, mark as done, and remove tasks
// Support a text-based user interface
// Load and save task lists to file. The solution may also include other creative features at your discretion in case you wish to show some flair.

using System.Xml.Linq;
using ToDoListProject;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;
using System.Diagnostics;

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
string projectName = todoly.SelectedProject.projectName;

bool go = true;

while (go)
{
    Console.WriteLine($">> {RED}Welcome{NORMAL} to {YELLOW}ToDoLy{NORMAL}");
    Console.WriteLine($">> You have {todoly.CountFinishedTasks()} tasks todo and {todoly.CountUnfinishedTasks()} tasks are done!");
    Console.WriteLine(">> Pick an option:");
    Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}1{NORMAL}) Show Task List (by date or project)");
    Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}2{NORMAL}) Add New Task");
    Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}3{NORMAL}) Edit Task (update, mark as done, remove)");
    Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}4{NORMAL}) Save and Quit");

    string input = Console.ReadLine();

    switch (input)
    {
        case "1":
            // Show a complete list of projects and tasks.
            todoly.ShowToDoList();
            break;
        case "2":
            // Add a new task.
            Console.WriteLine("Enter a name for the task.");
            string taskName = Console.ReadLine();
            todoly.AddTask(projectName, taskName);
            break;
        case "3":
            // Select a project.
            Console.WriteLine($"{CYAN}{UNDERLINE}{projectName}{NOUNDERLINE}{NORMAL}");
            

            // TODO: let user select another project by entering P.
            //projectName = Console.ReadLine();
            //Console.WriteLine($"{CYAN}{UNDERLINE}{projectName}{NOUNDERLINE}{NORMAL}");

            // Show a list of tasks for this project.
            int index = 1;
            foreach(ToDoListProject.Task item in todoly.SelectedProject.tasks)
            {
                Console.WriteLine($"({MAGENTA}{index}{NORMAL}) " + item.title);
                index++;
            }

            Console.WriteLine("Enter a number to edit a task: | To select another project - enter \"P\": | To quit - enter \"Q\": ");

            try
            {
                // Select the task to be edited or deleted.
                //Console.Write("To select a task you want to change - enter a number: | To quit - enter \"Q\": ");

                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int value)) 
                { 
                    int taskID = value - 1;
                    ToDoListProject.Task currentTask = todoly.SelectedProject.tasks.ElementAt(taskID);
                    Console.WriteLine("Task: " + currentTask.title.PadRight(20) + "Due date: " + currentTask.dueDate.PadRight(20) + "Status: " + currentTask.status);
                    Console.Write("To delete this task - enter \"D\" | To alter the name - enter \"N\" | To update the status - enter \"S\" | To quit - enter \"Q\": ");
                    // Edit the task.
                    Console.WriteLine("New name for the task?");
                    string newTitle = Console.ReadLine();
                    currentTask.title = newTitle;
                }
                else if(userInput == "P")
                {
                    //TODO
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            break;
        case "4":
            todoly.SaveToFile();
            go = false;
            break;
        default:
            //
            break;
    }

}




