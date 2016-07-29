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
      this.MainPanel = new System.Windows.Forms.Panel();
      this.PortLabel = new System.Windows.Forms.Label();
      this.IpLabel = new System.Windows.Forms.Label();
      this.StatusLabel = new System.Windows.Forms.Label();
      this.StatusInput = new System.Windows.Forms.TextBox();
      this.PortInput = new System.Windows.Forms.TextBox();
      this.IpInput = new System.Windows.Forms.TextBox();
      this.StopButton = new System.Windows.Forms.Button();
      this.StartButton = new System.Windows.Forms.Button();
      this.SendButton = new System.Windows.Forms.Button();
      this.MainPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // MainPanel
      // 
      this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.MainPanel.Controls.Add(this.SendButton);
      this.MainPanel.Controls.Add(this.PortLabel);
      this.MainPanel.Controls.Add(this.IpLabel);
      this.MainPanel.Controls.Add(this.StatusLabel);
      this.MainPanel.Controls.Add(this.StatusInput);
      this.MainPanel.Controls.Add(this.PortInput);
      this.MainPanel.Controls.Add(this.IpInput);
      this.MainPanel.Controls.Add(this.StopButton);
      this.MainPanel.Controls.Add(this.StartButton);
      this.MainPanel.Location = new System.Drawing.Point(13, 13);
      this.MainPanel.Name = "MainPanel";
      this.MainPanel.Size = new System.Drawing.Size(515, 257);
      this.MainPanel.TabIndex = 0;
      // 
      // PortLabel
      // 
      this.PortLabel.AutoSize = true;
      this.PortLabel.Location = new System.Drawing.Point(38, 134);
      this.PortLabel.Name = "PortLabel";
      this.PortLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.PortLabel.Size = new System.Drawing.Size(84, 17);
      this.PortLabel.TabIndex = 7;
      this.PortLabel.Text = "Server Port:";
      // 
      // IpLabel
      // 
      this.IpLabel.AutoSize = true;
      this.IpLabel.Location = new System.Drawing.Point(52, 85);
      this.IpLabel.Name = "IpLabel";
      this.IpLabel.Size = new System.Drawing.Size(70, 17);
      this.IpLabel.TabIndex = 6;
      this.IpLabel.Text = "Server IP:";
      // 
      // StatusLabel
      // 
      this.StatusLabel.AutoSize = true;
      this.StatusLabel.Location = new System.Drawing.Point(24, 35);
      this.StatusLabel.Name = "StatusLabel";
      this.StatusLabel.Size = new System.Drawing.Size(98, 17);
      this.StatusLabel.TabIndex = 5;
      this.StatusLabel.Text = "Server Status:";
      // 
      // StatusInput
      // 
      this.StatusInput.Enabled = false;
      this.StatusInput.Location = new System.Drawing.Point(128, 32);
      this.StatusInput.Name = "StatusInput";
      this.StatusInput.Size = new System.Drawing.Size(332, 22);
      this.StatusInput.TabIndex = 4;
      // 
      // PortInput
      // 
      this.PortInput.Location = new System.Drawing.Point(128, 131);
      this.PortInput.Name = "PortInput";
      this.PortInput.Size = new System.Drawing.Size(332, 22);
      this.PortInput.TabIndex = 3;
      // 
      // IpInput
      // 
      this.IpInput.Location = new System.Drawing.Point(128, 82);
      this.IpInput.Name = "IpInput";
      this.IpInput.Size = new System.Drawing.Size(332, 22);
      this.IpInput.TabIndex = 2;
      // 
      // StopButton
      // 
      this.StopButton.Location = new System.Drawing.Point(193, 198);
      this.StopButton.Name = "StopButton";
      this.StopButton.Size = new System.Drawing.Size(110, 38);
      this.StopButton.TabIndex = 1;
      this.StopButton.Text = "Stop Service";
      this.StopButton.UseVisualStyleBackColor = true;
      // 
      // StartButton
      // 
      this.StartButton.Location = new System.Drawing.Point(41, 198);
      this.StartButton.Name = "StartButton";
      this.StartButton.Size = new System.Drawing.Size(110, 38);
      this.StartButton.TabIndex = 0;
      this.StartButton.Text = "Start Service";
      this.StartButton.UseVisualStyleBackColor = true;
      // 
      // SendButton
      // 
      this.SendButton.Location = new System.Drawing.Point(350, 198);
      this.SendButton.Name = "SendButton";
      this.SendButton.Size = new System.Drawing.Size(110, 38);
      this.SendButton.TabIndex = 8;
      this.SendButton.Text = "Send Message";
      this.SendButton.UseVisualStyleBackColor = true;
      // 
      // SimulationControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(540, 282);
      this.Controls.Add(this.MainPanel);
      this.Name = "SimulationControl";
      this.Text = "Simulation Control";
      this.MainPanel.ResumeLayout(false);
      this.MainPanel.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel MainPanel;
    private System.Windows.Forms.Button StopButton;
    private System.Windows.Forms.Button StartButton;
    private System.Windows.Forms.Label PortLabel;
    private System.Windows.Forms.Label IpLabel;
    private System.Windows.Forms.Label StatusLabel;
    private System.Windows.Forms.TextBox StatusInput;
    private System.Windows.Forms.TextBox PortInput;
    private System.Windows.Forms.TextBox IpInput;
    private System.Windows.Forms.Button SendButton;
  }
}