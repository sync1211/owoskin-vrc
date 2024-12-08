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
            muscleIntensityTrackBar.Location = new Point(4, 499);
            muscleIntensityTrackBar.Maximum = 100;
            muscleIntensityTrackBar.Name = "muscleIntensityTrackBar";
            muscleIntensityTrackBar.Size = new Size(673, 45);
            muscleIntensityTrackBar.TabIndex = 3;
            muscleIntensityTrackBar.Scroll += MuscleIntensityTrackBar_Scroll;
            // 
            // intensityValueTextBox
            // 
            intensityValueTextBox.Location = new Point(349, 477);
            intensityValueTextBox.Name = "intensityValueTextBox";
            intensityValueTextBox.Size = new Size(35, 23);
            intensityValueTextBox.TabIndex = 4;
            intensityValueTextBox.Leave += IntensityValueTextBox_Exit;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(291, 481);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 5;
            label1.Text = "Intensity";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(385, 481);
            label2.Name = "label2";
            label2.Size = new Size(17, 15);
            label2.TabIndex = 6;
            label2.Text = "%";
            // 
            // muscleNameLabel
            // 
            muscleNameLabel.Anchor = AnchorStyles.Right;
            muscleNameLabel.ForeColor = Color.RoyalBlue;
            muscleNameLabel.Location = new Point(189, 481);
            muscleNameLabel.Name = "muscleNameLabel";
            muscleNameLabel.Size = new Size(105, 15);
            muscleNameLabel.TabIndex = 7;
            muscleNameLabel.Text = "muscleNameLabel";
            muscleNameLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // closeButton
            // 
            closeButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            closeButton.Location = new Point(594, 533);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 23);
            closeButton.TabIndex = 8;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += CloseButton_Click;
            // 
            // MuscleIntensityForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(681, 568);
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
    }
}