namespace OWOVRC.UI.Forms.Monitors
{
    partial class SpeedMonitorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpeedMonitorForm));
            velXLabel = new Label();
            velYLabel = new Label();
            velZLabel = new Label();
            velZTitle = new Label();
            velYTitle = new Label();
            velXTitle = new Label();
            closeButton = new Button();
            notRunningIndicator = new Label();
            topDirectionIndicator = new OWOVRC.UI.Controls.DirectionSpeedIndicator();
            topDirectionTitle = new Label();
            sideDirectionTitle = new Label();
            sideDirectionIndicator = new OWOVRC.UI.Controls.DirectionSpeedIndicator();
            SuspendLayout();
            // 
            // velXLabel
            // 
            velXLabel.AutoSize = true;
            velXLabel.Location = new Point(76, 9);
            velXLabel.Name = "velXLabel";
            velXLabel.Size = new Size(58, 15);
            velXLabel.TabIndex = 0;
            velXLabel.Text = "Velocity X";
            // 
            // velYLabel
            // 
            velYLabel.AutoSize = true;
            velYLabel.Location = new Point(76, 24);
            velYLabel.Name = "velYLabel";
            velYLabel.Size = new Size(58, 15);
            velYLabel.TabIndex = 1;
            velYLabel.Text = "Velocity Y";
            // 
            // velZLabel
            // 
            velZLabel.AutoSize = true;
            velZLabel.Location = new Point(76, 39);
            velZLabel.Name = "velZLabel";
            velZLabel.Size = new Size(58, 15);
            velZLabel.TabIndex = 2;
            velZLabel.Text = "Velocity Z";
            // 
            // velZTitle
            // 
            velZTitle.AutoSize = true;
            velZTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            velZTitle.Location = new Point(12, 39);
            velZTitle.Name = "velZTitle";
            velZTitle.Size = new Size(61, 15);
            velZTitle.TabIndex = 5;
            velZTitle.Text = "Velocity Z";
            // 
            // velYTitle
            // 
            velYTitle.AutoSize = true;
            velYTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            velYTitle.Location = new Point(12, 24);
            velYTitle.Name = "velYTitle";
            velYTitle.Size = new Size(61, 15);
            velYTitle.TabIndex = 4;
            velYTitle.Text = "Velocity Y";
            // 
            // velXTitle
            // 
            velXTitle.AutoSize = true;
            velXTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            velXTitle.Location = new Point(12, 9);
            velXTitle.Name = "velXTitle";
            velXTitle.Size = new Size(62, 15);
            velXTitle.TabIndex = 3;
            velXTitle.Text = "Velocity X";
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.DialogResult = DialogResult.Cancel;
            closeButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            closeButton.Location = new Point(192, 212);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 23);
            closeButton.TabIndex = 6;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += CloseButton_Click;
            // 
            // notRunningIndicator
            // 
            notRunningIndicator.AutoSize = true;
            notRunningIndicator.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            notRunningIndicator.Location = new Point(12, 209);
            notRunningIndicator.Name = "notRunningIndicator";
            notRunningIndicator.Size = new Size(166, 30);
            notRunningIndicator.TabIndex = 7;
            notRunningIndicator.Text = "Indicators will not be updated \r\nwhile OWOVRC is stopped.";
            // 
            // topDirectionIndicator
            // 
            topDirectionIndicator.DirectionIndicatorColor = Color.Red;
            topDirectionIndicator.Location = new Point(12, 79);
            topDirectionIndicator.MaxX = 100;
            topDirectionIndicator.MaxY = 100;
            topDirectionIndicator.Name = "topDirectionIndicator";
            topDirectionIndicator.Size = new Size(125, 125);
            topDirectionIndicator.TabIndex = 8;
            topDirectionIndicator.Text = "directionSpeedIndicator1";
            topDirectionIndicator.ValueX = 0;
            topDirectionIndicator.ValueY = 0;
            // 
            // topDirectionTitle
            // 
            topDirectionTitle.AutoSize = true;
            topDirectionTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            topDirectionTitle.Location = new Point(61, 61);
            topDirectionTitle.Name = "topDirectionTitle";
            topDirectionTitle.Size = new Size(27, 15);
            topDirectionTitle.TabIndex = 9;
            topDirectionTitle.Text = "Top";
            topDirectionTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // sideDirectionTitle
            // 
            sideDirectionTitle.AutoSize = true;
            sideDirectionTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sideDirectionTitle.Location = new Point(195, 61);
            sideDirectionTitle.Name = "sideDirectionTitle";
            sideDirectionTitle.Size = new Size(31, 15);
            sideDirectionTitle.TabIndex = 11;
            sideDirectionTitle.Text = "Side";
            sideDirectionTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // sideDirectionIndicator
            // 
            sideDirectionIndicator.DirectionIndicatorColor = Color.Red;
            sideDirectionIndicator.Location = new Point(143, 79);
            sideDirectionIndicator.MaxX = 100;
            sideDirectionIndicator.MaxY = 100;
            sideDirectionIndicator.Name = "sideDirectionIndicator";
            sideDirectionIndicator.Size = new Size(125, 125);
            sideDirectionIndicator.TabIndex = 10;
            sideDirectionIndicator.Text = "directionSpeedIndicator1";
            sideDirectionIndicator.ValueX = 0;
            sideDirectionIndicator.ValueY = 0;
            // 
            // SpeedMonitorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(279, 247);
            Controls.Add(sideDirectionTitle);
            Controls.Add(sideDirectionIndicator);
            Controls.Add(topDirectionTitle);
            Controls.Add(topDirectionIndicator);
            Controls.Add(notRunningIndicator);
            Controls.Add(closeButton);
            Controls.Add(velZTitle);
            Controls.Add(velYTitle);
            Controls.Add(velXTitle);
            Controls.Add(velZLabel);
            Controls.Add(velYLabel);
            Controls.Add(velXLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SpeedMonitorForm";
            Text = "Player Velocity";
            FormClosing += SpeedMonitorForm_FormClosing;
            Shown += SpeedMonitorForm_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label velXLabel;
        private Label velYLabel;
        private Label velZLabel;
        private Label velZTitle;
        private Label velYTitle;
        private Label velXTitle;
        private Button closeButton;
        private Label notRunningIndicator;
        private Controls.DirectionSpeedIndicator topDirectionIndicator;
        private Label topDirectionTitle;
        private Label sideDirectionTitle;
        private Controls.DirectionSpeedIndicator sideDirectionIndicator;
    }
}