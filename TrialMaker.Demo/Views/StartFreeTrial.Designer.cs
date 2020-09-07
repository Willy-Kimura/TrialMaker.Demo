namespace TrialMakerDemo.CSharp.Views
{
    partial class StartFreeTrial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartFreeTrial));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblContent = new System.Windows.Forms.Label();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.btnBeginFreeTrial = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semilight", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(97, 116);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(260, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Start your 30-day free trial!";
            // 
            // lblContent
            // 
            this.lblContent.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblContent.AutoSize = true;
            this.lblContent.Location = new System.Drawing.Point(37, 158);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(381, 105);
            this.lblContent.TabIndex = 1;
            this.lblContent.Text = resources.GetString("lblContent.Text");
            // 
            // pbIcon
            // 
            this.pbIcon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbIcon.Image = ((System.Drawing.Image)(resources.GetObject("pbIcon.Image")));
            this.pbIcon.Location = new System.Drawing.Point(177, 32);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(100, 79);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIcon.TabIndex = 2;
            this.pbIcon.TabStop = false;
            // 
            // btnBeginFreeTrial
            // 
            this.btnBeginFreeTrial.BackColor = System.Drawing.Color.Gold;
            this.btnBeginFreeTrial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBeginFreeTrial.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnBeginFreeTrial.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.btnBeginFreeTrial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBeginFreeTrial.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBeginFreeTrial.ForeColor = System.Drawing.Color.Black;
            this.btnBeginFreeTrial.Location = new System.Drawing.Point(38, 280);
            this.btnBeginFreeTrial.Name = "btnBeginFreeTrial";
            this.btnBeginFreeTrial.Size = new System.Drawing.Size(378, 36);
            this.btnBeginFreeTrial.TabIndex = 13;
            this.btnBeginFreeTrial.Text = "Continue";
            this.btnBeginFreeTrial.UseVisualStyleBackColor = false;
            // 
            // StartFreeTrial
            // 
            this.AcceptButton = this.btnBeginFreeTrial;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(454, 334);
            this.Controls.Add(this.btnBeginFreeTrial);
            this.Controls.Add(this.pbIcon);
            this.Controls.Add(this.lblContent);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "StartFreeTrial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Start Free Trial";
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.PictureBox pbIcon;
        private System.Windows.Forms.Button btnBeginFreeTrial;
    }
}