namespace WorkManagemnt
{
    partial class TaskInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskInfo));
            this.taskName = new System.Windows.Forms.TextBox();
            this.taskCompleted = new System.Windows.Forms.CheckBox();
            this.taskDescription = new System.Windows.Forms.RichTextBox();
            this.taskAttributes = new System.Windows.Forms.ListBox();
            this.description_label = new System.Windows.Forms.Label();
            this.attributes_label = new System.Windows.Forms.Label();
            this.add_attribute = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.deadline_label = new System.Windows.Forms.Label();
            this.deadline = new System.Windows.Forms.DateTimePicker();
            this.remove_attribute = new System.Windows.Forms.Button();
            this.taskMail = new System.Windows.Forms.PictureBox();
            this.taskDate = new System.Windows.Forms.TextBox();
            this.deadlineCheckbox = new System.Windows.Forms.CheckBox();
            this.no_deadline_panel = new System.Windows.Forms.Panel();
            this.no_deadline = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.taskMail)).BeginInit();
            this.no_deadline_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // taskName
            // 
            this.taskName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskName.Location = new System.Drawing.Point(12, 12);
            this.taskName.Name = "taskName";
            this.taskName.ReadOnly = true;
            this.taskName.Size = new System.Drawing.Size(779, 26);
            this.taskName.TabIndex = 1;
            // 
            // taskCompleted
            // 
            this.taskCompleted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.taskCompleted.AutoSize = true;
            this.taskCompleted.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.taskCompleted.ForeColor = System.Drawing.Color.Red;
            this.taskCompleted.Location = new System.Drawing.Point(576, 44);
            this.taskCompleted.Name = "taskCompleted";
            this.taskCompleted.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.taskCompleted.Size = new System.Drawing.Size(215, 33);
            this.taskCompleted.TabIndex = 3;
            this.taskCompleted.Text = "Not Completed";
            this.taskCompleted.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.taskCompleted.UseVisualStyleBackColor = true;
            this.taskCompleted.CheckedChanged += new System.EventHandler(this.taskCompleted_CheckedChanged);
            // 
            // taskDescription
            // 
            this.taskDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskDescription.Location = new System.Drawing.Point(12, 154);
            this.taskDescription.Name = "taskDescription";
            this.taskDescription.Size = new System.Drawing.Size(779, 135);
            this.taskDescription.TabIndex = 4;
            this.taskDescription.Text = "";
            // 
            // taskAttributes
            // 
            this.taskAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskAttributes.FormattingEnabled = true;
            this.taskAttributes.ItemHeight = 20;
            this.taskAttributes.Location = new System.Drawing.Point(12, 314);
            this.taskAttributes.Name = "taskAttributes";
            this.taskAttributes.Size = new System.Drawing.Size(779, 84);
            this.taskAttributes.TabIndex = 5;
            this.taskAttributes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.taskAttributes_MouseDoubleClick);
            // 
            // description_label
            // 
            this.description_label.AutoSize = true;
            this.description_label.Location = new System.Drawing.Point(12, 131);
            this.description_label.Name = "description_label";
            this.description_label.Size = new System.Drawing.Size(97, 20);
            this.description_label.TabIndex = 6;
            this.description_label.Text = "Description :";
            // 
            // attributes_label
            // 
            this.attributes_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.attributes_label.AutoSize = true;
            this.attributes_label.Location = new System.Drawing.Point(12, 291);
            this.attributes_label.Name = "attributes_label";
            this.attributes_label.Size = new System.Drawing.Size(86, 20);
            this.attributes_label.TabIndex = 7;
            this.attributes_label.Text = "Attributes :";
            // 
            // add_attribute
            // 
            this.add_attribute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.add_attribute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_attribute.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.add_attribute.Location = new System.Drawing.Point(752, 392);
            this.add_attribute.Name = "add_attribute";
            this.add_attribute.Size = new System.Drawing.Size(39, 35);
            this.add_attribute.TabIndex = 8;
            this.add_attribute.Text = "+";
            this.add_attribute.UseVisualStyleBackColor = true;
            this.add_attribute.Click += new System.EventHandler(this.add_attribute_Click);
            // 
            // save_button
            // 
            this.save_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.save_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.save_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.save_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.save_button.Location = new System.Drawing.Point(12, 437);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(779, 39);
            this.save_button.TabIndex = 9;
            this.save_button.Text = "Save";
            this.save_button.UseVisualStyleBackColor = false;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // deadline_label
            // 
            this.deadline_label.AutoSize = true;
            this.deadline_label.Location = new System.Drawing.Point(12, 79);
            this.deadline_label.Name = "deadline_label";
            this.deadline_label.Size = new System.Drawing.Size(80, 20);
            this.deadline_label.TabIndex = 11;
            this.deadline_label.Text = "Deadline :";
            // 
            // deadline
            // 
            this.deadline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deadline.Location = new System.Drawing.Point(40, 102);
            this.deadline.Name = "deadline";
            this.deadline.Size = new System.Drawing.Size(751, 26);
            this.deadline.TabIndex = 12;
            this.deadline.ValueChanged += new System.EventHandler(this.deadline_ValueChanged);
            // 
            // remove_attribute
            // 
            this.remove_attribute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.remove_attribute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.remove_attribute.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.remove_attribute.Location = new System.Drawing.Point(708, 392);
            this.remove_attribute.Name = "remove_attribute";
            this.remove_attribute.Size = new System.Drawing.Size(38, 35);
            this.remove_attribute.TabIndex = 13;
            this.remove_attribute.Text = "-";
            this.remove_attribute.UseVisualStyleBackColor = true;
            this.remove_attribute.Click += new System.EventHandler(this.remove_attribute_Click);
            // 
            // taskMail
            // 
            this.taskMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.taskMail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.taskMail.Image = global::WorkManagemnt.Properties.Resources.mail;
            this.taskMail.Location = new System.Drawing.Point(765, 12);
            this.taskMail.Name = "taskMail";
            this.taskMail.Size = new System.Drawing.Size(26, 26);
            this.taskMail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.taskMail.TabIndex = 14;
            this.taskMail.TabStop = false;
            this.taskMail.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // taskDate
            // 
            this.taskDate.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.taskDate.Location = new System.Drawing.Point(12, 44);
            this.taskDate.Name = "taskDate";
            this.taskDate.ReadOnly = true;
            this.taskDate.Size = new System.Drawing.Size(226, 26);
            this.taskDate.TabIndex = 2;
            // 
            // deadlineCheckbox
            // 
            this.deadlineCheckbox.AutoSize = true;
            this.deadlineCheckbox.Checked = true;
            this.deadlineCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deadlineCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.deadlineCheckbox.Location = new System.Drawing.Point(12, 105);
            this.deadlineCheckbox.Name = "deadlineCheckbox";
            this.deadlineCheckbox.Size = new System.Drawing.Size(22, 21);
            this.deadlineCheckbox.TabIndex = 15;
            this.deadlineCheckbox.UseVisualStyleBackColor = true;
            this.deadlineCheckbox.CheckedChanged += new System.EventHandler(this.deadlineCheckbox_CheckedChanged);
            // 
            // no_deadline_panel
            // 
            this.no_deadline_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.no_deadline_panel.BackColor = System.Drawing.Color.White;
            this.no_deadline_panel.Controls.Add(this.no_deadline);
            this.no_deadline_panel.Location = new System.Drawing.Point(40, 102);
            this.no_deadline_panel.Name = "no_deadline_panel";
            this.no_deadline_panel.Size = new System.Drawing.Size(751, 26);
            this.no_deadline_panel.TabIndex = 16;
            this.no_deadline_panel.Visible = false;
            // 
            // no_deadline
            // 
            this.no_deadline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.no_deadline.AutoSize = true;
            this.no_deadline.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.no_deadline.Location = new System.Drawing.Point(636, 2);
            this.no_deadline.Name = "no_deadline";
            this.no_deadline.Size = new System.Drawing.Size(106, 22);
            this.no_deadline.TabIndex = 0;
            this.no_deadline.Text = "No deadline";
            this.no_deadline.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TaskInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 479);
            this.Controls.Add(this.no_deadline_panel);
            this.Controls.Add(this.deadlineCheckbox);
            this.Controls.Add(this.taskMail);
            this.Controls.Add(this.remove_attribute);
            this.Controls.Add(this.deadline);
            this.Controls.Add(this.deadline_label);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.add_attribute);
            this.Controls.Add(this.attributes_label);
            this.Controls.Add(this.description_label);
            this.Controls.Add(this.taskAttributes);
            this.Controls.Add(this.taskDescription);
            this.Controls.Add(this.taskCompleted);
            this.Controls.Add(this.taskDate);
            this.Controls.Add(this.taskName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(825, 535);
            this.Name = "TaskInfo";
            this.Text = "Task";
            this.Load += new System.EventHandler(this.TaskInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.taskMail)).EndInit();
            this.no_deadline_panel.ResumeLayout(false);
            this.no_deadline_panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox taskName;
        private System.Windows.Forms.CheckBox taskCompleted;
        private System.Windows.Forms.RichTextBox taskDescription;
        private System.Windows.Forms.ListBox taskAttributes;
        private System.Windows.Forms.Label description_label;
        private System.Windows.Forms.Label attributes_label;
        private System.Windows.Forms.Button add_attribute;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Label deadline_label;
        private System.Windows.Forms.DateTimePicker deadline;
        private System.Windows.Forms.Button remove_attribute;
        private System.Windows.Forms.PictureBox taskMail;
        private System.Windows.Forms.TextBox taskDate;
        private System.Windows.Forms.CheckBox deadlineCheckbox;
        private System.Windows.Forms.Panel no_deadline_panel;
        private System.Windows.Forms.Label no_deadline;
    }
}