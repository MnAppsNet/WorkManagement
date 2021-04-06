using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkManagemnt
{
    public partial class TaskInfo : Form
    {
        private Task currentTask;
        private bool editMode;
        private Form1 controller; 

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
            if (deadlineCheckbox.Checked)
            {
                currentTask.Deadline = deadline.Value;
            }
            else
            {
                currentTask.Deadline = null;
            }
            this.Close();
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
            if (taskAttributes.SelectedIndex >= 0 && taskAttributes.SelectedIndex < taskAttributes.Items.Count)
            {
                Clipboard.SetText(taskAttributes.SelectedItem.ToString());
                controller.taskbarIcon.ShowBalloonTip(1500, "Copied to clipboard", " ", ToolTipIcon.Info);
            }
        }
    }
}