namespace OWOVRC.UI.Forms
{
    partial class SpeedTestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpeedTestForm));
            playerZBar = new TrackBar();
            playerXBar = new TrackBar();
            sideDirectionIndicator = new OWOVRC.UI.Controls.DirectionSpeedIndicator();
            topDirectionTitle = new Label();
            topDirectionIndicator = new OWOVRC.UI.Controls.DirectionSpeedIndicator();
            sideDirectionTitle = new Label();
            playerZBar2 = new TrackBar();
            playerYBar = new TrackBar();
            closeButton = new Button();
            startTestButton = new Button();
            stopTestButton = new Button();
            xSpeedLabel = new Label();
            xSpeedInput = new NumericUpDown();
            ySpeedInput = new NumericUpDown();
            ySpeedLabel = new Label();
            zSpeedInput = new NumericUpDown();
            zSpeedLabel = new Label();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)playerZBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)playerXBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)playerZBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)playerYBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)xSpeedInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ySpeedInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)zSpeedInput).BeginInit();
            SuspendLayout();
            // 
            // playerZBar
            // 
            playerZBar.LargeChange = 50;
            resources.ApplyResources(playerZBar, "playerZBar");
            playerZBar.Maximum = 100;
            playerZBar.Minimum = -100;
            playerZBar.Name = "playerZBar";
            playerZBar.SmallChange = 10;
            playerZBar.TickFrequency = 10;
            playerZBar.TickStyle = TickStyle.TopLeft;
            playerZBar.Scroll += PlayerZBar_Scroll;
            // 
            // playerXBar
            // 
            playerXBar.LargeChange = 50;
            resources.ApplyResources(playerXBar, "playerXBar");
            playerXBar.Maximum = 100;
            playerXBar.Minimum = -100;
            playerXBar.Name = "playerXBar";
            playerXBar.SmallChange = 10;
            playerXBar.TickFrequency = 10;
            playerXBar.Scroll += PlayerXBar_Scroll;
            // 
            // sideDirectionIndicator
            // 
            resources.ApplyResources(sideDirectionIndicator, "sideDirectionIndicator");
            sideDirectionIndicator.Name = "sideDirectionIndicator";
            // 
            // topDirectionTitle
            // 
            resources.ApplyResources(topDirectionTitle, "topDirectionTitle");
            topDirectionTitle.Name = "topDirectionTitle";
            // 
            // topDirectionIndicator
            // 
            resources.ApplyResources(topDirectionIndicator, "topDirectionIndicator");
            topDirectionIndicator.Name = "topDirectionIndicator";
            // 
            // sideDirectionTitle
            // 
            resources.ApplyResources(sideDirectionTitle, "sideDirectionTitle");
            sideDirectionTitle.Name = "sideDirectionTitle";
            // 
            // playerZBar2
            // 
            playerZBar2.LargeChange = 50;
            resources.ApplyResources(playerZBar2, "playerZBar2");
            playerZBar2.Maximum = 100;
            playerZBar2.Minimum = -100;
            playerZBar2.Name = "playerZBar2";
            playerZBar2.SmallChange = 10;
            playerZBar2.TickFrequency = 10;
            playerZBar2.Scroll += PlayerZBar2_Scroll;
            // 
            // playerYBar
            // 
            playerYBar.LargeChange = 50;
            resources.ApplyResources(playerYBar, "playerYBar");
            playerYBar.Maximum = 100;
            playerYBar.Minimum = -100;
            playerYBar.Name = "playerYBar";
            playerYBar.SmallChange = 10;
            playerYBar.TickFrequency = 10;
            playerYBar.TickStyle = TickStyle.TopLeft;
            playerYBar.Scroll += PlayerYBar_Scroll;
            // 
            // closeButton
            // 
            closeButton.DialogResult = DialogResult.Cancel;
            resources.ApplyResources(closeButton, "closeButton");
            closeButton.Name = "closeButton";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += CloseButton_Click;
            // 
            // startTestButton
            // 
            startTestButton.Image = Properties.Resources.Play;
            resources.ApplyResources(startTestButton, "startTestButton");
            startTestButton.Name = "startTestButton";
            startTestButton.UseVisualStyleBackColor = true;
            startTestButton.Click += StartTestButton_Click;
            // 
            // stopTestButton
            // 
            stopTestButton.Image = Properties.Resources.Pause;
            resources.ApplyResources(stopTestButton, "stopTestButton");
            stopTestButton.Name = "stopTestButton";
            stopTestButton.UseVisualStyleBackColor = true;
            stopTestButton.Click += StopTestButton_Click;
            // 
            // xSpeedLabel
            // 
            resources.ApplyResources(xSpeedLabel, "xSpeedLabel");
            xSpeedLabel.Name = "xSpeedLabel";
            // 
            // xSpeedInput
            // 
            xSpeedInput.DecimalPlaces = 2;
            resources.ApplyResources(xSpeedInput, "xSpeedInput");
            xSpeedInput.Name = "xSpeedInput";
            xSpeedInput.ValueChanged += XSpeedInput_ValueChanged;
            // 
            // ySpeedInput
            // 
            ySpeedInput.DecimalPlaces = 2;
            resources.ApplyResources(ySpeedInput, "ySpeedInput");
            ySpeedInput.Name = "ySpeedInput";
            ySpeedInput.ValueChanged += YSpeedInput_ValueChanged;
            // 
            // ySpeedLabel
            // 
            resources.ApplyResources(ySpeedLabel, "ySpeedLabel");
            ySpeedLabel.Name = "ySpeedLabel";
            // 
            // zSpeedInput
            // 
            zSpeedInput.DecimalPlaces = 2;
            resources.ApplyResources(zSpeedInput, "zSpeedInput");
            zSpeedInput.Name = "zSpeedInput";
            zSpeedInput.ValueChanged += ZSpeedInput_ValueChanged;
            // 
            // zSpeedLabel
            // 
            resources.ApplyResources(zSpeedLabel, "zSpeedLabel");
            zSpeedLabel.Name = "zSpeedLabel";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // SpeedTestForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(zSpeedInput);
            Controls.Add(zSpeedLabel);
            Controls.Add(ySpeedInput);
            Controls.Add(ySpeedLabel);
            Controls.Add(xSpeedInput);
            Controls.Add(xSpeedLabel);
            Controls.Add(startTestButton);
            Controls.Add(stopTestButton);
            Controls.Add(closeButton);
            Controls.Add(playerZBar2);
            Controls.Add(sideDirectionTitle);
            Controls.Add(sideDirectionIndicator);
            Controls.Add(topDirectionTitle);
            Controls.Add(topDirectionIndicator);
            Controls.Add(playerZBar);
            Controls.Add(label2);
            Controls.Add(playerXBar);
            Controls.Add(label1);
            Controls.Add(playerYBar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SpeedTestForm";
            Shown += SpeedTestForm_Shown;
            ((System.ComponentModel.ISupportInitialize)playerZBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)playerXBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)playerZBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)playerYBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)xSpeedInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)ySpeedInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)zSpeedInput).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar playerZBar;
        private TrackBar playerXBar;
        private Controls.DirectionSpeedIndicator sideDirectionIndicator;
        private Label topDirectionTitle;
        private Controls.DirectionSpeedIndicator topDirectionIndicator;
        private Label sideDirectionTitle;
        private TrackBar playerZBar2;
        private TrackBar playerYBar;
        private Button closeButton;
        private Button startTestButton;
        private Button stopTestButton;
        private Label xSpeedLabel;
        private NumericUpDown xSpeedInput;
        private NumericUpDown ySpeedInput;
        private Label ySpeedLabel;
        private NumericUpDown zSpeedInput;
        private Label zSpeedLabel;
        private Label label1;
        private Label label2;
    }
}