using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ToDoLy
{
    internal class Task
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public int Status { get; set; }

        public string StatusText {  get; set; }

        public Task(string title, DateTime dueDate, int status = 0)
        {
            Title = title;
            DueDate = dueDate;
            Status = status;
        }

        public void markAsDone()
        {
            //ToDo
        }

        public void Print()
        {
            StatusText = (Status == 1) ? "\u001b[92mFinished\u001b[39m" : "\u001b[93mUnfinished\u001b[39m";
            Console.WriteLine("Task: " + Title.PadRight(20) + "Due date: " + DueDate.ToString("yy-MM-dd").PadRight(20) + "Status: " + StatusText);
        }
    }
}
