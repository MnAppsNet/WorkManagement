using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using WorkManagemnt.Properties;

namespace WorkManagemnt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<Task> tasks;

        private Microsoft.Office.Interop.Outlook.Application oApp;
        private Microsoft.Office.Interop.Outlook.NameSpace oNS;
        private Microsoft.Office.Interop.Outlook.MAPIFolder oInbox;
        private string filespath;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Settings.Default.WorkingDirectory == "")
            {
                Settings.Default.WorkingDirectory = Directory.GetCurrentDirectory();
            }
            filespath = Settings.Default.WorkingDirectory;
            LoadData();

            workingDirectory.Text = Settings.Default.WorkingDirectory;
            mailCategory.Text = Settings.Default.MailTasksCategory;
            completedMailCategory.Text = Settings.Default.CompletedMailTasksCategory; //08.04.2021 - Load completed mail task category string

            mailCheck.Checked = Settings.Default.MailCheck;
            mailRefreshRate.Value = Settings.Default.RefreshRate;
            try
            {
                oApp = new Microsoft.Office.Interop.Outlook.Application();
                oNS = oApp.GetNamespace("MAPI");
                //oNS.Logon(Missing.Value, Missing.Value, false, true);
                getMailTasks(mailCategory.Text, completedMailCategory.Text);
            }
            catch
            {
                oApp = null;
                MessageBox.Show("Make sure you are connected to the internet and you have outlook open and connected to your account", "Failed To Check For Tasks", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            updateView();
        }

        private void getMailTasks(string TasksCategory, string CompletedTasksCategory)
        {
            //int max_mail_to_check = 50;
            //int i = 0;

            try
            {
                if (oApp == null)
                {
                    oApp = new Microsoft.Office.Interop.Outlook.Application();
                    oNS = oApp.GetNamespace("MAPI");
                }
            }
            catch
            {
                MessageBox.Show("Make sure you are connected to the internet and you have outlook open and connected to your account", "Failed To Check For Tasks", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            oInbox = oNS.GetDefaultFolder(OlDefaultFolders.olFolderInbox).Parent;

            List<MailItem> mailItems = new List<MailItem>(); ;

            getMails(oInbox, ref mailItems);

            bool new_items_found = false;

            mailItems.ForEach(mailItem =>
               {
                   if (mailItem == null)
                       return;
                   if (mailItem.Categories.Contains(TasksCategory))
                   {
                       if (!tasks.Exists(itm => itm.Name == mailItem.Subject && itm.Timestamp == mailItem.ReceivedTime))
                       {
                           new_items_found = true;
                           Task newTask = new Task(
                               mailItem.Subject,
                               mailItem.ReceivedTime,
                               mailItem);
                           newTask.Completed((mailItem.Categories.Contains(CompletedTasksCategory)) ? true : false); //08.04.2021 - Implement completed tasks setting effect
                           List<string> attr = new List<string>();
                           attr.Add("Requested by: " + mailItem.SenderName);
                           newTask.Attributes = attr;
                           //newTask.Description = mailItem.Body;
                           tasks.Add(newTask);
                           taskbarIcon.ShowBalloonTip(3000, "New Task Added", newTask.Name, ToolTipIcon.Info);
                           taskbarIcon.Tag = newTask;
                       }
                   }
                   //08.04.2021 - Mark completed mail tasks as completed - Begin >
                   else if (mailItem.Categories.Contains(CompletedTasksCategory))
                   {

                       Task task = tasks.Find(itm => itm.Name == mailItem.Subject && itm.Timestamp == mailItem.ReceivedTime);
                       if (task != null)
                       {
                           task.Completed(true);
                       }
                   }
                   //08.04.2021 - Mark completed mail tasks as completed - < End
               });

            if (new_items_found)
            {
                updateView();
                SaveData();
            }
        }

        private void getMails(MAPIFolder folder, ref List<MailItem> mailItems)
        {
            //Check all forlders and get mails :
            if (folder.Folders.Count == 0)
            {
                Items currentItems = folder.Items.Restrict("[Categories] = '" + Settings.Default.MailTasksCategory + "'");
                foreach( MailItem mi in currentItems)
                    mailItems.Add(mi);
            }
            else
            {
                foreach (MAPIFolder subFolder in folder.Folders)
                {
                    try
                    {
                        Items currentItems = folder.Items.Restrict("[Categories] = '" + Settings.Default.MailTasksCategory + "'");
                        foreach (MailItem mi in currentItems)
                            mailItems.Add(mi);
                    }
                    catch { }
                    getMails(subFolder, ref mailItems);
                }
            }
        }

        public void SaveData()
        {
            string file = filespath + @"\tasks.bin";
            using (Stream fl = File.Open(file, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fl, tasks);
            }
            Settings.Default.Save();
        }
        public void LoadData()
        {
            string file = filespath + @"\tasks.bin";
            if (File.Exists(file))
            {
                using (Stream fl = File.Open(file, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    object obj = bf.Deserialize(fl);
                    tasks = (List<Task>)obj;
                }
            }
            else
                tasks = new List<Task>();
        }

        private void close_settings_Click(object sender, EventArgs e)
        {
            settings_panel.Hide();
            SaveData();
        }

        private void settings_button_Click(object sender, EventArgs e)
        {
            settings_panel.Show();
        }

        private void mailCategory_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.MailTasksCategory = mailCategory.Text;
        }
        //08.042021 - Completed Mail Task Category Save Event - Start >
        private void completedMailCategory_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.CompletedMailTasksCategory = completedMailCategory.Text;
        }
        //08.042021 - Completed Mail Task Category Save Event - < End

        private void addTask_Click(object sender, EventArgs e)
        {
            Task newTask = new Task("", DateTime.Now);
            TaskInfo taskCreate = new TaskInfo(this, newTask, false);
            taskCreate.ShowDialog();
            if (newTask.Name != "")
            {
                tasks.Add(newTask);
            }
            updateView();
            SaveData();
        }

        private void updateView()
        {
            tasks.Sort((x, y) => DateTime.Compare(x.Timestamp, y.Timestamp));
            taskItems.Items.Clear();
            int i = 1;
            int completed_taks = 0;
            foreach (Task t in tasks)
            {
                string item = t.Name;
                if (t.IsCompleted)
                {
                    item = "[\u2714]  " + item;
                    completed_taks++;
                }
                else
                    item = "[__]  " + item;

                taskItems.Items.Insert(0, item);
                t.ListPossition = tasks.Count - i;
                i++;
            }
            this.Text = Settings.Default.APPLICATION_NAME + " - Tasks Completed: " + completed_taks.ToString() + ", To do: " + (tasks.Count - completed_taks);
        }

        private void taskItems_DoubleClick(object sender, EventArgs e)
        {
            if (taskItems.SelectedIndex >= 0 && taskItems.SelectedIndex < taskItems.Items.Count)
            {
                Task currentTask = tasks.Find(itm => itm.ListPossition == taskItems.SelectedIndex);
                TaskInfo taskEdit = new TaskInfo(this, currentTask, true);
                taskEdit.Show();
                updateView();
                SaveData();
            }
        }

        private void mailCheck_CheckedChanged(object sender, EventArgs e)
        {
            mailRefreshRate.Enabled = mailCheck.Checked;
            if (mailCheck.Checked)
                checkMail.Start();
            else
                checkMail.Stop();
            Settings.Default.MailCheck = mailCheck.Checked;
        }

        private void mailRefreshRate_ValueChanged(object sender, EventArgs e)
        {
            checkMail.Interval = (int)mailRefreshRate.Value * 60 * 1000;
            Settings.Default.RefreshRate = mailRefreshRate.Value;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taskItems_DoubleClick(null, null);
        }

        private void completedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taskStatus(true);
        }
        private void notCompletedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taskStatus(false);
        }
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeSelectedTask();
        }
        private void taskStatus(bool completed)
        {
            if (taskItems.SelectedIndex >= 0 && taskItems.SelectedIndex < taskItems.Items.Count)
            {
                Task currentTask = tasks.Find(itm => itm.ListPossition == taskItems.SelectedIndex);
                if (currentTask != null)
                {
                    currentTask.Completed(completed);
                    updateView();
                    SaveData();
                }
            }
        }
        private void removeSelectedTask()
        {
            if (taskItems.SelectedIndex >= 0 && taskItems.SelectedIndex < taskItems.Items.Count)
            {
                Task currentTask = tasks.Find(itm => itm.ListPossition == taskItems.SelectedIndex);
                if (currentTask != null)
                {
                    if (MessageBox.Show("Do you want to delete the task below?\nTask : " + currentTask.Name, "Delete Task ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        currentTask.DeleteMail();
                        tasks.Remove(currentTask);
                        updateView();
                        SaveData();
                    }
                }
            }
        }

        private void checkMail_Tick(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                getMailTasks(mailCategory.Text, completedMailCategory.Text);
            }
            catch
            {
                MessageBox.Show("Make sure you are connected to the internet and you have outlook open and connected to your account", "Failed To Check For Tasks", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if( e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void taskbarIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            if (taskbarIcon.Tag != null)
            {
                Task tsk = (Task)taskbarIcon.Tag;
                taskItems.SelectedIndex = tsk.ListPossition;
                taskItems_DoubleClick(null, null);
                taskbarIcon = null;
            }
        }

        private void taskbarIcon_DoubleClick(object sender, EventArgs e)
        {
            if (!this.Visible)
                this.Show();
            else
            {
                this.BringToFront();
                this.Activate();
                this.Focus();
            }
        }


        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public SendInputEventType type;
            public MouseKeybdhardwareInputUnion mkhi;
        }
        [StructLayout(LayoutKind.Explicit)]
        struct MouseKeybdhardwareInputUnion
        {
            [FieldOffset(0)]
            public MouseInputData mi;

            [FieldOffset(0)]
            public KEYBDINPUT ki;

            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }
        struct MouseInputData
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public MouseEventFlags dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [Flags]
        enum MouseEventFlags : uint
        {
            MOUSEEVENTF_MOVE = 0x0001,
            MOUSEEVENTF_LEFTDOWN = 0x0002,
            MOUSEEVENTF_LEFTUP = 0x0004,
            MOUSEEVENTF_RIGHTDOWN = 0x0008,
            MOUSEEVENTF_RIGHTUP = 0x0010,
            MOUSEEVENTF_MIDDLEDOWN = 0x0020,
            MOUSEEVENTF_MIDDLEUP = 0x0040,
            MOUSEEVENTF_XDOWN = 0x0080,
            MOUSEEVENTF_XUP = 0x0100,
            MOUSEEVENTF_WHEEL = 0x0800,
            MOUSEEVENTF_VIRTUALDESK = 0x4000,
            MOUSEEVENTF_ABSOLUTE = 0x8000
        }
        enum SendInputEventType : int
        {
            InputMouse,
            InputKeyboard,
            InputHardware
        }
        private void stayAwake_CheckedChanged(object sender, EventArgs e)
        {
            keepAwake.Enabled = stayAwake.Checked;
        }

        private void keepAwake_Tick(object sender, EventArgs e)
        {
            INPUT mouseInput = new INPUT();
            mouseInput.type = SendInputEventType.InputMouse;
            mouseInput.mkhi.mi.dx = 1;
            mouseInput.mkhi.mi.dx = 1;
            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
            mouseInput.mkhi.mi.dx = -1;
            mouseInput.mkhi.mi.dx = -1;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
        }
    }
}