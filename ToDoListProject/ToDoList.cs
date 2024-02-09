using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ToDoListProject
{
    internal class ToDoList
    {
       ///<summary>
       ///<para>A collection of Projects</para>
        ///</summary>
        public List<Project> Projects { get; }

        public ToDoList() 
        {
            Projects = new List<Project>();
        }

        public int CountTasks()
        {
            int sum = 0;

            foreach(var item in Projects)
            {
                sum += item.CountTasks();
            }
            return sum;
        }

        public void RemoveTask(string projectName, string task)
        {

        }

        public void ShowToDoList()
        {
            foreach(Project projectItem in Projects)
            {
                Console.WriteLine("\nProject: " + projectItem.projectName);
                projectItem.ShowTasks();
                Console.WriteLine("------------------------------------------------------------------------");
            }
        }

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

                Projects.Add(project1);
            }
        }
        public void SaveToFile()
        {
            XElement XMLdocument = new XElement("projects");

            foreach (Project projectItem in Projects)
            {
                XElement element = new XElement("project");
                element.SetAttributeValue("name", projectItem.projectName);

                foreach(Task taskItem in projectItem.tasks)
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
