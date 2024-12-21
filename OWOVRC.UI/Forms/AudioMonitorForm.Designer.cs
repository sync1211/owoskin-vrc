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
            subBassIndicatorLeft = new Audio.WinForms.Controls.BarIndicator();
            bandsBoxLeft = new GroupBox();
            leftSubBassDBLabel = new Label();
            bandsBoxRight = new GroupBox();
            rightSubBassDBLabel = new Label();
            label1 = new Label();
            subBassIndicatorRight = new Audio.WinForms.Controls.BarIndicator();
            label2 = new Label();
            maxDBLabel = new Label();
            leftBassDBLabel = new Label();
            label4 = new Label();
            bassIndicatorLeft = new Audio.WinForms.Controls.BarIndicator();
            rightBassDBLabel = new Label();
            label6 = new Label();
            bassIndicatorRight = new Audio.WinForms.Controls.BarIndicator();
            bandsBoxLeft.SuspendLayout();
            bandsBoxRight.SuspendLayout();
            SuspendLayout();
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            closeButton.Location = new Point(270, 265);
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
            label16.Text = "Sub-Bass";
            label16.TextAlign = ContentAlignment.TopCenter;
            // 
            // subBassIndicatorLeft
            // 
            subBassIndicatorLeft.BarColor = Color.Orange;
            subBassIndicatorLeft.Enabled = false;
            subBassIndicatorLeft.Location = new Point(18, 37);
            subBassIndicatorLeft.Max = 100;
            subBassIndicatorLeft.Min = 0;
            subBassIndicatorLeft.Name = "subBassIndicatorLeft";
            subBassIndicatorLeft.Size = new Size(63, 181);
            subBassIndicatorLeft.TabIndex = 15;
            subBassIndicatorLeft.Value = 0;
            // 
            // bandsBoxLeft
            // 
            bandsBoxLeft.Controls.Add(leftBassDBLabel);
            bandsBoxLeft.Controls.Add(label4);
            bandsBoxLeft.Controls.Add(bassIndicatorLeft);
            bandsBoxLeft.Controls.Add(leftSubBassDBLabel);
            bandsBoxLeft.Controls.Add(label16);
            bandsBoxLeft.Controls.Add(subBassIndicatorLeft);
            bandsBoxLeft.Location = new Point(12, 12);
            bandsBoxLeft.Name = "bandsBoxLeft";
            bandsBoxLeft.Size = new Size(162, 244);
            bandsBoxLeft.TabIndex = 7;
            bandsBoxLeft.TabStop = false;
            bandsBoxLeft.Text = "Left";
            // 
            // leftSubBassDBLabel
            // 
            leftSubBassDBLabel.Location = new Point(18, 221);
            leftSubBassDBLabel.Name = "leftSubBassDBLabel";
            leftSubBassDBLabel.Size = new Size(63, 18);
            leftSubBassDBLabel.TabIndex = 18;
            leftSubBassDBLabel.Text = "0db";
            leftSubBassDBLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // bandsBoxRight
            // 
            bandsBoxRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bandsBoxRight.Controls.Add(rightBassDBLabel);
            bandsBoxRight.Controls.Add(label6);
            bandsBoxRight.Controls.Add(bassIndicatorRight);
            bandsBoxRight.Controls.Add(rightSubBassDBLabel);
            bandsBoxRight.Controls.Add(label1);
            bandsBoxRight.Controls.Add(subBassIndicatorRight);
            bandsBoxRight.Location = new Point(184, 12);
            bandsBoxRight.Name = "bandsBoxRight";
            bandsBoxRight.Size = new Size(161, 244);
            bandsBoxRight.TabIndex = 17;
            bandsBoxRight.TabStop = false;
            bandsBoxRight.Text = "Right";
            // 
            // rightSubBassDBLabel
            // 
            rightSubBassDBLabel.Location = new Point(16, 221);
            rightSubBassDBLabel.Name = "rightSubBassDBLabel";
            rightSubBassDBLabel.Size = new Size(62, 18);
            rightSubBassDBLabel.TabIndex = 17;
            rightSubBassDBLabel.Text = "0db";
            rightSubBassDBLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(16, 19);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 16;
            label1.Text = "Sub-Bass";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // subBassIndicatorRight
            // 
            subBassIndicatorRight.BarColor = Color.Orange;
            subBassIndicatorRight.Enabled = false;
            subBassIndicatorRight.Location = new Point(16, 37);
            subBassIndicatorRight.Max = 100;
            subBassIndicatorRight.Min = 0;
            subBassIndicatorRight.Name = "subBassIndicatorRight";
            subBassIndicatorRight.Size = new Size(62, 181);
            subBassIndicatorRight.TabIndex = 15;
            subBassIndicatorRight.Value = 0;
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
            // leftBassDBLabel
            // 
            leftBassDBLabel.Location = new Point(87, 221);
            leftBassDBLabel.Name = "leftBassDBLabel";
            leftBassDBLabel.Size = new Size(63, 18);
            leftBassDBLabel.TabIndex = 21;
            leftBassDBLabel.Text = "0db";
            leftBassDBLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(87, 19);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 20;
            label4.Text = "Bass";
            label4.TextAlign = ContentAlignment.TopCenter;
            // 
            // bassIndicatorLeft
            // 
            bassIndicatorLeft.BarColor = Color.Orange;
            bassIndicatorLeft.Enabled = false;
            bassIndicatorLeft.Location = new Point(87, 37);
            bassIndicatorLeft.Max = 100;
            bassIndicatorLeft.Min = 0;
            bassIndicatorLeft.Name = "bassIndicatorLeft";
            bassIndicatorLeft.Size = new Size(63, 181);
            bassIndicatorLeft.TabIndex = 19;
            bassIndicatorLeft.Value = 0;
            // 
            // rightBassDBLabel
            // 
            rightBassDBLabel.Location = new Point(84, 221);
            rightBassDBLabel.Name = "rightBassDBLabel";
            rightBassDBLabel.Size = new Size(63, 18);
            rightBassDBLabel.TabIndex = 21;
            rightBassDBLabel.Text = "0db";
            rightBassDBLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label6.Location = new Point(84, 19);
            label6.Name = "label6";
            label6.Size = new Size(62, 15);
            label6.TabIndex = 20;
            label6.Text = "Bass";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // bassIndicatorRight
            // 
            bassIndicatorRight.BarColor = Color.Orange;
            bassIndicatorRight.Enabled = false;
            bassIndicatorRight.Location = new Point(84, 37);
            bassIndicatorRight.Max = 100;
            bassIndicatorRight.Min = 0;
            bassIndicatorRight.Name = "bassIndicatorRight";
            bassIndicatorRight.Size = new Size(63, 181);
            bassIndicatorRight.TabIndex = 19;
            bassIndicatorRight.Value = 0;
            // 
            // AudioMonitorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(357, 300);
            Controls.Add(maxDBLabel);
            Controls.Add(label2);
            Controls.Add(bandsBoxRight);
            Controls.Add(bandsBoxLeft);
            Controls.Add(closeButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AudioMonitorForm";
            Text = "Monitor";
            FormClosing += AudioMonitorForm_FormClosing;
            Shown += AudioMonitorForm_Shown;
            bandsBoxLeft.ResumeLayout(false);
            bandsBoxRight.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button closeButton;
        private Label label16;
        private Audio.WinForms.Controls.BarIndicator subBassIndicatorLeft;
        private GroupBox bandsBoxLeft;
        private GroupBox bandsBoxRight;
        private Label label1;
        private Audio.WinForms.Controls.BarIndicator subBassIndicatorRight;
        private Label label2;
        private Label maxDBLabel;
        private Label leftSubBassDBLabel;
        private Label rightSubBassDBLabel;
        private Label leftBassDBLabel;
        private Label label4;
        private Audio.WinForms.Controls.BarIndicator bassIndicatorLeft;
        private Label rightBassDBLabel;
        private Label label6;
        private Audio.WinForms.Controls.BarIndicator bassIndicatorRight;
    }
}