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
            closeButton = new Button();
            monitorButton = new Button();
            previewButton = new Button();
            speedTitle = new Label();
            speedLabel = new Label();
            sideDirectionTitle = new Label();
            sideDirectionIndicator = new OWOVRC.UI.Controls.DirectionSpeedIndicator();
            topDirectionTitle = new Label();
            topDirectionIndicator = new OWOVRC.UI.Controls.DirectionSpeedIndicator();
            notRunningIndicator = new Label();
            velZTitle = new Label();
            velYTitle = new Label();
            velXTitle = new Label();
            label13 = new Label();
            label14 = new Label();
            zSpeedInput = new NumericUpDown();
            ySpeedInput = new NumericUpDown();
            xSpeedInput = new NumericUpDown();
            startTestButton = new Button();
            stopTestButton = new Button();
            velZLabel = new Label();
            velYLabel = new Label();
            velXLabel = new Label();
            playerStateGroup = new GroupBox();
            groundedIndicator = new CheckBox();
            seatedIndicator = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)zSpeedInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ySpeedInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)xSpeedInput).BeginInit();
            playerStateGroup.SuspendLayout();
            SuspendLayout();
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.DialogResult = DialogResult.Cancel;
            closeButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            closeButton.Location = new Point(217, 314);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 23);
            closeButton.TabIndex = 6;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += CloseButton_Click;
            // 
            // monitorButton
            // 
            monitorButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            monitorButton.Location = new Point(74, 1);
            monitorButton.Name = "monitorButton";
            monitorButton.Size = new Size(75, 23);
            monitorButton.TabIndex = 19;
            monitorButton.Text = "Monitor";
            monitorButton.UseVisualStyleBackColor = true;
            monitorButton.Click += MonitorButton_Click;
            // 
            // previewButton
            // 
            previewButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            previewButton.Location = new Point(153, 1);
            previewButton.Name = "previewButton";
            previewButton.Size = new Size(75, 23);
            previewButton.TabIndex = 20;
            previewButton.Text = "Preview";
            previewButton.UseVisualStyleBackColor = true;
            previewButton.Click += PreviewButton_Click;
            // 
            // speedTitle
            // 
            speedTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            speedTitle.Location = new Point(14, 30);
            speedTitle.Name = "speedTitle";
            speedTitle.Size = new Size(64, 23);
            speedTitle.TabIndex = 63;
            speedTitle.Text = "Speed";
            speedTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // speedLabel
            // 
            speedLabel.Location = new Point(84, 30);
            speedLabel.Name = "speedLabel";
            speedLabel.Size = new Size(62, 23);
            speedLabel.TabIndex = 62;
            speedLabel.Text = "Speed";
            speedLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // sideDirectionTitle
            // 
            sideDirectionTitle.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            sideDirectionTitle.AutoSize = true;
            sideDirectionTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sideDirectionTitle.Location = new Point(206, 135);
            sideDirectionTitle.Name = "sideDirectionTitle";
            sideDirectionTitle.Size = new Size(31, 15);
            sideDirectionTitle.TabIndex = 61;
            sideDirectionTitle.Text = "Side";
            sideDirectionTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // sideDirectionIndicator
            // 
            sideDirectionIndicator.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            sideDirectionIndicator.DirectionIndicatorColor = Color.Red;
            sideDirectionIndicator.Location = new Point(157, 163);
            sideDirectionIndicator.MaxX = 100F;
            sideDirectionIndicator.MaxY = 100F;
            sideDirectionIndicator.Name = "sideDirectionIndicator";
            sideDirectionIndicator.Size = new Size(125, 125);
            sideDirectionIndicator.TabIndex = 60;
            sideDirectionIndicator.Text = "directionSpeedIndicator1";
            sideDirectionIndicator.ThresholdColor = Color.Orange;
            sideDirectionIndicator.ThresholdX = 100F;
            sideDirectionIndicator.ThresholdY = 100F;
            sideDirectionIndicator.ValueX = 0F;
            sideDirectionIndicator.ValueY = 0F;
            // 
            // topDirectionTitle
            // 
            topDirectionTitle.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            topDirectionTitle.AutoSize = true;
            topDirectionTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            topDirectionTitle.Location = new Point(63, 135);
            topDirectionTitle.Name = "topDirectionTitle";
            topDirectionTitle.Size = new Size(27, 15);
            topDirectionTitle.TabIndex = 59;
            topDirectionTitle.Text = "Top";
            topDirectionTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // topDirectionIndicator
            // 
            topDirectionIndicator.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            topDirectionIndicator.DirectionIndicatorColor = Color.Red;
            topDirectionIndicator.Location = new Point(13, 163);
            topDirectionIndicator.MaxX = 100F;
            topDirectionIndicator.MaxY = 100F;
            topDirectionIndicator.Name = "topDirectionIndicator";
            topDirectionIndicator.Size = new Size(125, 125);
            topDirectionIndicator.TabIndex = 58;
            topDirectionIndicator.Text = "directionSpeedIndicator1";
            topDirectionIndicator.ThresholdColor = Color.Orange;
            topDirectionIndicator.ThresholdX = 100F;
            topDirectionIndicator.ThresholdY = 100F;
            topDirectionIndicator.ValueX = 0F;
            topDirectionIndicator.ValueY = 0F;
            // 
            // notRunningIndicator
            // 
            notRunningIndicator.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            notRunningIndicator.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            notRunningIndicator.Location = new Point(12, 308);
            notRunningIndicator.Name = "notRunningIndicator";
            notRunningIndicator.Size = new Size(166, 32);
            notRunningIndicator.TabIndex = 57;
            notRunningIndicator.Text = "OWOVRC is stopped;\r\nthis dialog will not be usable.\r\n";
            // 
            // velZTitle
            // 
            velZTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            velZTitle.Location = new Point(14, 99);
            velZTitle.Name = "velZTitle";
            velZTitle.Size = new Size(61, 23);
            velZTitle.TabIndex = 56;
            velZTitle.Text = "Velocity Z";
            velZTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // velYTitle
            // 
            velYTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            velYTitle.Location = new Point(14, 76);
            velYTitle.Name = "velYTitle";
            velYTitle.Size = new Size(61, 23);
            velYTitle.TabIndex = 55;
            velYTitle.Text = "Velocity Y";
            velYTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // velXTitle
            // 
            velXTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            velXTitle.Location = new Point(14, 53);
            velXTitle.Name = "velXTitle";
            velXTitle.Size = new Size(64, 23);
            velXTitle.TabIndex = 54;
            velXTitle.Text = "Velocity X";
            velXTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label13.ImeMode = ImeMode.NoControl;
            label13.Location = new Point(69, 152);
            label13.Name = "label13";
            label13.Size = new Size(15, 15);
            label13.TabIndex = 71;
            label13.Text = "^";
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label14.ImeMode = ImeMode.NoControl;
            label14.Location = new Point(281, 218);
            label14.Name = "label14";
            label14.Size = new Size(15, 15);
            label14.TabIndex = 70;
            label14.Text = ">";
            // 
            // zSpeedInput
            // 
            zSpeedInput.DecimalPlaces = 2;
            zSpeedInput.Location = new Point(85, 101);
            zSpeedInput.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            zSpeedInput.Name = "zSpeedInput";
            zSpeedInput.Size = new Size(58, 23);
            zSpeedInput.TabIndex = 69;
            zSpeedInput.ValueChanged += SpeedInput_ValueChanged;
            // 
            // ySpeedInput
            // 
            ySpeedInput.DecimalPlaces = 2;
            ySpeedInput.Location = new Point(85, 78);
            ySpeedInput.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            ySpeedInput.Name = "ySpeedInput";
            ySpeedInput.Size = new Size(58, 23);
            ySpeedInput.TabIndex = 68;
            ySpeedInput.ValueChanged += SpeedInput_ValueChanged;
            // 
            // xSpeedInput
            // 
            xSpeedInput.DecimalPlaces = 2;
            xSpeedInput.Location = new Point(85, 55);
            xSpeedInput.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            xSpeedInput.Name = "xSpeedInput";
            xSpeedInput.Size = new Size(58, 23);
            xSpeedInput.TabIndex = 67;
            xSpeedInput.ValueChanged += SpeedInput_ValueChanged;
            // 
            // startTestButton
            // 
            startTestButton.Image = Properties.Resources.Play;
            startTestButton.ImeMode = ImeMode.NoControl;
            startTestButton.Location = new Point(262, 30);
            startTestButton.Name = "startTestButton";
            startTestButton.Size = new Size(34, 32);
            startTestButton.TabIndex = 66;
            startTestButton.UseVisualStyleBackColor = true;
            startTestButton.Click += StartTestButton_Click;
            // 
            // stopTestButton
            // 
            stopTestButton.Image = Properties.Resources.Pause;
            stopTestButton.ImeMode = ImeMode.NoControl;
            stopTestButton.Location = new Point(262, 30);
            stopTestButton.Name = "stopTestButton";
            stopTestButton.Size = new Size(34, 32);
            stopTestButton.TabIndex = 65;
            stopTestButton.UseVisualStyleBackColor = true;
            stopTestButton.Visible = false;
            stopTestButton.Click += StopTestButton_Click;
            // 
            // velZLabel
            // 
            velZLabel.Location = new Point(84, 99);
            velZLabel.Name = "velZLabel";
            velZLabel.Size = new Size(62, 23);
            velZLabel.TabIndex = 53;
            velZLabel.Text = "Velocity Z";
            velZLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // velYLabel
            // 
            velYLabel.Location = new Point(84, 76);
            velYLabel.Name = "velYLabel";
            velYLabel.Size = new Size(62, 23);
            velYLabel.TabIndex = 52;
            velYLabel.Text = "Velocity Y";
            velYLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // velXLabel
            // 
            velXLabel.Location = new Point(84, 53);
            velXLabel.Name = "velXLabel";
            velXLabel.Size = new Size(62, 23);
            velXLabel.TabIndex = 51;
            velXLabel.Text = "Velocity X";
            velXLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // playerStateGroup
            // 
            playerStateGroup.Controls.Add(groundedIndicator);
            playerStateGroup.Controls.Add(seatedIndicator);
            playerStateGroup.Location = new Point(191, 30);
            playerStateGroup.Name = "playerStateGroup";
            playerStateGroup.Size = new Size(105, 61);
            playerStateGroup.TabIndex = 64;
            playerStateGroup.TabStop = false;
            playerStateGroup.Text = "Player state";
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
            // SpeedMonitorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = closeButton;
            ClientSize = new Size(304, 349);
            Controls.Add(speedTitle);
            Controls.Add(speedLabel);
            Controls.Add(startTestButton);
            Controls.Add(stopTestButton);
            Controls.Add(sideDirectionTitle);
            Controls.Add(sideDirectionIndicator);
            Controls.Add(topDirectionTitle);
            Controls.Add(topDirectionIndicator);
            Controls.Add(notRunningIndicator);
            Controls.Add(velZTitle);
            Controls.Add(velYTitle);
            Controls.Add(velXTitle);
            Controls.Add(label13);
            Controls.Add(label14);
            Controls.Add(zSpeedInput);
            Controls.Add(ySpeedInput);
            Controls.Add(xSpeedInput);
            Controls.Add(velZLabel);
            Controls.Add(velYLabel);
            Controls.Add(velXLabel);
            Controls.Add(playerStateGroup);
            Controls.Add(previewButton);
            Controls.Add(monitorButton);
            Controls.Add(closeButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "SpeedMonitorForm";
            Text = "Player Velocity";
            FormClosing += SpeedMonitorForm_FormClosing;
            Shown += SpeedMonitorForm_Shown;
            ((System.ComponentModel.ISupportInitialize)zSpeedInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)ySpeedInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)xSpeedInput).EndInit();
            playerStateGroup.ResumeLayout(false);
            playerStateGroup.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button closeButton;
        private Button monitorButton;
        private Button previewButton;
        private Label speedTitle;
        private Label speedLabel;
        private Label sideDirectionTitle;
        private Controls.DirectionSpeedIndicator sideDirectionIndicator;
        private Label topDirectionTitle;
        private Controls.DirectionSpeedIndicator topDirectionIndicator;
        private Label notRunningIndicator;
        private Label velZTitle;
        private Label velYTitle;
        private Label velXTitle;
        private Label label13;
        private Label label14;
        private NumericUpDown zSpeedInput;
        private NumericUpDown ySpeedInput;
        private NumericUpDown xSpeedInput;
        private Button startTestButton;
        private Button stopTestButton;
        private Label velZLabel;
        private Label velYLabel;
        private Label velXLabel;
        private GroupBox playerStateGroup;
        private CheckBox seatedIndicator;
        private CheckBox groundedIndicator;
    }
}