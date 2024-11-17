namespace OWOVRC.UI
{
    partial class MainForm
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
            tabControl1 = new TabControl();
            collisionSettingsPage = new TabPage();
            applyCollisionSettingsButton = new Button();
            velocitySettingsPage = new TabPage();
            applyVelocitySettingsButton = new Button();
            logLevelComboBox = new ComboBox();
            label3 = new Label();
            logBox = new RichTextBox();
            connectionGroup = new GroupBox();
            oscPortInput = new TextBox();
            owoIPInput = new MaskedTextBox();
            label5 = new Label();
            label4 = new Label();
            startButton = new Button();
            oscStatusLabel = new Label();
            label2 = new Label();
            connectionStatusLabel = new Label();
            label1 = new Label();
            stopButton = new Button();
            groupBox1 = new GroupBox();
            label6 = new Label();
            label7 = new Label();
            tabControl1.SuspendLayout();
            collisionSettingsPage.SuspendLayout();
            velocitySettingsPage.SuspendLayout();
            connectionGroup.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(collisionSettingsPage);
            tabControl1.Controls.Add(velocitySettingsPage);
            tabControl1.Location = new Point(12, 118);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(368, 320);
            tabControl1.TabIndex = 0;
            // 
            // collisionSettingsPage
            // 
            collisionSettingsPage.Controls.Add(label6);
            collisionSettingsPage.Controls.Add(applyCollisionSettingsButton);
            collisionSettingsPage.Location = new Point(4, 24);
            collisionSettingsPage.Name = "collisionSettingsPage";
            collisionSettingsPage.Padding = new Padding(3);
            collisionSettingsPage.Size = new Size(360, 292);
            collisionSettingsPage.TabIndex = 0;
            collisionSettingsPage.Text = "Collision";
            collisionSettingsPage.UseVisualStyleBackColor = true;
            // 
            // applyCollisionSettingsButton
            // 
            applyCollisionSettingsButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            applyCollisionSettingsButton.Location = new Point(279, 263);
            applyCollisionSettingsButton.Name = "applyCollisionSettingsButton";
            applyCollisionSettingsButton.Size = new Size(75, 23);
            applyCollisionSettingsButton.TabIndex = 0;
            applyCollisionSettingsButton.Text = "Apply";
            applyCollisionSettingsButton.UseVisualStyleBackColor = true;
            applyCollisionSettingsButton.Click += ApplyCollisionSettingsButton_Click;
            // 
            // velocitySettingsPage
            // 
            velocitySettingsPage.Controls.Add(label7);
            velocitySettingsPage.Controls.Add(applyVelocitySettingsButton);
            velocitySettingsPage.Location = new Point(4, 24);
            velocitySettingsPage.Name = "velocitySettingsPage";
            velocitySettingsPage.Padding = new Padding(3);
            velocitySettingsPage.Size = new Size(360, 292);
            velocitySettingsPage.TabIndex = 1;
            velocitySettingsPage.Text = "Velocity";
            velocitySettingsPage.UseVisualStyleBackColor = true;
            // 
            // applyVelocitySettingsButton
            // 
            applyVelocitySettingsButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            applyVelocitySettingsButton.Location = new Point(279, 263);
            applyVelocitySettingsButton.Name = "applyVelocitySettingsButton";
            applyVelocitySettingsButton.Size = new Size(75, 23);
            applyVelocitySettingsButton.TabIndex = 0;
            applyVelocitySettingsButton.Text = "Apply";
            applyVelocitySettingsButton.UseVisualStyleBackColor = true;
            applyVelocitySettingsButton.Click += ApplyVelocitySettingsButton_Click;
            // 
            // logLevelComboBox
            // 
            logLevelComboBox.FormattingEnabled = true;
            logLevelComboBox.Location = new Point(78, 22);
            logLevelComboBox.Name = "logLevelComboBox";
            logLevelComboBox.Size = new Size(121, 23);
            logLevelComboBox.TabIndex = 3;
            logLevelComboBox.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(12, 25);
            label3.Name = "label3";
            label3.Size = new Size(60, 17);
            label3.TabIndex = 2;
            label3.Text = "Log level";
            // 
            // logBox
            // 
            logBox.BackColor = Color.Black;
            logBox.Location = new Point(12, 51);
            logBox.Name = "logBox";
            logBox.Size = new Size(384, 263);
            logBox.TabIndex = 1;
            logBox.Text = "";
            // 
            // connectionGroup
            // 
            connectionGroup.Controls.Add(oscPortInput);
            connectionGroup.Controls.Add(owoIPInput);
            connectionGroup.Controls.Add(label5);
            connectionGroup.Controls.Add(label4);
            connectionGroup.Controls.Add(startButton);
            connectionGroup.Controls.Add(oscStatusLabel);
            connectionGroup.Controls.Add(label2);
            connectionGroup.Controls.Add(connectionStatusLabel);
            connectionGroup.Controls.Add(label1);
            connectionGroup.Controls.Add(stopButton);
            connectionGroup.Location = new Point(12, 12);
            connectionGroup.Name = "connectionGroup";
            connectionGroup.Size = new Size(776, 100);
            connectionGroup.TabIndex = 1;
            connectionGroup.TabStop = false;
            connectionGroup.Text = "Connection";
            // 
            // oscPortInput
            // 
            oscPortInput.Location = new Point(73, 42);
            oscPortInput.Name = "oscPortInput";
            oscPortInput.Size = new Size(100, 23);
            oscPortInput.TabIndex = 9;
            oscPortInput.TextChanged += OscPortInput_TextChanged;
            // 
            // owoIPInput
            // 
            owoIPInput.Location = new Point(73, 16);
            owoIPInput.Name = "owoIPInput";
            owoIPInput.Size = new Size(100, 23);
            owoIPInput.TabIndex = 8;
            owoIPInput.Leave += OwoIPInput_Exit;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.Location = new Point(10, 19);
            label5.Name = "label5";
            label5.Size = new Size(51, 15);
            label5.TabIndex = 7;
            label5.Text = "OWO IP";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(10, 45);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 6;
            label4.Text = "OSC Port";
            // 
            // startButton
            // 
            startButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            startButton.Location = new Point(644, 71);
            startButton.Name = "startButton";
            startButton.Size = new Size(126, 23);
            startButton.TabIndex = 4;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += StartButton_Click;
            // 
            // oscStatusLabel
            // 
            oscStatusLabel.AutoSize = true;
            oscStatusLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            oscStatusLabel.ForeColor = Color.Red;
            oscStatusLabel.Location = new Point(687, 34);
            oscStatusLabel.Name = "oscStatusLabel";
            oscStatusLabel.Size = new Size(54, 15);
            oscStatusLabel.TabIndex = 3;
            oscStatusLabel.Text = "Stopped";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(644, 34);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 2;
            label2.Text = "OSC:";
            // 
            // connectionStatusLabel
            // 
            connectionStatusLabel.AutoSize = true;
            connectionStatusLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            connectionStatusLabel.ForeColor = Color.Red;
            connectionStatusLabel.Location = new Point(687, 19);
            connectionStatusLabel.Name = "connectionStatusLabel";
            connectionStatusLabel.Size = new Size(83, 15);
            connectionStatusLabel.TabIndex = 1;
            connectionStatusLabel.Text = "Disconnected";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(642, 19);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 0;
            label1.Text = "OWO:";
            // 
            // stopButton
            // 
            stopButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            stopButton.Location = new Point(644, 71);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(126, 23);
            stopButton.TabIndex = 5;
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Visible = false;
            stopButton.Click += StopButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(logLevelComboBox);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(logBox);
            groupBox1.Location = new Point(386, 118);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(402, 320);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Log";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 6);
            label6.Name = "label6";
            label6.Size = new Size(125, 15);
            label6.TabIndex = 1;
            label6.Text = "TODO: Implement me!";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 6);
            label7.Name = "label7";
            label7.Size = new Size(125, 15);
            label7.TabIndex = 1;
            label7.Text = "TODO: Implement me!";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(connectionGroup);
            Controls.Add(tabControl1);
            Name = "MainForm";
            Text = "Form1";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            tabControl1.ResumeLayout(false);
            collisionSettingsPage.ResumeLayout(false);
            collisionSettingsPage.PerformLayout();
            velocitySettingsPage.ResumeLayout(false);
            velocitySettingsPage.PerformLayout();
            connectionGroup.ResumeLayout(false);
            connectionGroup.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage velocitySettingsPage;
        private RichTextBox logBox;
        private GroupBox connectionGroup;
        private Label oscStatusLabel;
        private Label label2;
        private Label connectionStatusLabel;
        private Label label1;
        private Button startButton;
        private Button stopButton;
        private ComboBox logLevelComboBox;
        private Label label3;
        private TextBox oscPortInput;
        private MaskedTextBox owoIPInput;
        private Label label5;
        private Label label4;
        private GroupBox groupBox1;
        private TabPage collisionSettingsPage;
        private Button applyCollisionSettingsButton;
        private Button applyVelocitySettingsButton;
        private Label label6;
        private Label label7;
    }
}
