using OWOVRC.Audio.UI.Controls;

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
            subBassIndicatorLeft = new BarIndicator();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            label7 = new Label();
            brillianceIndicatorLeft = new BarIndicator();
            label6 = new Label();
            presenceIndicatorLeft = new BarIndicator();
            label5 = new Label();
            highMidIndicatorLeft = new BarIndicator();
            label4 = new Label();
            midIndicatorLeft = new BarIndicator();
            label3 = new Label();
            lowMidIndicatorLeft = new BarIndicator();
            label1 = new Label();
            bassIndicatorLeft = new BarIndicator();
            label2 = new Label();
            groupBox3 = new GroupBox();
            label8 = new Label();
            brillianceIndicatorRight = new BarIndicator();
            label9 = new Label();
            presenceIndicatorRight = new BarIndicator();
            label10 = new Label();
            highMidIndicatorRight = new BarIndicator();
            label11 = new Label();
            midIndicatorRight = new BarIndicator();
            label12 = new Label();
            lowMidIndicatorRight = new BarIndicator();
            label13 = new Label();
            bassIndicatorRight = new BarIndicator();
            label14 = new Label();
            subBassIndicatorRight = new BarIndicator();
            label15 = new Label();
            maxAmplitudeLabel = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
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
            // subBassIndicatorLeft
            // 
            subBassIndicatorLeft.BarColor = Color.Orange;
            subBassIndicatorLeft.Enabled = false;
            subBassIndicatorLeft.Location = new Point(23, 22);
            subBassIndicatorLeft.Max = 100;
            subBassIndicatorLeft.Min = 0;
            subBassIndicatorLeft.Name = "subBassIndicatorLeft";
            subBassIndicatorLeft.Size = new Size(45, 181);
            subBassIndicatorLeft.TabIndex = 4;
            subBassIndicatorLeft.Value = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(startButton);
            groupBox1.Controls.Add(stopButton);
            groupBox1.Location = new Point(509, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(92, 82);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Control";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(brillianceIndicatorLeft);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(presenceIndicatorLeft);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(highMidIndicatorLeft);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(midIndicatorLeft);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(lowMidIndicatorLeft);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(bassIndicatorLeft);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(subBassIndicatorLeft);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(491, 227);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Left";
            // 
            // label7
            // 
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label7.Location = new Point(420, 206);
            label7.Name = "label7";
            label7.Size = new Size(62, 15);
            label7.TabIndex = 26;
            label7.Text = "Brilliance";
            label7.TextAlign = ContentAlignment.TopCenter;
            // 
            // brillianceIndicatorLeft
            // 
            brillianceIndicatorLeft.BarColor = Color.Orange;
            brillianceIndicatorLeft.Enabled = false;
            brillianceIndicatorLeft.Location = new Point(427, 22);
            brillianceIndicatorLeft.Max = 100;
            brillianceIndicatorLeft.Min = 0;
            brillianceIndicatorLeft.Name = "brillianceIndicatorLeft";
            brillianceIndicatorLeft.Size = new Size(45, 181);
            brillianceIndicatorLeft.TabIndex = 25;
            brillianceIndicatorLeft.Value = 0;
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label6.Location = new Point(352, 206);
            label6.Name = "label6";
            label6.Size = new Size(62, 15);
            label6.TabIndex = 24;
            label6.Text = "Presence";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // presenceIndicatorLeft
            // 
            presenceIndicatorLeft.BarColor = Color.Orange;
            presenceIndicatorLeft.Enabled = false;
            presenceIndicatorLeft.Location = new Point(359, 22);
            presenceIndicatorLeft.Max = 100;
            presenceIndicatorLeft.Min = 0;
            presenceIndicatorLeft.Name = "presenceIndicatorLeft";
            presenceIndicatorLeft.Size = new Size(45, 181);
            presenceIndicatorLeft.TabIndex = 23;
            presenceIndicatorLeft.Value = 0;
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.Location = new Point(284, 206);
            label5.Name = "label5";
            label5.Size = new Size(62, 15);
            label5.TabIndex = 22;
            label5.Text = "High-Mid";
            label5.TextAlign = ContentAlignment.TopCenter;
            // 
            // highMidIndicatorLeft
            // 
            highMidIndicatorLeft.BarColor = Color.Orange;
            highMidIndicatorLeft.Enabled = false;
            highMidIndicatorLeft.Location = new Point(291, 22);
            highMidIndicatorLeft.Max = 100;
            highMidIndicatorLeft.Min = 0;
            highMidIndicatorLeft.Name = "highMidIndicatorLeft";
            highMidIndicatorLeft.Size = new Size(45, 181);
            highMidIndicatorLeft.TabIndex = 21;
            highMidIndicatorLeft.Value = 0;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(216, 206);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 20;
            label4.Text = "Midrange";
            label4.TextAlign = ContentAlignment.TopCenter;
            // 
            // midIndicatorLeft
            // 
            midIndicatorLeft.BarColor = Color.Orange;
            midIndicatorLeft.Enabled = false;
            midIndicatorLeft.Location = new Point(223, 22);
            midIndicatorLeft.Max = 100;
            midIndicatorLeft.Min = 0;
            midIndicatorLeft.Name = "midIndicatorLeft";
            midIndicatorLeft.Size = new Size(45, 181);
            midIndicatorLeft.TabIndex = 19;
            midIndicatorLeft.Value = 0;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(148, 206);
            label3.Name = "label3";
            label3.Size = new Size(62, 15);
            label3.TabIndex = 18;
            label3.Text = "Low-Mid";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // lowMidIndicatorLeft
            // 
            lowMidIndicatorLeft.BarColor = Color.Orange;
            lowMidIndicatorLeft.Enabled = false;
            lowMidIndicatorLeft.Location = new Point(159, 22);
            lowMidIndicatorLeft.Max = 100;
            lowMidIndicatorLeft.Min = 0;
            lowMidIndicatorLeft.Name = "lowMidIndicatorLeft";
            lowMidIndicatorLeft.Size = new Size(45, 181);
            lowMidIndicatorLeft.TabIndex = 17;
            lowMidIndicatorLeft.Value = 0;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(80, 206);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 16;
            label1.Text = "Bass";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // bassIndicatorLeft
            // 
            bassIndicatorLeft.BarColor = Color.Orange;
            bassIndicatorLeft.Enabled = false;
            bassIndicatorLeft.Location = new Point(91, 22);
            bassIndicatorLeft.Max = 100;
            bassIndicatorLeft.Min = 0;
            bassIndicatorLeft.Name = "bassIndicatorLeft";
            bassIndicatorLeft.Size = new Size(45, 181);
            bassIndicatorLeft.TabIndex = 15;
            bassIndicatorLeft.Value = 0;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(12, 206);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 14;
            label2.Text = "Sub-Bass";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(brillianceIndicatorRight);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(presenceIndicatorRight);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(highMidIndicatorRight);
            groupBox3.Controls.Add(label11);
            groupBox3.Controls.Add(midIndicatorRight);
            groupBox3.Controls.Add(label12);
            groupBox3.Controls.Add(lowMidIndicatorRight);
            groupBox3.Controls.Add(label13);
            groupBox3.Controls.Add(bassIndicatorRight);
            groupBox3.Controls.Add(label14);
            groupBox3.Controls.Add(subBassIndicatorRight);
            groupBox3.Location = new Point(607, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(491, 227);
            groupBox3.TabIndex = 7;
            groupBox3.TabStop = false;
            groupBox3.Text = "Right";
            // 
            // label8
            // 
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label8.Location = new Point(416, 206);
            label8.Name = "label8";
            label8.Size = new Size(62, 15);
            label8.TabIndex = 26;
            label8.Text = "Brilliance";
            label8.TextAlign = ContentAlignment.TopCenter;
            // 
            // brillianceIndicatorRight
            // 
            brillianceIndicatorRight.BarColor = Color.Orange;
            brillianceIndicatorRight.Enabled = false;
            brillianceIndicatorRight.Location = new Point(428, 22);
            brillianceIndicatorRight.Max = 100;
            brillianceIndicatorRight.Min = 0;
            brillianceIndicatorRight.Name = "brillianceIndicatorRight";
            brillianceIndicatorRight.Size = new Size(45, 181);
            brillianceIndicatorRight.TabIndex = 25;
            brillianceIndicatorRight.Value = 0;
            // 
            // label9
            // 
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label9.Location = new Point(348, 206);
            label9.Name = "label9";
            label9.Size = new Size(62, 15);
            label9.TabIndex = 24;
            label9.Text = "Presence";
            label9.TextAlign = ContentAlignment.TopCenter;
            // 
            // presenceIndicatorRight
            // 
            presenceIndicatorRight.BarColor = Color.Orange;
            presenceIndicatorRight.Enabled = false;
            presenceIndicatorRight.Location = new Point(360, 22);
            presenceIndicatorRight.Max = 100;
            presenceIndicatorRight.Min = 0;
            presenceIndicatorRight.Name = "presenceIndicatorRight";
            presenceIndicatorRight.Size = new Size(45, 181);
            presenceIndicatorRight.TabIndex = 23;
            presenceIndicatorRight.Value = 0;
            // 
            // label10
            // 
            label10.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label10.Location = new Point(280, 206);
            label10.Name = "label10";
            label10.Size = new Size(62, 15);
            label10.TabIndex = 22;
            label10.Text = "High-Mid";
            label10.TextAlign = ContentAlignment.TopCenter;
            // 
            // highMidIndicatorRight
            // 
            highMidIndicatorRight.BarColor = Color.Orange;
            highMidIndicatorRight.Enabled = false;
            highMidIndicatorRight.Location = new Point(292, 22);
            highMidIndicatorRight.Max = 100;
            highMidIndicatorRight.Min = 0;
            highMidIndicatorRight.Name = "highMidIndicatorRight";
            highMidIndicatorRight.Size = new Size(45, 181);
            highMidIndicatorRight.TabIndex = 21;
            highMidIndicatorRight.Value = 0;
            // 
            // label11
            // 
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label11.Location = new Point(212, 206);
            label11.Name = "label11";
            label11.Size = new Size(62, 15);
            label11.TabIndex = 20;
            label11.Text = "Midrange";
            label11.TextAlign = ContentAlignment.TopCenter;
            // 
            // midIndicatorRight
            // 
            midIndicatorRight.BarColor = Color.Orange;
            midIndicatorRight.Enabled = false;
            midIndicatorRight.Location = new Point(224, 22);
            midIndicatorRight.Max = 100;
            midIndicatorRight.Min = 0;
            midIndicatorRight.Name = "midIndicatorRight";
            midIndicatorRight.Size = new Size(45, 181);
            midIndicatorRight.TabIndex = 19;
            midIndicatorRight.Value = 0;
            // 
            // label12
            // 
            label12.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label12.Location = new Point(144, 206);
            label12.Name = "label12";
            label12.Size = new Size(62, 15);
            label12.TabIndex = 18;
            label12.Text = "Low-Mid";
            label12.TextAlign = ContentAlignment.TopCenter;
            // 
            // lowMidIndicatorRight
            // 
            lowMidIndicatorRight.BarColor = Color.Orange;
            lowMidIndicatorRight.Enabled = false;
            lowMidIndicatorRight.Location = new Point(156, 22);
            lowMidIndicatorRight.Max = 100;
            lowMidIndicatorRight.Min = 0;
            lowMidIndicatorRight.Name = "lowMidIndicatorRight";
            lowMidIndicatorRight.Size = new Size(45, 181);
            lowMidIndicatorRight.TabIndex = 17;
            lowMidIndicatorRight.Value = 0;
            // 
            // label13
            // 
            label13.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label13.Location = new Point(76, 206);
            label13.Name = "label13";
            label13.Size = new Size(62, 15);
            label13.TabIndex = 16;
            label13.Text = "Bass";
            label13.TextAlign = ContentAlignment.TopCenter;
            // 
            // bassIndicatorRight
            // 
            bassIndicatorRight.BarColor = Color.Orange;
            bassIndicatorRight.Enabled = false;
            bassIndicatorRight.Location = new Point(88, 22);
            bassIndicatorRight.Max = 100;
            bassIndicatorRight.Min = 0;
            bassIndicatorRight.Name = "bassIndicatorRight";
            bassIndicatorRight.Size = new Size(45, 181);
            bassIndicatorRight.TabIndex = 15;
            bassIndicatorRight.Value = 0;
            // 
            // label14
            // 
            label14.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label14.Location = new Point(8, 206);
            label14.Name = "label14";
            label14.Size = new Size(62, 15);
            label14.TabIndex = 14;
            label14.Text = "Sub-Bass";
            label14.TextAlign = ContentAlignment.TopCenter;
            // 
            // subBassIndicatorRight
            // 
            subBassIndicatorRight.BarColor = Color.Orange;
            subBassIndicatorRight.Enabled = false;
            subBassIndicatorRight.Location = new Point(20, 22);
            subBassIndicatorRight.Max = 100;
            subBassIndicatorRight.Min = 0;
            subBassIndicatorRight.Name = "subBassIndicatorRight";
            subBassIndicatorRight.Size = new Size(45, 181);
            subBassIndicatorRight.TabIndex = 4;
            subBassIndicatorRight.Value = 0;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(509, 97);
            label15.Name = "label15";
            label15.Size = new Size(92, 15);
            label15.TabIndex = 8;
            label15.Text = "Max Amplitude";
            // 
            // maxAmplitudeLabel
            // 
            maxAmplitudeLabel.Location = new Point(515, 112);
            maxAmplitudeLabel.Name = "maxAmplitudeLabel";
            maxAmplitudeLabel.Size = new Size(75, 23);
            maxAmplitudeLabel.TabIndex = 9;
            maxAmplitudeLabel.Text = "0";
            maxAmplitudeLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // AudioDemoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1107, 245);
            Controls.Add(maxAmplitudeLabel);
            Controls.Add(label15);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "AudioDemoForm";
            ShowIcon = false;
            Text = "Audio Capture Test";
            FormClosing += AudioDemoForm_FormClosing;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button startButton;
        private Button stopButton;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label1;
        private BarIndicator subBassIndicatorLeft;
        private BarIndicator brillianceIndicatorLeft;
        private BarIndicator presenceIndicatorLeft;
        private BarIndicator highMidIndicatorLeft;
        private BarIndicator midIndicatorLeft;
        private BarIndicator lowMidIndicatorLeft;
        private BarIndicator bassIndicatorLeft;
        private Label label2;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private GroupBox groupBox3;
        private BarIndicator brillianceIndicatorRight;
        private BarIndicator presenceIndicatorRight;
        private BarIndicator highMidIndicatorRight;
        private BarIndicator midIndicatorRight;
        private BarIndicator lowMidIndicatorRight;
        private BarIndicator bassIndicatorRight;
        private BarIndicator subBassIndicatorRight;
        private Label label7;
        private Label label15;
        private Label maxAmplitudeLabel;
    }
}
