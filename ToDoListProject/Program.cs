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
string projectName = todoly.SelectedProject.projectName;
bool go = true;
bool rerun = false;
string input="";

while (go)
{
    

    // check whether to continue with the previous input or select another one.
    if (!rerun) 
    {
        Console.WriteLine($">> {RED}Welcome{NORMAL} to {YELLOW}ToDoLy{NORMAL}");
        Console.WriteLine($">> You have {todoly.CountFinishedTasks()} tasks todo and {todoly.CountUnfinishedTasks()} tasks are done!");
        Console.WriteLine(">> Pick an option:");
        Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}1{NORMAL}) Show Task List (by date or project)");
        Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}2{NORMAL}) Add New Task");
        Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}3{NORMAL}) Edit Task (update, mark as done, remove)");
        Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}4{NORMAL}) Save and Quit");

        input = Console.ReadLine();
    }
    rerun = true; 

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
            Console.WriteLine("---------------------------------------------------------------------------------------");
            //Console.WriteLine($"Project: {CYAN}{UNDERLINE}{projectName}{NOUNDERLINE}{NORMAL}");
            Console.WriteLine($"Tasks to be done in {projectName}:");
            // Show a list of tasks for this project.
   
            int index = 1;
            foreach(ToDoLy.Task item in todoly.SelectedProject.tasks)
            {
                string statusDescription = item.status == 1 ? "Finished" : "Unfinished";
                Console.WriteLine($"\n({MAGENTA}{index}{NORMAL}) * " + item.title.PadRight(20) + "Due date: " + item.dueDate.PadRight(20) + "Status: " + statusDescription);
                index++;
            }

            Console.WriteLine("\nEnter a number to edit a task: | To select another project - enter \"P\": | To quit - enter \"Q\": ");

            try
            {
                // Select the task to be edited or deleted.
                string userInput = Console.ReadLine().ToLower();

                if (int.TryParse(userInput, out int value)) 
                { 
                    int taskID = value - 1;
                    ToDoLy.Task currentTask = todoly.SelectedProject.tasks.ElementAt(taskID);
                    string statusDescription = currentTask.status == 1 ? "Finished" : "Unfinished";

                    Console.WriteLine("---------------------------------------------------------------------------------------");
                    Console.WriteLine("Task: " + currentTask.title.PadRight(20) + "Due date: " + currentTask.dueDate.PadRight(20) + "Status: " + statusDescription);
                    
                    // select options
                    Console.Write("\nTo delete this task - enter \"D\" | To alter the name - enter \"N\" | To update the status - enter \"S\" | To quit - enter \"Q\": ");
                    userInput = Console.ReadLine();
                    TaskOptions(currentTask, userInput);
                    
                }
                else if(userInput == "p")
                { 
                    foreach(KeyValuePair<string, ToDoLy.Project> projectItem in todoly.Projects)
                    {
                        Console.WriteLine($"{CYAN}{projectItem.Value.projectName}{ NORMAL}");
                    }

                    Console.WriteLine("Select one of the projects: ");
                    projectName = Console.ReadLine();
                    todoly.SetCurrentProject(projectName);
                    rerun = true;
                    //Console.WriteLine($"{CYAN}{UNDERLINE}{projectName}{NOUNDERLINE}{NORMAL}");
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
            break;
    }
}

void TaskOptions(ToDoLy.Task task1, string options)
{
    switch (options.ToLower())
    {
        case "d":
            // delete
            break;
        case "n":
            // alter the name.
            Console.Write("\nEnter a new name for this task: ");
            string newTitle = Console.ReadLine();
            task1.title = newTitle;
            break;
        case "s":
            // update status
            Console.WriteLine("\nIf the task is finished - type \"Y\". Otherwise \"N\".");
            string userInput = Console.ReadLine().ToLower();
            if (userInput == "y")
            {
                task1.status = 1;
            }
            else if (userInput == "n")
            {
                task1.status = 0;
            }
            break;
        case "q":
            break;
        default:
            break;
    }
    return;
}




