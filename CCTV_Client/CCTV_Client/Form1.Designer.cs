namespace CCTV_Client
{
    partial class CCTVClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CCTVClient));
            this.CamImageBox = new Emgu.CV.UI.ImageBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.IPBox = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.PortBox = new System.Windows.Forms.TextBox();
            this.IPLabel = new System.Windows.Forms.Label();
            this.Config = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.CamImageBox)).BeginInit();
            this.Config.SuspendLayout();
            this.SuspendLayout();
            // 
            // CamImageBox
            // 
            resources.ApplyResources(this.CamImageBox, "CamImageBox");
            this.CamImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CamImageBox.Name = "CamImageBox";
            this.CamImageBox.TabStop = false;
            // 
            // StartButton
            // 
            resources.ApplyResources(this.StartButton, "StartButton");
            this.StartButton.Name = "StartButton";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // IPBox
            // 
            resources.ApplyResources(this.IPBox, "IPBox");
            this.IPBox.Name = "IPBox";
            // 
            // PortLabel
            // 
            resources.ApplyResources(this.PortLabel, "PortLabel");
            this.PortLabel.Name = "PortLabel";
            // 
            // PortBox
            // 
            resources.ApplyResources(this.PortBox, "PortBox");
            this.PortBox.Name = "PortBox";
            // 
            // IPLabel
            // 
            resources.ApplyResources(this.IPLabel, "IPLabel");
            this.IPLabel.Name = "IPLabel";
            // 
            // Config
            // 
            resources.ApplyResources(this.Config, "Config");
            this.Config.Controls.Add(this.IPLabel);
            this.Config.Controls.Add(this.IPBox);
            this.Config.Controls.Add(this.PortBox);
            this.Config.Controls.Add(this.PortLabel);
            this.Config.Name = "Config";
            this.Config.TabStop = false;
            // 
            // CCTVClient
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Config);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.CamImageBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CCTVClient";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.CamImageBox)).EndInit();
            this.Config.ResumeLayout(false);
            this.Config.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox CamImageBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox IPBox;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.TextBox PortBox;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.GroupBox Config;
    }
}

