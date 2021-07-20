using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WorkManagemnt
{
    public partial class TaskInfo : Form
    {
        private Task currentTask;
        private bool editMode;
        private Form1 controller;
        private CheckBox selectedSubtask; //06.04.2021 - Added selected subtask variable

        public TaskInfo(Form1 Controller, Task task, bool editmode)
        {
            InitializeComponent();
            if (task == null)
            {
                this.Close();
                return;
            }
            currentTask = task;
            editMode = editmode;
            controller = Controller;
        }

        private void taskCompleted_CheckedChanged(object sender, EventArgs e)
        {
            if (taskCompleted.Checked)
            {
                taskCompleted.ForeColor = Color.Green;
                taskCompleted.Text = "Completed";
            }
            else
            {
                taskCompleted.ForeColor = Color.Red;
                taskCompleted.Text = "Not Completed";
            }
        }

        private void TaskInfo_Load(object sender, EventArgs e)
        {

            if (editMode)
            {
                taskCompleted.Checked = currentTask.IsCompleted;
                taskName.Text = currentTask.Name;
                taskDate.Text = currentTask.Timestamp.ToString("G");
                if (currentTask.Deadline != null)
                {
                    deadline_label.Show();
                    deadline.Show();
                    deadlineCheckbox.Checked = true;
                    deadline.Value = (DateTime)currentTask.Deadline;
                }
                else
                {
                    deadlineCheckbox.Checked = false;
                }
                try
                {
                    taskDescription.Rtf = currentTask.Description;
                }
                catch { taskDescription.Text = currentTask.Description; }
                if (currentTask.Attributes != null)
                {
                    foreach (string s in currentTask.Attributes)
                    {
                        taskAttributes.Items.Add(s);
                    }
                }
                //06.04.2021 - Subtasks handling. Start >
                if (currentTask.Subtasks != null)
                {
                    //Add existing subtasks
                    foreach (string[] s in currentTask.Subtasks)
                    {
                        if (s.Length < 2)
                        {
                            continue;
                        }
                        add_sub_task(s[0], (s[1].Replace(" ","") == "" || s[1] == "false") ? false : true);
                    }
                    //Arrange subtasks in the list :
                    arrange_subtasks();
                }
                //06.04.2021 - Subtasks handling. < End
                if (currentTask.HasMail)
                    taskMail.Show();
                else
                    taskMail.Hide();
            }
            else
            {
                deadline_label.Show();
                deadline.Show();
                taskMail.Hide();
                taskName.ReadOnly = false;
                taskDate.Tag = DateTime.Now;
                taskDate.Text = DateTime.Now.ToString("G");
                deadline.Value = DateTime.Now.AddDays(7);
                deadlineCheckbox.Checked = true;
            }
            if (deadlineCheckbox.Checked)
            {
                if (deadline.Value < DateTime.Now)
                {
                    deadline_label.ForeColor = Color.Red;
                }
            }
        }

        //06.04.2021 - Add subtask functionalities - Start >
        private void add_sub_task(string subtask, bool completed = false)
        {
            CheckBox cb = new CheckBox();
            cb.Name = "SubTask_" + subtasksList.Controls.Count.ToString(); //Set control name
            cb.Checked = completed; //Set state
            cb.AutoSize = false; //Remove autosizing feture
            cb.Font = new Font(FontFamily.GenericMonospace, 10); //Set a monospaced font
            //Add subtask text
            cb.Text = subtask;
            //Add subtask to subtasksList controls
            subtasksList.Controls.Add(cb);
            //Add subtask selection event
            cb.MouseDown += onSubtaskClick;
            //Handle subtask control size :
            resize_subtask_control(cb);
        }
        private void resize_subtasks()
        {
            for (int i = 0; i < subtasksList.Controls.Count; i++)
            {
                if (!(subtasksList.Controls[i] is CheckBox)) continue;
                resize_subtask_control((CheckBox)subtasksList.Controls[i]);
            }
        }
        private void resize_subtask_control(CheckBox cb)
        {
            SizeF charSize = get_font_char_size(cb.Font, ' '); //Monospace font all chars has the same size
            int availableArea = subtasksList.Width - 20 - 35; //Checkbox consumes ~35px and ~20px consumes the scrollbar
            int charsPerLine = (int)Math.Floor(availableArea / charSize.Width);
            charsPerLine -= (charsPerLine > 10) ? 5 : 0;
            int lines = (int)Math.Ceiling(cb.Text.Length * 1.0 / charsPerLine) + 1;
            cb.Size = new Size(subtasksList.Width - 20, lines * ((int)(charSize.Height + 10))); //20 px is reserved for the scroller
        }
        private void arrange_subtasks()
        {
            //Reset scrollbar possition :
            subtasksList.AutoScrollPosition = new Point(subtasksList.AutoScrollPosition.X, 0);
            subtasksList.VerticalScroll.Value = 0;
            subtasksList.Refresh();

            int currentY = 0;
            int offsetY = 5;
            Control c;
            for (int i = subtasksList.Controls.Count - 1; i >= 0; i--)
            {
                c = subtasksList.Controls[i];
                if (!(c is CheckBox)) continue;
                c.Location = new Point(0, currentY);
                currentY += c.Height + offsetY;
            }
        }
        private SizeF get_font_char_size(Font font, char character)
        {
            Image fakeImage = new Bitmap(1, 1); 
            Graphics graphics = Graphics.FromImage(fakeImage);
            SizeF size = graphics.MeasureString(character.ToString(), font);
            fakeImage.Dispose();
            return size;
        }
        //06.04.2021 - Add subtask functionalities - < End

        private void remove_attribute_Click(object sender, EventArgs e)
        {
            if (taskAttributes.SelectedIndex >= 0 && taskAttributes.SelectedIndex < taskAttributes.Items.Count)
            {
                taskAttributes.Items.RemoveAt(taskAttributes.SelectedIndex);
            }
        }

        private void add_attribute_Click(object sender, EventArgs e)
        {
            string attribute = Microsoft.VisualBasic.Interaction.InputBox("Give a new attribute for this task", "New task attribute", "");
            if (attribute != "")
                taskAttributes.Items.Add(attribute);
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            if (!editMode)
            {
                if (taskName.Text == "" || taskDate.Text == "")
                {
                    MessageBox.Show("Please give a task name");
                    return;
                }
                currentTask.Name = taskName.Text;
                currentTask.Timestamp = (DateTime)taskDate.Tag;
            }

            currentTask.Completed(taskCompleted.Checked);
            if (taskDescription.Rtf != "")
                currentTask.Description = taskDescription.Rtf;
            else
                currentTask.Description = taskDescription.Text;

            List<string> attr = new List<string>();
            foreach (string s in taskAttributes.Items)
            {
                attr.Add(s);
            }
            currentTask.Attributes = attr;
            //06.04.2021 - subtask saving - Start >
            List<string[]> subtask = new List<string[]>();
            foreach (Control c in subtasksList.Controls)
            {
                if (!(c is CheckBox))
                    continue;
                string[] st = new string[2];
                st[0] = ((CheckBox)c).Text;
                st[1] = (((CheckBox)c).Checked) ? "X" : "";
                subtask.Add(st);
            }
            currentTask.Subtasks = subtask;
            //06.04.2021 - subtask saving - End > 
            if (deadlineCheckbox.Checked)
            {
                currentTask.Deadline = deadline.Value;
            }
            else
            {
                currentTask.Deadline = null;
            }
            this.Close();
            controller.UpdateView();
            controller.SaveData();
            controller.Update();
            controller.Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (currentTask.HasMail)
                currentTask.DisplayMail();
        }

        private void deadline_ValueChanged(object sender, EventArgs e)
        {
            if (deadline.Value < DateTime.Now)
                deadline_label.ForeColor = Color.Red;
            else
                deadline_label.ForeColor = Color.Black;
        }

        private void deadlineCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!deadlineCheckbox.Checked)
            {
                no_deadline_panel.Show();
                no_deadline_panel.Location = deadline.Location;
                no_deadline_panel.Size = deadline.Size;
            }
            else
                no_deadline_panel.Hide();
        }

        private void taskAttributes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (taskAttributes.SelectedIndex >= 0 && taskAttributes.SelectedIndex < taskAttributes.Items.Count && e.Button == MouseButtons.Middle)
            {
                Clipboard.SetText(taskAttributes.SelectedItem.ToString());
                controller.taskbarIcon.ShowBalloonTip(1500, "Copied to clipboard", " ", ToolTipIcon.Info);
            }
        }

        //06.04.2021 - Subtask events - Start >
        private void remove_subtask_Click(object sender, EventArgs e)
        {
            subtasksList.Controls.Remove(selectedSubtask);
            selectedSubtask = null;
            arrange_subtasks();
        }

        private void add_subtask_Click(object sender, EventArgs e)
        {
            string subtask = Microsoft.VisualBasic.Interaction.InputBox("Add a subtask", "New subtask under this parent", "");
            if (subtask.Trim() == "") return;

            add_sub_task(subtask, false);
            arrange_subtasks();
        }
        private void onSubtaskClick(object sender, MouseEventArgs e)
        {
            if (!(sender is CheckBox)) return;

            if (e.Button == MouseButtons.Middle)
            {
                Clipboard.SetText(((CheckBox)sender).Text);
                controller.taskbarIcon.ShowBalloonTip(1500, "Copied to clipboard", " ", ToolTipIcon.Info);
            }

            if (selectedSubtask != null)
            {
                selectedSubtask.BackColor = subtasksList.BackColor;
            }
            selectedSubtask = (CheckBox)sender;
            selectedSubtask.BackColor = Color.LightGray;
        }

        private void TaskInfo_Resize(object sender, EventArgs e)
        {
            resize_subtasks();
            arrange_subtasks();
        }
        //06.04.2021 - Subtask events - < End
    }
}