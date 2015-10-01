namespace Worker.Views
{
    partial class WorkloadView
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
            this.labelForStatus = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.jobIdLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelForStatus
            // 
            this.labelForStatus.AutoSize = true;
            this.labelForStatus.Location = new System.Drawing.Point(13, 13);
            this.labelForStatus.Name = "labelForStatus";
            this.labelForStatus.Size = new System.Drawing.Size(97, 13);
            this.labelForStatus.TabIndex = 0;
            this.labelForStatus.Text = "Current Job Status:";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(117, 13);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(35, 13);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Current Job Id:";
            // 
            // jobIdLabel
            // 
            this.jobIdLabel.AutoSize = true;
            this.jobIdLabel.Location = new System.Drawing.Point(120, 30);
            this.jobIdLabel.Name = "jobIdLabel";
            this.jobIdLabel.Size = new System.Drawing.Size(35, 13);
            this.jobIdLabel.TabIndex = 3;
            this.jobIdLabel.Text = "label2";
            // 
            // WorkloadView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 178);
            this.Controls.Add(this.jobIdLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.labelForStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "WorkloadView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "proggr worker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelForStatus;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label jobIdLabel;
    }
}

