using OWOVRC.UI.Controls;

namespace OWOVRC.UI.Forms
{
    partial class MuscleIntensityForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MuscleIntensityForm));
            showFrontButton = new Button();
            muscleGroupsTabControl = new TabControl();
            frontMusclesPage = new TabPage();
            abdominalLMuscle = new SelectableMuscle();
            abdominalRMuscle = new SelectableMuscle();
            armLMuscle = new SelectableMuscle();
            pectoralLMuscle = new SelectableMuscle();
            pectoralRMuscle = new SelectableMuscle();
            armRMuscle = new SelectableMuscle();
            frontMusclesImg = new PictureBox();
            backMusclesPage = new TabPage();
            lumbarLMuscle = new SelectableMuscle();
            lumbarRMuscle = new SelectableMuscle();
            dorsalLMuscle = new SelectableMuscle();
            dorsalRMuscle = new SelectableMuscle();
            backMusclesImg = new PictureBox();
            showBackButton = new Button();
            muscleIntensityTrackBar = new TrackBar();
            intensityValueTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            muscleNameLabel = new Label();
            closeButton = new Button();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            resetButton = new Button();
            label6 = new Label();
            panel1 = new Panel();
            label8 = new Label();
            label7 = new Label();
            testSensationButton = new Button();
            muscleGroupsTabControl.SuspendLayout();
            frontMusclesPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)abdominalLMuscle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)abdominalRMuscle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)armLMuscle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pectoralLMuscle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pectoralRMuscle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)armRMuscle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)frontMusclesImg).BeginInit();
            backMusclesPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lumbarLMuscle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lumbarRMuscle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dorsalLMuscle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dorsalRMuscle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)backMusclesImg).BeginInit();
            ((System.ComponentModel.ISupportInitialize)muscleIntensityTrackBar).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // showFrontButton
            // 
            showFrontButton.Location = new Point(266, 9);
            showFrontButton.Name = "showFrontButton";
            showFrontButton.Size = new Size(75, 23);
            showFrontButton.TabIndex = 0;
            showFrontButton.Text = "Front";
            showFrontButton.UseVisualStyleBackColor = true;
            showFrontButton.Click += ShowFrontButton_Click;
            // 
            // muscleGroupsTabControl
            // 
            muscleGroupsTabControl.Appearance = TabAppearance.FlatButtons;
            muscleGroupsTabControl.Controls.Add(frontMusclesPage);
            muscleGroupsTabControl.Controls.Add(backMusclesPage);
            muscleGroupsTabControl.Location = new Point(0, 7);
            muscleGroupsTabControl.Multiline = true;
            muscleGroupsTabControl.Name = "muscleGroupsTabControl";
            muscleGroupsTabControl.SelectedIndex = 0;
            muscleGroupsTabControl.Size = new Size(681, 473);
            muscleGroupsTabControl.TabIndex = 1;
            // 
            // frontMusclesPage
            // 
            frontMusclesPage.Controls.Add(abdominalLMuscle);
            frontMusclesPage.Controls.Add(abdominalRMuscle);
            frontMusclesPage.Controls.Add(armLMuscle);
            frontMusclesPage.Controls.Add(pectoralLMuscle);
            frontMusclesPage.Controls.Add(pectoralRMuscle);
            frontMusclesPage.Controls.Add(armRMuscle);
            frontMusclesPage.Controls.Add(frontMusclesImg);
            frontMusclesPage.Location = new Point(4, 27);
            frontMusclesPage.Name = "frontMusclesPage";
            frontMusclesPage.Padding = new Padding(3);
            frontMusclesPage.Size = new Size(673, 442);
            frontMusclesPage.TabIndex = 0;
            frontMusclesPage.Text = "Front";
            frontMusclesPage.UseVisualStyleBackColor = true;
            // 
            // abdominalLMuscle
            // 
            abdominalLMuscle.Active = false;
            abdominalLMuscle.ActiveImage = Properties.Resources.muscleAbdominalL_Active;
            abdominalLMuscle.BackgroundImage = Properties.Resources.muscleAbdominalL_Inactive;
            abdominalLMuscle.BackgroundImageLayout = ImageLayout.Zoom;
            abdominalLMuscle.InactiveImage = Properties.Resources.muscleAbdominalL_Inactive;
            abdominalLMuscle.Location = new Point(358, 269);
            abdominalLMuscle.Muscle = MusclesEnum.Abdominal_L;
            abdominalLMuscle.Name = "abdominalLMuscle";
            abdominalLMuscle.Size = new Size(65, 142);
            abdominalLMuscle.TabIndex = 6;
            abdominalLMuscle.TabStop = false;
            // 
            // abdominalRMuscle
            // 
            abdominalRMuscle.Active = false;
            abdominalRMuscle.ActiveImage = Properties.Resources.muscleAbdominalR_Active;
            abdominalRMuscle.BackgroundImage = Properties.Resources.muscleAbdominalR_Inactive;
            abdominalRMuscle.BackgroundImageLayout = ImageLayout.Zoom;
            abdominalRMuscle.InactiveImage = Properties.Resources.muscleAbdominalR_Inactive;
            abdominalRMuscle.Location = new Point(248, 269);
            abdominalRMuscle.Muscle = MusclesEnum.Abdominal_R;
            abdominalRMuscle.Name = "abdominalRMuscle";
            abdominalRMuscle.Size = new Size(65, 142);
            abdominalRMuscle.TabIndex = 3;
            abdominalRMuscle.TabStop = false;
            // 
            // armLMuscle
            // 
            armLMuscle.Active = false;
            armLMuscle.ActiveImage = Properties.Resources.muscleArmL_Active;
            armLMuscle.BackgroundImage = Properties.Resources.muscleArmL_Inactive;
            armLMuscle.BackgroundImageLayout = ImageLayout.Zoom;
            armLMuscle.InactiveImage = Properties.Resources.muscleArmL_Inactive;
            armLMuscle.Location = new Point(456, 169);
            armLMuscle.Muscle = MusclesEnum.Arm_L;
            armLMuscle.Name = "armLMuscle";
            armLMuscle.Size = new Size(85, 96);
            armLMuscle.TabIndex = 5;
            armLMuscle.TabStop = false;
            // 
            // pectoralLMuscle
            // 
            pectoralLMuscle.Active = false;
            pectoralLMuscle.ActiveImage = Properties.Resources.musclePectoralL_Active;
            pectoralLMuscle.BackgroundImage = Properties.Resources.musclePectoralL_Inactive;
            pectoralLMuscle.BackgroundImageLayout = ImageLayout.Zoom;
            pectoralLMuscle.InactiveImage = Properties.Resources.musclePectoralL_Inactive;
            pectoralLMuscle.Location = new Point(337, 121);
            pectoralLMuscle.Muscle = MusclesEnum.Pectoral_L;
            pectoralLMuscle.Name = "pectoralLMuscle";
            pectoralLMuscle.Size = new Size(112, 58);
            pectoralLMuscle.TabIndex = 4;
            pectoralLMuscle.TabStop = false;
            // 
            // pectoralRMuscle
            // 
            pectoralRMuscle.Active = false;
            pectoralRMuscle.ActiveImage = Properties.Resources.musclePectoralR_Active;
            pectoralRMuscle.BackgroundImage = Properties.Resources.musclePectoralR_Inactive;
            pectoralRMuscle.BackgroundImageLayout = ImageLayout.Zoom;
            pectoralRMuscle.InactiveImage = Properties.Resources.musclePectoralR_Inactive;
            pectoralRMuscle.Location = new Point(224, 120);
            pectoralRMuscle.Muscle = MusclesEnum.Pectoral_R;
            pectoralRMuscle.Name = "pectoralRMuscle";
            pectoralRMuscle.Size = new Size(112, 58);
            pectoralRMuscle.TabIndex = 3;
            pectoralRMuscle.TabStop = false;
            // 
            // armRMuscle
            // 
            armRMuscle.Active = false;
            armRMuscle.ActiveImage = Properties.Resources.muscleArmR_Active;
            armRMuscle.BackgroundImage = Properties.Resources.muscleArmR_Inactive;
            armRMuscle.BackgroundImageLayout = ImageLayout.Zoom;
            armRMuscle.InactiveImage = Properties.Resources.muscleArmR_Inactive;
            armRMuscle.Location = new Point(130, 173);
            armRMuscle.Muscle = MusclesEnum.Arm_R;
            armRMuscle.Name = "armRMuscle";
            armRMuscle.Size = new Size(85, 96);
            armRMuscle.TabIndex = 3;
            armRMuscle.TabStop = false;
            // 
            // frontMusclesImg
            // 
            frontMusclesImg.BackgroundImage = Properties.Resources.musclesFrontClear;
            frontMusclesImg.BackgroundImageLayout = ImageLayout.Zoom;
            frontMusclesImg.Location = new Point(0, 0);
            frontMusclesImg.Name = "frontMusclesImg";
            frontMusclesImg.Size = new Size(669, 459);
            frontMusclesImg.TabIndex = 0;
            frontMusclesImg.TabStop = false;
            // 
            // backMusclesPage
            // 
            backMusclesPage.Controls.Add(lumbarLMuscle);
            backMusclesPage.Controls.Add(lumbarRMuscle);
            backMusclesPage.Controls.Add(dorsalLMuscle);
            backMusclesPage.Controls.Add(dorsalRMuscle);
            backMusclesPage.Controls.Add(backMusclesImg);
            backMusclesPage.Location = new Point(4, 27);
            backMusclesPage.Name = "backMusclesPage";
            backMusclesPage.Padding = new Padding(3);
            backMusclesPage.Size = new Size(673, 442);
            backMusclesPage.TabIndex = 1;
            backMusclesPage.Text = "Back";
            backMusclesPage.UseVisualStyleBackColor = true;
            // 
            // lumbarLMuscle
            // 
            lumbarLMuscle.Active = false;
            lumbarLMuscle.ActiveImage = Properties.Resources.muscleLumbarL_Active;
            lumbarLMuscle.BackgroundImage = Properties.Resources.muscleLumbarL_Inactive;
            lumbarLMuscle.BackgroundImageLayout = ImageLayout.Zoom;
            lumbarLMuscle.InactiveImage = Properties.Resources.muscleLumbarL_Inactive;
            lumbarLMuscle.Location = new Point(348, 300);
            lumbarLMuscle.Muscle = MusclesEnum.Lumbar_L;
            lumbarLMuscle.Name = "lumbarLMuscle";
            lumbarLMuscle.Size = new Size(68, 68);
            lumbarLMuscle.TabIndex = 6;
            lumbarLMuscle.TabStop = false;
            // 
            // lumbarRMuscle
            // 
            lumbarRMuscle.Active = false;
            lumbarRMuscle.ActiveImage = Properties.Resources.muscleLumbarR_Active;
            lumbarRMuscle.BackgroundImage = Properties.Resources.muscleLumbarR_Inactive;
            lumbarRMuscle.BackgroundImageLayout = ImageLayout.Zoom;
            lumbarRMuscle.InactiveImage = Properties.Resources.muscleLumbarR_Inactive;
            lumbarRMuscle.Location = new Point(258, 300);
            lumbarRMuscle.Muscle = MusclesEnum.Lumbar_R;
            lumbarRMuscle.Name = "lumbarRMuscle";
            lumbarRMuscle.Size = new Size(68, 68);
            lumbarRMuscle.TabIndex = 4;
            lumbarRMuscle.TabStop = false;
            // 
            // dorsalLMuscle
            // 
            dorsalLMuscle.Active = false;
            dorsalLMuscle.ActiveImage = Properties.Resources.muscleDorsalL_Active;
            dorsalLMuscle.BackgroundImage = Properties.Resources.muscleDorsalL_Inactive;
            dorsalLMuscle.BackgroundImageLayout = ImageLayout.Zoom;
            dorsalLMuscle.InactiveImage = Properties.Resources.muscleDorsalL_Inactive;
            dorsalLMuscle.Location = new Point(345, 192);
            dorsalLMuscle.Muscle = MusclesEnum.Dorsal_L;
            dorsalLMuscle.Name = "dorsalLMuscle";
            dorsalLMuscle.Size = new Size(74, 108);
            dorsalLMuscle.TabIndex = 5;
            dorsalLMuscle.TabStop = false;
            // 
            // dorsalRMuscle
            // 
            dorsalRMuscle.Active = false;
            dorsalRMuscle.ActiveImage = Properties.Resources.muscleDorsalR_Active;
            dorsalRMuscle.BackgroundImage = Properties.Resources.muscleDorsalR_Inactive;
            dorsalRMuscle.BackgroundImageLayout = ImageLayout.Zoom;
            dorsalRMuscle.InactiveImage = Properties.Resources.muscleDorsalR_Inactive;
            dorsalRMuscle.Location = new Point(253, 192);
            dorsalRMuscle.Muscle = MusclesEnum.Dorsal_R;
            dorsalRMuscle.Name = "dorsalRMuscle";
            dorsalRMuscle.Size = new Size(74, 108);
            dorsalRMuscle.TabIndex = 4;
            dorsalRMuscle.TabStop = false;
            // 
            // backMusclesImg
            // 
            backMusclesImg.BackgroundImage = Properties.Resources.musclesBackClear;
            backMusclesImg.BackgroundImageLayout = ImageLayout.Zoom;
            backMusclesImg.Location = new Point(4, 0);
            backMusclesImg.Name = "backMusclesImg";
            backMusclesImg.Size = new Size(669, 459);
            backMusclesImg.TabIndex = 1;
            backMusclesImg.TabStop = false;
            // 
            // showBackButton
            // 
            showBackButton.Location = new Point(341, 9);
            showBackButton.Name = "showBackButton";
            showBackButton.Size = new Size(75, 23);
            showBackButton.TabIndex = 2;
            showBackButton.Text = "Back";
            showBackButton.UseVisualStyleBackColor = true;
            showBackButton.Click += ShowBackButton_Click;
            // 
            // muscleIntensityTrackBar
            // 
            muscleIntensityTrackBar.AutoSize = false;
            muscleIntensityTrackBar.LargeChange = 25;
            muscleIntensityTrackBar.Location = new Point(4, 494);
            muscleIntensityTrackBar.Maximum = 200;
            muscleIntensityTrackBar.Name = "muscleIntensityTrackBar";
            muscleIntensityTrackBar.Size = new Size(673, 45);
            muscleIntensityTrackBar.SmallChange = 25;
            muscleIntensityTrackBar.TabIndex = 3;
            muscleIntensityTrackBar.Value = 100;
            muscleIntensityTrackBar.Scroll += MuscleIntensityTrackBar_Scroll;
            // 
            // intensityValueTextBox
            // 
            intensityValueTextBox.Location = new Point(349, 471);
            intensityValueTextBox.Name = "intensityValueTextBox";
            intensityValueTextBox.Size = new Size(35, 23);
            intensityValueTextBox.TabIndex = 4;
            intensityValueTextBox.Leave += IntensityValueTextBox_Exit;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(291, 475);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 5;
            label1.Text = "Intensity";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(385, 477);
            label2.Name = "label2";
            label2.Size = new Size(17, 15);
            label2.TabIndex = 6;
            label2.Text = "%";
            // 
            // muscleNameLabel
            // 
            muscleNameLabel.ForeColor = Color.RoyalBlue;
            muscleNameLabel.Location = new Point(189, 475);
            muscleNameLabel.Name = "muscleNameLabel";
            muscleNameLabel.Size = new Size(105, 14);
            muscleNameLabel.TabIndex = 7;
            muscleNameLabel.Text = "muscleNameLabel";
            muscleNameLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            closeButton.Location = new Point(598, 555);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 23);
            closeButton.TabIndex = 8;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += CloseButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 534);
            label3.Name = "label3";
            label3.Size = new Size(13, 15);
            label3.TabIndex = 9;
            label3.Text = "0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(650, 534);
            label4.Name = "label4";
            label4.Size = new Size(25, 15);
            label4.TabIndex = 10;
            label4.Text = "200";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(327, 534);
            label5.Name = "label5";
            label5.Size = new Size(25, 15);
            label5.TabIndex = 11;
            label5.Text = "100";
            // 
            // resetButton
            // 
            resetButton.Location = new Point(12, 555);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(75, 23);
            resetButton.TabIndex = 12;
            resetButton.Text = "Reset";
            resetButton.UseVisualStyleBackColor = true;
            resetButton.Click += ResetButton_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Light", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = SystemColors.ControlDarkDark;
            label6.Location = new Point(-2, -4);
            label6.Name = "label6";
            label6.Size = new Size(16, 25);
            label6.TabIndex = 13;
            label6.Text = "I";
            // 
            // panel1
            // 
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Location = new Point(12, 516);
            panel1.Name = "panel1";
            panel1.Size = new Size(657, 32);
            panel1.TabIndex = 14;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Light", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = SystemColors.ControlDarkDark;
            label8.Location = new Point(644, -4);
            label8.Name = "label8";
            label8.Size = new Size(16, 25);
            label8.TabIndex = 16;
            label8.Text = "I";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Light", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = SystemColors.ControlDarkDark;
            label7.Location = new Point(321, -4);
            label7.Name = "label7";
            label7.Size = new Size(16, 25);
            label7.TabIndex = 15;
            label7.Text = "I";
            // 
            // testSensationButton
            // 
            testSensationButton.Image = Properties.Resources.Play;
            testSensationButton.Location = new Point(404, 471);
            testSensationButton.Name = "testSensationButton";
            testSensationButton.Size = new Size(23, 23);
            testSensationButton.TabIndex = 15;
            testSensationButton.UseVisualStyleBackColor = true;
            testSensationButton.Click += TestSensationButton_Click;
            // 
            // MuscleIntensityForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(681, 590);
            Controls.Add(testSensationButton);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(panel1);
            Controls.Add(resetButton);
            Controls.Add(closeButton);
            Controls.Add(muscleNameLabel);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(intensityValueTextBox);
            Controls.Add(muscleIntensityTrackBar);
            Controls.Add(showBackButton);
            Controls.Add(showFrontButton);
            Controls.Add(muscleGroupsTabControl);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MuscleIntensityForm";
            Text = "Per Muscle Intensity";
            FormClosing += MuscleIntensityForm_FormClosing;
            Shown += MuscleIntensityForm_Shown;
            muscleGroupsTabControl.ResumeLayout(false);
            frontMusclesPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)abdominalLMuscle).EndInit();
            ((System.ComponentModel.ISupportInitialize)abdominalRMuscle).EndInit();
            ((System.ComponentModel.ISupportInitialize)armLMuscle).EndInit();
            ((System.ComponentModel.ISupportInitialize)pectoralLMuscle).EndInit();
            ((System.ComponentModel.ISupportInitialize)pectoralRMuscle).EndInit();
            ((System.ComponentModel.ISupportInitialize)armRMuscle).EndInit();
            ((System.ComponentModel.ISupportInitialize)frontMusclesImg).EndInit();
            backMusclesPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)lumbarLMuscle).EndInit();
            ((System.ComponentModel.ISupportInitialize)lumbarRMuscle).EndInit();
            ((System.ComponentModel.ISupportInitialize)dorsalLMuscle).EndInit();
            ((System.ComponentModel.ISupportInitialize)dorsalRMuscle).EndInit();
            ((System.ComponentModel.ISupportInitialize)backMusclesImg).EndInit();
            ((System.ComponentModel.ISupportInitialize)muscleIntensityTrackBar).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button showFrontButton;
        private TabControl muscleGroupsTabControl;
        private TabPage frontMusclesPage;
        private TabPage backMusclesPage;
        private Button showBackButton;
        private PictureBox frontMusclesImg;
        private PictureBox backMusclesImg;
        private SelectableMuscle armRMuscle;
        private SelectableMuscle pectoralRMuscle;
        private SelectableMuscle pectoralLMuscle;
        private SelectableMuscle armLMuscle;
        private SelectableMuscle abdominalRMuscle;
        private SelectableMuscle abdominalLMuscle;
        private TrackBar muscleIntensityTrackBar;
        private SelectableMuscle dorsalRMuscle;
        private SelectableMuscle dorsalLMuscle;
        private SelectableMuscle lumbarRMuscle;
        private SelectableMuscle lumbarLMuscle;
        private TextBox intensityValueTextBox;
        private Label label1;
        private Label label2;
        private Label muscleNameLabel;
        private Button closeButton;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button resetButton;
        private Label label6;
        private Panel panel1;
        private Label label7;
        private Label label8;
        private Button testSensationButton;
    }
}