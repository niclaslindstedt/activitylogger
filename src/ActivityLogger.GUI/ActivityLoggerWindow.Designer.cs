namespace ActivityLogger.GUI
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
            this.labelActiveTime = new System.Windows.Forms.Label();
            this.progressBarActiveTime = new System.Windows.Forms.ProgressBar();
            this.labelActiveTimeValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelActiveTime
            // 
            this.labelActiveTime.AutoSize = true;
            this.labelActiveTime.Location = new System.Drawing.Point(13, 12);
            this.labelActiveTime.Name = "labelActiveTime";
            this.labelActiveTime.Size = new System.Drawing.Size(62, 13);
            this.labelActiveTime.TabIndex = 0;
            this.labelActiveTime.Text = "Active time:";
            // 
            // progressBarActiveTime
            // 
            this.progressBarActiveTime.Location = new System.Drawing.Point(172, 12);
            this.progressBarActiveTime.Name = "progressBarActiveTime";
            this.progressBarActiveTime.Size = new System.Drawing.Size(100, 13);
            this.progressBarActiveTime.TabIndex = 1;
            // 
            // label_activeTimeValue
            // 
            this.labelActiveTimeValue.AutoSize = true;
            this.labelActiveTimeValue.Location = new System.Drawing.Point(82, 12);
            this.labelActiveTimeValue.Name = "labelActiveTimeValue";
            this.labelActiveTimeValue.Size = new System.Drawing.Size(0, 13);
            this.labelActiveTimeValue.TabIndex = 2;
            // 
            // ActivityLoggerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.labelActiveTimeValue);
            this.Controls.Add(this.progressBarActiveTime);
            this.Controls.Add(this.labelActiveTime);
            this.Name = "ActivityLoggerWindow";
            this.Text = "Activity Logger (c) Niclas Lindstedt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelActiveTime;
        private System.Windows.Forms.ProgressBar progressBarActiveTime;
        private System.Windows.Forms.Label labelActiveTimeValue;
    }
}

