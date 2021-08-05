namespace WorkManagemnt
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.settings_panel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.mailsFromDays = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tasksPerPage = new System.Windows.Forms.NumericUpDown();
            this.tasks_per_page_label = new System.Windows.Forms.Label();
            this.stayAwake = new System.Windows.Forms.CheckBox();
            this.completedMailCategory = new System.Windows.Forms.TextBox();
            this.mailRefreshRate = new System.Windows.Forms.NumericUpDown();
            this.mailCheck = new System.Windows.Forms.CheckBox();
            this.mailCategoryLabel = new System.Windows.Forms.Label();
            this.mailBodyInTask = new System.Windows.Forms.CheckBox();
            this.workingDirectoryLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.completedMailCategoryLabel = new System.Windows.Forms.Label();
            this.mailCategory = new System.Windows.Forms.TextBox();
            this.workingDirectory = new System.Windows.Forms.TextBox();
            this.close_settings = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.settings_button = new System.Windows.Forms.Button();
            this.addTask = new System.Windows.Forms.Button();
            this.checkMail = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.taskbarIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.taskbarOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.keepAwake = new System.Windows.Forms.Timer(this.components);
            this.taskItems = new System.Windows.Forms.Panel();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.completedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notCompletedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.searchBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.searchStatus = new System.Windows.Forms.ComboBox();
            this.status_label = new System.Windows.Forms.Label();
            this.settings_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mailsFromDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tasksPerPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mailRefreshRate)).BeginInit();
            this.taskbarOptions.SuspendLayout();
            this.taskOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // settings_panel
            // 
            this.settings_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.settings_panel.Controls.Add(this.label5);
            this.settings_panel.Controls.Add(this.mailsFromDays);
            this.settings_panel.Controls.Add(this.label2);
            this.settings_panel.Controls.Add(this.tasksPerPage);
            this.settings_panel.Controls.Add(this.tasks_per_page_label);
            this.settings_panel.Controls.Add(this.stayAwake);
            this.settings_panel.Controls.Add(this.completedMailCategory);
            this.settings_panel.Controls.Add(this.mailRefreshRate);
            this.settings_panel.Controls.Add(this.mailCheck);
            this.settings_panel.Controls.Add(this.mailCategoryLabel);
            this.settings_panel.Controls.Add(this.mailBodyInTask);
            this.settings_panel.Controls.Add(this.workingDirectoryLabel);
            this.settings_panel.Controls.Add(this.label3);
            this.settings_panel.Controls.Add(this.completedMailCategoryLabel);
            this.settings_panel.Controls.Add(this.mailCategory);
            this.settings_panel.Controls.Add(this.workingDirectory);
            this.settings_panel.Controls.Add(this.close_settings);
            this.settings_panel.Controls.Add(this.label4);
            this.settings_panel.Location = new System.Drawing.Point(1, 314);
            this.settings_panel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.settings_panel.Name = "settings_panel";
            this.settings_panel.Size = new System.Drawing.Size(768, 148);
            this.settings_panel.TabIndex = 0;
            this.settings_panel.Visible = false;
            this.settings_panel.VisibleChanged += new System.EventHandler(this.settings_panel_VisibleChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(392, 8);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "days";
            // 
            // mailsFromDays
            // 
            this.mailsFromDays.Location = new System.Drawing.Point(350, 6);
            this.mailsFromDays.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mailsFromDays.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.mailsFromDays.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.mailsFromDays.Name = "mailsFromDays";
            this.mailsFromDays.Size = new System.Drawing.Size(38, 20);
            this.mailsFromDays.TabIndex = 16;
            this.mailsFromDays.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.mailsFromDays.ValueChanged += new System.EventHandler(this.mailsFromDays_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(239, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Check eMails from last ";
            // 
            // tasksPerPage
            // 
            this.tasksPerPage.Location = new System.Drawing.Point(324, 26);
            this.tasksPerPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tasksPerPage.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.tasksPerPage.Name = "tasksPerPage";
            this.tasksPerPage.Size = new System.Drawing.Size(47, 20);
            this.tasksPerPage.TabIndex = 14;
            this.tasksPerPage.ValueChanged += new System.EventHandler(this.tasksPerPage_ValueChanged);
            // 
            // tasks_per_page_label
            // 
            this.tasks_per_page_label.AutoSize = true;
            this.tasks_per_page_label.Location = new System.Drawing.Point(239, 27);
            this.tasks_per_page_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tasks_per_page_label.Name = "tasks_per_page_label";
            this.tasks_per_page_label.Size = new System.Drawing.Size(84, 13);
            this.tasks_per_page_label.TabIndex = 13;
            this.tasks_per_page_label.Text = "Tasks per page:";
            // 
            // stayAwake
            // 
            this.stayAwake.AutoSize = true;
            this.stayAwake.Location = new System.Drawing.Point(3, 45);
            this.stayAwake.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.stayAwake.Name = "stayAwake";
            this.stayAwake.Size = new System.Drawing.Size(121, 17);
            this.stayAwake.TabIndex = 7;
            this.stayAwake.Text = "Keep System Active";
            this.stayAwake.UseVisualStyleBackColor = true;
            this.stayAwake.CheckedChanged += new System.EventHandler(this.stayAwake_CheckedChanged);
            // 
            // completedMailCategory
            // 
            this.completedMailCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.completedMailCategory.Location = new System.Drawing.Point(162, 124);
            this.completedMailCategory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.completedMailCategory.Name = "completedMailCategory";
            this.completedMailCategory.Size = new System.Drawing.Size(599, 20);
            this.completedMailCategory.TabIndex = 11;
            this.completedMailCategory.TextChanged += new System.EventHandler(this.completedMailCategory_TextChanged);
            // 
            // mailRefreshRate
            // 
            this.mailRefreshRate.Location = new System.Drawing.Point(137, 5);
            this.mailRefreshRate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mailRefreshRate.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.mailRefreshRate.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.mailRefreshRate.Name = "mailRefreshRate";
            this.mailRefreshRate.Size = new System.Drawing.Size(45, 20);
            this.mailRefreshRate.TabIndex = 7;
            this.mailRefreshRate.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.mailRefreshRate.ValueChanged += new System.EventHandler(this.mailRefreshRate_ValueChanged);
            // 
            // mailCheck
            // 
            this.mailCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mailCheck.AutoSize = true;
            this.mailCheck.Checked = true;
            this.mailCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mailCheck.Location = new System.Drawing.Point(-382, -179);
            this.mailCheck.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mailCheck.Name = "mailCheck";
            this.mailCheck.Size = new System.Drawing.Size(15, 14);
            this.mailCheck.TabIndex = 9;
            this.mailCheck.UseVisualStyleBackColor = true;
            this.mailCheck.CheckedChanged += new System.EventHandler(this.mailCheck_CheckedChanged);
            // 
            // mailCategoryLabel
            // 
            this.mailCategoryLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mailCategoryLabel.AutoSize = true;
            this.mailCategoryLabel.Location = new System.Drawing.Point(3, 107);
            this.mailCategoryLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mailCategoryLabel.Name = "mailCategoryLabel";
            this.mailCategoryLabel.Size = new System.Drawing.Size(104, 13);
            this.mailCategoryLabel.TabIndex = 0;
            this.mailCategoryLabel.Text = "Mail Task Category :";
            // 
            // mailBodyInTask
            // 
            this.mailBodyInTask.AutoSize = true;
            this.mailBodyInTask.Location = new System.Drawing.Point(3, 26);
            this.mailBodyInTask.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mailBodyInTask.Name = "mailBodyInTask";
            this.mailBodyInTask.Size = new System.Drawing.Size(201, 17);
            this.mailBodyInTask.TabIndex = 12;
            this.mailBodyInTask.Text = "Retrieve mail body in task description";
            this.mailBodyInTask.UseVisualStyleBackColor = true;
            this.mailBodyInTask.CheckedChanged += new System.EventHandler(this.mailBodyInTask_CheckedChanged);
            // 
            // workingDirectoryLabel
            // 
            this.workingDirectoryLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workingDirectoryLabel.AutoSize = true;
            this.workingDirectoryLabel.Location = new System.Drawing.Point(3, 86);
            this.workingDirectoryLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.workingDirectoryLabel.Name = "workingDirectoryLabel";
            this.workingDirectoryLabel.Size = new System.Drawing.Size(98, 13);
            this.workingDirectoryLabel.TabIndex = 4;
            this.workingDirectoryLabel.Text = "Working Directory :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Check email for tasks every ";
            // 
            // completedMailCategoryLabel
            // 
            this.completedMailCategoryLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.completedMailCategoryLabel.AutoSize = true;
            this.completedMailCategoryLabel.Location = new System.Drawing.Point(3, 127);
            this.completedMailCategoryLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.completedMailCategoryLabel.Name = "completedMailCategoryLabel";
            this.completedMailCategoryLabel.Size = new System.Drawing.Size(157, 13);
            this.completedMailCategoryLabel.TabIndex = 10;
            this.completedMailCategoryLabel.Text = "Completed Mail Task Category :";
            // 
            // mailCategory
            // 
            this.mailCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mailCategory.Location = new System.Drawing.Point(162, 103);
            this.mailCategory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mailCategory.Name = "mailCategory";
            this.mailCategory.Size = new System.Drawing.Size(599, 20);
            this.mailCategory.TabIndex = 1;
            this.mailCategory.TextChanged += new System.EventHandler(this.mailCategory_TextChanged);
            // 
            // workingDirectory
            // 
            this.workingDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workingDirectory.Location = new System.Drawing.Point(162, 82);
            this.workingDirectory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.workingDirectory.Name = "workingDirectory";
            this.workingDirectory.ReadOnly = true;
            this.workingDirectory.Size = new System.Drawing.Size(599, 20);
            this.workingDirectory.TabIndex = 5;
            // 
            // close_settings
            // 
            this.close_settings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.close_settings.AutoSize = true;
            this.close_settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.close_settings.Location = new System.Drawing.Point(739, 2);
            this.close_settings.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.close_settings.Name = "close_settings";
            this.close_settings.Size = new System.Drawing.Size(21, 20);
            this.close_settings.TabIndex = 3;
            this.close_settings.Text = "X";
            this.close_settings.Click += new System.EventHandler(this.close_settings_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(183, 8);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "minutes";
            // 
            // settings_button
            // 
            this.settings_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.settings_button.Location = new System.Drawing.Point(684, 465);
            this.settings_button.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.settings_button.Name = "settings_button";
            this.settings_button.Size = new System.Drawing.Size(85, 21);
            this.settings_button.TabIndex = 3;
            this.settings_button.Text = "Settings";
            this.settings_button.UseVisualStyleBackColor = true;
            this.settings_button.Click += new System.EventHandler(this.settings_button_Click);
            // 
            // addTask
            // 
            this.addTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.addTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.addTask.ForeColor = System.Drawing.Color.Blue;
            this.addTask.Location = new System.Drawing.Point(243, 465);
            this.addTask.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.addTask.Name = "addTask";
            this.addTask.Size = new System.Drawing.Size(437, 42);
            this.addTask.TabIndex = 4;
            this.addTask.Text = "+ Add Task +";
            this.addTask.UseVisualStyleBackColor = false;
            this.addTask.Click += new System.EventHandler(this.addTask_Click);
            // 
            // checkMail
            // 
            this.checkMail.Interval = 120000;
            this.checkMail.Tick += new System.EventHandler(this.checkMail_Tick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.button1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.button1.Location = new System.Drawing.Point(684, 486);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 21);
            this.button1.TabIndex = 6;
            this.button1.Text = "Check Mail";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // taskbarIcon
            // 
            this.taskbarIcon.ContextMenuStrip = this.taskbarOptions;
            this.taskbarIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("taskbarIcon.Icon")));
            this.taskbarIcon.Text = "WorkManager";
            this.taskbarIcon.Visible = true;
            this.taskbarIcon.BalloonTipClicked += new System.EventHandler(this.taskbarIcon_BalloonTipClicked);
            this.taskbarIcon.DoubleClick += new System.EventHandler(this.taskbarIcon_DoubleClick);
            // 
            // taskbarOptions
            // 
            this.taskbarOptions.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.taskbarOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem5});
            this.taskbarOptions.Name = "taskOptions";
            this.taskbarOptions.Size = new System.Drawing.Size(186, 70);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItem1.Text = "Open";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItem2.Text = "Check mails for tasks";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.toolStripMenuItem5.ForeColor = System.Drawing.Color.Red;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItem5.Text = "Exit";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // keepAwake
            // 
            this.keepAwake.Interval = 60000;
            this.keepAwake.Tick += new System.EventHandler(this.keepAwake_Tick);
            // 
            // taskItems
            // 
            this.taskItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskItems.AutoScroll = true;
            this.taskItems.ContextMenuStrip = this.taskbarOptions;
            this.taskItems.Location = new System.Drawing.Point(1, 1);
            this.taskItems.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.taskItems.Name = "taskItems";
            this.taskItems.Size = new System.Drawing.Size(769, 461);
            this.taskItems.TabIndex = 8;
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.editToolStripMenuItem.Text = "Open";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // markAsToolStripMenuItem
            // 
            this.markAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.completedToolStripMenuItem,
            this.notCompletedToolStripMenuItem});
            this.markAsToolStripMenuItem.Name = "markAsToolStripMenuItem";
            this.markAsToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.markAsToolStripMenuItem.Text = "Mark As";
            // 
            // completedToolStripMenuItem
            // 
            this.completedToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.completedToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.completedToolStripMenuItem.Name = "completedToolStripMenuItem";
            this.completedToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.completedToolStripMenuItem.Text = "Completed";
            this.completedToolStripMenuItem.Click += new System.EventHandler(this.completedToolStripMenuItem_Click);
            // 
            // notCompletedToolStripMenuItem
            // 
            this.notCompletedToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.notCompletedToolStripMenuItem.ForeColor = System.Drawing.Color.Red;
            this.notCompletedToolStripMenuItem.Name = "notCompletedToolStripMenuItem";
            this.notCompletedToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.notCompletedToolStripMenuItem.Text = "Not Completed";
            this.notCompletedToolStripMenuItem.Click += new System.EventHandler(this.notCompletedToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.removeToolStripMenuItem.ForeColor = System.Drawing.Color.Red;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // taskOptions
            // 
            this.taskOptions.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.taskOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.markAsToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.taskOptions.Name = "taskOptions";
            this.taskOptions.Size = new System.Drawing.Size(122, 70);
            // 
            // searchBox
            // 
            this.searchBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchBox.Location = new System.Drawing.Point(5, 465);
            this.searchBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(231, 20);
            this.searchBox.TabIndex = 10;
            this.searchBox.Text = "Search...";
            this.searchBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.searchBox.Enter += new System.EventHandler(this.searchBox_Enter);
            this.searchBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.searchBox_KeyUp);
            this.searchBox.Leave += new System.EventHandler(this.searchBox_Leave);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(219, 467);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // searchStatus
            // 
            this.searchStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchStatus.FormattingEnabled = true;
            this.searchStatus.Location = new System.Drawing.Point(40, 486);
            this.searchStatus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.searchStatus.Name = "searchStatus";
            this.searchStatus.Size = new System.Drawing.Size(197, 21);
            this.searchStatus.TabIndex = 12;
            this.searchStatus.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // status_label
            // 
            this.status_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.status_label.AutoSize = true;
            this.status_label.Location = new System.Drawing.Point(-1, 488);
            this.status_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(40, 13);
            this.status_label.TabIndex = 13;
            this.status_label.Text = "Status:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 510);
            this.Controls.Add(this.status_label);
            this.Controls.Add(this.searchStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.addTask);
            this.Controls.Add(this.settings_button);
            this.Controls.Add(this.settings_panel);
            this.Controls.Add(this.taskItems);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(603, 359);
            this.Name = "Form1";
            this.Text = "WorkManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.settings_panel.ResumeLayout(false);
            this.settings_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mailsFromDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tasksPerPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mailRefreshRate)).EndInit();
            this.taskbarOptions.ResumeLayout(false);
            this.taskOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel settings_panel;
        private System.Windows.Forms.TextBox mailCategory;
        private System.Windows.Forms.Label mailCategoryLabel;
        private System.Windows.Forms.Label close_settings;
        private System.Windows.Forms.Button settings_button;
        private System.Windows.Forms.TextBox workingDirectory;
        private System.Windows.Forms.Label workingDirectoryLabel;
        private System.Windows.Forms.Button addTask;
        private System.Windows.Forms.CheckBox mailCheck;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown mailRefreshRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer checkMail;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip taskbarOptions;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        public System.Windows.Forms.NotifyIcon taskbarIcon;
        private System.Windows.Forms.CheckBox stayAwake;
        private System.Windows.Forms.Timer keepAwake;
        private System.Windows.Forms.TextBox completedMailCategory;
        private System.Windows.Forms.Label completedMailCategoryLabel;
        private System.Windows.Forms.CheckBox mailBodyInTask;
        private System.Windows.Forms.Panel taskItems;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem completedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem notCompletedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip taskOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label status_label;
        private System.Windows.Forms.NumericUpDown tasksPerPage;
        private System.Windows.Forms.Label tasks_per_page_label;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown mailsFromDays;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox searchBox;
        public System.Windows.Forms.ComboBox searchStatus;
    }
}

