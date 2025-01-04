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
            subBassIndicatorLeft = new Audio.WinForms.Controls.BarIndicator();
            bandsBoxLeft = new GroupBox();
            leftTrebleDBLabel = new Label();
            LeftTrebleTitle = new Label();
            trebleIndicatorLeft = new Audio.WinForms.Controls.BarIndicator();
            leftBassDBLabel = new Label();
            leftBassTitle = new Label();
            bassIndicatorLeft = new Audio.WinForms.Controls.BarIndicator();
            leftSubBassDBLabel = new Label();
            bandsBoxRight = new GroupBox();
            rightTrebleDBLabel = new Label();
            rightBassDBLabel = new Label();
            rightTrebleTitle = new Label();
            rightBassTitle = new Label();
            trebleIndicatorRight = new Audio.WinForms.Controls.BarIndicator();
            bassIndicatorRight = new Audio.WinForms.Controls.BarIndicator();
            rightSubBassDBLabel = new Label();
            rightSubBassTitle = new Label();
            subBassIndicatorRight = new Audio.WinForms.Controls.BarIndicator();
            label2 = new Label();
            maxDBLabel = new Label();
            bandsBoxLeft.SuspendLayout();
            bandsBoxRight.SuspendLayout();
            SuspendLayout();
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            closeButton.Location = new Point(415, 265);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 23);
            closeButton.TabIndex = 0;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += CloseButton_Click;
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
            bandsBoxLeft.Size = new Size(239, 244);
            bandsBoxLeft.TabIndex = 7;
            bandsBoxLeft.TabStop = false;
            bandsBoxLeft.Text = "Left";
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
            bandsBoxRight.Controls.Add(rightTrebleDBLabel);
            bandsBoxRight.Controls.Add(rightBassDBLabel);
            bandsBoxRight.Controls.Add(rightTrebleTitle);
            bandsBoxRight.Controls.Add(rightBassTitle);
            bandsBoxRight.Controls.Add(trebleIndicatorRight);
            bandsBoxRight.Controls.Add(bassIndicatorRight);
            bandsBoxRight.Controls.Add(rightSubBassDBLabel);
            bandsBoxRight.Controls.Add(rightSubBassTitle);
            bandsBoxRight.Controls.Add(subBassIndicatorRight);
            bandsBoxRight.Location = new Point(258, 12);
            bandsBoxRight.Name = "bandsBoxRight";
            bandsBoxRight.Size = new Size(232, 244);
            bandsBoxRight.TabIndex = 17;
            bandsBoxRight.TabStop = false;
            bandsBoxRight.Text = "Right";
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
            ClientSize = new Size(502, 300);
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
    }
}