using System.Xml.Linq;

namespace ToDoLy
{
    internal class ToDoList
    {
       ///<summary>
       /// A collection of Projects.
        ///</summary>
        public Dictionary<string, Project> Projects { get; }
        public Dictionary<string,Project> SortedProjects { get; set; }

        public Project SelectedProject { get; set; }

        public ToDoList() 
        {
            Projects = [];

            LoadFile("projects.xml");
            SetCurrentProject("Work"); // TODO: remember from previous settings.
            SortTasksByDateAscending(); // default sorting method.
        }

        /// <summary>
        /// Select the project to work on.
        /// </summary>
        /// <param name="projectName"></param>
        public void SetCurrentProject(string projectName)
        {
            if (Projects.ContainsKey(projectName))
            {
                SelectedProject = Projects[projectName];
            }
        }

        /// <summary>
        /// Add a new project.
        /// </summary>
        public void AddProject(string projectName)
        {
            try
            {
                if (!Projects.ContainsKey(projectName))
                {
                    Projects.Add(projectName, new Project(projectName));
                    SelectedProject = Projects[projectName];

                    // Add a dummy task to be edited later
                    SelectedProject.tasks.Add(new Task());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Count finished tasks.
        /// </summary>
        /// <returns>Number of finished tasks</returns>
        public int CountFinishedTasks()
        {
            int sum = 0;

            foreach (KeyValuePair<string, Project> item in Projects)
            {
                sum += item.Value.CountTasksByCompletion(true);
            }
            return sum;
        }

        /// <summary>
        /// Count unfinished tasks.
        /// </summary>
        /// <returns>Number of finished tasks</returns>
        public int CountUnfinishedTasks()
        {
            int sum = 0;

            foreach(KeyValuePair<string, Project> item in Projects)
            {
                sum += item.Value.CountTasksByCompletion(false);
            }
            return sum;
        }

        /// <summary>
        /// Add task.
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="taskName"></param>
        /// <param name="date"></param>
        /// <param name="status"></param>
        public void AddTask(string projectName, string taskName, DateTime date, int status=0)
        {
            if (Projects.ContainsKey(projectName))
            {
                Projects[projectName].addTask(taskName, date, status);
            }
            else
            {
                Project project1 = new Project(projectName);
                project1.addTask(taskName, date, status);
                Projects.Add(projectName, project1);
            }
        }

        /// <summary>
        /// Remove a task.
        /// </summary>
        /// <returns>True if removing the task was successful.</returns>
        /// <param name="task1"></param>
        public bool RemoveTask(Task task1)
        {
            try
            {
                if (Projects.ContainsKey(SelectedProject.projectName))
                {
                    Projects[SelectedProject.projectName].RemoveTask(task1);

                    // If all tasks are deleted, delete the project.
                    if(Projects[SelectedProject.projectName].tasks.Count == 0)
                    {
                        Projects.Remove(SelectedProject.projectName);
                        SetCurrentProject("Work"); // Set the default project.
                    }

                    return true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Edit a task.
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="taskIndex"></param>
        /// <param name="newTitle"></param>
        public void EditTask(string projectName, int taskIndex, string newTitle)
        {
            Projects[projectName].EditTask(taskIndex, newTitle);
        }

        /// <summary>
        /// Sort tasks by the project name.
        /// </summary>
        public void SortTasksByProject()
        {
            // Sort dictionary by the project name.
            SortedProjects = Projects.OrderBy(item=> item.Value.projectName).ToDictionary(); 
        }

        /// <summary>
        /// Sort tasks by date ascending
        /// </summary>
        public void SortTasksByDateAscending()
        {
            // Sort each project task list by date
            foreach (KeyValuePair<string, Project> projectItem in Projects)
            {
                projectItem.Value.SortTasksByDateAscending();
            }

            // Sort each project according to the date of the first task
            SortedProjects = Projects.OrderBy(item => item.Value.sortedTasks[0].DueDate).ToDictionary();
        }

        /// <summary>
        /// Sort tasks by date descending.
        /// </summary>
        public void SortTasksByDateDescending()
        {
            // Sort each project task list by date
            foreach (KeyValuePair<string, Project> projectItem in Projects)
            {
                projectItem.Value.SortTasksByDateDescending();
            }

            // Sort each project according to the date of the first task
            SortedProjects = Projects.OrderByDescending(item => item.Value.sortedTasks[0].DueDate).ToDictionary();

        }

        /// <summary>
        /// Shows an entire list of projects and tasks.
        /// </summary>
        public void ShowToDoList()
        {
            Console.WriteLine("To-Do List");
            Console.WriteLine("----------");
            foreach (KeyValuePair<string, Project> projectItem in SortedProjects)
            {
                Console.WriteLine("" + projectItem.Value.projectName);
                projectItem.Value.ShowTasks();
                Console.WriteLine("---------------------------------------------------------------------------");
            }
        }

        /// <summary>
        /// Loads an XML file and maps it to objects.
        /// </summary>
        /// <param name="name"></param>
        public void LoadFile(string filename)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "projects.xml");
            XElement XMLdocument = XElement.Load(filePath);

            IEnumerable<XElement> elements = from item in XMLdocument.Descendants("project")
                                             select item;

            foreach (XElement element in elements)
            {
                string projectName = (string)element.Attribute("name");
                Project project1 = new Project(projectName);

                foreach (XElement child in element.Elements("task"))
                {
                    string title = (string)child.Attribute("title");
                    string date = ((string)child.Element("date").Value);
                    int status = int.Parse((string)child.Element("status").Value);
                    project1.addTask(title, Convert.ToDateTime(date), status);
                   
                }

                Projects.Add(project1.projectName, project1);
            }
        }

        /// <summary>
        /// Maps objects and saves it as an XM file.
        /// </summary>
        public void SaveToFile()
        {
            XElement XMLdocument = new XElement("projects");

            foreach (KeyValuePair<string, Project> projectItem in Projects)
            {
                XElement element = new XElement("project");
                element.SetAttributeValue("name", projectItem.Value.projectName);

                foreach(Task taskItem in projectItem.Value.tasks)
                {
                    string title = (string)taskItem.Title;
                    int status = (int)taskItem.Status;

                    XElement childElement = new XElement("task",
                        new XElement("date", taskItem.DueDate.ToString("yy-MM-dd")),
                        new XElement("status", taskItem.Status.ToString()));

                    childElement.SetAttributeValue("title", taskItem.Title);
                    element.Add(childElement);
                }
                XMLdocument.Add(element);
            }

            string filePath = Path.Combine(Environment.CurrentDirectory, "projects.xml");
            XMLdocument.Save(filePath);
        }
    }
}
