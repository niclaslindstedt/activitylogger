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
            this.progressBarProgress = new System.Windows.Forms.ProgressBar();
            this.labelActiveProcess = new System.Windows.Forms.Label();
            this.labelActiveProcessValue = new System.Windows.Forms.Label();
            this.labelActivityTime = new System.Windows.Forms.Label();
            this.labelActivityTimeValue = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabProcesses = new System.Windows.Forms.TabPage();
            this.textBoxProcesses = new System.Windows.Forms.TextBox();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.labelAxisX = new System.Windows.Forms.Label();
            this.labelAxisY = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.groupBoxProcessInformation = new System.Windows.Forms.GroupBox();
            this.groupBoxProgressInformation = new System.Windows.Forms.GroupBox();
            this.progressBarTimeUntilIdle = new System.Windows.Forms.ProgressBar();
            this.labelTimeUntilIdle = new System.Windows.Forms.Label();
            this.progressBarProcessShare = new System.Windows.Forms.ProgressBar();
            this.labelProcessShare = new System.Windows.Forms.Label();
            this.labelProcessTime = new System.Windows.Forms.Label();
            this.labelProcessTimeValue = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabProcesses.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.groupBoxProcessInformation.SuspendLayout();
            this.groupBoxProgressInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelProgress
            // 
            this.labelProgress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(6, 18);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(51, 13);
            this.labelProgress.TabIndex = 0;
            this.labelProgress.Text = "Progress:";
            // 
            // progressBarProgress
            // 
            this.progressBarProgress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.progressBarProgress.Location = new System.Drawing.Point(104, 18);
            this.progressBarProgress.Name = "progressBarProgress";
            this.progressBarProgress.Size = new System.Drawing.Size(172, 13);
            this.progressBarProgress.TabIndex = 1;
            // 
            // labelActiveProcess
            // 
            this.labelActiveProcess.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelActiveProcess.AutoSize = true;
            this.labelActiveProcess.Location = new System.Drawing.Point(1, 50);
            this.labelActiveProcess.Name = "labelActiveProcess";
            this.labelActiveProcess.Size = new System.Drawing.Size(80, 13);
            this.labelActiveProcess.TabIndex = 3;
            this.labelActiveProcess.Text = "Active process:";
            // 
            // labelActiveProcessValue
            // 
            this.labelActiveProcessValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelActiveProcessValue.AutoSize = true;
            this.labelActiveProcessValue.Location = new System.Drawing.Point(96, 50);
            this.labelActiveProcessValue.Name = "labelActiveProcessValue";
            this.labelActiveProcessValue.Size = new System.Drawing.Size(85, 13);
            this.labelActiveProcessValue.TabIndex = 5;
            this.labelActiveProcessValue.Text = "{Active process}";
            // 
            // labelActivityTime
            // 
            this.labelActivityTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelActivityTime.AutoSize = true;
            this.labelActivityTime.Location = new System.Drawing.Point(1, 18);
            this.labelActivityTime.Name = "labelActivityTime";
            this.labelActivityTime.Size = new System.Drawing.Size(66, 13);
            this.labelActivityTime.TabIndex = 6;
            this.labelActivityTime.Text = "Activity time:";
            // 
            // labelActivityTimeValue
            // 
            this.labelActivityTimeValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelActivityTimeValue.AutoSize = true;
            this.labelActivityTimeValue.Location = new System.Drawing.Point(96, 18);
            this.labelActivityTimeValue.Name = "labelActivityTimeValue";
            this.labelActivityTimeValue.Size = new System.Drawing.Size(57, 13);
            this.labelActivityTimeValue.TabIndex = 7;
            this.labelActivityTimeValue.Text = "{01:00:00}";
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabProcesses);
            this.tabControl.Controls.Add(this.tabLog);
            this.tabControl.Location = new System.Drawing.Point(12, 293);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(560, 154);
            this.tabControl.TabIndex = 8;
            // 
            // tabProcesses
            // 
            this.tabProcesses.Controls.Add(this.textBoxProcesses);
            this.tabProcesses.Location = new System.Drawing.Point(4, 22);
            this.tabProcesses.Name = "tabProcesses";
            this.tabProcesses.Padding = new System.Windows.Forms.Padding(3);
            this.tabProcesses.Size = new System.Drawing.Size(552, 128);
            this.tabProcesses.TabIndex = 0;
            this.tabProcesses.Text = "Processes";
            this.tabProcesses.UseVisualStyleBackColor = true;
            // 
            // textBoxProcesses
            // 
            this.textBoxProcesses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProcesses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxProcesses.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProcesses.Location = new System.Drawing.Point(7, 4);
            this.textBoxProcesses.Multiline = true;
            this.textBoxProcesses.Name = "textBoxProcesses";
            this.textBoxProcesses.Size = new System.Drawing.Size(539, 118);
            this.textBoxProcesses.TabIndex = 0;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.textBoxLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(549, 128);
            this.tabLog.TabIndex = 1;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLog.Location = new System.Drawing.Point(7, 4);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(536, 118);
            this.textBoxLog.TabIndex = 0;
            // 
            // labelAxisX
            // 
            this.labelAxisX.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAxisX.AutoSize = true;
            this.labelAxisX.BackColor = System.Drawing.Color.White;
            this.labelAxisX.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAxisX.Location = new System.Drawing.Point(17, 28);
            this.labelAxisX.Name = "labelAxisX";
            this.labelAxisX.Size = new System.Drawing.Size(10, 12);
            this.labelAxisX.TabIndex = 9;
            this.labelAxisX.Text = "1";
            // 
            // labelAxisY
            // 
            this.labelAxisY.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAxisY.AutoSize = true;
            this.labelAxisY.BackColor = System.Drawing.Color.White;
            this.labelAxisY.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAxisY.Location = new System.Drawing.Point(17, 179);
            this.labelAxisY.Name = "labelAxisY";
            this.labelAxisY.Size = new System.Drawing.Size(10, 12);
            this.labelAxisY.TabIndex = 10;
            this.labelAxisY.Text = "0";
            this.labelAxisY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(490, 453);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 11;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // groupBoxProcessInformation
            // 
            this.groupBoxProcessInformation.Controls.Add(this.labelProcessTime);
            this.groupBoxProcessInformation.Controls.Add(this.labelProcessTimeValue);
            this.groupBoxProcessInformation.Controls.Add(this.labelActivityTime);
            this.groupBoxProcessInformation.Controls.Add(this.labelActiveProcess);
            this.groupBoxProcessInformation.Controls.Add(this.labelActiveProcessValue);
            this.groupBoxProcessInformation.Controls.Add(this.labelActivityTimeValue);
            this.groupBoxProcessInformation.Location = new System.Drawing.Point(12, 216);
            this.groupBoxProcessInformation.Name = "groupBoxProcessInformation";
            this.groupBoxProcessInformation.Size = new System.Drawing.Size(267, 71);
            this.groupBoxProcessInformation.TabIndex = 12;
            this.groupBoxProcessInformation.TabStop = false;
            this.groupBoxProcessInformation.Text = "Process information";
            // 
            // groupBoxProgressInformation
            // 
            this.groupBoxProgressInformation.Controls.Add(this.progressBarProcessShare);
            this.groupBoxProgressInformation.Controls.Add(this.labelProcessShare);
            this.groupBoxProgressInformation.Controls.Add(this.progressBarTimeUntilIdle);
            this.groupBoxProgressInformation.Controls.Add(this.labelTimeUntilIdle);
            this.groupBoxProgressInformation.Controls.Add(this.progressBarProgress);
            this.groupBoxProgressInformation.Controls.Add(this.labelProgress);
            this.groupBoxProgressInformation.Location = new System.Drawing.Point(286, 216);
            this.groupBoxProgressInformation.Name = "groupBoxProgressInformation";
            this.groupBoxProgressInformation.Size = new System.Drawing.Size(286, 71);
            this.groupBoxProgressInformation.TabIndex = 13;
            this.groupBoxProgressInformation.TabStop = false;
            this.groupBoxProgressInformation.Text = "Progress information";
            // 
            // progressBarTimeUntilIdle
            // 
            this.progressBarTimeUntilIdle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.progressBarTimeUntilIdle.Location = new System.Drawing.Point(104, 50);
            this.progressBarTimeUntilIdle.Name = "progressBarTimeUntilIdle";
            this.progressBarTimeUntilIdle.Size = new System.Drawing.Size(172, 13);
            this.progressBarTimeUntilIdle.TabIndex = 3;
            // 
            // labelTimeUntilIdle
            // 
            this.labelTimeUntilIdle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTimeUntilIdle.AutoSize = true;
            this.labelTimeUntilIdle.Location = new System.Drawing.Point(6, 50);
            this.labelTimeUntilIdle.Name = "labelTimeUntilIdle";
            this.labelTimeUntilIdle.Size = new System.Drawing.Size(74, 13);
            this.labelTimeUntilIdle.TabIndex = 2;
            this.labelTimeUntilIdle.Text = "Time until idle:";
            // 
            // progressBarProcessShare
            // 
            this.progressBarProcessShare.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.progressBarProcessShare.Location = new System.Drawing.Point(104, 34);
            this.progressBarProcessShare.Name = "progressBarProcessShare";
            this.progressBarProcessShare.Size = new System.Drawing.Size(172, 13);
            this.progressBarProcessShare.TabIndex = 5;
            // 
            // labelProcessShare
            // 
            this.labelProcessShare.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelProcessShare.AutoSize = true;
            this.labelProcessShare.Location = new System.Drawing.Point(6, 34);
            this.labelProcessShare.Name = "labelProcessShare";
            this.labelProcessShare.Size = new System.Drawing.Size(77, 13);
            this.labelProcessShare.TabIndex = 4;
            this.labelProcessShare.Text = "Process share:";
            // 
            // labelProcessTime
            // 
            this.labelProcessTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelProcessTime.AutoSize = true;
            this.labelProcessTime.Location = new System.Drawing.Point(1, 34);
            this.labelProcessTime.Name = "labelProcessTime";
            this.labelProcessTime.Size = new System.Drawing.Size(70, 13);
            this.labelProcessTime.TabIndex = 8;
            this.labelProcessTime.Text = "Process time:";
            // 
            // labelProcessTimeValue
            // 
            this.labelProcessTimeValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelProcessTimeValue.AutoSize = true;
            this.labelProcessTimeValue.Location = new System.Drawing.Point(96, 34);
            this.labelProcessTimeValue.Name = "labelProcessTimeValue";
            this.labelProcessTimeValue.Size = new System.Drawing.Size(57, 13);
            this.labelProcessTimeValue.TabIndex = 9;
            this.labelProcessTimeValue.Text = "{01:00:00}";
            // 
            // ActivityLoggerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 486);
            this.Controls.Add(this.groupBoxProgressInformation);
            this.Controls.Add(this.groupBoxProcessInformation);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.labelAxisY);
            this.Controls.Add(this.labelAxisX);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ActivityLoggerWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Activity Logger (c) Niclas Lindstedt";
            this.tabControl.ResumeLayout(false);
            this.tabProcesses.ResumeLayout(false);
            this.tabProcesses.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.groupBoxProcessInformation.ResumeLayout(false);
            this.groupBoxProcessInformation.PerformLayout();
            this.groupBoxProgressInformation.ResumeLayout(false);
            this.groupBoxProgressInformation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.ProgressBar progressBarProgress;
        private System.Windows.Forms.Label labelActiveProcess;
        private System.Windows.Forms.Label labelActiveProcessValue;
        private System.Windows.Forms.Label labelActivityTime;
        private System.Windows.Forms.Label labelActivityTimeValue;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabProcesses;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TextBox textBoxProcesses;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Label labelAxisX;
        private System.Windows.Forms.Label labelAxisY;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.GroupBox groupBoxProcessInformation;
        private System.Windows.Forms.GroupBox groupBoxProgressInformation;
        private System.Windows.Forms.Label labelProcessTime;
        private System.Windows.Forms.Label labelProcessTimeValue;
        private System.Windows.Forms.ProgressBar progressBarProcessShare;
        private System.Windows.Forms.Label labelProcessShare;
        private System.Windows.Forms.ProgressBar progressBarTimeUntilIdle;
        private System.Windows.Forms.Label labelTimeUntilIdle;
    }
}

