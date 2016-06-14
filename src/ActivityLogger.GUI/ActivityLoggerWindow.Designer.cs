namespace AL.Gui
{
    partial class ActivityLoggerWindow
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
            this.labelProgress = new System.Windows.Forms.Label();
            this.progressBarActiveTime = new System.Windows.Forms.ProgressBar();
            this.labelActiveProcess = new System.Windows.Forms.Label();
            this.labelActiveProcessValue = new System.Windows.Forms.Label();
            this.labelActiveTime = new System.Windows.Forms.Label();
            this.labelActiveTimeValue = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabProcesses = new System.Windows.Forms.TabPage();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.textBoxProcesses = new System.Windows.Forms.TextBox();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabProcesses.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(12, 38);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(79, 13);
            this.labelProgress.TabIndex = 0;
            this.labelProgress.Text = "Work progress:";
            // 
            // progressBarActiveTime
            // 
            this.progressBarActiveTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarActiveTime.Location = new System.Drawing.Point(107, 34);
            this.progressBarActiveTime.Name = "progressBarActiveTime";
            this.progressBarActiveTime.Size = new System.Drawing.Size(324, 22);
            this.progressBarActiveTime.TabIndex = 1;
            // 
            // labelActiveProcess
            // 
            this.labelActiveProcess.AutoSize = true;
            this.labelActiveProcess.Location = new System.Drawing.Point(12, 67);
            this.labelActiveProcess.Name = "labelActiveProcess";
            this.labelActiveProcess.Size = new System.Drawing.Size(80, 13);
            this.labelActiveProcess.TabIndex = 3;
            this.labelActiveProcess.Text = "Active process:";
            // 
            // labelActiveProcessValue
            // 
            this.labelActiveProcessValue.AutoSize = true;
            this.labelActiveProcessValue.Location = new System.Drawing.Point(107, 67);
            this.labelActiveProcessValue.Name = "labelActiveProcessValue";
            this.labelActiveProcessValue.Size = new System.Drawing.Size(0, 13);
            this.labelActiveProcessValue.TabIndex = 5;
            // 
            // labelActiveTime
            // 
            this.labelActiveTime.AutoSize = true;
            this.labelActiveTime.Location = new System.Drawing.Point(12, 9);
            this.labelActiveTime.Name = "labelActiveTime";
            this.labelActiveTime.Size = new System.Drawing.Size(70, 13);
            this.labelActiveTime.TabIndex = 6;
            this.labelActiveTime.Text = "Process time:";
            // 
            // labelActiveTimeValue
            // 
            this.labelActiveTimeValue.AutoSize = true;
            this.labelActiveTimeValue.Location = new System.Drawing.Point(107, 9);
            this.labelActiveTimeValue.Name = "labelActiveTimeValue";
            this.labelActiveTimeValue.Size = new System.Drawing.Size(0, 13);
            this.labelActiveTimeValue.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabProcesses);
            this.tabControl1.Controls.Add(this.tabLog);
            this.tabControl1.Location = new System.Drawing.Point(15, 95);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(416, 154);
            this.tabControl1.TabIndex = 8;
            // 
            // tabProcesses
            // 
            this.tabProcesses.Controls.Add(this.textBoxProcesses);
            this.tabProcesses.Location = new System.Drawing.Point(4, 22);
            this.tabProcesses.Name = "tabProcesses";
            this.tabProcesses.Padding = new System.Windows.Forms.Padding(3);
            this.tabProcesses.Size = new System.Drawing.Size(408, 128);
            this.tabProcesses.TabIndex = 0;
            this.tabProcesses.Text = "Processes";
            this.tabProcesses.UseVisualStyleBackColor = true;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.textBoxLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(408, 128);
            this.tabLog.TabIndex = 1;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // textBoxProcesses
            // 
            this.textBoxProcesses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxProcesses.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProcesses.Location = new System.Drawing.Point(7, 4);
            this.textBoxProcesses.Multiline = true;
            this.textBoxProcesses.Name = "textBoxProcesses";
            this.textBoxProcesses.Size = new System.Drawing.Size(395, 118);
            this.textBoxProcesses.TabIndex = 0;
            // 
            // textBoxLog
            // 
            this.textBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLog.Location = new System.Drawing.Point(7, 4);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(395, 118);
            this.textBoxLog.TabIndex = 0;
            // 
            // ActivityLoggerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 261);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.labelActiveTimeValue);
            this.Controls.Add(this.labelActiveTime);
            this.Controls.Add(this.labelActiveProcessValue);
            this.Controls.Add(this.labelActiveProcess);
            this.Controls.Add(this.progressBarActiveTime);
            this.Controls.Add(this.labelProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ActivityLoggerWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Activity Logger (c) Niclas Lindstedt";
            this.tabControl1.ResumeLayout(false);
            this.tabProcesses.ResumeLayout(false);
            this.tabProcesses.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.ProgressBar progressBarActiveTime;
        private System.Windows.Forms.Label labelActiveProcess;
        private System.Windows.Forms.Label labelActiveProcessValue;
        private System.Windows.Forms.Label labelActiveTime;
        private System.Windows.Forms.Label labelActiveTimeValue;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabProcesses;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TextBox textBoxProcesses;
        private System.Windows.Forms.TextBox textBoxLog;
    }
}

