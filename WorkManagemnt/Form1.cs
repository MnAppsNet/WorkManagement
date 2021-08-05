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
        private int vissibleItems = 0;

        private TaskItem selectedItem = null; //20.07.2021 - New task items logic
        private int page = 1; //22.07.2021 - New item pages logic

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.WorkingDirectory == "")
            {
                Properties.Settings.Default.WorkingDirectory = Directory.GetCurrentDirectory();
            }
            filespath = Properties.Settings.Default.WorkingDirectory;
            LoadData();
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
            List<MailItem> mailItems = new List<MailItem>();

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
                           try
                           {
                               taskbarIcon.ShowBalloonTip(3000, "New Task Added", newTask.Name, ToolTipIcon.Info);
                               taskbarIcon.Tag = newTask;
                           }
                           catch { }
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
                UpdateView(searchBox.Text,searchStatus.SelectedItem.ToString().Substring(0,1));
                SaveData();
            }
        }

        private void getMails(MAPIFolder folder, ref List<MailItem> mailItems)
        {
            string parameters = "[Categories] = '" + Properties.Settings.Default.MailTasksCategory + "'";

            //Check all forlders and get mails :
            if (folder.Folders.Count == 0)
            {
                Items currentItems = folder.Items.Restrict(parameters);
                foreach( MailItem mi in currentItems)
                    if (mi.ReceivedTime > DateTime.Now.AddDays(-1 * Properties.Settings.Default.MailFromDays))
                        mailItems.Add(mi);
            }
            else
            {
                foreach (MAPIFolder subFolder in folder.Folders)
                {
                    try
                    {
                        Items currentItems = folder.Items.Restrict(parameters);
                        foreach (MailItem mi in currentItems)
                            if (mi.ReceivedTime > DateTime.Now.AddDays(-1 * Properties.Settings.Default.MailFromDays))
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
            Properties.Settings.Default.Save();
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

            //Load Settings :
            workingDirectory.Text = Properties.Settings.Default.WorkingDirectory;
            mailCategory.Text = Properties.Settings.Default.MailTasksCategory;
            completedMailCategory.Text = Properties.Settings.Default.CompletedMailTasksCategory; //08.04.2021 - Load completed mail task category string
            mailBodyInTask.Checked = Properties.Settings.Default.MailBodyInDescription; //08.04.2021 - Load mail task in description setting state
            mailCheck.Checked = Properties.Settings.Default.MailCheck;
            mailRefreshRate.Value = Properties.Settings.Default.RefreshRate;
            mailsFromDays.Value = Properties.Settings.Default.MailFromDays;
            //21.07.2021 - Add possible status search terms - Start >
            searchStatus.Items.Add(Settings.Strings.SearchAllStatus);
            foreach (System.Reflection.FieldInfo p in typeof(Settings.Status).GetFields())
            {
                searchStatus.Items.Add(p.GetValue(null) + " " + p.Name);
            }
            searchStatus.SelectedIndex = 0;
            tasksPerPage.Value = Settings.General.ItemsPerPage;
            //21.07.2021 - Add possible status search terms - < End
            Settings.General.ItemsPerPage = (UInt16)Properties.Settings.Default.TasksPerPage; //22.07.2021 - Paging functionality, set tasks per page setting
            tasksPerPage.Value = Settings.General.ItemsPerPage;
        }

        private void close_settings_Click(object sender, EventArgs e)
        {
            settings_panel.Hide();
            SaveData();
        }

        private void settings_button_Click(object sender, EventArgs e)
        {
            settings_panel.Visible = !settings_panel.Visible;
        }

        private void mailCategory_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MailTasksCategory = mailCategory.Text;
        }
        //08.04.2021 - Completed Mail Task Category Save Event - Start >
        private void completedMailCategory_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CompletedMailTasksCategory = completedMailCategory.Text;
        }
        //08.04.2021 - Completed Mail Task Category Save Event - < End

        //08.04.2021 - Add mail body in task description setting - Start >
        private void mailBodyInTask_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MailBodyInDescription = mailBodyInTask.Checked;
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
            searchStatus.Text = Settings.Strings.SearchBoxPlaceholder;
            searchStatus.SelectedIndex = 0;
            searchStatus.Text = Settings.Strings.SearchAllStatus;
            SaveData();
        }

        public void UpdateView(string searchTerm = "", string status = "")
        {
            tasks.Sort((x, y) => DateTime.Compare(x.Timestamp, y.Timestamp));
            taskItems.Controls.Clear();
            taskItems.BackColor = Settings.Colors.TasksPanelBackground;
            int i = 0;
            int j = 0;
            int completed_taks = 0;
            if (searchTerm == Settings.Strings.SearchBoxPlaceholder) searchTerm = ""; //22.07.2021 - Search functionality
            if (status != "") if (status == Settings.Strings.SearchAllStatus) status = ""; else status = status.Substring(0, 1); //22.07.2021 - Search functionality

            vissibleItems = 0;
            Task t;
            string itemStatus = "";
            for (int k = tasks.Count - 1; k >= 0; k--)
            {
                t = tasks[k];
                j++;
                if (j < (page - 1)  * Settings.General.ItemsPerPage) continue; //22.07.2021 - Skip items from previous pages
                if (i == Settings.General.ItemsPerPage) break; //22.07.2021 - Maximum limit per page is reached
                
                if (t.Name != null)
                {
                    if (searchTerm != "" && !t.Name.ToLower().Contains(searchTerm.ToLower())) continue; //If doesn't contain search term, skip item
                    //05.08.2021 - Active status shows everything except completed - Start >
                    itemStatus = t.GetStatus();
                    if (status != "") {
                        if (status == Settings.Status.Active)
                        {
                            if (itemStatus == Settings.Status.Completed) continue;
                        } else if ( !(itemStatus == status) ) continue;  //if doesn't contain status, skip item
                    }
                    //05.08.2021 - Active status shows everything except completed - < End
                }
                string item = t.Name;
                TaskItem ts = new TaskItem();
                //Set default status :
                setStatus(ts, t.GetStatus());

                //Change status if not default :
                if (t.IsCompleted)
                    completed_taks++;

                ts.title.Text = item;
                taskItems.Controls.Add(ts);
                ts.Height = Settings.Sizes.ItemHeight;
                ts.Location = new Point(0, i * (ts.Height + Settings.Margins.ItemsBottom));
                ts.Width = taskItems.Width - 10;

                ts.MouseDown += selectItem;
                ts.title.MouseDown += selectItem;
                ts.status.MouseDown += selectItem;
                ts.DoubleClick += taskItems_DoubleClick;
                ts.title.DoubleClick += taskItems_DoubleClick;
                ts.status.DoubleClick += taskItems_DoubleClick;
                ts.BackColor = Settings.Colors.TaskItemsBackground;

                ts.ContextMenuStrip = taskOptions;
                ts.title.ContextMenuStrip = taskOptions;
                ts.status.ContextMenuStrip = taskOptions;

                ts.Tag = t; //21.07.2021 - Set the task item on the Tag of task view item for easy finding
                t.ListPossition = taskItems.Controls.Count;
                i++;
            }
            vissibleItems = i;
            //22.07.2021 - Handle task pages - Start >
            if (i == Settings.General.ItemsPerPage && i > 0 || page > 1)
            {
                Button previousButton = new Button();
                previousButton.BackColor = taskItems.BackColor;
                previousButton.Width = taskItems.Width / 2;
                previousButton.Height = Settings.Sizes.ItemHeight;
                previousButton.TextAlign = ContentAlignment.MiddleCenter;
                previousButton.Click += NextPage_Click;
                
                if (page * Settings.General.ItemsPerPage < tasks.Count)
                {
                    Button nextButton = new Button();
                    taskItems.Controls.Add(nextButton);
                    nextButton.BackColor = previousButton.BackColor;
                    nextButton.Width = previousButton.Width;
                    nextButton.Height = previousButton.Height;
                    nextButton.TextAlign = previousButton.TextAlign;
                    nextButton.Click += NextPage_Click;
                    nextButton.Location = new Point(nextButton.Width, (vissibleItems) * (previousButton.Height + Settings.Margins.ItemsBottom));
                    nextButton.Text = Settings.Strings.NextPage;
                    nextButton.BringToFront();
                }
                if (page > 1)
                {
                    taskItems.Controls.Add(previousButton);
                    previousButton.Location = new Point(0, (vissibleItems) * (previousButton.Height + Settings.Margins.ItemsBottom));
                    previousButton.Text = Settings.Strings.PreviousPage;
                }
                else { previousButton.Dispose(); }
            }
            //22.07.2021 - Handle task pages - < End

            //20.07.2021 - Update the item view handler with the new list items - < End
            this.Text = Properties.Settings.Default.APPLICATION_NAME + " - Tasks Completed: " + completed_taks.ToString() + ", To do: " + (vissibleItems - completed_taks) + " | Total Tasks : " + tasks.Count;
        }

        //22.07.2021 - Go to next page - Start >
        private void NextPage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == Settings.Strings.NextPage)
                page++;
            else
                page--;
            UpdateView(searchBox.Text, searchStatus.SelectedItem.ToString());
        }
        //22.07.2021 - Go to next page - < End

        //21.07.2021 - Resize items in task item list - Start >
        private void resizeItems()
        {
            foreach (Control c in taskItems.Controls)
            {
                if (!(c is TaskItem)) continue;
                c.Width = taskItems.Width + Settings.Margins.ItemsRight;
            }
        }
        //21.07.2021 - Resize items in task item list - < End

        //20.07.2021 - New Item List Functions - Start >
        private void setStatus(TaskItem task, string status)
        {
            task.status.Text = status;
            Color taskColor = Settings.Colors.Default;
            switch (status) {
                case Settings.Status.Active: taskColor = Settings.Colors.ActiveTask; break; 
                case Settings.Status.Completed: taskColor = Settings.Colors.CompletedTask; break; 
                case Settings.Status.Delayed: taskColor = Settings.Colors.DelayedTask; break; 
            }
            task.status.ForeColor = taskColor;
        }
        private void selectItem(object sender, MouseEventArgs e)
        {
            deselectAllItems();
            if (sender is TaskItem)
            {
                selectedItem = (TaskItem)sender;
                selectedItem.BackColor = Settings.Colors.SelectedItemBackground;
            }
            else if (((Control)sender).Parent is TaskItem)
            {
                selectedItem = (TaskItem)((Control)sender).Parent;
                selectedItem.BackColor = Settings.Colors.SelectedItemBackground;
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
            //        c.BackColor = Settings.SelectedItemBackground;
            //    }
            //    return (TaskItem)c;
            //}
            //return null; // < Not found
            selectedItem = (TaskItem)taskItems.Controls[index];
            selectedItem.BackColor = Settings.Colors.SelectedItemBackground;
            return selectedItem;
        }
        //private TaskItem selectedItem()
        //{
        //    foreach (Control c in taskItems.Controls)
        //    {
        //        if (!(c is TaskItem)) continue;
        //        if (c.BackColor == Settings.SelectedItemBackground)
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
                Task currentTask = (Task)selectedItem.Tag;  //20.07.2021 - New task items logic - End <
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
            Properties.Settings.Default.MailCheck = mailCheck.Checked;
        }

        private void mailRefreshRate_ValueChanged(object sender, EventArgs e)
        {
            checkMail.Interval = (int)mailRefreshRate.Value * 60 * 1000;
            Properties.Settings.Default.RefreshRate = mailRefreshRate.Value;
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
                Task currentTask = (Task)selectedItem.Tag; //20.07.2021 - New task items logic - End <
                if (currentTask != null)
                {
                    currentTask.Completed(completed);
                    setStatus(selectedItem, (completed)?Settings.Status.Completed :Settings.Status.Active); //21.07.2021 - Performance improvements
                    SaveData();
                }
            }
        }
        private void removeSelectedTask()
        {
            //20.07.2021 - New task items logic - Start >
            if (selectedItem != null)
            {
                Task currentTask = (Task)selectedItem.Tag; //20.07.2021 - New task items logic - End <
                if (currentTask != null)
                {
                    if (MessageBox.Show("Do you want to delete the task below?\nTask : " + currentTask.Name, "Delete Task ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        currentTask.DeleteMail();
                        tasks.Remove(currentTask);
                        UpdateView(searchBox.Text, searchStatus.SelectedItem.ToString());
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
            //try
            //{
                getMailTasks(mailCategory.Text, completedMailCategory.Text, mailBodyInTask.Checked);
                taskbarIcon.ShowBalloonTip(1500, "Mail tasks checked", " ", ToolTipIcon.Info);
            //}
            //catch
            //{
            //   MessageBox.Show("Make sure you are connected to the internet and you have outlook open and connected to your account", "Failed To Check For Tasks", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
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
        //21.07.2021 - Search functionality - Start >
        private void searchBox_Leave(object sender, EventArgs e)
        {
            if (searchBox.Text == "")
            {
                searchBox.Text = Settings.Strings.SearchBoxPlaceholder;
            }
        }

        private void searchBox_Enter(object sender, EventArgs e)
        {
            if (searchBox.Text == Settings.Strings.SearchBoxPlaceholder)
                searchBox.Text = "";
        }

        private void searchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateView(searchBox.Text, searchStatus.SelectedItem.ToString());
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            resizeItems();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Focus();
            searchBox.Text = Settings.Strings.SearchBoxPlaceholder;
            if (tasks.Count != vissibleItems)
                UpdateView("", searchStatus.SelectedItem.ToString());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (searchStatus.SelectedIndex == -1) searchStatus.SelectedIndex = 0;
            if (searchStatus.Focused) {
                if (searchStatus.SelectedIndex == 0)
                {
                    if (vissibleItems != tasks.Count)
                        UpdateView(searchBox.Text, "");
                }
                else
                {
                    UpdateView(searchBox.Text, searchStatus.SelectedItem.ToString());
                }
            }
        }

        private void tasksPerPage_ValueChanged(object sender, EventArgs e)
        {
            Settings.General.ItemsPerPage = (UInt16)tasksPerPage.Value;
            Properties.Settings.Default.TasksPerPage = Settings.General.ItemsPerPage;
            Properties.Settings.Default.Save();
        }

        private void settings_panel_VisibleChanged(object sender, EventArgs e)
        {
            if (!settings_panel.Visible)
            {
                UpdateView(searchBox.Text, searchStatus.Text);
            }
        }

        private void mailsFromDays_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MailFromDays = (int)mailsFromDays.Value;
        }
        //21.07.2021 - Search functionality - < End
    }
}