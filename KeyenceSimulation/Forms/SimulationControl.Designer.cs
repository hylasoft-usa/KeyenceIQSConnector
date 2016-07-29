namespace KeyenceSimulation.Forms
{
  partial class SimulationControl
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
      this.StartButton = new System.Windows.Forms.Button();
      this.StopButton = new System.Windows.Forms.Button();
      this.SendButton = new System.Windows.Forms.Button();
      this.IpInput = new System.Windows.Forms.TextBox();
      this.PortInput = new System.Windows.Forms.TextBox();
      this.StatusInput = new System.Windows.Forms.TextBox();
      this.StatusLabel = new System.Windows.Forms.Label();
      this.IpLabel = new System.Windows.Forms.Label();
      this.PortLabel = new System.Windows.Forms.Label();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.InfoBoxLabel = new System.Windows.Forms.Label();
      this.InfoBox = new System.Windows.Forms.ListBox();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // StartButton
      // 
      this.StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.StartButton.Location = new System.Drawing.Point(251, 177);
      this.StartButton.Name = "StartButton";
      this.StartButton.Size = new System.Drawing.Size(110, 38);
      this.StartButton.TabIndex = 0;
      this.StartButton.Text = "Start Service";
      this.StartButton.UseVisualStyleBackColor = true;
      // 
      // StopButton
      // 
      this.StopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.StopButton.Location = new System.Drawing.Point(393, 177);
      this.StopButton.Name = "StopButton";
      this.StopButton.Size = new System.Drawing.Size(110, 38);
      this.StopButton.TabIndex = 1;
      this.StopButton.Text = "Stop Service";
      this.StopButton.UseVisualStyleBackColor = true;
      // 
      // SendButton
      // 
      this.SendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.SendButton.Location = new System.Drawing.Point(540, 177);
      this.SendButton.Name = "SendButton";
      this.SendButton.Size = new System.Drawing.Size(169, 38);
      this.SendButton.TabIndex = 8;
      this.SendButton.Text = "Send Message";
      this.SendButton.UseVisualStyleBackColor = true;
      // 
      // IpInput
      // 
      this.IpInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.IpInput.Location = new System.Drawing.Point(129, 87);
      this.IpInput.Name = "IpInput";
      this.IpInput.Size = new System.Drawing.Size(757, 22);
      this.IpInput.TabIndex = 2;
      // 
      // PortInput
      // 
      this.PortInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.PortInput.Location = new System.Drawing.Point(129, 136);
      this.PortInput.Name = "PortInput";
      this.PortInput.Size = new System.Drawing.Size(757, 22);
      this.PortInput.TabIndex = 3;
      // 
      // StatusInput
      // 
      this.StatusInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.StatusInput.Enabled = false;
      this.StatusInput.Location = new System.Drawing.Point(129, 37);
      this.StatusInput.Name = "StatusInput";
      this.StatusInput.Size = new System.Drawing.Size(757, 22);
      this.StatusInput.TabIndex = 4;
      // 
      // StatusLabel
      // 
      this.StatusLabel.AutoSize = true;
      this.StatusLabel.Location = new System.Drawing.Point(25, 40);
      this.StatusLabel.Name = "StatusLabel";
      this.StatusLabel.Size = new System.Drawing.Size(98, 17);
      this.StatusLabel.TabIndex = 5;
      this.StatusLabel.Text = "Server Status:";
      // 
      // IpLabel
      // 
      this.IpLabel.AutoSize = true;
      this.IpLabel.Location = new System.Drawing.Point(53, 90);
      this.IpLabel.Name = "IpLabel";
      this.IpLabel.Size = new System.Drawing.Size(70, 17);
      this.IpLabel.TabIndex = 6;
      this.IpLabel.Text = "Server IP:";
      // 
      // PortLabel
      // 
      this.PortLabel.AutoSize = true;
      this.PortLabel.Location = new System.Drawing.Point(39, 139);
      this.PortLabel.Name = "PortLabel";
      this.PortLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.PortLabel.Size = new System.Drawing.Size(84, 17);
      this.PortLabel.TabIndex = 7;
      this.PortLabel.Text = "Server Port:";
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.InfoBox);
      this.splitContainer1.Panel1.Controls.Add(this.InfoBoxLabel);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.SendButton);
      this.splitContainer1.Panel2.Controls.Add(this.StatusLabel);
      this.splitContainer1.Panel2.Controls.Add(this.PortLabel);
      this.splitContainer1.Panel2.Controls.Add(this.StartButton);
      this.splitContainer1.Panel2.Controls.Add(this.IpLabel);
      this.splitContainer1.Panel2.Controls.Add(this.StopButton);
      this.splitContainer1.Panel2.Controls.Add(this.IpInput);
      this.splitContainer1.Panel2.Controls.Add(this.StatusInput);
      this.splitContainer1.Panel2.Controls.Add(this.PortInput);
      this.splitContainer1.Size = new System.Drawing.Size(965, 590);
      this.splitContainer1.SplitterDistance = 361;
      this.splitContainer1.TabIndex = 1;
      // 
      // InfoBoxLabel
      // 
      this.InfoBoxLabel.AutoSize = true;
      this.InfoBoxLabel.Location = new System.Drawing.Point(12, 9);
      this.InfoBoxLabel.Name = "InfoBoxLabel";
      this.InfoBoxLabel.Size = new System.Drawing.Size(133, 17);
      this.InfoBoxLabel.TabIndex = 1;
      this.InfoBoxLabel.Text = "Last Message Sent:";
      // 
      // InfoBox
      // 
      this.InfoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.InfoBox.FormattingEnabled = true;
      this.InfoBox.ItemHeight = 16;
      this.InfoBox.Location = new System.Drawing.Point(15, 30);
      this.InfoBox.Name = "InfoBox";
      this.InfoBox.Size = new System.Drawing.Size(938, 324);
      this.InfoBox.TabIndex = 2;
      // 
      // SimulationControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.ClientSize = new System.Drawing.Size(965, 590);
      this.Controls.Add(this.splitContainer1);
      this.MaximizeBox = false;
      this.Name = "SimulationControl";
      this.Text = "Simulation Control";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button StartButton;
    private System.Windows.Forms.Button StopButton;
    private System.Windows.Forms.Button SendButton;
    private System.Windows.Forms.Label PortLabel;
    private System.Windows.Forms.Label IpLabel;
    private System.Windows.Forms.Label StatusLabel;
    private System.Windows.Forms.TextBox StatusInput;
    private System.Windows.Forms.TextBox PortInput;
    private System.Windows.Forms.TextBox IpInput;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Label InfoBoxLabel;
    private System.Windows.Forms.ListBox InfoBox;

  }
}