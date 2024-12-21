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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tabControl1 = new TabControl();
            collidersSettingsPage = new TabPage();
            configureCollidersIntensityButton = new Button();
            collidersHelpLinkLabel = new LinkLabel();
            label12 = new Label();
            collidersPriorityInput = new NumericUpDown();
            label6 = new Label();
            velocityBasedGroupBox = new GroupBox();
            label9 = new Label();
            collidersSpeedMultiplierInput = new NumericUpDown();
            label8 = new Label();
            collidersMinIntensityInput = new NumericUpDown();
            collidersUseVelocityCheckbox = new CheckBox();
            collidersAllowContinuousCheckbox = new CheckBox();
            collidersEnabledCheckbox = new CheckBox();
            applyCollisionSettingsButton = new Button();
            velocitySettingsPage = new TabPage();
            notVeryHelpfulLabel = new Label();
            label11 = new Label();
            label13 = new Label();
            velocitySpeedCapInput = new NumericUpDown();
            velocityPriorityInput = new NumericUpDown();
            velocityIgnoreWhenSeatedCheckbox = new CheckBox();
            groupBox2 = new GroupBox();
            label10 = new Label();
            velocityImpactEnabledCheckbox = new CheckBox();
            velocityMinImpactInput = new NumericUpDown();
            label7 = new Label();
            velocityThresholdInput = new NumericUpDown();
            velocityIgnoreWhenGroundedCheckbox = new CheckBox();
            velocityEnabledCheckbox = new CheckBox();
            applyVelocitySettingsButton = new Button();
            owiSettingsPage = new TabPage();
            label17 = new Label();
            owiIntensityInput = new NumericUpDown();
            label16 = new Label();
            owiUpdateIntervalInput = new NumericUpDown();
            groupBox3 = new GroupBox();
            label15 = new Label();
            owiLinkLabel = new LinkLabel();
            label14 = new Label();
            owiPriorityInput = new NumericUpDown();
            owiEnabledCheckbox = new CheckBox();
            applyOwiSettingsButton = new Button();
            oscPresetsPage = new TabPage();
            presetsHelpLinkLabel = new LinkLabel();
            label18 = new Label();
            oscPresetsPriorityInput = new NumericUpDown();
            oscPresetsEnabledCheckbox = new CheckBox();
            openOscPresetsFormButton = new Button();
            applyOscPresetsSettingsButton = new Button();
            audioResponsePage = new TabPage();
            audioDeviceSelectButton = new Button();
            label25 = new Label();
            audioMaxIntensityInput = new NumericUpDown();
            label24 = new Label();
            label23 = new Label();
            audioMaxBassInput = new NumericUpDown();
            audioMonitorButton = new Button();
            label22 = new Label();
            audioMinBassInput = new NumericUpDown();
            groupBox4 = new GroupBox();
            label20 = new Label();
            label21 = new Label();
            audioPriorityInput = new NumericUpDown();
            audioEnabledCheckbox = new CheckBox();
            applyAudioSettingsButton = new Button();
            logLevelComboBox = new ComboBox();
            label3 = new Label();
            logBox = new RichTextBox();
            connectionGroup = new GroupBox();
            openDiscoveryButton = new Button();
            owiStatusLabel = new Label();
            label19 = new Label();
            stopSensationsButton = new Button();
            oscPortInput = new NumericUpDown();
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
            helpToolTip = new ToolTip(components);
            tabControl1.SuspendLayout();
            collidersSettingsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)collidersPriorityInput).BeginInit();
            velocityBasedGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)collidersSpeedMultiplierInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)collidersMinIntensityInput).BeginInit();
            velocitySettingsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)velocitySpeedCapInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)velocityPriorityInput).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)velocityMinImpactInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)velocityThresholdInput).BeginInit();
            owiSettingsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)owiIntensityInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)owiUpdateIntervalInput).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)owiPriorityInput).BeginInit();
            oscPresetsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)oscPresetsPriorityInput).BeginInit();
            audioResponsePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)audioMaxIntensityInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)audioMaxBassInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)audioMinBassInput).BeginInit();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)audioPriorityInput).BeginInit();
            connectionGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)oscPortInput).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(collidersSettingsPage);
            tabControl1.Controls.Add(velocitySettingsPage);
            tabControl1.Controls.Add(owiSettingsPage);
            tabControl1.Controls.Add(oscPresetsPage);
            tabControl1.Controls.Add(audioResponsePage);
            tabControl1.Location = new Point(12, 118);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(368, 303);
            tabControl1.TabIndex = 0;
            helpToolTip.SetToolTip(tabControl1, "Available effects");
            // 
            // collidersSettingsPage
            // 
            collidersSettingsPage.Controls.Add(configureCollidersIntensityButton);
            collidersSettingsPage.Controls.Add(collidersHelpLinkLabel);
            collidersSettingsPage.Controls.Add(label12);
            collidersSettingsPage.Controls.Add(collidersPriorityInput);
            collidersSettingsPage.Controls.Add(label6);
            collidersSettingsPage.Controls.Add(velocityBasedGroupBox);
            collidersSettingsPage.Controls.Add(collidersEnabledCheckbox);
            collidersSettingsPage.Controls.Add(applyCollisionSettingsButton);
            collidersSettingsPage.Location = new Point(4, 24);
            collidersSettingsPage.Name = "collidersSettingsPage";
            collidersSettingsPage.Padding = new Padding(3);
            collidersSettingsPage.Size = new Size(360, 275);
            collidersSettingsPage.TabIndex = 0;
            collidersSettingsPage.Text = "Colliders";
            collidersSettingsPage.ToolTipText = "Avatar collider effects";
            collidersSettingsPage.UseVisualStyleBackColor = true;
            // 
            // configureCollidersIntensityButton
            // 
            configureCollidersIntensityButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            configureCollidersIntensityButton.Location = new Point(262, 55);
            configureCollidersIntensityButton.Name = "configureCollidersIntensityButton";
            configureCollidersIntensityButton.Size = new Size(89, 23);
            configureCollidersIntensityButton.TabIndex = 11;
            configureCollidersIntensityButton.Text = "Configure";
            helpToolTip.SetToolTip(configureCollidersIntensityButton, "Confirm muscle intensities for this effect");
            configureCollidersIntensityButton.UseVisualStyleBackColor = true;
            configureCollidersIntensityButton.Click += ConfigureCollidersIntensityButton_Click;
            // 
            // collidersHelpLinkLabel
            // 
            collidersHelpLinkLabel.AutoSize = true;
            collidersHelpLinkLabel.Location = new Point(6, 250);
            collidersHelpLinkLabel.Name = "collidersHelpLinkLabel";
            collidersHelpLinkLabel.Size = new Size(120, 15);
            collidersHelpLinkLabel.TabIndex = 10;
            collidersHelpLinkLabel.TabStop = true;
            collidersHelpLinkLabel.Text = "Avatar colliders setup";
            helpToolTip.SetToolTip(collidersHelpLinkLabel, "Open the avatar setup wiki page");
            collidersHelpLinkLabel.LinkClicked += CollidersHelpLinkLabel_LinkClicked;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(6, 30);
            label12.Name = "label12";
            label12.Size = new Size(45, 15);
            label12.TabIndex = 9;
            label12.Text = "Priority";
            helpToolTip.SetToolTip(label12, "Speicifies the priority of this effect (0 = lowest)");
            // 
            // collidersPriorityInput
            // 
            collidersPriorityInput.Location = new Point(262, 26);
            collidersPriorityInput.Name = "collidersPriorityInput";
            collidersPriorityInput.Size = new Size(89, 23);
            collidersPriorityInput.TabIndex = 8;
            helpToolTip.SetToolTip(collidersPriorityInput, "Speicifies the priority of this effect (0 = lowest)");
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 59);
            label6.Name = "label6";
            label6.Size = new Size(73, 15);
            label6.TabIndex = 7;
            label6.Text = "Intensity (%)";
            helpToolTip.SetToolTip(label6, "Specifies the intensity of this effect");
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
            helpToolTip.SetToolTip(label9, "Multiplier for velocity-based sensation scaling (Supports ");
            // 
            // collidersSpeedMultiplierInput
            // 
            collidersSpeedMultiplierInput.DecimalPlaces = 2;
            collidersSpeedMultiplierInput.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            collidersSpeedMultiplierInput.Location = new Point(247, 104);
            collidersSpeedMultiplierInput.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            collidersSpeedMultiplierInput.Name = "collidersSpeedMultiplierInput";
            collidersSpeedMultiplierInput.Size = new Size(89, 23);
            collidersSpeedMultiplierInput.TabIndex = 10;
            helpToolTip.SetToolTip(collidersSpeedMultiplierInput, "Multiplier for velocity-based sensation scaling (Supports ");
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 78);
            label8.Name = "label8";
            label8.Size = new Size(76, 15);
            label8.TabIndex = 9;
            label8.Text = "Min Intensity";
            helpToolTip.SetToolTip(label8, "The base intensity for velocity-based collisions. (Idle intensity)");
            // 
            // collidersMinIntensityInput
            // 
            collidersMinIntensityInput.Location = new Point(247, 75);
            collidersMinIntensityInput.Name = "collidersMinIntensityInput";
            collidersMinIntensityInput.Size = new Size(89, 23);
            collidersMinIntensityInput.TabIndex = 8;
            helpToolTip.SetToolTip(collidersMinIntensityInput, "The base intensity for velocity-based collisions. (Idle intensity)");
            // 
            // collidersUseVelocityCheckbox
            // 
            collidersUseVelocityCheckbox.AutoSize = true;
            collidersUseVelocityCheckbox.Location = new Point(6, 22);
            collidersUseVelocityCheckbox.Name = "collidersUseVelocityCheckbox";
            collidersUseVelocityCheckbox.Size = new Size(68, 19);
            collidersUseVelocityCheckbox.TabIndex = 3;
            collidersUseVelocityCheckbox.Text = "Enabled";
            helpToolTip.SetToolTip(collidersUseVelocityCheckbox, "Enables velocity-based intensity on collision.\r\nThe velocity is calculated using the distance to the center of the collider.");
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
            helpToolTip.SetToolTip(collidersAllowContinuousCheckbox, "Enables feedback on collision while the intersecting collider is not moving. (Zero velocity)");
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
            helpToolTip.SetToolTip(collidersEnabledCheckbox, "Enables avatar collider interactions");
            collidersEnabledCheckbox.UseVisualStyleBackColor = true;
            // 
            // applyCollisionSettingsButton
            // 
            applyCollisionSettingsButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            applyCollisionSettingsButton.Location = new Point(279, 246);
            applyCollisionSettingsButton.Name = "applyCollisionSettingsButton";
            applyCollisionSettingsButton.Size = new Size(75, 23);
            applyCollisionSettingsButton.TabIndex = 0;
            applyCollisionSettingsButton.Text = "Apply";
            helpToolTip.SetToolTip(applyCollisionSettingsButton, "Save and apply settings");
            applyCollisionSettingsButton.UseVisualStyleBackColor = true;
            applyCollisionSettingsButton.Click += ApplyCollidersSettingsButton_Click;
            // 
            // velocitySettingsPage
            // 
            velocitySettingsPage.Controls.Add(notVeryHelpfulLabel);
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
            velocitySettingsPage.ToolTipText = "Player velocity-based effects";
            velocitySettingsPage.UseVisualStyleBackColor = true;
            // 
            // notVeryHelpfulLabel
            // 
            notVeryHelpfulLabel.AutoSize = true;
            notVeryHelpfulLabel.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            notVeryHelpfulLabel.ForeColor = SystemColors.ControlDarkDark;
            notVeryHelpfulLabel.Location = new Point(6, 250);
            notVeryHelpfulLabel.Name = "notVeryHelpfulLabel";
            notVeryHelpfulLabel.Size = new Size(130, 15);
            notVeryHelpfulLabel.TabIndex = 16;
            notVeryHelpfulLabel.Text = "No exta setup required!";
            helpToolTip.SetToolTip(notVeryHelpfulLabel, "This effect uses built-in VRChat OSC messages.\r\nAs a result it will work with any Avatar without any setup. :)");
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 83);
            label11.Name = "label11";
            label11.Size = new Size(96, 15);
            label11.TabIndex = 11;
            label11.Text = "Max speed (m/s)";
            helpToolTip.SetToolTip(label11, "Maximum speed for wind effects (used for scaling)");
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 30);
            label13.Name = "label13";
            label13.Size = new Size(45, 15);
            label13.TabIndex = 15;
            label13.Text = "Priority";
            helpToolTip.SetToolTip(label13, "Speicifies the priority of this effect (0 = lowest)");
            // 
            // velocitySpeedCapInput
            // 
            velocitySpeedCapInput.DecimalPlaces = 2;
            velocitySpeedCapInput.Location = new Point(262, 83);
            velocitySpeedCapInput.Name = "velocitySpeedCapInput";
            velocitySpeedCapInput.Size = new Size(89, 23);
            velocitySpeedCapInput.TabIndex = 10;
            helpToolTip.SetToolTip(velocitySpeedCapInput, "Maximum speed for wind effects (used for scaling)");
            // 
            // velocityPriorityInput
            // 
            velocityPriorityInput.Location = new Point(262, 26);
            velocityPriorityInput.Name = "velocityPriorityInput";
            velocityPriorityInput.Size = new Size(89, 23);
            velocityPriorityInput.TabIndex = 14;
            helpToolTip.SetToolTip(velocityPriorityInput, "Speicifies the priority of this effect (0 = lowest)");
            // 
            // velocityIgnoreWhenSeatedCheckbox
            // 
            velocityIgnoreWhenSeatedCheckbox.AutoSize = true;
            velocityIgnoreWhenSeatedCheckbox.Location = new Point(6, 138);
            velocityIgnoreWhenSeatedCheckbox.Name = "velocityIgnoreWhenSeatedCheckbox";
            velocityIgnoreWhenSeatedCheckbox.Size = new Size(129, 19);
            velocityIgnoreWhenSeatedCheckbox.TabIndex = 13;
            velocityIgnoreWhenSeatedCheckbox.Text = "Ignore when seated";
            helpToolTip.SetToolTip(velocityIgnoreWhenSeatedCheckbox, "Disables velocity-based effects when the player is seated");
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
            helpToolTip.SetToolTip(label10, "Minimum velocity to reach to trigger the impact effect on deceleration");
            // 
            // velocityImpactEnabledCheckbox
            // 
            velocityImpactEnabledCheckbox.AutoSize = true;
            velocityImpactEnabledCheckbox.Location = new Point(6, 22);
            velocityImpactEnabledCheckbox.Name = "velocityImpactEnabledCheckbox";
            velocityImpactEnabledCheckbox.Size = new Size(68, 19);
            velocityImpactEnabledCheckbox.TabIndex = 3;
            velocityImpactEnabledCheckbox.Text = "Enabled";
            helpToolTip.SetToolTip(velocityImpactEnabledCheckbox, "Enables impact sensation on sudden deceleration.\r\nThis effect plays whenever the speed falls under the Min. velocity within a second after moving faster than the Min. impact velocity.");
            velocityImpactEnabledCheckbox.UseVisualStyleBackColor = true;
            // 
            // velocityMinImpactInput
            // 
            velocityMinImpactInput.DecimalPlaces = 2;
            velocityMinImpactInput.Location = new Point(247, 41);
            velocityMinImpactInput.Name = "velocityMinImpactInput";
            velocityMinImpactInput.Size = new Size(89, 23);
            velocityMinImpactInput.TabIndex = 10;
            helpToolTip.SetToolTip(velocityMinImpactInput, "Minimum velocity to reach to trigger the impact effect on deceleration");
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 58);
            label7.Name = "label7";
            label7.Size = new Size(107, 15);
            label7.TabIndex = 9;
            label7.Text = "Min. velocity (m/s)";
            helpToolTip.SetToolTip(label7, "Minimum speed for triggering wind effects");
            // 
            // velocityThresholdInput
            // 
            velocityThresholdInput.DecimalPlaces = 2;
            velocityThresholdInput.Location = new Point(262, 55);
            velocityThresholdInput.Name = "velocityThresholdInput";
            velocityThresholdInput.Size = new Size(89, 23);
            velocityThresholdInput.TabIndex = 8;
            helpToolTip.SetToolTip(velocityThresholdInput, "Minimum speed for triggering wind effects");
            // 
            // velocityIgnoreWhenGroundedCheckbox
            // 
            velocityIgnoreWhenGroundedCheckbox.AutoSize = true;
            velocityIgnoreWhenGroundedCheckbox.Location = new Point(6, 113);
            velocityIgnoreWhenGroundedCheckbox.Name = "velocityIgnoreWhenGroundedCheckbox";
            velocityIgnoreWhenGroundedCheckbox.Size = new Size(147, 19);
            velocityIgnoreWhenGroundedCheckbox.TabIndex = 4;
            velocityIgnoreWhenGroundedCheckbox.Text = "Ignore when grounded";
            helpToolTip.SetToolTip(velocityIgnoreWhenGroundedCheckbox, "Disables velocity-based effects when the player is standing on the ground\r\n");
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
            helpToolTip.SetToolTip(applyVelocitySettingsButton, "Save and apply settings");
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
            owiSettingsPage.ToolTipText = "OWOWorldIntegration connector";
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
            helpToolTip.SetToolTip(label17, "Specifies the intensity of this effect");
            // 
            // owiIntensityInput
            // 
            owiIntensityInput.Location = new Point(262, 86);
            owiIntensityInput.Name = "owiIntensityInput";
            owiIntensityInput.Size = new Size(89, 23);
            owiIntensityInput.TabIndex = 24;
            helpToolTip.SetToolTip(owiIntensityInput, "Specifies the intensity of this effect");
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(6, 59);
            label16.Name = "label16";
            label16.Size = new Size(123, 15);
            label16.TabIndex = 23;
            label16.Text = "Log scan interval (ms)";
            helpToolTip.SetToolTip(label16, "Time between checking the VRChat log for new sensations from OWI");
            // 
            // owiUpdateIntervalInput
            // 
            owiUpdateIntervalInput.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            owiUpdateIntervalInput.Location = new Point(262, 55);
            owiUpdateIntervalInput.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            owiUpdateIntervalInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            owiUpdateIntervalInput.Name = "owiUpdateIntervalInput";
            owiUpdateIntervalInput.Size = new Size(89, 23);
            owiUpdateIntervalInput.TabIndex = 22;
            helpToolTip.SetToolTip(owiUpdateIntervalInput, "Time between checking the VRChat log for new sensations from OWI");
            owiUpdateIntervalInput.Value = new decimal(new int[] { 1, 0, 0, 0 });
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
            helpToolTip.SetToolTip(owiLinkLabel, "Open the OWOWorldIntegrator GitHub page");
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
            helpToolTip.SetToolTip(label14, "Speicifies the priority of this effect (0 = lowest)");
            // 
            // owiPriorityInput
            // 
            owiPriorityInput.Location = new Point(262, 26);
            owiPriorityInput.Name = "owiPriorityInput";
            owiPriorityInput.Size = new Size(89, 23);
            owiPriorityInput.TabIndex = 18;
            helpToolTip.SetToolTip(owiPriorityInput, "Speicifies the priority of this effect (0 = lowest)");
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
            helpToolTip.SetToolTip(applyOwiSettingsButton, "Save and apply settings");
            applyOwiSettingsButton.UseVisualStyleBackColor = true;
            applyOwiSettingsButton.Click += ApplyOwiSettingsButton_Click;
            // 
            // oscPresetsPage
            // 
            oscPresetsPage.Controls.Add(presetsHelpLinkLabel);
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
            oscPresetsPage.ToolTipText = "Custom sensation presets";
            oscPresetsPage.UseVisualStyleBackColor = true;
            // 
            // presetsHelpLinkLabel
            // 
            presetsHelpLinkLabel.AutoSize = true;
            presetsHelpLinkLabel.Location = new Point(6, 250);
            presetsHelpLinkLabel.Name = "presetsHelpLinkLabel";
            presetsHelpLinkLabel.Size = new Size(116, 15);
            presetsHelpLinkLabel.TabIndex = 23;
            presetsHelpLinkLabel.TabStop = true;
            presetsHelpLinkLabel.Text = "Avatar triggers setup";
            helpToolTip.SetToolTip(presetsHelpLinkLabel, "Open the avatar setup wiki page");
            presetsHelpLinkLabel.LinkClicked += PresetsHelpLinkLabel_LinkClicked;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(6, 30);
            label18.Name = "label18";
            label18.Size = new Size(45, 15);
            label18.TabIndex = 22;
            label18.Text = "Priority";
            helpToolTip.SetToolTip(label18, "Speicifies the priority of this effect (0 = lowest)");
            // 
            // oscPresetsPriorityInput
            // 
            oscPresetsPriorityInput.Location = new Point(262, 26);
            oscPresetsPriorityInput.Name = "oscPresetsPriorityInput";
            oscPresetsPriorityInput.Size = new Size(89, 23);
            oscPresetsPriorityInput.TabIndex = 21;
            helpToolTip.SetToolTip(oscPresetsPriorityInput, "Speicifies the priority of this effect (0 = lowest)");
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
            helpToolTip.SetToolTip(openOscPresetsFormButton, "Opens a dialog to configure sensation presets");
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
            helpToolTip.SetToolTip(applyOscPresetsSettingsButton, "Save and apply settings");
            applyOscPresetsSettingsButton.UseVisualStyleBackColor = true;
            applyOscPresetsSettingsButton.Click += ApplyOscPresetsSettingsButton_Click;
            // 
            // audioResponsePage
            // 
            audioResponsePage.Controls.Add(audioDeviceSelectButton);
            audioResponsePage.Controls.Add(label25);
            audioResponsePage.Controls.Add(audioMaxIntensityInput);
            audioResponsePage.Controls.Add(label24);
            audioResponsePage.Controls.Add(label23);
            audioResponsePage.Controls.Add(audioMaxBassInput);
            audioResponsePage.Controls.Add(audioMonitorButton);
            audioResponsePage.Controls.Add(label22);
            audioResponsePage.Controls.Add(audioMinBassInput);
            audioResponsePage.Controls.Add(groupBox4);
            audioResponsePage.Controls.Add(label21);
            audioResponsePage.Controls.Add(audioPriorityInput);
            audioResponsePage.Controls.Add(audioEnabledCheckbox);
            audioResponsePage.Controls.Add(applyAudioSettingsButton);
            audioResponsePage.Location = new Point(4, 24);
            audioResponsePage.Name = "audioResponsePage";
            audioResponsePage.Size = new Size(360, 275);
            audioResponsePage.TabIndex = 4;
            audioResponsePage.Text = "Audio";
            audioResponsePage.UseVisualStyleBackColor = true;
            // 
            // audioDeviceSelectButton
            // 
            audioDeviceSelectButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            audioDeviceSelectButton.Location = new Point(262, 144);
            audioDeviceSelectButton.Name = "audioDeviceSelectButton";
            audioDeviceSelectButton.Size = new Size(89, 23);
            audioDeviceSelectButton.TabIndex = 36;
            audioDeviceSelectButton.Text = "Configure";
            helpToolTip.SetToolTip(audioDeviceSelectButton, "Configure which audio device the effect uses.\r\nShift-Click to show all available audio devices (input and output).");
            audioDeviceSelectButton.UseVisualStyleBackColor = true;
            audioDeviceSelectButton.Click += AudioDeviceSelectButton_Click;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(6, 148);
            label25.Name = "label25";
            label25.Size = new Size(76, 15);
            label25.TabIndex = 34;
            label25.Text = "Audio device";
            helpToolTip.SetToolTip(label25, "Configure which audio device the effect uses.");
            // 
            // audioMaxIntensityInput
            // 
            audioMaxIntensityInput.Location = new Point(262, 113);
            audioMaxIntensityInput.Name = "audioMaxIntensityInput";
            audioMaxIntensityInput.Size = new Size(89, 23);
            audioMaxIntensityInput.TabIndex = 33;
            helpToolTip.SetToolTip(audioMaxIntensityInput, "Specifies the maximum intensity of this effect");
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(6, 117);
            label24.Name = "label24";
            label24.Size = new Size(73, 15);
            label24.TabIndex = 32;
            label24.Text = "Intensity (%)";
            helpToolTip.SetToolTip(label24, "Specifies the maximum intensity of this effect");
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(6, 88);
            label23.Name = "label23";
            label23.Size = new Size(81, 15);
            label23.TabIndex = 31;
            label23.Text = "Max Bass (db)";
            helpToolTip.SetToolTip(label23, "Specifies the maximum db for bass effects.");
            // 
            // audioMaxBassInput
            // 
            audioMaxBassInput.Location = new Point(262, 84);
            audioMaxBassInput.Name = "audioMaxBassInput";
            audioMaxBassInput.Size = new Size(89, 23);
            audioMaxBassInput.TabIndex = 30;
            helpToolTip.SetToolTip(audioMaxBassInput, "Specifies the maximum db for bass effects.\r\nThis value is mostly used for intensity scaling.");
            // 
            // audioMonitorButton
            // 
            audioMonitorButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            audioMonitorButton.Location = new Point(6, 246);
            audioMonitorButton.Name = "audioMonitorButton";
            audioMonitorButton.Size = new Size(89, 23);
            audioMonitorButton.TabIndex = 29;
            audioMonitorButton.Text = "Monitor";
            audioMonitorButton.UseVisualStyleBackColor = true;
            audioMonitorButton.Click += AudioMonitorButton_Click;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(6, 59);
            label22.Name = "label22";
            label22.Size = new Size(79, 15);
            label22.TabIndex = 28;
            label22.Text = "Min Bass (db)";
            helpToolTip.SetToolTip(label22, "Specifies the minimum db threshold for bass effects.");
            // 
            // audioMinBassInput
            // 
            audioMinBassInput.Location = new Point(262, 55);
            audioMinBassInput.Name = "audioMinBassInput";
            audioMinBassInput.Size = new Size(89, 23);
            audioMinBassInput.TabIndex = 27;
            helpToolTip.SetToolTip(audioMinBassInput, "Specifies the minimum db threshold for bass effects.\r\nBass levels below this threshold will not trigger any sensations.");
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label20);
            groupBox4.Location = new Point(6, 173);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(348, 63);
            groupBox4.TabIndex = 26;
            groupBox4.TabStop = false;
            groupBox4.Text = "Information";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.FlatStyle = FlatStyle.Popup;
            label20.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label20.ForeColor = SystemColors.ControlDarkDark;
            label20.Location = new Point(16, 19);
            label20.Name = "label20";
            label20.Size = new Size(308, 34);
            label20.TabIndex = 20;
            label20.Text = "This effect attempts to create sensations based on the \r\nbass level of the system's audio output.\r\n";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(6, 30);
            label21.Name = "label21";
            label21.Size = new Size(45, 15);
            label21.TabIndex = 25;
            label21.Text = "Priority";
            helpToolTip.SetToolTip(label21, "Speicifies the priority of this effect (0 = lowest)");
            // 
            // audioPriorityInput
            // 
            audioPriorityInput.Location = new Point(262, 26);
            audioPriorityInput.Name = "audioPriorityInput";
            audioPriorityInput.Size = new Size(89, 23);
            audioPriorityInput.TabIndex = 24;
            helpToolTip.SetToolTip(audioPriorityInput, "Speicifies the priority of this effect (0 = lowest)");
            // 
            // audioEnabledCheckbox
            // 
            audioEnabledCheckbox.AutoSize = true;
            audioEnabledCheckbox.Location = new Point(6, 6);
            audioEnabledCheckbox.Name = "audioEnabledCheckbox";
            audioEnabledCheckbox.Size = new Size(68, 19);
            audioEnabledCheckbox.TabIndex = 23;
            audioEnabledCheckbox.Text = "Enabled";
            audioEnabledCheckbox.UseVisualStyleBackColor = true;
            // 
            // applyAudioSettingsButton
            // 
            applyAudioSettingsButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            applyAudioSettingsButton.Location = new Point(279, 246);
            applyAudioSettingsButton.Name = "applyAudioSettingsButton";
            applyAudioSettingsButton.Size = new Size(75, 23);
            applyAudioSettingsButton.TabIndex = 22;
            applyAudioSettingsButton.Text = "Apply";
            helpToolTip.SetToolTip(applyAudioSettingsButton, "Save and apply settings");
            applyAudioSettingsButton.UseVisualStyleBackColor = true;
            applyAudioSettingsButton.Click += ApplyAudioSettingsButton_Click;
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
            connectionGroup.Controls.Add(openDiscoveryButton);
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
            // openDiscoveryButton
            // 
            openDiscoveryButton.Image = Properties.Resources.Search;
            openDiscoveryButton.Location = new Point(150, 16);
            openDiscoveryButton.Name = "openDiscoveryButton";
            openDiscoveryButton.Size = new Size(23, 23);
            openDiscoveryButton.TabIndex = 13;
            helpToolTip.SetToolTip(openDiscoveryButton, "Scan for active MyOWO apps on the local network.\r\nThe app needs to be searching for games to be detected.");
            openDiscoveryButton.UseVisualStyleBackColor = true;
            openDiscoveryButton.Click += OpenDiscoveryButtom_Click;
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
            helpToolTip.SetToolTip(label19, "Status of the OWOWorldIntegration connector");
            // 
            // stopSensationsButton
            // 
            stopSensationsButton.Enabled = false;
            stopSensationsButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            stopSensationsButton.Location = new Point(6, 71);
            stopSensationsButton.Name = "stopSensationsButton";
            stopSensationsButton.Size = new Size(167, 23);
            stopSensationsButton.TabIndex = 10;
            stopSensationsButton.Text = "Stop all sensations";
            helpToolTip.SetToolTip(stopSensationsButton, "Force-stop all sensations.");
            stopSensationsButton.Click += StopSensationsButton_Click;
            // 
            // oscPortInput
            // 
            oscPortInput.Location = new Point(73, 42);
            oscPortInput.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            oscPortInput.Minimum = new decimal(new int[] { 1024, 0, 0, 0 });
            oscPortInput.Name = "oscPortInput";
            oscPortInput.Size = new Size(100, 23);
            oscPortInput.TabIndex = 9;
            helpToolTip.SetToolTip(oscPortInput, "Port to listen for OSC messages from VRChat");
            oscPortInput.Value = new decimal(new int[] { 1024, 0, 0, 0 });
            oscPortInput.Leave += OscPortInput_Exit;
            // 
            // owoIPInput
            // 
            owoIPInput.Location = new Point(74, 16);
            owoIPInput.Name = "owoIPInput";
            owoIPInput.Size = new Size(76, 23);
            owoIPInput.TabIndex = 8;
            owoIPInput.Text = "127.0.0.1";
            helpToolTip.SetToolTip(owoIPInput, "IP of the OWO app to connect to");
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
            helpToolTip.SetToolTip(label5, "IP of the OWO app to connect to");
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
            helpToolTip.SetToolTip(label4, "Port to listen for OSC messages from VRChat");
            // 
            // startButton
            // 
            startButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            startButton.Location = new Point(644, 71);
            startButton.Name = "startButton";
            startButton.Size = new Size(126, 23);
            startButton.TabIndex = 4;
            startButton.Text = "Start";
            helpToolTip.SetToolTip(startButton, "Connect and start sensation processing");
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
            helpToolTip.SetToolTip(label2, "Status of the OSC listener");
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
            helpToolTip.SetToolTip(label1, "Status of the OWO app");
            // 
            // stopButton
            // 
            stopButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            stopButton.Location = new Point(644, 71);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(126, 23);
            stopButton.TabIndex = 5;
            stopButton.Text = "Stop";
            helpToolTip.SetToolTip(stopButton, "Disconnect from OWO and VRChat");
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
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            Text = "OWOVRC";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            tabControl1.ResumeLayout(false);
            collidersSettingsPage.ResumeLayout(false);
            collidersSettingsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)collidersPriorityInput).EndInit();
            velocityBasedGroupBox.ResumeLayout(false);
            velocityBasedGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)collidersSpeedMultiplierInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)collidersMinIntensityInput).EndInit();
            velocitySettingsPage.ResumeLayout(false);
            velocitySettingsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)velocitySpeedCapInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)velocityPriorityInput).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)velocityMinImpactInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)velocityThresholdInput).EndInit();
            owiSettingsPage.ResumeLayout(false);
            owiSettingsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)owiIntensityInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)owiUpdateIntervalInput).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)owiPriorityInput).EndInit();
            oscPresetsPage.ResumeLayout(false);
            oscPresetsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)oscPresetsPriorityInput).EndInit();
            audioResponsePage.ResumeLayout(false);
            audioResponsePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)audioMaxIntensityInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)audioMaxBassInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)audioMinBassInput).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)audioPriorityInput).EndInit();
            connectionGroup.ResumeLayout(false);
            connectionGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)oscPortInput).EndInit();
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
        private NumericUpDown oscPortInput;
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
        private Label label8;
        private NumericUpDown collidersMinIntensityInput;
        private Label label9;
        private NumericUpDown collidersSpeedMultiplierInput;
        private CheckBox velocityEnabledCheckbox;
        private Label label7;
        private NumericUpDown velocityThresholdInput;
        private Label label10;
        private NumericUpDown velocityMinImpactInput;
        private GroupBox groupBox2;
        private Label label11;
        private NumericUpDown velocitySpeedCapInput;
        private CheckBox velocityImpactEnabledCheckbox;
        private CheckBox velocityIgnoreWhenGroundedCheckbox;
        private CheckBox velocityIgnoreWhenSeatedCheckbox;
        private Button stopSensationsButton;
        private Label label12;
        private NumericUpDown collidersPriorityInput;
        private Label label13;
        private NumericUpDown velocityPriorityInput;
        private TabPage owiSettingsPage;
        private LinkLabel owiLinkLabel;
        private Label label14;
        private NumericUpDown owiPriorityInput;
        private CheckBox owiEnabledCheckbox;
        private Button applyOwiSettingsButton;
        private Label label15;
        private GroupBox groupBox3;
        private Label label16;
        private NumericUpDown owiUpdateIntervalInput;
        private Label label17;
        private NumericUpDown owiIntensityInput;
        private Label owiStatusLabel;
        private Label label19;
        private TabPage oscPresetsPage;
        private Button applyOscPresetsSettingsButton;
        private Button openOscPresetsFormButton;
        private Label label18;
        private NumericUpDown oscPresetsPriorityInput;
        private CheckBox oscPresetsEnabledCheckbox;
        private ToolTip helpToolTip;
        private LinkLabel collidersHelpLinkLabel;
        private LinkLabel presetsHelpLinkLabel;
        private Label notVeryHelpfulLabel;
        private Button configureCollidersIntensityButton;
        private Button openDiscoveryButton;
        private TabPage audioResponsePage;
        private GroupBox groupBox4;
        private Label label20;
        private Label label21;
        private NumericUpDown audioPriorityInput;
        private CheckBox audioEnabledCheckbox;
        private Button applyAudioSettingsButton;
        private Label label22;
        private NumericUpDown audioMinBassInput;
        private Button audioMonitorButton;
        private Label label23;
        private NumericUpDown audioMaxBassInput;
        private NumericUpDown audioMaxIntensityInput;
        private Label label24;
        private Label label25;
        private Button audioDeviceSelectButton;
    }
}
