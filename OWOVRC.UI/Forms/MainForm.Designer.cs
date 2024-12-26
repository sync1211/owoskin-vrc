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
            collidersPriorityLabel = new Label();
            collidersPriorityInput = new NumericUpDown();
            configureCollidersIntensityLabel = new Label();
            velocityBasedGroupBox = new GroupBox();
            collidersSpeedMultiplierLabel = new Label();
            collidersSpeedMultiplierInput = new NumericUpDown();
            collidersMinIntensityLabel = new Label();
            collidersMinIntensityInput = new NumericUpDown();
            collidersUseVelocityCheckbox = new CheckBox();
            collidersAllowContinuousCheckbox = new CheckBox();
            collidersEnabledCheckbox = new CheckBox();
            applyCollisionSettingsButton = new Button();
            velocitySettingsPage = new TabPage();
            notVeryHelpfulLabel = new Label();
            velocitySpeedCapLabel = new Label();
            velocityPriorityLabel = new Label();
            velocitySpeedCapInput = new NumericUpDown();
            velocityPriorityInput = new NumericUpDown();
            velocityIgnoreWhenSeatedCheckbox = new CheckBox();
            velocityImpactGroup = new GroupBox();
            velocityMinImpactLabel = new Label();
            velocityImpactEnabledCheckbox = new CheckBox();
            velocityMinImpactInput = new NumericUpDown();
            velocityThresholdLabel = new Label();
            velocityThresholdInput = new NumericUpDown();
            velocityIgnoreWhenGroundedCheckbox = new CheckBox();
            velocityEnabledCheckbox = new CheckBox();
            applyVelocitySettingsButton = new Button();
            owiSettingsPage = new TabPage();
            owiIntensityLabel = new Label();
            owiIntensityInput = new NumericUpDown();
            owiUpdateIntervalLabel = new Label();
            owiUpdateIntervalInput = new NumericUpDown();
            owiInformationGroup = new GroupBox();
            owiInfoLabel = new Label();
            owiLinkLabel = new LinkLabel();
            owiPriorityLabel = new Label();
            owiPriorityInput = new NumericUpDown();
            owiEnabledCheckbox = new CheckBox();
            applyOwiSettingsButton = new Button();
            oscPresetsPage = new TabPage();
            presetsHelpLinkLabel = new LinkLabel();
            oscPresetsPriorityLabel = new Label();
            oscPresetsPriorityInput = new NumericUpDown();
            oscPresetsEnabledCheckbox = new CheckBox();
            openOscPresetsFormButton = new Button();
            applyOscPresetsSettingsButton = new Button();
            audioResponsePage = new TabPage();
            audioDeviceSelectButton = new Button();
            audioMonitorButton = new Button();
            applyAudioSettingsButton = new Button();
            audioEnabledCheckbox = new CheckBox();
            audioSettingsPriorityPanel1 = new Controls.AudioSettingsPriorityPanel();
            logLevelComboBox = new ComboBox();
            label3 = new Label();
            logBox = new RichTextBox();
            connectionGroup = new GroupBox();
            audioStatusLabel = new Label();
            audioStatusTitle = new Label();
            openDiscoveryButton = new Button();
            owiStatusLabel = new Label();
            owiStatusTitle = new Label();
            stopSensationsButton = new Button();
            oscPortInput = new NumericUpDown();
            owoIPInput = new MaskedTextBox();
            owoIPTitle = new Label();
            oscPortTitle = new Label();
            startButton = new Button();
            oscStatusLabel = new Label();
            oscStatusTitle = new Label();
            connectionStatusLabel = new Label();
            owoStatusTitle = new Label();
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
            velocityImpactGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)velocityMinImpactInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)velocityThresholdInput).BeginInit();
            owiSettingsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)owiIntensityInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)owiUpdateIntervalInput).BeginInit();
            owiInformationGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)owiPriorityInput).BeginInit();
            oscPresetsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)oscPresetsPriorityInput).BeginInit();
            audioResponsePage.SuspendLayout();
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
            collidersSettingsPage.Controls.Add(collidersPriorityLabel);
            collidersSettingsPage.Controls.Add(collidersPriorityInput);
            collidersSettingsPage.Controls.Add(configureCollidersIntensityLabel);
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
            // collidersPriorityLabel
            // 
            collidersPriorityLabel.AutoSize = true;
            collidersPriorityLabel.Location = new Point(6, 30);
            collidersPriorityLabel.Name = "collidersPriorityLabel";
            collidersPriorityLabel.Size = new Size(45, 15);
            collidersPriorityLabel.TabIndex = 9;
            collidersPriorityLabel.Text = "Priority";
            helpToolTip.SetToolTip(collidersPriorityLabel, "Speicifies the priority of this effect (0 = lowest)");
            // 
            // collidersPriorityInput
            // 
            collidersPriorityInput.Location = new Point(262, 26);
            collidersPriorityInput.Name = "collidersPriorityInput";
            collidersPriorityInput.Size = new Size(89, 23);
            collidersPriorityInput.TabIndex = 8;
            helpToolTip.SetToolTip(collidersPriorityInput, "Speicifies the priority of this effect (0 = lowest)");
            // 
            // configureCollidersIntensityLabel
            // 
            configureCollidersIntensityLabel.AutoSize = true;
            configureCollidersIntensityLabel.Location = new Point(6, 59);
            configureCollidersIntensityLabel.Name = "configureCollidersIntensityLabel";
            configureCollidersIntensityLabel.Size = new Size(73, 15);
            configureCollidersIntensityLabel.TabIndex = 7;
            configureCollidersIntensityLabel.Text = "Intensity (%)";
            helpToolTip.SetToolTip(configureCollidersIntensityLabel, "Specifies the intensity of this effect");
            // 
            // velocityBasedGroupBox
            // 
            velocityBasedGroupBox.Controls.Add(collidersSpeedMultiplierLabel);
            velocityBasedGroupBox.Controls.Add(collidersSpeedMultiplierInput);
            velocityBasedGroupBox.Controls.Add(collidersMinIntensityLabel);
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
            // collidersSpeedMultiplierLabel
            // 
            collidersSpeedMultiplierLabel.AutoSize = true;
            collidersSpeedMultiplierLabel.Location = new Point(6, 107);
            collidersSpeedMultiplierLabel.Name = "collidersSpeedMultiplierLabel";
            collidersSpeedMultiplierLabel.Size = new Size(93, 15);
            collidersSpeedMultiplierLabel.TabIndex = 11;
            collidersSpeedMultiplierLabel.Text = "Speed multiplier";
            helpToolTip.SetToolTip(collidersSpeedMultiplierLabel, "Multiplier for velocity-based sensation scaling (Supports ");
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
            // collidersMinIntensityLabel
            // 
            collidersMinIntensityLabel.AutoSize = true;
            collidersMinIntensityLabel.Location = new Point(6, 78);
            collidersMinIntensityLabel.Name = "collidersMinIntensityLabel";
            collidersMinIntensityLabel.Size = new Size(76, 15);
            collidersMinIntensityLabel.TabIndex = 9;
            collidersMinIntensityLabel.Text = "Min Intensity";
            helpToolTip.SetToolTip(collidersMinIntensityLabel, "The base intensity for velocity-based collisions. (Idle intensity)");
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
            velocitySettingsPage.Controls.Add(velocitySpeedCapLabel);
            velocitySettingsPage.Controls.Add(velocityPriorityLabel);
            velocitySettingsPage.Controls.Add(velocitySpeedCapInput);
            velocitySettingsPage.Controls.Add(velocityPriorityInput);
            velocitySettingsPage.Controls.Add(velocityIgnoreWhenSeatedCheckbox);
            velocitySettingsPage.Controls.Add(velocityImpactGroup);
            velocitySettingsPage.Controls.Add(velocityThresholdLabel);
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
            // velocitySpeedCapLabel
            // 
            velocitySpeedCapLabel.AutoSize = true;
            velocitySpeedCapLabel.Location = new Point(6, 83);
            velocitySpeedCapLabel.Name = "velocitySpeedCapLabel";
            velocitySpeedCapLabel.Size = new Size(96, 15);
            velocitySpeedCapLabel.TabIndex = 11;
            velocitySpeedCapLabel.Text = "Max speed (m/s)";
            helpToolTip.SetToolTip(velocitySpeedCapLabel, "Maximum speed for wind effects (used for scaling)");
            // 
            // velocityPriorityLabel
            // 
            velocityPriorityLabel.AutoSize = true;
            velocityPriorityLabel.Location = new Point(6, 30);
            velocityPriorityLabel.Name = "velocityPriorityLabel";
            velocityPriorityLabel.Size = new Size(45, 15);
            velocityPriorityLabel.TabIndex = 15;
            velocityPriorityLabel.Text = "Priority";
            helpToolTip.SetToolTip(velocityPriorityLabel, "Speicifies the priority of this effect (0 = lowest)");
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
            // velocityImpactGroup
            // 
            velocityImpactGroup.Controls.Add(velocityMinImpactLabel);
            velocityImpactGroup.Controls.Add(velocityImpactEnabledCheckbox);
            velocityImpactGroup.Controls.Add(velocityMinImpactInput);
            velocityImpactGroup.Location = new Point(6, 165);
            velocityImpactGroup.Name = "velocityImpactGroup";
            velocityImpactGroup.Size = new Size(345, 75);
            velocityImpactGroup.TabIndex = 12;
            velocityImpactGroup.TabStop = false;
            velocityImpactGroup.Text = "Impact";
            // 
            // velocityMinImpactLabel
            // 
            velocityMinImpactLabel.AutoSize = true;
            velocityMinImpactLabel.Location = new Point(6, 44);
            velocityMinImpactLabel.Name = "velocityMinImpactLabel";
            velocityMinImpactLabel.Size = new Size(147, 15);
            velocityMinImpactLabel.TabIndex = 11;
            velocityMinImpactLabel.Text = "Min. impact velocity (m/s)";
            helpToolTip.SetToolTip(velocityMinImpactLabel, "Minimum velocity to reach to trigger the impact effect on deceleration");
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
            // velocityThresholdLabel
            // 
            velocityThresholdLabel.AutoSize = true;
            velocityThresholdLabel.Location = new Point(6, 58);
            velocityThresholdLabel.Name = "velocityThresholdLabel";
            velocityThresholdLabel.Size = new Size(107, 15);
            velocityThresholdLabel.TabIndex = 9;
            velocityThresholdLabel.Text = "Min. velocity (m/s)";
            helpToolTip.SetToolTip(velocityThresholdLabel, "Minimum speed for triggering wind effects");
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
            owiSettingsPage.Controls.Add(owiIntensityLabel);
            owiSettingsPage.Controls.Add(owiIntensityInput);
            owiSettingsPage.Controls.Add(owiUpdateIntervalLabel);
            owiSettingsPage.Controls.Add(owiUpdateIntervalInput);
            owiSettingsPage.Controls.Add(owiInformationGroup);
            owiSettingsPage.Controls.Add(owiLinkLabel);
            owiSettingsPage.Controls.Add(owiPriorityLabel);
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
            // owiIntensityLabel
            // 
            owiIntensityLabel.AutoSize = true;
            owiIntensityLabel.Location = new Point(6, 89);
            owiIntensityLabel.Name = "owiIntensityLabel";
            owiIntensityLabel.Size = new Size(73, 15);
            owiIntensityLabel.TabIndex = 25;
            owiIntensityLabel.Text = "Intensity (%)";
            helpToolTip.SetToolTip(owiIntensityLabel, "Specifies the intensity of this effect");
            // 
            // owiIntensityInput
            // 
            owiIntensityInput.Location = new Point(262, 86);
            owiIntensityInput.Name = "owiIntensityInput";
            owiIntensityInput.Size = new Size(89, 23);
            owiIntensityInput.TabIndex = 24;
            helpToolTip.SetToolTip(owiIntensityInput, "Specifies the intensity of this effect");
            // 
            // owiUpdateIntervalLabel
            // 
            owiUpdateIntervalLabel.AutoSize = true;
            owiUpdateIntervalLabel.Location = new Point(6, 59);
            owiUpdateIntervalLabel.Name = "owiUpdateIntervalLabel";
            owiUpdateIntervalLabel.Size = new Size(123, 15);
            owiUpdateIntervalLabel.TabIndex = 23;
            owiUpdateIntervalLabel.Text = "Log scan interval (ms)";
            helpToolTip.SetToolTip(owiUpdateIntervalLabel, "Time between checking the VRChat log for new sensations from OWI");
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
            // owiInformationGroup
            // 
            owiInformationGroup.Controls.Add(owiInfoLabel);
            owiInformationGroup.Location = new Point(6, 115);
            owiInformationGroup.Name = "owiInformationGroup";
            owiInformationGroup.Size = new Size(348, 121);
            owiInformationGroup.TabIndex = 21;
            owiInformationGroup.TabStop = false;
            owiInformationGroup.Text = "Information";
            // 
            // owiInfoLabel
            // 
            owiInfoLabel.AutoSize = true;
            owiInfoLabel.FlatStyle = FlatStyle.Popup;
            owiInfoLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            owiInfoLabel.ForeColor = SystemColors.ControlDarkDark;
            owiInfoLabel.Location = new Point(21, 23);
            owiInfoLabel.Name = "owiInfoLabel";
            owiInfoLabel.Size = new Size(308, 85);
            owiInfoLabel.TabIndex = 20;
            owiInfoLabel.Text = "This effect receives data from worlds\r\nusing OWO World Integrator.\r\n\r\nTo use this effect, please enable debug logging in your\r\nVRChat client and visit a world that supports OWI.";
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
            // owiPriorityLabel
            // 
            owiPriorityLabel.AutoSize = true;
            owiPriorityLabel.Location = new Point(6, 30);
            owiPriorityLabel.Name = "owiPriorityLabel";
            owiPriorityLabel.Size = new Size(45, 15);
            owiPriorityLabel.TabIndex = 19;
            owiPriorityLabel.Text = "Priority";
            helpToolTip.SetToolTip(owiPriorityLabel, "Speicifies the priority of this effect (0 = lowest)");
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
            oscPresetsPage.Controls.Add(oscPresetsPriorityLabel);
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
            // oscPresetsPriorityLabel
            // 
            oscPresetsPriorityLabel.AutoSize = true;
            oscPresetsPriorityLabel.Location = new Point(6, 30);
            oscPresetsPriorityLabel.Name = "oscPresetsPriorityLabel";
            oscPresetsPriorityLabel.Size = new Size(45, 15);
            oscPresetsPriorityLabel.TabIndex = 22;
            oscPresetsPriorityLabel.Text = "Priority";
            helpToolTip.SetToolTip(oscPresetsPriorityLabel, "Speicifies the priority of this effect (0 = lowest)");
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
            audioResponsePage.Controls.Add(audioMonitorButton);
            audioResponsePage.Controls.Add(applyAudioSettingsButton);
            audioResponsePage.Controls.Add(audioEnabledCheckbox);
            audioResponsePage.Controls.Add(audioSettingsPriorityPanel1);
            audioResponsePage.Location = new Point(4, 24);
            audioResponsePage.Name = "audioResponsePage";
            audioResponsePage.Size = new Size(360, 275);
            audioResponsePage.TabIndex = 5;
            audioResponsePage.Text = "Audio";
            audioResponsePage.UseVisualStyleBackColor = true;
            // 
            // audioDeviceSelectButton
            // 
            audioDeviceSelectButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            audioDeviceSelectButton.Location = new Point(254, 3);
            audioDeviceSelectButton.Name = "audioDeviceSelectButton";
            audioDeviceSelectButton.Size = new Size(100, 23);
            audioDeviceSelectButton.TabIndex = 37;
            audioDeviceSelectButton.Text = "Select Device";
            helpToolTip.SetToolTip(audioDeviceSelectButton, "Configure which audio device the effect uses.\r\nShift-Click to show all available audio devices (input and output).");
            audioDeviceSelectButton.UseVisualStyleBackColor = true;
            audioDeviceSelectButton.Click += AudioDeviceSelectButton_Click;
            // 
            // audioMonitorButton
            // 
            audioMonitorButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            audioMonitorButton.Location = new Point(6, 246);
            audioMonitorButton.Name = "audioMonitorButton";
            audioMonitorButton.Size = new Size(89, 23);
            audioMonitorButton.TabIndex = 31;
            audioMonitorButton.Text = "Monitor";
            audioMonitorButton.UseVisualStyleBackColor = true;
            audioMonitorButton.Click += AudioMonitorButton_Click;
            // 
            // applyAudioSettingsButton
            // 
            applyAudioSettingsButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            applyAudioSettingsButton.Location = new Point(279, 246);
            applyAudioSettingsButton.Name = "applyAudioSettingsButton";
            applyAudioSettingsButton.Size = new Size(75, 23);
            applyAudioSettingsButton.TabIndex = 30;
            applyAudioSettingsButton.Text = "Apply";
            helpToolTip.SetToolTip(applyAudioSettingsButton, "Save and apply settings");
            applyAudioSettingsButton.UseVisualStyleBackColor = true;
            applyAudioSettingsButton.Click += ApplyAudioSettingsButton_Click;
            // 
            // audioEnabledCheckbox
            // 
            audioEnabledCheckbox.AutoSize = true;
            audioEnabledCheckbox.Location = new Point(6, 6);
            audioEnabledCheckbox.Name = "audioEnabledCheckbox";
            audioEnabledCheckbox.Size = new Size(68, 19);
            audioEnabledCheckbox.TabIndex = 1;
            audioEnabledCheckbox.Text = "Enabled";
            audioEnabledCheckbox.UseVisualStyleBackColor = true;
            // 
            // audioSettingsPriorityPanel1
            // 
            audioSettingsPriorityPanel1.BackColor = Color.DimGray;
            audioSettingsPriorityPanel1.Location = new Point(5, 28);
            audioSettingsPriorityPanel1.Name = "audioSettingsPriorityPanel1";
            audioSettingsPriorityPanel1.Size = new Size(349, 212);
            audioSettingsPriorityPanel1.TabIndex = 0;
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
            connectionGroup.Controls.Add(audioStatusLabel);
            connectionGroup.Controls.Add(audioStatusTitle);
            connectionGroup.Controls.Add(openDiscoveryButton);
            connectionGroup.Controls.Add(owiStatusLabel);
            connectionGroup.Controls.Add(owiStatusTitle);
            connectionGroup.Controls.Add(stopSensationsButton);
            connectionGroup.Controls.Add(oscPortInput);
            connectionGroup.Controls.Add(owoIPInput);
            connectionGroup.Controls.Add(owoIPTitle);
            connectionGroup.Controls.Add(oscPortTitle);
            connectionGroup.Controls.Add(startButton);
            connectionGroup.Controls.Add(oscStatusLabel);
            connectionGroup.Controls.Add(oscStatusTitle);
            connectionGroup.Controls.Add(connectionStatusLabel);
            connectionGroup.Controls.Add(owoStatusTitle);
            connectionGroup.Controls.Add(stopButton);
            connectionGroup.Location = new Point(12, 12);
            connectionGroup.Name = "connectionGroup";
            connectionGroup.Size = new Size(776, 100);
            connectionGroup.TabIndex = 1;
            connectionGroup.TabStop = false;
            connectionGroup.Text = "Connection";
            // 
            // audioStatusLabel
            // 
            audioStatusLabel.AutoSize = true;
            audioStatusLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            audioStatusLabel.ForeColor = Color.Red;
            audioStatusLabel.Location = new Point(687, 56);
            audioStatusLabel.Name = "audioStatusLabel";
            audioStatusLabel.Size = new Size(54, 15);
            audioStatusLabel.TabIndex = 15;
            audioStatusLabel.Text = "Stopped";
            // 
            // audioStatusTitle
            // 
            audioStatusTitle.AutoSize = true;
            audioStatusTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            audioStatusTitle.Location = new Point(644, 56);
            audioStatusTitle.Name = "audioStatusTitle";
            audioStatusTitle.Size = new Size(42, 15);
            audioStatusTitle.TabIndex = 14;
            audioStatusTitle.Text = "Audio:";
            helpToolTip.SetToolTip(audioStatusTitle, "Status of the audio capture\r\n");
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
            owiStatusLabel.Location = new Point(687, 41);
            owiStatusLabel.Name = "owiStatusLabel";
            owiStatusLabel.Size = new Size(54, 15);
            owiStatusLabel.TabIndex = 12;
            owiStatusLabel.Text = "Stopped";
            // 
            // owiStatusTitle
            // 
            owiStatusTitle.AutoSize = true;
            owiStatusTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            owiStatusTitle.Location = new Point(644, 41);
            owiStatusTitle.Name = "owiStatusTitle";
            owiStatusTitle.Size = new Size(35, 15);
            owiStatusTitle.TabIndex = 11;
            owiStatusTitle.Text = "OWI:";
            helpToolTip.SetToolTip(owiStatusTitle, "Status of the OWOWorldIntegration connector");
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
            // owoIPTitle
            // 
            owoIPTitle.AutoSize = true;
            owoIPTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            owoIPTitle.Location = new Point(10, 19);
            owoIPTitle.Name = "owoIPTitle";
            owoIPTitle.Size = new Size(51, 15);
            owoIPTitle.TabIndex = 7;
            owoIPTitle.Text = "OWO IP";
            helpToolTip.SetToolTip(owoIPTitle, "IP of the OWO app to connect to");
            // 
            // oscPortTitle
            // 
            oscPortTitle.AutoSize = true;
            oscPortTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            oscPortTitle.Location = new Point(10, 45);
            oscPortTitle.Name = "oscPortTitle";
            oscPortTitle.Size = new Size(57, 15);
            oscPortTitle.TabIndex = 6;
            oscPortTitle.Text = "OSC Port";
            helpToolTip.SetToolTip(oscPortTitle, "Port to listen for OSC messages from VRChat");
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
            oscStatusLabel.Location = new Point(687, 25);
            oscStatusLabel.Name = "oscStatusLabel";
            oscStatusLabel.Size = new Size(54, 15);
            oscStatusLabel.TabIndex = 3;
            oscStatusLabel.Text = "Stopped";
            // 
            // oscStatusTitle
            // 
            oscStatusTitle.AutoSize = true;
            oscStatusTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            oscStatusTitle.Location = new Point(644, 25);
            oscStatusTitle.Name = "oscStatusTitle";
            oscStatusTitle.Size = new Size(33, 15);
            oscStatusTitle.TabIndex = 2;
            oscStatusTitle.Text = "OSC:";
            helpToolTip.SetToolTip(oscStatusTitle, "Status of the OSC listener");
            // 
            // connectionStatusLabel
            // 
            connectionStatusLabel.AutoSize = true;
            connectionStatusLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            connectionStatusLabel.ForeColor = Color.Red;
            connectionStatusLabel.Location = new Point(687, 10);
            connectionStatusLabel.Name = "connectionStatusLabel";
            connectionStatusLabel.Size = new Size(83, 15);
            connectionStatusLabel.TabIndex = 1;
            connectionStatusLabel.Text = "Disconnected";
            // 
            // owoStatusTitle
            // 
            owoStatusTitle.AutoSize = true;
            owoStatusTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            owoStatusTitle.Location = new Point(642, 10);
            owoStatusTitle.Name = "owoStatusTitle";
            owoStatusTitle.Size = new Size(40, 15);
            owoStatusTitle.TabIndex = 0;
            owoStatusTitle.Text = "OWO:";
            helpToolTip.SetToolTip(owoStatusTitle, "Status of the OWO app");
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
            velocityImpactGroup.ResumeLayout(false);
            velocityImpactGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)velocityMinImpactInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)velocityThresholdInput).EndInit();
            owiSettingsPage.ResumeLayout(false);
            owiSettingsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)owiIntensityInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)owiUpdateIntervalInput).EndInit();
            owiInformationGroup.ResumeLayout(false);
            owiInformationGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)owiPriorityInput).EndInit();
            oscPresetsPage.ResumeLayout(false);
            oscPresetsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)oscPresetsPriorityInput).EndInit();
            audioResponsePage.ResumeLayout(false);
            audioResponsePage.PerformLayout();
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
        private Label oscStatusTitle;
        private Label connectionStatusLabel;
        private Label owoStatusTitle;
        private Button startButton;
        private Button stopButton;
        private ComboBox logLevelComboBox;
        private Label label3;
        private NumericUpDown oscPortInput;
        private MaskedTextBox owoIPInput;
        private Label owoIPTitle;
        private Label oscPortTitle;
        private GroupBox groupBox1;
        private TabPage collidersSettingsPage;
        private Button applyCollisionSettingsButton;
        private Button applyVelocitySettingsButton;
        private CheckBox collidersEnabledCheckbox;
        private Label configureCollidersIntensityLabel;
        private GroupBox velocityBasedGroupBox;
        private CheckBox collidersUseVelocityCheckbox;
        private CheckBox collidersAllowContinuousCheckbox;
        private Label collidersMinIntensityLabel;
        private NumericUpDown collidersMinIntensityInput;
        private Label collidersSpeedMultiplierLabel;
        private NumericUpDown collidersSpeedMultiplierInput;
        private CheckBox velocityEnabledCheckbox;
        private Label velocityThresholdLabel;
        private NumericUpDown velocityThresholdInput;
        private Label velocityMinImpactLabel;
        private NumericUpDown velocityMinImpactInput;
        private GroupBox velocityImpactGroup;
        private Label velocitySpeedCapLabel;
        private NumericUpDown velocitySpeedCapInput;
        private CheckBox velocityImpactEnabledCheckbox;
        private CheckBox velocityIgnoreWhenGroundedCheckbox;
        private CheckBox velocityIgnoreWhenSeatedCheckbox;
        private Button stopSensationsButton;
        private Label collidersPriorityLabel;
        private NumericUpDown collidersPriorityInput;
        private Label velocityPriorityLabel;
        private NumericUpDown velocityPriorityInput;
        private TabPage owiSettingsPage;
        private LinkLabel owiLinkLabel;
        private Label owiPriorityLabel;
        private NumericUpDown owiPriorityInput;
        private CheckBox owiEnabledCheckbox;
        private Button applyOwiSettingsButton;
        private Label owiInfoLabel;
        private GroupBox owiInformationGroup;
        private Label owiUpdateIntervalLabel;
        private NumericUpDown owiUpdateIntervalInput;
        private Label owiIntensityLabel;
        private NumericUpDown owiIntensityInput;
        private Label owiStatusLabel;
        private Label owiStatusTitle;
        private TabPage oscPresetsPage;
        private Button applyOscPresetsSettingsButton;
        private Button openOscPresetsFormButton;
        private Label oscPresetsPriorityLabel;
        private NumericUpDown oscPresetsPriorityInput;
        private CheckBox oscPresetsEnabledCheckbox;
        private ToolTip helpToolTip;
        private LinkLabel collidersHelpLinkLabel;
        private LinkLabel presetsHelpLinkLabel;
        private Label notVeryHelpfulLabel;
        private Button configureCollidersIntensityButton;
        private Button openDiscoveryButton;
        private TabPage audioResponsePage;
        private CheckBox audioEnabledCheckbox;
        private Button applyAudioSettingsButton;
        private Button audioDeviceSelectButton;
        private Controls.AudioSettingsPriorityPanel audioSettingsPriorityPanel1;
        private Button audioMonitorButton;
        private Label audioStatusLabel;
        private Label audioStatusTitle;
    }
}
