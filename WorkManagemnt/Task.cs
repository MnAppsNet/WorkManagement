using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        public DateTime? Deadline;

        public int ListPossition = -1;

        private bool completed = false;
        private string emailPath = "";

        public Task(string name, DateTime timestamp, Microsoft.Office.Interop.Outlook.MailItem mail = null, string description = "", List<string> attributes = null, DateTime? deadline = null)
        {
            Name = name;
            Timestamp = timestamp;
            //Mail = mail;
            Description = description;
            Attributes = attributes;
            Deadline = deadline;

            if (mail != null)
            {
                string folder = Settings.Default.WorkingDirectory + @"\Mails";
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

    }
}
