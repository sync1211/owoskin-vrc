namespace OWOVRC.UI.Forms
{
    partial class AudioMonitorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioMonitorForm));
            closeButton = new Button();
            label16 = new Label();
            bassIndicatorLeft = new Audio.WinForms.Controls.BarIndicator();
            groupBox2 = new GroupBox();
            groupBox1 = new GroupBox();
            label1 = new Label();
            bassIndicatorRight = new Audio.WinForms.Controls.BarIndicator();
            label2 = new Label();
            maxDBLabel = new Label();
            rightBassDBLabel = new Label();
            leftBassDBLabel = new Label();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            closeButton.Location = new Point(128, 265);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 23);
            closeButton.TabIndex = 0;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += CloseButton_Click;
            // 
            // label16
            // 
            label16.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label16.Location = new Point(18, 19);
            label16.Name = "label16";
            label16.Size = new Size(62, 15);
            label16.TabIndex = 16;
            label16.Text = "Bass";
            label16.TextAlign = ContentAlignment.TopCenter;
            // 
            // bassIndicatorLeft
            // 
            bassIndicatorLeft.BarColor = Color.Orange;
            bassIndicatorLeft.Enabled = false;
            bassIndicatorLeft.Location = new Point(18, 37);
            bassIndicatorLeft.Max = 100;
            bassIndicatorLeft.Min = 0;
            bassIndicatorLeft.Name = "bassIndicatorLeft";
            bassIndicatorLeft.Size = new Size(63, 181);
            bassIndicatorLeft.TabIndex = 15;
            bassIndicatorLeft.Value = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(leftBassDBLabel);
            groupBox2.Controls.Add(label16);
            groupBox2.Controls.Add(bassIndicatorLeft);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(94, 244);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Left";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rightBassDBLabel);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(bassIndicatorRight);
            groupBox1.Location = new Point(112, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(92, 244);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Right";
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(16, 19);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 16;
            label1.Text = "Bass";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // bassIndicatorRight
            // 
            bassIndicatorRight.BarColor = Color.Orange;
            bassIndicatorRight.Enabled = false;
            bassIndicatorRight.Location = new Point(16, 37);
            bassIndicatorRight.Max = 100;
            bassIndicatorRight.Min = 0;
            bassIndicatorRight.Name = "bassIndicatorRight";
            bassIndicatorRight.Size = new Size(62, 181);
            bassIndicatorRight.TabIndex = 15;
            bassIndicatorRight.Value = 0;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 269);
            label2.Name = "label2";
            label2.Size = new Size(34, 15);
            label2.TabIndex = 18;
            label2.Text = "Max:";
            // 
            // maxDBLabel
            // 
            maxDBLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            maxDBLabel.AutoSize = true;
            maxDBLabel.Location = new Point(48, 269);
            maxDBLabel.Name = "maxDBLabel";
            maxDBLabel.Size = new Size(27, 15);
            maxDBLabel.TabIndex = 19;
            maxDBLabel.Text = "0db";
            // 
            // rightBassDBLabel
            // 
            rightBassDBLabel.Location = new Point(16, 221);
            rightBassDBLabel.Name = "rightBassDBLabel";
            rightBassDBLabel.Size = new Size(62, 18);
            rightBassDBLabel.TabIndex = 17;
            rightBassDBLabel.Text = "0db";
            rightBassDBLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // leftBassDBLabel
            // 
            leftBassDBLabel.Location = new Point(18, 221);
            leftBassDBLabel.Name = "leftBassDBLabel";
            leftBassDBLabel.Size = new Size(63, 18);
            leftBassDBLabel.TabIndex = 18;
            leftBassDBLabel.Text = "0db";
            leftBassDBLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // AudioMonitorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(215, 300);
            Controls.Add(maxDBLabel);
            Controls.Add(label2);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Controls.Add(closeButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AudioMonitorForm";
            Text = "Monitor";
            FormClosing += AudioMonitorForm_FormClosing;
            Shown += AudioMonitorForm_Shown;
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button closeButton;
        private Label label16;
        private Audio.WinForms.Controls.BarIndicator bassIndicatorLeft;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private Label label1;
        private Audio.WinForms.Controls.BarIndicator bassIndicatorRight;
        private Label label2;
        private Label maxDBLabel;
        private Label leftBassDBLabel;
        private Label rightBassDBLabel;
    }
}