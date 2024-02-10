// Requirements
// Model a task with a task title, due date, status and project
// Display a collection of tasks that can be sorted both by date and project
//Support the ability tO add, edit, mark as done, and remove tasks
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
todoly.LoadFile("../../../projects.xml");
bool go = true;

//Console.WriteLine(todoly.Projects["projekt3"].projectName);
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
            todoly.ShowToDoList();
            break;
        case "2":
            Console.WriteLine("Enter a name for the task.");
            string taskName = Console.ReadLine();
            todoly.AddTask("projekt3", taskName);
            break;
        case "3":
            Console.WriteLine("pressed 3");
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




