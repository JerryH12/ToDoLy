using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace ToDoListProject
{
    internal class Project
    {
        /// <summary>
        /// A collection of Tasks.
        /// </summary>
        public List<Task> tasks { get;set; }
        private List<Task> sortedTasks;
       
        public string projectName { get; set; }

        public Project(string name)
        {
            projectName = name;
            tasks = new List<Task>();
            sortedTasks = tasks;
            //SortTasksByDate();
        }

        public void SortTasksByProject()
        {
            //List<Task> sortedTasks = new List<Task>();
            sortedTasks = tasks.OrderBy(item => item.title).ToList(); // todo: sortera efter projekt.
        }

        public void SortTasksByDate()
        {
            //List<Task> sortedTasks = new List<Task>();
            sortedTasks = tasks.OrderBy(item => item.dueDate).ToList();
        }

        

        /// <summary>
        /// Counts the number of Tasks by completion
        /// </summary>
        /// <returns>int</returns>
        public int CountTasksByCompletion(bool isCompleted)
        {
            int status = isCompleted == false ? 0 : 1;
            int countedUnfinishedTasks = tasks.Where(task => task.status == status).Count();
            return countedUnfinishedTasks;
        }

        public int CountTasks()
        {
            return tasks.Count;
        }

        public void ShowTasks()
        {
            foreach (var task in sortedTasks)
            {
                Console.WriteLine("Task: " + task.title.PadRight(20) + "Due date: " + task.dueDate.PadRight(20) + "Status: " + task.status);
            }
        }

        public void EditTask(int index, string newTitle)
        {
            Task task1 = tasks.ElementAt(index);
            task1.title = newTitle;
        }

        public void addTask(string title, string date, int status)
        {
            Task task1 = new Task(title, date, status);
            tasks.Add(task1);
        }

        public void RemoveTask(Task task1)
        {
            tasks.Remove(task1);
        }

       

      
    }
}
