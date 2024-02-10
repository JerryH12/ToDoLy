using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoListProject
{
    internal class ToDoList
    {
       ///<summary>
       /// A collection of Projects.
        ///</summary>
        public Dictionary<string, Project> Projects { get; }

        public ToDoList() 
        {
            Projects = [];
        }

        public int CountFinishedTasks()
        {
            int sum = 0;

            foreach (KeyValuePair<string, Project> item in Projects)
            {
                sum += item.Value.CountTasksByCompletion(true);
            }
            return sum;
        }
        public int CountUnfinishedTasks()
        {
            int sum = 0;

            foreach(KeyValuePair<string, Project> item in Projects)
            {
                sum += item.Value.CountTasksByCompletion(false);
            }
            return sum;
        }

        public void AddTask(string projectName, string taskName, string date="31/12", int status=0)
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

        public void RemoveTask(string projectName, string task)
        {
            // ToDo
            if (Projects.ContainsKey(projectName))
            {
                //Projects[projectName].RemoveTask();
            }
           
        }

        /// <summary>
        /// Shows an entire list of Projects and Tasks.
        /// </summary>
        public void ShowToDoList()
        {
            foreach(KeyValuePair<string, Project> projectItem in Projects)
            {
                Console.WriteLine("\nProject: " + projectItem.Value.projectName);
                projectItem.Value.ShowTasks();
                Console.WriteLine("------------------------------------------------------------------------");
            }
        }

        /// <summary>
        /// Loads an XML file and maps it to objects.
        /// </summary>
        /// <param name="name"></param>
        public void LoadFile(string name)
        { 
            string filename = name;
           // string path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
           // Console.Write(path);
           // var projectsFilePath = Path.Combine(path, filename);
            XElement XMLdocument = XElement.Load(name);

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
                    project1.addTask(title, date, status);
                   
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
                    string title = (string)taskItem.title;
                    int status = (int)taskItem.status;

                    XElement childElement = new XElement("task",
                        new XElement("date", taskItem.dueDate),
                        new XElement("status", taskItem.status.ToString()));

                    childElement.SetAttributeValue("title", taskItem.title);
                    element.Add(childElement);
                }
                XMLdocument.Add(element);
            }
            XMLdocument.Save("nexxml.xml");
        }
    }
}
