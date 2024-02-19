using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace ToDoLy
{
    internal class Project
    {
        /// <summary>
        /// A collection of Tasks.
        /// </summary>
        public List<Task> tasks { get;set; }
        public List<Task> sortedTasks { get; set; }
       
        public string projectName { get; set; }

        public Project(string name)
        {
            projectName = name;
            tasks = new List<Task>();
            sortedTasks = tasks;
        }

        public void SortTasksByDateAscending()
        {
            sortedTasks = tasks.OrderBy(item => item.DueDate).ToList();
        }

        public void SortTasksByDateDescending()
        {
            sortedTasks = tasks.OrderByDescending(item => item.DueDate).ToList();
        }

        /// <summary>
        /// Counts the number of Tasks by completion
        /// </summary>
        /// <returns>int</returns>
        public int CountTasksByCompletion(bool isCompleted)
        {
            int status = isCompleted == false ? 0 : 1;
            int countedUnfinishedTasks = tasks.Where(task => task.Status == status).Count();
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
               task.Print();
            }
        }

        public void EditTask(int index, string newTitle)
        {
            Task task1 = tasks.ElementAt(index);
            task1.Title = newTitle;
        }

        public void addTask(string title, DateTime date, int status)
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
