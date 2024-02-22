namespace ToDoLy
{
    internal class Task
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public int Status { get; set; }

        public string StatusText {  get; set; }

        public Task()
        {
            Title = "";
            DueDate = new DateTime();
            Status = 0;
        }

        public Task(string title, DateTime dueDate, int status = 0)
        {
            Title = title;
            DueDate = dueDate;
            Status = status;
        }

        public void MarkAsDone()
        {
            Status = 1;
        }
  
        public void Print()
        {
            // Unfinished tasks will be yellow.
            StatusText = (Status == 1) ? "\u001b[92mFinished\u001b[39m" : "\u001b[93mUnfinished\u001b[39m";

            TimeSpan span = DueDate.Subtract(DateTime.Now);
            
            // Indicate deadlines close to or past due by making them red.
            if ((span.Days < 2) && Status == 0)
            {
                StatusText = "\u001b[91mUnfinished\u001b[39m";
            }

            Console.WriteLine("Task: " + Title.PadRight(20) + "Due date: " + DueDate.ToString("yy-MM-dd").PadRight(20) + "Status: " + StatusText);
        }
    }
}
