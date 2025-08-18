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
            leftSubBassTitle = new Label();
            subBassIndicatorLeft = new OWOVRC.Audio.WinForms.Controls.BarIndicator();
            bandsBoxLeft = new GroupBox();
            leftMidDBLabel = new Label();
            leftMidTitle = new Label();
            midIndicatorLeft = new OWOVRC.Audio.WinForms.Controls.BarIndicator();
            leftLowMidDBLabel = new Label();
            leftLowMidTitle = new Label();
            lowMidIndicatorLeft = new OWOVRC.Audio.WinForms.Controls.BarIndicator();
            leftTrebleDBLabel = new Label();
            LeftTrebleTitle = new Label();
            trebleIndicatorLeft = new OWOVRC.Audio.WinForms.Controls.BarIndicator();
            leftBassDBLabel = new Label();
            leftBassTitle = new Label();
            bassIndicatorLeft = new OWOVRC.Audio.WinForms.Controls.BarIndicator();
            leftSubBassDBLabel = new Label();
            bandsBoxRight = new GroupBox();
            rightMidDBLabel = new Label();
            rightMidTitle = new Label();
            midIndicatorRight = new OWOVRC.Audio.WinForms.Controls.BarIndicator();
            rightLowMidDBLabel = new Label();
            rightLowMidTitle = new Label();
            lowMidIndicatorRight = new OWOVRC.Audio.WinForms.Controls.BarIndicator();
            rightTrebleDBLabel = new Label();
            rightBassDBLabel = new Label();
            rightTrebleTitle = new Label();
            rightBassTitle = new Label();
            trebleIndicatorRight = new OWOVRC.Audio.WinForms.Controls.BarIndicator();
            bassIndicatorRight = new OWOVRC.Audio.WinForms.Controls.BarIndicator();
            rightSubBassDBLabel = new Label();
            rightSubBassTitle = new Label();
            subBassIndicatorRight = new OWOVRC.Audio.WinForms.Controls.BarIndicator();
            label2 = new Label();
            maxDBLabel = new Label();
            bandsBoxLeft.SuspendLayout();
            bandsBoxRight.SuspendLayout();
            SuspendLayout();
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.DialogResult = DialogResult.OK;
            closeButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            closeButton.Location = new Point(690, 265);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 23);
            closeButton.TabIndex = 0;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            // 
            // leftSubBassTitle
            // 
            leftSubBassTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            leftSubBassTitle.Location = new Point(18, 19);
            leftSubBassTitle.Name = "leftSubBassTitle";
            leftSubBassTitle.Size = new Size(62, 15);
            leftSubBassTitle.TabIndex = 16;
            leftSubBassTitle.Text = "Sub-Bass";
            leftSubBassTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // subBassIndicatorLeft
            // 
            subBassIndicatorLeft.BarColor = Color.Firebrick;
            subBassIndicatorLeft.Enabled = false;
            subBassIndicatorLeft.IndicatorColor = Color.Gray;
            subBassIndicatorLeft.IndicatorValue = 75;
            subBassIndicatorLeft.Location = new Point(18, 37);
            subBassIndicatorLeft.Max = 100;
            subBassIndicatorLeft.Min = 0;
            subBassIndicatorLeft.Name = "subBassIndicatorLeft";
            subBassIndicatorLeft.Size = new Size(63, 181);
            subBassIndicatorLeft.TabIndex = 15;
            subBassIndicatorLeft.Value = 50;
            // 
            // bandsBoxLeft
            // 
            bandsBoxLeft.Controls.Add(leftMidDBLabel);
            bandsBoxLeft.Controls.Add(leftMidTitle);
            bandsBoxLeft.Controls.Add(midIndicatorLeft);
            bandsBoxLeft.Controls.Add(leftLowMidDBLabel);
            bandsBoxLeft.Controls.Add(leftLowMidTitle);
            bandsBoxLeft.Controls.Add(lowMidIndicatorLeft);
            bandsBoxLeft.Controls.Add(leftTrebleDBLabel);
            bandsBoxLeft.Controls.Add(LeftTrebleTitle);
            bandsBoxLeft.Controls.Add(trebleIndicatorLeft);
            bandsBoxLeft.Controls.Add(leftBassDBLabel);
            bandsBoxLeft.Controls.Add(leftBassTitle);
            bandsBoxLeft.Controls.Add(bassIndicatorLeft);
            bandsBoxLeft.Controls.Add(leftSubBassDBLabel);
            bandsBoxLeft.Controls.Add(leftSubBassTitle);
            bandsBoxLeft.Controls.Add(subBassIndicatorLeft);
            bandsBoxLeft.Location = new Point(12, 12);
            bandsBoxLeft.Name = "bandsBoxLeft";
            bandsBoxLeft.Size = new Size(376, 244);
            bandsBoxLeft.TabIndex = 7;
            bandsBoxLeft.TabStop = false;
            bandsBoxLeft.Text = "Left";
            // 
            // leftMidDBLabel
            // 
            leftMidDBLabel.Font = new Font("Segoe UI", 9F);
            leftMidDBLabel.Location = new Point(293, 221);
            leftMidDBLabel.Name = "leftMidDBLabel";
            leftMidDBLabel.Size = new Size(63, 18);
            leftMidDBLabel.TabIndex = 30;
            leftMidDBLabel.Text = "0db";
            leftMidDBLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // leftMidTitle
            // 
            leftMidTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            leftMidTitle.Location = new Point(293, 19);
            leftMidTitle.Name = "leftMidTitle";
            leftMidTitle.Size = new Size(62, 15);
            leftMidTitle.TabIndex = 29;
            leftMidTitle.Text = "Mid";
            leftMidTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // midIndicatorLeft
            // 
            midIndicatorLeft.BarColor = Color.DarkOrange;
            midIndicatorLeft.Enabled = false;
            midIndicatorLeft.IndicatorColor = Color.Gray;
            midIndicatorLeft.IndicatorValue = 75;
            midIndicatorLeft.Location = new Point(293, 37);
            midIndicatorLeft.Max = 100;
            midIndicatorLeft.Min = 0;
            midIndicatorLeft.Name = "midIndicatorLeft";
            midIndicatorLeft.Size = new Size(63, 181);
            midIndicatorLeft.TabIndex = 28;
            midIndicatorLeft.Value = 50;
            // 
            // leftLowMidDBLabel
            // 
            leftLowMidDBLabel.Font = new Font("Segoe UI", 9F);
            leftLowMidDBLabel.Location = new Point(225, 221);
            leftLowMidDBLabel.Name = "leftLowMidDBLabel";
            leftLowMidDBLabel.Size = new Size(63, 18);
            leftLowMidDBLabel.TabIndex = 27;
            leftLowMidDBLabel.Text = "0db";
            leftLowMidDBLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // leftLowMidTitle
            // 
            leftLowMidTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            leftLowMidTitle.Location = new Point(225, 19);
            leftLowMidTitle.Name = "leftLowMidTitle";
            leftLowMidTitle.Size = new Size(62, 15);
            leftLowMidTitle.TabIndex = 26;
            leftLowMidTitle.Text = "Low-Mid";
            leftLowMidTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // lowMidIndicatorLeft
            // 
            lowMidIndicatorLeft.BarColor = Color.MidnightBlue;
            lowMidIndicatorLeft.Enabled = false;
            lowMidIndicatorLeft.IndicatorColor = Color.Gray;
            lowMidIndicatorLeft.IndicatorValue = 75;
            lowMidIndicatorLeft.Location = new Point(225, 37);
            lowMidIndicatorLeft.Max = 100;
            lowMidIndicatorLeft.Min = 0;
            lowMidIndicatorLeft.Name = "lowMidIndicatorLeft";
            lowMidIndicatorLeft.Size = new Size(63, 181);
            lowMidIndicatorLeft.TabIndex = 25;
            lowMidIndicatorLeft.Value = 50;
            // 
            // leftTrebleDBLabel
            // 
            leftTrebleDBLabel.Font = new Font("Segoe UI", 9F);
            leftTrebleDBLabel.Location = new Point(156, 221);
            leftTrebleDBLabel.Name = "leftTrebleDBLabel";
            leftTrebleDBLabel.Size = new Size(63, 18);
            leftTrebleDBLabel.TabIndex = 24;
            leftTrebleDBLabel.Text = "0db";
            leftTrebleDBLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // LeftTrebleTitle
            // 
            LeftTrebleTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LeftTrebleTitle.Location = new Point(156, 19);
            LeftTrebleTitle.Name = "LeftTrebleTitle";
            LeftTrebleTitle.Size = new Size(62, 15);
            LeftTrebleTitle.TabIndex = 23;
            LeftTrebleTitle.Text = "Treble";
            LeftTrebleTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // trebleIndicatorLeft
            // 
            trebleIndicatorLeft.BarColor = Color.LightBlue;
            trebleIndicatorLeft.Enabled = false;
            trebleIndicatorLeft.IndicatorColor = Color.Gray;
            trebleIndicatorLeft.IndicatorValue = 75;
            trebleIndicatorLeft.Location = new Point(156, 37);
            trebleIndicatorLeft.Max = 100;
            trebleIndicatorLeft.Min = 0;
            trebleIndicatorLeft.Name = "trebleIndicatorLeft";
            trebleIndicatorLeft.Size = new Size(63, 181);
            trebleIndicatorLeft.TabIndex = 22;
            trebleIndicatorLeft.Value = 50;
            // 
            // leftBassDBLabel
            // 
            leftBassDBLabel.Font = new Font("Segoe UI", 9F);
            leftBassDBLabel.Location = new Point(87, 221);
            leftBassDBLabel.Name = "leftBassDBLabel";
            leftBassDBLabel.Size = new Size(63, 18);
            leftBassDBLabel.TabIndex = 21;
            leftBassDBLabel.Text = "0db";
            leftBassDBLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // leftBassTitle
            // 
            leftBassTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            leftBassTitle.Location = new Point(87, 19);
            leftBassTitle.Name = "leftBassTitle";
            leftBassTitle.Size = new Size(62, 15);
            leftBassTitle.TabIndex = 20;
            leftBassTitle.Text = "Bass";
            leftBassTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // bassIndicatorLeft
            // 
            bassIndicatorLeft.BarColor = Color.DarkRed;
            bassIndicatorLeft.Enabled = false;
            bassIndicatorLeft.IndicatorColor = Color.Gray;
            bassIndicatorLeft.IndicatorValue = 75;
            bassIndicatorLeft.Location = new Point(87, 37);
            bassIndicatorLeft.Max = 100;
            bassIndicatorLeft.Min = 0;
            bassIndicatorLeft.Name = "bassIndicatorLeft";
            bassIndicatorLeft.Size = new Size(63, 181);
            bassIndicatorLeft.TabIndex = 19;
            bassIndicatorLeft.Value = 50;
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
            bandsBoxRight.Controls.Add(rightMidDBLabel);
            bandsBoxRight.Controls.Add(rightMidTitle);
            bandsBoxRight.Controls.Add(midIndicatorRight);
            bandsBoxRight.Controls.Add(rightLowMidDBLabel);
            bandsBoxRight.Controls.Add(rightLowMidTitle);
            bandsBoxRight.Controls.Add(lowMidIndicatorRight);
            bandsBoxRight.Controls.Add(rightTrebleDBLabel);
            bandsBoxRight.Controls.Add(rightBassDBLabel);
            bandsBoxRight.Controls.Add(rightTrebleTitle);
            bandsBoxRight.Controls.Add(rightBassTitle);
            bandsBoxRight.Controls.Add(trebleIndicatorRight);
            bandsBoxRight.Controls.Add(bassIndicatorRight);
            bandsBoxRight.Controls.Add(rightSubBassDBLabel);
            bandsBoxRight.Controls.Add(rightSubBassTitle);
            bandsBoxRight.Controls.Add(subBassIndicatorRight);
            bandsBoxRight.Location = new Point(395, 12);
            bandsBoxRight.Name = "bandsBoxRight";
            bandsBoxRight.Size = new Size(370, 244);
            bandsBoxRight.TabIndex = 17;
            bandsBoxRight.TabStop = false;
            bandsBoxRight.Text = "Right";
            // 
            // rightMidDBLabel
            // 
            rightMidDBLabel.Font = new Font("Segoe UI", 9F);
            rightMidDBLabel.Location = new Point(290, 221);
            rightMidDBLabel.Name = "rightMidDBLabel";
            rightMidDBLabel.Size = new Size(63, 18);
            rightMidDBLabel.TabIndex = 33;
            rightMidDBLabel.Text = "0db";
            rightMidDBLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // rightMidTitle
            // 
            rightMidTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            rightMidTitle.Location = new Point(290, 19);
            rightMidTitle.Name = "rightMidTitle";
            rightMidTitle.Size = new Size(62, 15);
            rightMidTitle.TabIndex = 32;
            rightMidTitle.Text = "Mid";
            rightMidTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // midIndicatorRight
            // 
            midIndicatorRight.BarColor = Color.DarkOrange;
            midIndicatorRight.Enabled = false;
            midIndicatorRight.IndicatorColor = Color.Gray;
            midIndicatorRight.IndicatorValue = 75;
            midIndicatorRight.Location = new Point(290, 37);
            midIndicatorRight.Max = 100;
            midIndicatorRight.Min = 0;
            midIndicatorRight.Name = "midIndicatorRight";
            midIndicatorRight.Size = new Size(63, 181);
            midIndicatorRight.TabIndex = 31;
            midIndicatorRight.Value = 50;
            // 
            // rightLowMidDBLabel
            // 
            rightLowMidDBLabel.Font = new Font("Segoe UI", 9F);
            rightLowMidDBLabel.Location = new Point(221, 221);
            rightLowMidDBLabel.Name = "rightLowMidDBLabel";
            rightLowMidDBLabel.Size = new Size(63, 18);
            rightLowMidDBLabel.TabIndex = 30;
            rightLowMidDBLabel.Text = "0db";
            rightLowMidDBLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // rightLowMidTitle
            // 
            rightLowMidTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            rightLowMidTitle.Location = new Point(221, 19);
            rightLowMidTitle.Name = "rightLowMidTitle";
            rightLowMidTitle.Size = new Size(62, 15);
            rightLowMidTitle.TabIndex = 29;
            rightLowMidTitle.Text = "Low-Mid";
            rightLowMidTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // lowMidIndicatorRight
            // 
            lowMidIndicatorRight.BarColor = Color.MidnightBlue;
            lowMidIndicatorRight.Enabled = false;
            lowMidIndicatorRight.IndicatorColor = Color.Gray;
            lowMidIndicatorRight.IndicatorValue = 75;
            lowMidIndicatorRight.Location = new Point(221, 37);
            lowMidIndicatorRight.Max = 100;
            lowMidIndicatorRight.Min = 0;
            lowMidIndicatorRight.Name = "lowMidIndicatorRight";
            lowMidIndicatorRight.Size = new Size(63, 181);
            lowMidIndicatorRight.TabIndex = 28;
            lowMidIndicatorRight.Value = 50;
            // 
            // rightTrebleDBLabel
            // 
            rightTrebleDBLabel.Font = new Font("Segoe UI", 9F);
            rightTrebleDBLabel.Location = new Point(153, 221);
            rightTrebleDBLabel.Name = "rightTrebleDBLabel";
            rightTrebleDBLabel.Size = new Size(63, 18);
            rightTrebleDBLabel.TabIndex = 27;
            rightTrebleDBLabel.Text = "0db";
            rightTrebleDBLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // rightBassDBLabel
            // 
            rightBassDBLabel.Font = new Font("Segoe UI", 9F);
            rightBassDBLabel.Location = new Point(84, 221);
            rightBassDBLabel.Name = "rightBassDBLabel";
            rightBassDBLabel.Size = new Size(63, 18);
            rightBassDBLabel.TabIndex = 21;
            rightBassDBLabel.Text = "0db";
            rightBassDBLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // rightTrebleTitle
            // 
            rightTrebleTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            rightTrebleTitle.Location = new Point(153, 19);
            rightTrebleTitle.Name = "rightTrebleTitle";
            rightTrebleTitle.Size = new Size(62, 15);
            rightTrebleTitle.TabIndex = 26;
            rightTrebleTitle.Text = "Treble";
            rightTrebleTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // rightBassTitle
            // 
            rightBassTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            rightBassTitle.Location = new Point(84, 19);
            rightBassTitle.Name = "rightBassTitle";
            rightBassTitle.Size = new Size(62, 15);
            rightBassTitle.TabIndex = 20;
            rightBassTitle.Text = "Bass";
            rightBassTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // trebleIndicatorRight
            // 
            trebleIndicatorRight.BarColor = Color.LightBlue;
            trebleIndicatorRight.Enabled = false;
            trebleIndicatorRight.IndicatorColor = Color.Gray;
            trebleIndicatorRight.IndicatorValue = 75;
            trebleIndicatorRight.Location = new Point(153, 37);
            trebleIndicatorRight.Max = 100;
            trebleIndicatorRight.Min = 0;
            trebleIndicatorRight.Name = "trebleIndicatorRight";
            trebleIndicatorRight.Size = new Size(63, 181);
            trebleIndicatorRight.TabIndex = 25;
            trebleIndicatorRight.Value = 50;
            // 
            // bassIndicatorRight
            // 
            bassIndicatorRight.BarColor = Color.DarkRed;
            bassIndicatorRight.Enabled = false;
            bassIndicatorRight.IndicatorColor = Color.Gray;
            bassIndicatorRight.IndicatorValue = 75;
            bassIndicatorRight.Location = new Point(84, 37);
            bassIndicatorRight.Max = 100;
            bassIndicatorRight.Min = 0;
            bassIndicatorRight.Name = "bassIndicatorRight";
            bassIndicatorRight.Size = new Size(63, 181);
            bassIndicatorRight.TabIndex = 19;
            bassIndicatorRight.Value = 50;
            // 
            // rightSubBassDBLabel
            // 
            rightSubBassDBLabel.Font = new Font("Segoe UI", 9F);
            rightSubBassDBLabel.Location = new Point(16, 221);
            rightSubBassDBLabel.Name = "rightSubBassDBLabel";
            rightSubBassDBLabel.Size = new Size(62, 18);
            rightSubBassDBLabel.TabIndex = 17;
            rightSubBassDBLabel.Text = "0db";
            rightSubBassDBLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // rightSubBassTitle
            // 
            rightSubBassTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            rightSubBassTitle.Location = new Point(16, 19);
            rightSubBassTitle.Name = "rightSubBassTitle";
            rightSubBassTitle.Size = new Size(62, 15);
            rightSubBassTitle.TabIndex = 16;
            rightSubBassTitle.Text = "Sub-Bass";
            rightSubBassTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // subBassIndicatorRight
            // 
            subBassIndicatorRight.BarColor = Color.Firebrick;
            subBassIndicatorRight.Enabled = false;
            subBassIndicatorRight.IndicatorColor = Color.Gray;
            subBassIndicatorRight.IndicatorValue = 75;
            subBassIndicatorRight.Location = new Point(16, 37);
            subBassIndicatorRight.Max = 100;
            subBassIndicatorRight.Min = 0;
            subBassIndicatorRight.Name = "subBassIndicatorRight";
            subBassIndicatorRight.Size = new Size(62, 181);
            subBassIndicatorRight.TabIndex = 15;
            subBassIndicatorRight.Value = 50;
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
            maxDBLabel.Size = new Size(28, 15);
            maxDBLabel.TabIndex = 19;
            maxDBLabel.Text = "0db";
            // 
            // AudioMonitorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = closeButton;
            ClientSize = new Size(777, 300);
            Controls.Add(maxDBLabel);
            Controls.Add(label2);
            Controls.Add(bandsBoxRight);
            Controls.Add(bandsBoxLeft);
            Controls.Add(closeButton);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
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
        private Label leftSubBassTitle;
        private Audio.WinForms.Controls.BarIndicator subBassIndicatorLeft;
        private GroupBox bandsBoxLeft;
        private GroupBox bandsBoxRight;
        private Label rightSubBassTitle;
        private Audio.WinForms.Controls.BarIndicator subBassIndicatorRight;
        private Label label2;
        private Label maxDBLabel;
        private Label leftSubBassDBLabel;
        private Label rightSubBassDBLabel;
        private Label leftBassDBLabel;
        private Label leftBassTitle;
        private Audio.WinForms.Controls.BarIndicator bassIndicatorLeft;
        private Label rightBassDBLabel;
        private Label rightBassTitle;
        private Audio.WinForms.Controls.BarIndicator bassIndicatorRight;
        private Label leftTrebleDBLabel;
        private Label LeftTrebleTitle;
        private Audio.WinForms.Controls.BarIndicator trebleIndicatorLeft;
        private Label rightTrebleDBLabel;
        private Label rightTrebleTitle;
        private Audio.WinForms.Controls.BarIndicator trebleIndicatorRight;
        private Label leftMidDBLabel;
        private Audio.WinForms.Controls.BarIndicator midIndicatorLeft;
        private Label leftLowMidDBLabel;
        private Audio.WinForms.Controls.BarIndicator lowMidIndicatorLeft;
        private Label rightMidDBLabel;
        private Label rightMidTitle;
        private Audio.WinForms.Controls.BarIndicator midIndicatorRight;
        private Label rightLowMidDBLabel;
        private Label rightLowMidTitle;
        private Audio.WinForms.Controls.BarIndicator lowMidIndicatorRight;
        private Label leftMidTitle;
        private Label leftLowMidTitle;
    }
}