using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        private TaskItem selectedItem = null; //20.07.2021 - New task items logic

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
            mailBodyInTask.Checked = Settings.Default.MailBodyInDescription; //08.04.2021 - Load mail task in description setting state

            mailCheck.Checked = Settings.Default.MailCheck;
            mailRefreshRate.Value = Settings.Default.RefreshRate;
            try
            {
                oApp = new Microsoft.Office.Interop.Outlook.Application();
                oNS = oApp.GetNamespace("MAPI");
                //oNS.Logon(Missing.Value, Missing.Value, false, true);
                getMailTasks(mailCategory.Text, completedMailCategory.Text, mailBodyInTask.Checked);
            }
            catch
            {
                oApp = null;
                //MessageBox.Show("Make sure you are connected to the internet and you have outlook open and connected to your account", "Failed To Check For Tasks", MessageBoxButtons.OK, MessageBoxIcon.Information);
                checkMail.Enabled = false;
            }
            UpdateView();
        }

        private void getMailTasks(string TasksCategory, string CompletedTasksCategory, bool MailBodyInTask)
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
                return;
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
                               mailItem,
                               description:(MailBodyInTask)?System.Text.ASCIIEncoding.ASCII.GetString(mailItem.RTFBody as byte[]):"");
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
                UpdateView();
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
        //08.04.2021 - Completed Mail Task Category Save Event - Start >
        private void completedMailCategory_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.CompletedMailTasksCategory = completedMailCategory.Text;
        }
        //08.04.2021 - Completed Mail Task Category Save Event - < End

        //08.04.2021 - Add mail body in task description setting - Start >
        private void mailBodyInTask_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.MailBodyInDescription = mailBodyInTask.Checked;
        }
        //08.04.2021 - Add mail body in task description setting - < End

        private void addTask_Click(object sender, EventArgs e)
        {
            Task newTask = new Task("", DateTime.Now);
            TaskInfo taskCreate = new TaskInfo(this, newTask, false);
            taskCreate.ShowDialog();
            if (newTask.Name != "")
            {
                tasks.Add(newTask);
            }
            UpdateView();
            SaveData();
        }

        public void UpdateView()
        {
            tasks.Sort((x, y) => DateTime.Compare(x.Timestamp, y.Timestamp));
            taskItems.Controls.Clear();
            int i = 1;
            int completed_taks = 0;
            //20.07.2021 - Update the item view handler with the new list items - Start >
            foreach (Task t in tasks)
            {
                string item = t.Name;
                TaskItem ts = new TaskItem();
                //Set default status :
                ts.status.ForeColor = Color.Orange;
                ts.status.Text = ">>";

                //Change status if not default :
                if (t.IsCompleted)
                {
                    ts.status.Text = "\u2714";
                    ts.status.ForeColor = Color.Green;
                    completed_taks++;
                }
                else if (t.Deadline != null)
                {
                    if (t.Deadline.Value < DateTime.Now)
                    {
                        ts.status.ForeColor = Color.Red;
                        ts.status.Text = "\u26A0";
                    }
                }
                
                ts.title.Text = item;
                taskItems.Controls.Add(ts);
                ts.Location = new Point(0, (tasks.Count - i) * (ts.Height + 5));
                ts.Width = taskItems.Width - 10;

                ts.MouseDown += selectItem;
                ts.title.MouseDown += selectItem;
                ts.status.MouseDown += selectItem;
                ts.DoubleClick += taskItems_DoubleClick;
                ts.title.DoubleClick += taskItems_DoubleClick;
                ts.status.DoubleClick += taskItems_DoubleClick;

                ts.Tag = taskItems.Controls.Count;
                t.ListPossition = (int)ts.Tag;
                i++;
            }
            //20.07.2021 - Update the item view handler with the new list items - < End
            this.Text = Settings.Default.APPLICATION_NAME + " - Tasks Completed: " + completed_taks.ToString() + ", To do: " + (tasks.Count - completed_taks);
        }

        //20.07.2021 - New Item List Functions - Start >
        private void selectItem(object sender, MouseEventArgs e)
        {
            deselectAllItems();
            if (sender is TaskItem)
            {
                selectedItem = (TaskItem)sender;
                selectedItem.BackColor = Color.LightGray;
            }
            else if (((Control)sender).Parent is TaskItem)
            {
                selectedItem = (TaskItem)((Control)sender).Parent;
                selectedItem.BackColor = Color.LightGray;
            }
        }
        private void deselectAllItems()
        {
            //foreach( Control c in taskItems.Controls)
            //{
            //    if (!(c is TaskItem)) continue;
            //    c.BackColor = taskItems.BackColor;
            //}
            if (selectedItem != null)
                selectedItem.BackColor = taskItems.BackColor;
            selectedItem = null;
        }
        private TaskItem selectItem(int index)
        {
            deselectAllItems();
            //foreach (Control c in taskItems.Controls)
            //{
            //    if (!(c is TaskItem)) continue;
            //    if ((int)c.Tag == index)
            //    {
            //        c.BackColor = Color.LightGray;
            //    }
            //    return (TaskItem)c;
            //}
            //return null; // < Not found
            selectedItem = (TaskItem)taskItems.Controls[index];
            selectedItem.BackColor = Color.LightGray;
            return selectedItem;
        }
        //private TaskItem selectedItem()
        //{
        //    foreach (Control c in taskItems.Controls)
        //    {
        //        if (!(c is TaskItem)) continue;
        //        if (c.BackColor == Color.LightGray)
        //            return (TaskItem)c;
        //    }
        //    return null; // < Not found
        //}
        //20.07.2021 - New Item List Functions - End <

        private void taskItems_DoubleClick(object sender, EventArgs e)
        {
            //20.07.2021 - New task items logic - Start >
            if (selectedItem != null)
            {
                Task currentTask = tasks.Find(itm => itm.ListPossition == (int)selectedItem.Tag);  //20.07.2021 - New task items logic - End <
                TaskInfo taskEdit = new TaskInfo(this, currentTask, true);
                taskEdit.Show();
                //UpdateView();
                //SaveData();
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
            //20.07.2021 - New task items logic - Start >
            if (selectedItem != null)
            {
                Task currentTask = tasks.Find(itm => itm.ListPossition == (int)selectedItem.Tag); //20.07.2021 - New task items logic - End <
                if (currentTask != null)
                {
                    currentTask.Completed(completed);
                    UpdateView();
                    SaveData();
                }
            }
        }
        private void removeSelectedTask()
        {
            //20.07.2021 - New task items logic - Start >
            if (selectedItem != null)
            {
                Task currentTask = tasks.Find(itm => itm.ListPossition == (int)selectedItem.Tag); //20.07.2021 - New task items logic - End <
                if (currentTask != null)
                {
                    if (MessageBox.Show("Do you want to delete the task below?\nTask : " + currentTask.Name, "Delete Task ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        currentTask.DeleteMail();
                        tasks.Remove(currentTask);
                        UpdateView();
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
                getMailTasks(mailCategory.Text, completedMailCategory.Text, mailBodyInTask.Checked);
                taskbarIcon.ShowBalloonTip(1500, "Mail tasks checked", " ", ToolTipIcon.Info);
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

                selectItem(tsk.ListPossition); //20.07.2021 - New task items logic

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