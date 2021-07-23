using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WorkManagemnt.Properties;

namespace WorkManagemnt
{
    [Serializable]
    public class Task
    {
        public bool IsCompleted { get { return completed; } }
        public bool HasMail { get { return ((emailPath == "")?false:true); } }
        public string Name; //(*)
        public DateTime Timestamp; //(*)
        //public Microsoft.Office.Interop.Outlook.MailItem Mail;
        public string Description;
        public List<string> Attributes;
        public List<string[]> Subtasks; //06.04.2021 - Subtasks added
        public DateTime? Deadline;

        public int ListPossition = -1;

        private bool completed = false;
        private string emailPath = "";

        public Task(string name, DateTime timestamp, Microsoft.Office.Interop.Outlook.MailItem mail = null, string description = "", List<string> attributes = null, DateTime? deadline = null, List<string[]> subtasks = null)
        {
            Name = name;
            Timestamp = timestamp;
            //Mail = mail;
            Description = description;
            Attributes = attributes;
            Deadline = deadline;
            Subtasks = subtasks; //06.04.2021 - Subtasks added

            if (mail != null)
            {
                string folder = Properties.Settings.Default.WorkingDirectory + @"\Mails";
                if (!System.IO.Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string filename = new string((name + "_" + timestamp.ToString("yyyyMMddHHmmss") + ".msg").Select(c => System.IO.Path.GetInvalidFileNameChars().Contains(c) ? '_' : c).ToArray());
                string fullpath = folder + @"\" + filename;
                if (!File.Exists(fullpath))
                {
                    mail.SaveAs(fullpath);
                }
                emailPath = fullpath;
            }
        }

        public void Completed(bool flag) { completed = flag; }
        public void DisplayMail()
        {
            System.Diagnostics.Process.Start(emailPath);
        }
        //08.04.2021 - Added Mail Deletion On Task Removal - Start >
        public void DeleteMail()
        {
            if (emailPath == "") return;
            try
            {
                File.Delete(emailPath);
                emailPath = "";
            }
            catch { }
        }
        //08.04.2021 - Added Mail Deletion On Task Removal - < End

        //22.07.2021 - Get Task Status - Start > 
        public string GetStatus()
        {
            if (completed) return Settings.Status.Completed;
            if (Deadline != null) if (Deadline.Value < DateTime.Now) return Settings.Status.Delayed;
            return Settings.Status.Active;
        }
        //22.07.2021 - Get Task Status - < End

    }
}
