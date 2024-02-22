using ToDoLy;

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

bool go = true;

while (go)
{
    string input = "";

    // The main menu
    Console.WriteLine($"{CYAN}>>{NORMAL} {BOLD}{MAGENTA}WELCOME TO TODOLY{NORMAL}{NOBOLD}");
    Console.WriteLine($"{CYAN}>>{NORMAL} You have {YELLOW}{todoly.CountUnfinishedTasks()} tasks todo{NORMAL} and {GREEN}{todoly.CountFinishedTasks()} tasks are done!{GREEN}{NORMAL}");
    Console.WriteLine($"{CYAN}>>{NORMAL} Pick an option:");
    Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}1{NORMAL}) Show To-Do list");
    Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}2{NORMAL}) List Sorted by Project");
    Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}3{NORMAL}) List Sorted by Date Descending");
    Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}4{NORMAL}) See and Edit Tasks in {CYAN}{todoly.SelectedProject.projectName}{NORMAL}");
    //Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}5{NORMAL}) Add New Task in {CYAN}{todoly.SelectedProject.projectName}{NORMAL}");
    Console.WriteLine($"{CYAN}>>{NORMAL} ({MAGENTA}5{NORMAL}) Save and Quit");

    input = Console.ReadLine();
     
    MainMenuAction(input); 
}

// perform actions from the main menu.
void MainMenuAction(string options)
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
            // Sort by project name.
            todoly.SortTasksByProject();
            todoly.ShowToDoList();
            break; 
        case "3":
            // Sort by date and descending order.
            todoly.SortTasksByDateDescending();
            todoly.ShowToDoList();
            break;
        case "4":
            // Edit a task
            EditTaskMenu(); 
            break;
        case "5":
            // Save and quit.
            todoly.SaveToFile();
            go = false;
            break;
        default:
            break;
    }
    return;
}

// Create a new project and a task.
void CreateNewProject(string projectName)
{
    Console.WriteLine("Do you wish to create it? Enter \"Y\". Otherwise any key to quit: ");
    string userInput = Console.ReadLine();

   if(userInput.ToLower() == "y")
    {
        try
        {
            todoly.AddProject(projectName);
            ToDoLy.Task task1 = todoly.SelectedProject.tasks.First();

            Console.WriteLine("Add your first task for the projekt. Enter a title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Enter the date it needs to be finished (yy-MM-dd): ");

            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                throw new FormatException();
            }

            task1.Title = title;
            task1.DueDate = date;
            task1.Status = 0;

            Console.WriteLine("Done!");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"{RED}{ex.Message}{NORMAL}");
        }
        return;
    }
}

// Edit the task menu
void EditTaskMenu()
{
    string projectName = todoly.SelectedProject.projectName;

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

    Console.WriteLine("\nTo edit task - enter a number: | To select another project - enter \"P\": | To add a task - enter \"A\": | \"Q\" to quit: ");

    try
    {
        // Select the task to be edited or deleted.
        string userInput = Console.ReadLine().ToLower();

        if(userInput == "a")
        {
            AddTask();
            EditTaskMenu();
        }
        else if (int.TryParse(userInput, out int value))
        {
            int taskID = value - 1;
            ToDoLy.Task currentTask = todoly.SelectedProject.tasks.ElementAt(taskID);
            string statusDescription = currentTask.Status == 1 ? $"{GREEN}Finished{NORMAL}" : $"{YELLOW}Unfinished{NORMAL}";

            Console.WriteLine("---------------------------------------------------------------------------------------");
            Console.WriteLine("Task: " + currentTask.Title.PadRight(20) + "Due date: " + currentTask.DueDate.ToString("yy-MM-dd").PadRight(20) + "Status: " + statusDescription);

            // Select options.
            Console.WriteLine("\nTo delete this task - enter \"D\" | To alter the title - enter \"T\" | To update the status - enter \"S\" | \"Q\" to quit: ");
            userInput = Console.ReadLine();

            // Do the task operations.
            TaskOperations(currentTask, userInput);
        }
        else if (userInput == "p")
        {
            // Show a list of projects.
            foreach (KeyValuePair<string, ToDoLy.Project> projectItem in todoly.Projects)
            {
                Console.WriteLine($"{CYAN}{projectItem.Value.projectName}{NORMAL}");
            }

            // Select the project to work on.
            Console.Write("Select one of these projects: ");
            userInput = Console.ReadLine();

            if (todoly.Projects.ContainsKey(userInput)) // todo kan göras i objektet
            {
                todoly.SetCurrentProject(userInput);
                Console.WriteLine("Done!");
            }
            else if (userInput.ToLower() != "q")
            {
                Console.WriteLine($"{RED}The selected project doesn't exist!{NORMAL}");
                CreateNewProject(userInput);
            }
          
            EditTaskMenu(); // repeat this function.
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return;
}

// Task operations.
void TaskOperations(ToDoLy.Task task1, string options)
{
    string userInput = "";

    switch (options.ToLower())
    {
        case "d":
            // Delete the task.    
            if (todoly.RemoveTask(task1))
            {
                Console.WriteLine("Task removed successfully!");
            }
            else
            {
                Console.WriteLine($"{RED}The task could not be removed!{NORMAL}");
            }         
            break;
        case "t":
            // alter the title.
            Console.Write("\nEnter a new title for this task: | \"Q\" to quit: ");
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
    // return to previous menu.
    EditTaskMenu();
}

void AddTask()
{
    try
    {
        // Add a new task.
        string projectName = todoly.SelectedProject.projectName;
        Console.Write("Enter a name for the new task: ");
        string taskName = Console.ReadLine();
        Console.Write("Enter the date for the deadline: ");
        string dueDate = Console.ReadLine();
        todoly.AddTask(projectName, taskName, Convert.ToDateTime(dueDate));
        Console.WriteLine("Task added successfully!");
        EditTaskMenu();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{RED}{ex.Message}{NORMAL}");
    }
    return;
}




