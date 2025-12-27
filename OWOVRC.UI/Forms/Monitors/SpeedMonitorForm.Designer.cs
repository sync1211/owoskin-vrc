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
            speedTitle = new Label();
            speedLabel = new Label();
            groundedIndicator = new CheckBox();
            seatedIndicator = new CheckBox();
            playerStateGroup = new GroupBox();
            playerStateGroup.SuspendLayout();
            SuspendLayout();
            // 
            // velXLabel
            // 
            velXLabel.AutoSize = true;
            velXLabel.Location = new Point(76, 22);
            velXLabel.Name = "velXLabel";
            velXLabel.Size = new Size(58, 15);
            velXLabel.TabIndex = 0;
            velXLabel.Text = "Velocity X";
            // 
            // velYLabel
            // 
            velYLabel.AutoSize = true;
            velYLabel.Location = new Point(76, 37);
            velYLabel.Name = "velYLabel";
            velYLabel.Size = new Size(58, 15);
            velYLabel.TabIndex = 1;
            velYLabel.Text = "Velocity Y";
            // 
            // velZLabel
            // 
            velZLabel.AutoSize = true;
            velZLabel.Location = new Point(76, 52);
            velZLabel.Name = "velZLabel";
            velZLabel.Size = new Size(58, 15);
            velZLabel.TabIndex = 2;
            velZLabel.Text = "Velocity Z";
            // 
            // velZTitle
            // 
            velZTitle.AutoSize = true;
            velZTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            velZTitle.Location = new Point(12, 52);
            velZTitle.Name = "velZTitle";
            velZTitle.Size = new Size(61, 15);
            velZTitle.TabIndex = 5;
            velZTitle.Text = "Velocity Z";
            // 
            // velYTitle
            // 
            velYTitle.AutoSize = true;
            velYTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            velYTitle.Location = new Point(12, 37);
            velYTitle.Name = "velYTitle";
            velYTitle.Size = new Size(61, 15);
            velYTitle.TabIndex = 4;
            velYTitle.Text = "Velocity Y";
            // 
            // velXTitle
            // 
            velXTitle.AutoSize = true;
            velXTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            velXTitle.Location = new Point(12, 22);
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
            closeButton.Location = new Point(192, 231);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 23);
            closeButton.TabIndex = 6;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += CloseButton_Click;
            // 
            // notRunningIndicator
            // 
            notRunningIndicator.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            notRunningIndicator.AutoSize = true;
            notRunningIndicator.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            notRunningIndicator.Location = new Point(12, 228);
            notRunningIndicator.Name = "notRunningIndicator";
            notRunningIndicator.Size = new Size(166, 30);
            notRunningIndicator.TabIndex = 7;
            notRunningIndicator.Text = "Indicators will not be updated \r\nwhile OWOVRC is stopped.";
            // 
            // topDirectionIndicator
            // 
            topDirectionIndicator.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            topDirectionIndicator.DirectionIndicatorColor = Color.Red;
            topDirectionIndicator.Location = new Point(12, 91);
            topDirectionIndicator.MaxX = 100F;
            topDirectionIndicator.MaxY = 100F;
            topDirectionIndicator.Name = "topDirectionIndicator";
            topDirectionIndicator.Size = new Size(125, 125);
            topDirectionIndicator.TabIndex = 8;
            topDirectionIndicator.Text = "directionSpeedIndicator1";
            topDirectionIndicator.ThresholdColor = Color.Orange;
            topDirectionIndicator.ThresholdX = 100F;
            topDirectionIndicator.ThresholdY = 100F;
            topDirectionIndicator.ValueX = 0F;
            topDirectionIndicator.ValueY = 0F;
            // 
            // topDirectionTitle
            // 
            topDirectionTitle.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            topDirectionTitle.AutoSize = true;
            topDirectionTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            topDirectionTitle.Location = new Point(61, 71);
            topDirectionTitle.Name = "topDirectionTitle";
            topDirectionTitle.Size = new Size(27, 15);
            topDirectionTitle.TabIndex = 9;
            topDirectionTitle.Text = "Top";
            topDirectionTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // sideDirectionTitle
            // 
            sideDirectionTitle.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            sideDirectionTitle.AutoSize = true;
            sideDirectionTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sideDirectionTitle.Location = new Point(195, 71);
            sideDirectionTitle.Name = "sideDirectionTitle";
            sideDirectionTitle.Size = new Size(31, 15);
            sideDirectionTitle.TabIndex = 11;
            sideDirectionTitle.Text = "Side";
            sideDirectionTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // sideDirectionIndicator
            // 
            sideDirectionIndicator.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            sideDirectionIndicator.DirectionIndicatorColor = Color.Red;
            sideDirectionIndicator.Location = new Point(143, 91);
            sideDirectionIndicator.MaxX = 100F;
            sideDirectionIndicator.MaxY = 100F;
            sideDirectionIndicator.Name = "sideDirectionIndicator";
            sideDirectionIndicator.Size = new Size(125, 125);
            sideDirectionIndicator.TabIndex = 10;
            sideDirectionIndicator.Text = "directionSpeedIndicator1";
            sideDirectionIndicator.ThresholdColor = Color.Orange;
            sideDirectionIndicator.ThresholdX = 100F;
            sideDirectionIndicator.ThresholdY = 100F;
            sideDirectionIndicator.ValueX = 0F;
            sideDirectionIndicator.ValueY = 0F;
            // 
            // speedTitle
            // 
            speedTitle.AutoSize = true;
            speedTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            speedTitle.Location = new Point(12, 7);
            speedTitle.Name = "speedTitle";
            speedTitle.Size = new Size(42, 15);
            speedTitle.TabIndex = 13;
            speedTitle.Text = "Speed";
            // 
            // speedLabel
            // 
            speedLabel.AutoSize = true;
            speedLabel.Location = new Point(76, 7);
            speedLabel.Name = "speedLabel";
            speedLabel.Size = new Size(39, 15);
            speedLabel.TabIndex = 12;
            speedLabel.Text = "Speed";
            // 
            // groundedIndicator
            // 
            groundedIndicator.AutoCheck = false;
            groundedIndicator.AutoSize = true;
            groundedIndicator.Location = new Point(10, 15);
            groundedIndicator.Name = "groundedIndicator";
            groundedIndicator.Size = new Size(79, 19);
            groundedIndicator.TabIndex = 16;
            groundedIndicator.Text = "Grounded";
            groundedIndicator.UseVisualStyleBackColor = true;
            // 
            // seatedIndicator
            // 
            seatedIndicator.AutoCheck = false;
            seatedIndicator.AutoSize = true;
            seatedIndicator.Location = new Point(10, 36);
            seatedIndicator.Name = "seatedIndicator";
            seatedIndicator.Size = new Size(61, 19);
            seatedIndicator.TabIndex = 17;
            seatedIndicator.Text = "Seated";
            seatedIndicator.UseVisualStyleBackColor = true;
            // 
            // playerStateGroup
            // 
            playerStateGroup.Controls.Add(seatedIndicator);
            playerStateGroup.Controls.Add(groundedIndicator);
            playerStateGroup.Location = new Point(162, 7);
            playerStateGroup.Name = "playerStateGroup";
            playerStateGroup.Size = new Size(105, 61);
            playerStateGroup.TabIndex = 18;
            playerStateGroup.TabStop = false;
            playerStateGroup.Text = "Player state";
            // 
            // SpeedMonitorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = closeButton;
            ClientSize = new Size(279, 266);
            Controls.Add(playerStateGroup);
            Controls.Add(speedTitle);
            Controls.Add(speedLabel);
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
            Name = "SpeedMonitorForm";
            Text = "Player Velocity";
            FormClosing += SpeedMonitorForm_FormClosing;
            Shown += SpeedMonitorForm_Shown;
            playerStateGroup.ResumeLayout(false);
            playerStateGroup.PerformLayout();
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
        private Label speedTitle;
        private Label speedLabel;
        private CheckBox groundedIndicator;
        private CheckBox seatedIndicator;
        private GroupBox playerStateGroup;
    }
}