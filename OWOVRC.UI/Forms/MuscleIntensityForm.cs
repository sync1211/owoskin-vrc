﻿using OWOGame;
using OWOVRC.Classes.Settings;
using OWOVRC.UI.Controls;
using Serilog;

namespace OWOVRC.UI.Forms
{
    public partial class MuscleIntensityForm : Form
    {
        private int currentMuscleID = 0;
        private readonly Dictionary<int, int> muscleIntensities;
        private readonly SelectableMuscle[] selectableMuscles;

        public MuscleIntensityForm(Dictionary<int, int> intensities)
        {
            InitializeComponent();
            this.muscleIntensities = intensities;

            selectableMuscles = [
                pectoralRMuscle,
                pectoralLMuscle,
                armRMuscle,
                armLMuscle,
                abdominalRMuscle,
                abdominalLMuscle,
                dorsalRMuscle,
                dorsalLMuscle,
                lumbarLMuscle,
                lumbarRMuscle
            ];

            // Hide header of TabControl
            muscleGroupsTabControl.ItemSize = new Size(0, 1);
            muscleGroupsTabControl.SizeMode = TabSizeMode.Fixed;

            RegisterClickEvents();

            UpdatePageIndicator();
            UpdateActiveMuscleGroup();
        }

        private void RegisterClickEvents()
        {
            foreach (SelectableMuscle muscle in selectableMuscles)
            {
                muscle.MouseClick += SelectableMuscle_Click;
            }
        }

        private void SelectableMuscle_Click(object? sender, EventArgs e)
        {
            if (sender is not SelectableMuscle muscle)
            {
                return;
            }
            currentMuscleID = muscle.MuscleID;
            UpdateActiveMuscleGroup();
        }

        private void ShowFrontButton_Click(object sender, EventArgs e)
        {
            muscleGroupsTabControl.SelectedIndex = 0;

            currentMuscleID = Muscle.Pectoral_R.id;

            UpdatePageIndicator();
            UpdateActiveMuscleGroup();
        }

        private void ShowBackButton_Click(object sender, EventArgs e)
        {
            muscleGroupsTabControl.SelectedIndex = 1;

            currentMuscleID = Muscle.Dorsal_R.id;

            UpdatePageIndicator();
            UpdateActiveMuscleGroup();
        }

        private void UpdatePageIndicator()
        {
            showFrontButton.Enabled = muscleGroupsTabControl.SelectedIndex != 0;
            showBackButton.Enabled = muscleGroupsTabControl.SelectedIndex != 1;
        }

        private void UpdateActiveMuscleGroup()
        {
            muscleNameLabel.Text = String.Empty;
            foreach (SelectableMuscle muscle in selectableMuscles)
            {
                muscle.Active = muscle.MuscleID == currentMuscleID;

                if (muscle.Active)
                {
                    muscleNameLabel.Text = muscle.Muscle.ToString();
                }
            }

            UpdateTrackBar();
        }

        private void UpdateTrackBar()
        {
            if (!muscleIntensities.TryGetValue(currentMuscleID, out int intensity))
            {
                intensity = 0;
            }

            muscleIntensityTrackBar.Value = intensity;
            intensityValueTextBox.Text = muscleIntensityTrackBar.Value.ToString();
        }

        private void MuscleIntensityTrackBar_Scroll(object sender, EventArgs e)
        {
            intensityValueTextBox.Text = muscleIntensityTrackBar.Value.ToString();

            muscleIntensities[currentMuscleID] = muscleIntensityTrackBar.Value;
        }

        private void IntensityValueTextBox_Exit(object sender, EventArgs e)
        {
            if (!int.TryParse(intensityValueTextBox.Text, out int value) || value < 0 || value > 100)
            {
                intensityValueTextBox.Text = muscleIntensityTrackBar.Value.ToString();
            }

            muscleIntensities[currentMuscleID] = value;

            UpdateActiveMuscleGroup();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
