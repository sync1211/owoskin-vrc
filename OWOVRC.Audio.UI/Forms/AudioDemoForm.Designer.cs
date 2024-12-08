namespace OWOVRC.Audio.UI
{
    partial class AudioDemoForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            startButton = new Button();
            stopButton = new Button();
            subBassIndicator = new TrackBar();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            label7 = new Label();
            brillianceIndicator = new TrackBar();
            label6 = new Label();
            presenceIndicator = new TrackBar();
            label5 = new Label();
            highMidIndicator = new TrackBar();
            label4 = new Label();
            midIndicator = new TrackBar();
            label3 = new Label();
            lowMidIndicator = new TrackBar();
            label1 = new Label();
            bassIndicator = new TrackBar();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)subBassIndicator).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)brillianceIndicator).BeginInit();
            ((System.ComponentModel.ISupportInitialize)presenceIndicator).BeginInit();
            ((System.ComponentModel.ISupportInitialize)highMidIndicator).BeginInit();
            ((System.ComponentModel.ISupportInitialize)midIndicator).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lowMidIndicator).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bassIndicator).BeginInit();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            startButton.Location = new Point(6, 22);
            startButton.Name = "startButton";
            startButton.Size = new Size(75, 23);
            startButton.TabIndex = 0;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += StartButton_Click;
            // 
            // stopButton
            // 
            stopButton.Enabled = false;
            stopButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            stopButton.Location = new Point(6, 51);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(75, 23);
            stopButton.TabIndex = 1;
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += StopButton_Click;
            // 
            // subBassIndicator
            // 
            subBassIndicator.Enabled = false;
            subBassIndicator.LargeChange = 1;
            subBassIndicator.Location = new Point(23, 22);
            subBassIndicator.Maximum = 100;
            subBassIndicator.Name = "subBassIndicator";
            subBassIndicator.Orientation = Orientation.Vertical;
            subBassIndicator.Size = new Size(45, 181);
            subBassIndicator.TabIndex = 4;
            subBassIndicator.TickStyle = TickStyle.None;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(startButton);
            groupBox1.Controls.Add(stopButton);
            groupBox1.Location = new Point(566, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(92, 82);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Control";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(brillianceIndicator);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(presenceIndicator);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(highMidIndicator);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(midIndicator);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(lowMidIndicator);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(bassIndicator);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(subBassIndicator);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(548, 227);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Indicators";
            // 
            // label7
            // 
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label7.Location = new Point(414, 206);
            label7.Name = "label7";
            label7.Size = new Size(62, 15);
            label7.TabIndex = 26;
            label7.Text = "Brilliance";
            label7.TextAlign = ContentAlignment.TopCenter;
            // 
            // brillianceIndicator
            // 
            brillianceIndicator.Enabled = false;
            brillianceIndicator.LargeChange = 1;
            brillianceIndicator.Location = new Point(431, 22);
            brillianceIndicator.Maximum = 100;
            brillianceIndicator.Name = "brillianceIndicator";
            brillianceIndicator.Orientation = Orientation.Vertical;
            brillianceIndicator.Size = new Size(45, 181);
            brillianceIndicator.TabIndex = 25;
            brillianceIndicator.TickStyle = TickStyle.None;
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label6.Location = new Point(346, 206);
            label6.Name = "label6";
            label6.Size = new Size(62, 15);
            label6.TabIndex = 24;
            label6.Text = "Presence";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // presenceIndicator
            // 
            presenceIndicator.Enabled = false;
            presenceIndicator.LargeChange = 1;
            presenceIndicator.Location = new Point(363, 22);
            presenceIndicator.Maximum = 100;
            presenceIndicator.Name = "presenceIndicator";
            presenceIndicator.Orientation = Orientation.Vertical;
            presenceIndicator.Size = new Size(45, 181);
            presenceIndicator.TabIndex = 23;
            presenceIndicator.TickStyle = TickStyle.None;
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.Location = new Point(278, 206);
            label5.Name = "label5";
            label5.Size = new Size(62, 15);
            label5.TabIndex = 22;
            label5.Text = "High-Mid";
            label5.TextAlign = ContentAlignment.TopCenter;
            // 
            // highMidIndicator
            // 
            highMidIndicator.Enabled = false;
            highMidIndicator.LargeChange = 1;
            highMidIndicator.Location = new Point(295, 22);
            highMidIndicator.Maximum = 100;
            highMidIndicator.Name = "highMidIndicator";
            highMidIndicator.Orientation = Orientation.Vertical;
            highMidIndicator.Size = new Size(45, 181);
            highMidIndicator.TabIndex = 21;
            highMidIndicator.TickStyle = TickStyle.None;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(210, 206);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 20;
            label4.Text = "Midrange";
            label4.TextAlign = ContentAlignment.TopCenter;
            // 
            // midIndicator
            // 
            midIndicator.Enabled = false;
            midIndicator.LargeChange = 1;
            midIndicator.Location = new Point(227, 22);
            midIndicator.Maximum = 100;
            midIndicator.Name = "midIndicator";
            midIndicator.Orientation = Orientation.Vertical;
            midIndicator.Size = new Size(45, 181);
            midIndicator.TabIndex = 19;
            midIndicator.TickStyle = TickStyle.None;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(142, 206);
            label3.Name = "label3";
            label3.Size = new Size(62, 15);
            label3.TabIndex = 18;
            label3.Text = "Low-Mid";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // lowMidIndicator
            // 
            lowMidIndicator.Enabled = false;
            lowMidIndicator.LargeChange = 1;
            lowMidIndicator.Location = new Point(159, 22);
            lowMidIndicator.Maximum = 100;
            lowMidIndicator.Name = "lowMidIndicator";
            lowMidIndicator.Orientation = Orientation.Vertical;
            lowMidIndicator.Size = new Size(45, 181);
            lowMidIndicator.TabIndex = 17;
            lowMidIndicator.TickStyle = TickStyle.None;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(74, 206);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 16;
            label1.Text = "Bass";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // bassIndicator
            // 
            bassIndicator.Enabled = false;
            bassIndicator.LargeChange = 1;
            bassIndicator.Location = new Point(91, 22);
            bassIndicator.Maximum = 100;
            bassIndicator.Name = "bassIndicator";
            bassIndicator.Orientation = Orientation.Vertical;
            bassIndicator.Size = new Size(45, 181);
            bassIndicator.TabIndex = 15;
            bassIndicator.TickStyle = TickStyle.None;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(6, 206);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 14;
            label2.Text = "Sub-Bass";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // AudioDemoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(669, 250);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "AudioDemoForm";
            ShowIcon = false;
            Text = "Audio Capture Test";
            FormClosing += AudioDemoForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)subBassIndicator).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)brillianceIndicator).EndInit();
            ((System.ComponentModel.ISupportInitialize)presenceIndicator).EndInit();
            ((System.ComponentModel.ISupportInitialize)highMidIndicator).EndInit();
            ((System.ComponentModel.ISupportInitialize)midIndicator).EndInit();
            ((System.ComponentModel.ISupportInitialize)lowMidIndicator).EndInit();
            ((System.ComponentModel.ISupportInitialize)bassIndicator).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button startButton;
        private Button stopButton;
        private TrackBar subBassIndicator;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label7;
        private TrackBar brillianceIndicator;
        private Label label6;
        private TrackBar presenceIndicator;
        private Label label5;
        private TrackBar highMidIndicator;
        private Label label4;
        private TrackBar midIndicator;
        private Label label3;
        private TrackBar lowMidIndicator;
        private Label label1;
        private TrackBar bassIndicator;
        private Label label2;
    }
}
