using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListProject
{
    internal class Task
    {
        public string title { get; set; }
        public string dueDate { get; set; }
        public int status { get; set; }

        public Task(string title) 
        {
            this.title = title;
        }

        public Task(string title, string dueDate, int status)
        {
            this.title = title;
            this.dueDate = dueDate;
            this.status = status;
        }

        public void add()
        {
            //ToDo
        }

        public void remove()
        {
            //ToDo
        }

        public void edit()
        {
            //ToDo
        }

        public void markAsDone()
        {
            //ToDo
        }
    }
}
