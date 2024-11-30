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
            collidersSettingsPage = new TabPage();
            label12 = new Label();
            collidersPriorityInput = new TextBox();
            label6 = new Label();
            velocityBasedGroupBox = new GroupBox();
            label9 = new Label();
            collidersSpeedMultiplierInput = new TextBox();
            label8 = new Label();
            collidersMinIntensityInput = new TextBox();
            collidersUseVelocityCheckbox = new CheckBox();
            collidersAllowContinuousCheckbox = new CheckBox();
            collidersEnabledCheckbox = new CheckBox();
            collidersIntensityInput = new TextBox();
            applyCollisionSettingsButton = new Button();
            velocitySettingsPage = new TabPage();
            label11 = new Label();
            label13 = new Label();
            velocitySpeedCapInput = new TextBox();
            velocityPriorityInput = new TextBox();
            velocityIgnoreWhenSeatedCheckbox = new CheckBox();
            groupBox2 = new GroupBox();
            label10 = new Label();
            velocityImpactEnabledCheckbox = new CheckBox();
            velocityMinImpactInput = new TextBox();
            label7 = new Label();
            velocityThresholdInput = new TextBox();
            velocityIgnoreWhenGroundedCheckbox = new CheckBox();
            velocityEnabledCheckbox = new CheckBox();
            applyVelocitySettingsButton = new Button();
            owiSettingsPage = new TabPage();
            label17 = new Label();
            owiIntensityInput = new TextBox();
            label16 = new Label();
            owiUpdateIntervalInput = new TextBox();
            groupBox3 = new GroupBox();
            label15 = new Label();
            owiLinkLabel = new LinkLabel();
            label14 = new Label();
            owiPriorityInput = new TextBox();
            owiEnabledCheckbox = new CheckBox();
            applyOwiSettingsButton = new Button();
            oscPresetsPage = new TabPage();
            label18 = new Label();
            oscPresetsPriorityInput = new TextBox();
            oscPresetsEnabledCheckbox = new CheckBox();
            openOscPresetsFormButton = new Button();
            applyOscPresetsSettingsButton = new Button();
            logLevelComboBox = new ComboBox();
            label3 = new Label();
            logBox = new RichTextBox();
            connectionGroup = new GroupBox();
            owiStatusLabel = new Label();
            label19 = new Label();
            stopSensationsButton = new Button();
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
            tabControl1.SuspendLayout();
            collidersSettingsPage.SuspendLayout();
            velocityBasedGroupBox.SuspendLayout();
            velocitySettingsPage.SuspendLayout();
            groupBox2.SuspendLayout();
            owiSettingsPage.SuspendLayout();
            groupBox3.SuspendLayout();
            oscPresetsPage.SuspendLayout();
            connectionGroup.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(collidersSettingsPage);
            tabControl1.Controls.Add(velocitySettingsPage);
            tabControl1.Controls.Add(owiSettingsPage);
            tabControl1.Controls.Add(oscPresetsPage);
            tabControl1.Location = new Point(12, 118);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(368, 303);
            tabControl1.TabIndex = 0;
            // 
            // collidersSettingsPage
            // 
            collidersSettingsPage.Controls.Add(label12);
            collidersSettingsPage.Controls.Add(collidersPriorityInput);
            collidersSettingsPage.Controls.Add(label6);
            collidersSettingsPage.Controls.Add(velocityBasedGroupBox);
            collidersSettingsPage.Controls.Add(collidersEnabledCheckbox);
            collidersSettingsPage.Controls.Add(collidersIntensityInput);
            collidersSettingsPage.Controls.Add(applyCollisionSettingsButton);
            collidersSettingsPage.Location = new Point(4, 24);
            collidersSettingsPage.Name = "collidersSettingsPage";
            collidersSettingsPage.Padding = new Padding(3);
            collidersSettingsPage.Size = new Size(360, 275);
            collidersSettingsPage.TabIndex = 0;
            collidersSettingsPage.Text = "Colliders";
            collidersSettingsPage.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(6, 30);
            label12.Name = "label12";
            label12.Size = new Size(45, 15);
            label12.TabIndex = 9;
            label12.Text = "Priority";
            // 
            // collidersPriorityInput
            // 
            collidersPriorityInput.Location = new Point(262, 26);
            collidersPriorityInput.Name = "collidersPriorityInput";
            collidersPriorityInput.Size = new Size(89, 23);
            collidersPriorityInput.TabIndex = 8;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 59);
            label6.Name = "label6";
            label6.Size = new Size(73, 15);
            label6.TabIndex = 7;
            label6.Text = "Intensity (%)";
            // 
            // velocityBasedGroupBox
            // 
            velocityBasedGroupBox.Controls.Add(label9);
            velocityBasedGroupBox.Controls.Add(collidersSpeedMultiplierInput);
            velocityBasedGroupBox.Controls.Add(label8);
            velocityBasedGroupBox.Controls.Add(collidersMinIntensityInput);
            velocityBasedGroupBox.Controls.Add(collidersUseVelocityCheckbox);
            velocityBasedGroupBox.Controls.Add(collidersAllowContinuousCheckbox);
            velocityBasedGroupBox.Location = new Point(6, 85);
            velocityBasedGroupBox.Name = "velocityBasedGroupBox";
            velocityBasedGroupBox.Size = new Size(348, 135);
            velocityBasedGroupBox.TabIndex = 5;
            velocityBasedGroupBox.TabStop = false;
            velocityBasedGroupBox.Text = "Velocity-Based";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 107);
            label9.Name = "label9";
            label9.Size = new Size(93, 15);
            label9.TabIndex = 11;
            label9.Text = "Speed multiplier";
            // 
            // collidersSpeedMultiplierInput
            // 
            collidersSpeedMultiplierInput.Location = new Point(247, 104);
            collidersSpeedMultiplierInput.Name = "collidersSpeedMultiplierInput";
            collidersSpeedMultiplierInput.Size = new Size(89, 23);
            collidersSpeedMultiplierInput.TabIndex = 10;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 78);
            label8.Name = "label8";
            label8.Size = new Size(76, 15);
            label8.TabIndex = 9;
            label8.Text = "Min Intensity";
            // 
            // collidersMinIntensityInput
            // 
            collidersMinIntensityInput.Location = new Point(247, 75);
            collidersMinIntensityInput.Name = "collidersMinIntensityInput";
            collidersMinIntensityInput.Size = new Size(89, 23);
            collidersMinIntensityInput.TabIndex = 8;
            // 
            // collidersUseVelocityCheckbox
            // 
            collidersUseVelocityCheckbox.AutoSize = true;
            collidersUseVelocityCheckbox.Location = new Point(6, 22);
            collidersUseVelocityCheckbox.Name = "collidersUseVelocityCheckbox";
            collidersUseVelocityCheckbox.Size = new Size(68, 19);
            collidersUseVelocityCheckbox.TabIndex = 3;
            collidersUseVelocityCheckbox.Text = "Enabled";
            collidersUseVelocityCheckbox.UseVisualStyleBackColor = true;
            // 
            // collidersAllowContinuousCheckbox
            // 
            collidersAllowContinuousCheckbox.AutoSize = true;
            collidersAllowContinuousCheckbox.Location = new Point(6, 47);
            collidersAllowContinuousCheckbox.Name = "collidersAllowContinuousCheckbox";
            collidersAllowContinuousCheckbox.Size = new Size(195, 19);
            collidersAllowContinuousCheckbox.TabIndex = 4;
            collidersAllowContinuousCheckbox.Text = "Continuous sensation when idle";
            collidersAllowContinuousCheckbox.UseVisualStyleBackColor = true;
            // 
            // collidersEnabledCheckbox
            // 
            collidersEnabledCheckbox.AutoSize = true;
            collidersEnabledCheckbox.Location = new Point(6, 6);
            collidersEnabledCheckbox.Name = "collidersEnabledCheckbox";
            collidersEnabledCheckbox.Size = new Size(68, 19);
            collidersEnabledCheckbox.TabIndex = 2;
            collidersEnabledCheckbox.Text = "Enabled";
            collidersEnabledCheckbox.UseVisualStyleBackColor = true;
            // 
            // collidersIntensityInput
            // 
            collidersIntensityInput.Location = new Point(262, 56);
            collidersIntensityInput.Name = "collidersIntensityInput";
            collidersIntensityInput.Size = new Size(89, 23);
            collidersIntensityInput.TabIndex = 6;
            // 
            // applyCollisionSettingsButton
            // 
            applyCollisionSettingsButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            applyCollisionSettingsButton.Location = new Point(279, 246);
            applyCollisionSettingsButton.Name = "applyCollisionSettingsButton";
            applyCollisionSettingsButton.Size = new Size(75, 23);
            applyCollisionSettingsButton.TabIndex = 0;
            applyCollisionSettingsButton.Text = "Apply";
            applyCollisionSettingsButton.UseVisualStyleBackColor = true;
            applyCollisionSettingsButton.Click += ApplyCollidersSettingsButton_Click;
            // 
            // velocitySettingsPage
            // 
            velocitySettingsPage.Controls.Add(label11);
            velocitySettingsPage.Controls.Add(label13);
            velocitySettingsPage.Controls.Add(velocitySpeedCapInput);
            velocitySettingsPage.Controls.Add(velocityPriorityInput);
            velocitySettingsPage.Controls.Add(velocityIgnoreWhenSeatedCheckbox);
            velocitySettingsPage.Controls.Add(groupBox2);
            velocitySettingsPage.Controls.Add(label7);
            velocitySettingsPage.Controls.Add(velocityThresholdInput);
            velocitySettingsPage.Controls.Add(velocityIgnoreWhenGroundedCheckbox);
            velocitySettingsPage.Controls.Add(velocityEnabledCheckbox);
            velocitySettingsPage.Controls.Add(applyVelocitySettingsButton);
            velocitySettingsPage.Location = new Point(4, 24);
            velocitySettingsPage.Name = "velocitySettingsPage";
            velocitySettingsPage.Padding = new Padding(3);
            velocitySettingsPage.Size = new Size(360, 275);
            velocitySettingsPage.TabIndex = 1;
            velocitySettingsPage.Text = "Velocity";
            velocitySettingsPage.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 83);
            label11.Name = "label11";
            label11.Size = new Size(96, 15);
            label11.TabIndex = 11;
            label11.Text = "Max speed (m/s)";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 30);
            label13.Name = "label13";
            label13.Size = new Size(45, 15);
            label13.TabIndex = 15;
            label13.Text = "Priority";
            // 
            // velocitySpeedCapInput
            // 
            velocitySpeedCapInput.Location = new Point(262, 83);
            velocitySpeedCapInput.Name = "velocitySpeedCapInput";
            velocitySpeedCapInput.Size = new Size(89, 23);
            velocitySpeedCapInput.TabIndex = 10;
            // 
            // velocityPriorityInput
            // 
            velocityPriorityInput.Location = new Point(262, 26);
            velocityPriorityInput.Name = "velocityPriorityInput";
            velocityPriorityInput.Size = new Size(89, 23);
            velocityPriorityInput.TabIndex = 14;
            // 
            // velocityIgnoreWhenSeatedCheckbox
            // 
            velocityIgnoreWhenSeatedCheckbox.AutoSize = true;
            velocityIgnoreWhenSeatedCheckbox.Location = new Point(6, 138);
            velocityIgnoreWhenSeatedCheckbox.Name = "velocityIgnoreWhenSeatedCheckbox";
            velocityIgnoreWhenSeatedCheckbox.Size = new Size(129, 19);
            velocityIgnoreWhenSeatedCheckbox.TabIndex = 13;
            velocityIgnoreWhenSeatedCheckbox.Text = "Ignore when seated";
            velocityIgnoreWhenSeatedCheckbox.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(velocityImpactEnabledCheckbox);
            groupBox2.Controls.Add(velocityMinImpactInput);
            groupBox2.Location = new Point(6, 165);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(345, 75);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Impact";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 44);
            label10.Name = "label10";
            label10.Size = new Size(147, 15);
            label10.TabIndex = 11;
            label10.Text = "Min. impact velocity (m/s)";
            // 
            // velocityImpactEnabledCheckbox
            // 
            velocityImpactEnabledCheckbox.AutoSize = true;
            velocityImpactEnabledCheckbox.Location = new Point(6, 22);
            velocityImpactEnabledCheckbox.Name = "velocityImpactEnabledCheckbox";
            velocityImpactEnabledCheckbox.Size = new Size(68, 19);
            velocityImpactEnabledCheckbox.TabIndex = 3;
            velocityImpactEnabledCheckbox.Text = "Enabled";
            velocityImpactEnabledCheckbox.UseVisualStyleBackColor = true;
            // 
            // velocityMinImpactInput
            // 
            velocityMinImpactInput.Location = new Point(247, 41);
            velocityMinImpactInput.Name = "velocityMinImpactInput";
            velocityMinImpactInput.Size = new Size(89, 23);
            velocityMinImpactInput.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 58);
            label7.Name = "label7";
            label7.Size = new Size(107, 15);
            label7.TabIndex = 9;
            label7.Text = "Min. velocity (m/s)";
            // 
            // velocityThresholdInput
            // 
            velocityThresholdInput.Location = new Point(262, 55);
            velocityThresholdInput.Name = "velocityThresholdInput";
            velocityThresholdInput.Size = new Size(89, 23);
            velocityThresholdInput.TabIndex = 8;
            // 
            // velocityIgnoreWhenGroundedCheckbox
            // 
            velocityIgnoreWhenGroundedCheckbox.AutoSize = true;
            velocityIgnoreWhenGroundedCheckbox.Location = new Point(6, 113);
            velocityIgnoreWhenGroundedCheckbox.Name = "velocityIgnoreWhenGroundedCheckbox";
            velocityIgnoreWhenGroundedCheckbox.Size = new Size(147, 19);
            velocityIgnoreWhenGroundedCheckbox.TabIndex = 4;
            velocityIgnoreWhenGroundedCheckbox.Text = "Ignore when grounded";
            velocityIgnoreWhenGroundedCheckbox.UseVisualStyleBackColor = true;
            // 
            // velocityEnabledCheckbox
            // 
            velocityEnabledCheckbox.AutoSize = true;
            velocityEnabledCheckbox.Location = new Point(6, 6);
            velocityEnabledCheckbox.Name = "velocityEnabledCheckbox";
            velocityEnabledCheckbox.Size = new Size(68, 19);
            velocityEnabledCheckbox.TabIndex = 3;
            velocityEnabledCheckbox.Text = "Enabled";
            velocityEnabledCheckbox.UseVisualStyleBackColor = true;
            // 
            // applyVelocitySettingsButton
            // 
            applyVelocitySettingsButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            applyVelocitySettingsButton.Location = new Point(279, 246);
            applyVelocitySettingsButton.Name = "applyVelocitySettingsButton";
            applyVelocitySettingsButton.Size = new Size(75, 23);
            applyVelocitySettingsButton.TabIndex = 0;
            applyVelocitySettingsButton.Text = "Apply";
            applyVelocitySettingsButton.UseVisualStyleBackColor = true;
            applyVelocitySettingsButton.Click += ApplyVelocitySettingsButton_Click;
            // 
            // owiSettingsPage
            // 
            owiSettingsPage.Controls.Add(label17);
            owiSettingsPage.Controls.Add(owiIntensityInput);
            owiSettingsPage.Controls.Add(label16);
            owiSettingsPage.Controls.Add(owiUpdateIntervalInput);
            owiSettingsPage.Controls.Add(groupBox3);
            owiSettingsPage.Controls.Add(owiLinkLabel);
            owiSettingsPage.Controls.Add(label14);
            owiSettingsPage.Controls.Add(owiPriorityInput);
            owiSettingsPage.Controls.Add(owiEnabledCheckbox);
            owiSettingsPage.Controls.Add(applyOwiSettingsButton);
            owiSettingsPage.Location = new Point(4, 24);
            owiSettingsPage.Name = "owiSettingsPage";
            owiSettingsPage.Size = new Size(360, 275);
            owiSettingsPage.TabIndex = 2;
            owiSettingsPage.Text = "OWI";
            owiSettingsPage.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(6, 89);
            label17.Name = "label17";
            label17.Size = new Size(73, 15);
            label17.TabIndex = 25;
            label17.Text = "Intensity (%)";
            // 
            // owiIntensityInput
            // 
            owiIntensityInput.Location = new Point(262, 86);
            owiIntensityInput.Name = "owiIntensityInput";
            owiIntensityInput.Size = new Size(89, 23);
            owiIntensityInput.TabIndex = 24;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(6, 59);
            label16.Name = "label16";
            label16.Size = new Size(112, 15);
            label16.TabIndex = 23;
            label16.Text = "Log scan delay (ms)";
            // 
            // owiUpdateIntervalInput
            // 
            owiUpdateIntervalInput.Location = new Point(262, 55);
            owiUpdateIntervalInput.Name = "owiUpdateIntervalInput";
            owiUpdateIntervalInput.Size = new Size(89, 23);
            owiUpdateIntervalInput.TabIndex = 22;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label15);
            groupBox3.Location = new Point(6, 115);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(348, 121);
            groupBox3.TabIndex = 21;
            groupBox3.TabStop = false;
            groupBox3.Text = "Information";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.FlatStyle = FlatStyle.Popup;
            label15.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label15.ForeColor = SystemColors.ControlDarkDark;
            label15.Location = new Point(21, 23);
            label15.Name = "label15";
            label15.Size = new Size(308, 85);
            label15.TabIndex = 20;
            label15.Text = "This effect receives data from worlds\r\nusing OWO World Integrator.\r\n\r\nTo use this effect, please enable debug logging in your\r\nVRChat client and visit a world that supports OWI.";
            // 
            // owiLinkLabel
            // 
            owiLinkLabel.AutoSize = true;
            owiLinkLabel.Location = new Point(6, 239);
            owiLinkLabel.Name = "owiLinkLabel";
            owiLinkLabel.Size = new Size(133, 30);
            owiLinkLabel.TabIndex = 0;
            owiLinkLabel.TabStop = true;
            owiLinkLabel.Text = "OWOWorldIntegrator\r\nby RevoForge && SonoVr\r\n";
            owiLinkLabel.LinkClicked += OwiLinkLabel_LinkClicked;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(6, 30);
            label14.Name = "label14";
            label14.Size = new Size(45, 15);
            label14.TabIndex = 19;
            label14.Text = "Priority";
            // 
            // owiPriorityInput
            // 
            owiPriorityInput.Location = new Point(262, 26);
            owiPriorityInput.Name = "owiPriorityInput";
            owiPriorityInput.Size = new Size(89, 23);
            owiPriorityInput.TabIndex = 18;
            // 
            // owiEnabledCheckbox
            // 
            owiEnabledCheckbox.AutoSize = true;
            owiEnabledCheckbox.Location = new Point(6, 6);
            owiEnabledCheckbox.Name = "owiEnabledCheckbox";
            owiEnabledCheckbox.Size = new Size(68, 19);
            owiEnabledCheckbox.TabIndex = 17;
            owiEnabledCheckbox.Text = "Enabled";
            owiEnabledCheckbox.UseVisualStyleBackColor = true;
            // 
            // applyOwiSettingsButton
            // 
            applyOwiSettingsButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            applyOwiSettingsButton.Location = new Point(279, 246);
            applyOwiSettingsButton.Name = "applyOwiSettingsButton";
            applyOwiSettingsButton.Size = new Size(75, 23);
            applyOwiSettingsButton.TabIndex = 16;
            applyOwiSettingsButton.Text = "Apply";
            applyOwiSettingsButton.UseVisualStyleBackColor = true;
            applyOwiSettingsButton.Click += ApplyOwiSettingsButton_Click;
            // 
            // oscPresetsPage
            // 
            oscPresetsPage.Controls.Add(label18);
            oscPresetsPage.Controls.Add(oscPresetsPriorityInput);
            oscPresetsPage.Controls.Add(oscPresetsEnabledCheckbox);
            oscPresetsPage.Controls.Add(openOscPresetsFormButton);
            oscPresetsPage.Controls.Add(applyOscPresetsSettingsButton);
            oscPresetsPage.Location = new Point(4, 24);
            oscPresetsPage.Name = "oscPresetsPage";
            oscPresetsPage.Padding = new Padding(3);
            oscPresetsPage.Size = new Size(360, 275);
            oscPresetsPage.TabIndex = 3;
            oscPresetsPage.Text = "Presets";
            oscPresetsPage.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(6, 30);
            label18.Name = "label18";
            label18.Size = new Size(45, 15);
            label18.TabIndex = 22;
            label18.Text = "Priority";
            // 
            // oscPresetsPriorityInput
            // 
            oscPresetsPriorityInput.Location = new Point(262, 26);
            oscPresetsPriorityInput.Name = "oscPresetsPriorityInput";
            oscPresetsPriorityInput.Size = new Size(89, 23);
            oscPresetsPriorityInput.TabIndex = 21;
            // 
            // oscPresetsEnabledCheckbox
            // 
            oscPresetsEnabledCheckbox.AutoSize = true;
            oscPresetsEnabledCheckbox.Location = new Point(6, 6);
            oscPresetsEnabledCheckbox.Name = "oscPresetsEnabledCheckbox";
            oscPresetsEnabledCheckbox.Size = new Size(68, 19);
            oscPresetsEnabledCheckbox.TabIndex = 20;
            oscPresetsEnabledCheckbox.Text = "Enabled";
            oscPresetsEnabledCheckbox.UseVisualStyleBackColor = true;
            // 
            // openOscPresetsFormButton
            // 
            openOscPresetsFormButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            openOscPresetsFormButton.Location = new Point(96, 98);
            openOscPresetsFormButton.Name = "openOscPresetsFormButton";
            openOscPresetsFormButton.Size = new Size(148, 38);
            openOscPresetsFormButton.TabIndex = 18;
            openOscPresetsFormButton.Text = "Configure";
            openOscPresetsFormButton.UseVisualStyleBackColor = true;
            openOscPresetsFormButton.Click += OpenOscPresetsFormButton_Click;
            // 
            // applyOscPresetsSettingsButton
            // 
            applyOscPresetsSettingsButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            applyOscPresetsSettingsButton.Location = new Point(279, 246);
            applyOscPresetsSettingsButton.Name = "applyOscPresetsSettingsButton";
            applyOscPresetsSettingsButton.Size = new Size(75, 23);
            applyOscPresetsSettingsButton.TabIndex = 17;
            applyOscPresetsSettingsButton.Text = "Apply";
            applyOscPresetsSettingsButton.UseVisualStyleBackColor = true;
            applyOscPresetsSettingsButton.Click += ApplyOscPresetsSettingsButton_Click;
            // 
            // logLevelComboBox
            // 
            logLevelComboBox.FormattingEnabled = true;
            logLevelComboBox.Location = new Point(72, 21);
            logLevelComboBox.Name = "logLevelComboBox";
            logLevelComboBox.Size = new Size(121, 23);
            logLevelComboBox.TabIndex = 3;
            logLevelComboBox.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(6, 24);
            label3.Name = "label3";
            label3.Size = new Size(60, 17);
            label3.TabIndex = 2;
            label3.Text = "Log level";
            // 
            // logBox
            // 
            logBox.BackColor = Color.Black;
            logBox.Location = new Point(6, 51);
            logBox.Name = "logBox";
            logBox.Size = new Size(390, 246);
            logBox.TabIndex = 1;
            logBox.Text = "";
            // 
            // connectionGroup
            // 
            connectionGroup.Controls.Add(owiStatusLabel);
            connectionGroup.Controls.Add(label19);
            connectionGroup.Controls.Add(stopSensationsButton);
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
            // owiStatusLabel
            // 
            owiStatusLabel.AutoSize = true;
            owiStatusLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            owiStatusLabel.ForeColor = Color.Red;
            owiStatusLabel.Location = new Point(687, 50);
            owiStatusLabel.Name = "owiStatusLabel";
            owiStatusLabel.Size = new Size(54, 15);
            owiStatusLabel.TabIndex = 12;
            owiStatusLabel.Text = "Stopped";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label19.Location = new Point(644, 50);
            label19.Name = "label19";
            label19.Size = new Size(35, 15);
            label19.TabIndex = 11;
            label19.Text = "OWI:";
            // 
            // stopSensationsButton
            // 
            stopSensationsButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            stopSensationsButton.Location = new Point(6, 71);
            stopSensationsButton.Name = "stopSensationsButton";
            stopSensationsButton.Size = new Size(167, 23);
            stopSensationsButton.TabIndex = 10;
            stopSensationsButton.Text = "Stop all sensations";
            stopSensationsButton.UseVisualStyleBackColor = true;
            stopSensationsButton.Click += StopSensationsButton_Click;
            // 
            // oscPortInput
            // 
            oscPortInput.Location = new Point(73, 42);
            oscPortInput.Name = "oscPortInput";
            oscPortInput.Size = new Size(100, 23);
            oscPortInput.TabIndex = 9;
            oscPortInput.Leave += OscPortInput_Exit;
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
            groupBox1.Size = new Size(402, 303);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Log";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 423);
            Controls.Add(groupBox1);
            Controls.Add(connectionGroup);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            ShowIcon = false;
            Text = "OWOVRC";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            tabControl1.ResumeLayout(false);
            collidersSettingsPage.ResumeLayout(false);
            collidersSettingsPage.PerformLayout();
            velocityBasedGroupBox.ResumeLayout(false);
            velocityBasedGroupBox.PerformLayout();
            velocitySettingsPage.ResumeLayout(false);
            velocitySettingsPage.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            owiSettingsPage.ResumeLayout(false);
            owiSettingsPage.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            oscPresetsPage.ResumeLayout(false);
            oscPresetsPage.PerformLayout();
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
        private TabPage collidersSettingsPage;
        private Button applyCollisionSettingsButton;
        private Button applyVelocitySettingsButton;
        private CheckBox collidersEnabledCheckbox;
        private Label label6;
        private GroupBox velocityBasedGroupBox;
        private CheckBox collidersUseVelocityCheckbox;
        private CheckBox collidersAllowContinuousCheckbox;
        private TextBox collidersIntensityInput;
        private Label label8;
        private TextBox collidersMinIntensityInput;
        private Label label9;
        private TextBox collidersSpeedMultiplierInput;
        private CheckBox velocityEnabledCheckbox;
        private Label label7;
        private TextBox velocityThresholdInput;
        private Label label10;
        private TextBox velocityMinImpactInput;
        private GroupBox groupBox2;
        private Label label11;
        private TextBox velocitySpeedCapInput;
        private CheckBox velocityImpactEnabledCheckbox;
        private CheckBox velocityIgnoreWhenGroundedCheckbox;
        private CheckBox velocityIgnoreWhenSeatedCheckbox;
        private Button stopSensationsButton;
        private Label label12;
        private TextBox collidersPriorityInput;
        private Label label13;
        private TextBox velocityPriorityInput;
        private TabPage owiSettingsPage;
        private LinkLabel owiLinkLabel;
        private Label label14;
        private TextBox owiPriorityInput;
        private CheckBox owiEnabledCheckbox;
        private Button applyOwiSettingsButton;
        private Label label15;
        private GroupBox groupBox3;
        private Label label16;
        private TextBox owiUpdateIntervalInput;
        private Label label17;
        private TextBox owiIntensityInput;
        private Label owiStatusLabel;
        private Label label19;
        private TabPage oscPresetsPage;
        private Button applyOscPresetsSettingsButton;
        private Button openOscPresetsFormButton;
        private Label label18;
        private TextBox oscPresetsPriorityInput;
        private CheckBox oscPresetsEnabledCheckbox;
    }
}
