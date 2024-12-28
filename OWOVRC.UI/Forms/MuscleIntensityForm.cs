using OWOGame;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.UI.Controls;
using OWOVRC.UI.Forms.Dialogs;

namespace OWOVRC.UI.Forms
{
    public partial class MuscleIntensityForm : Form
    {
        private int currentMuscleID;
        private readonly Dictionary<int, int> muscleIntensities;
        private readonly SelectableMuscle[] selectableMuscles;
        private readonly Sensation? testSensation;
        private readonly OWOHelper? owoHelper;

        private const string SENSATION_NAME = "IntensityTest";

        /// <summary>
        /// Creates a new instance of the form.
        /// </summary>
        /// <param name="intensities">A dictionary of muscleIDs and intensities to be modified by the user.</param>
        /// <param name="sensationForTest">(optional) An example sensation for previewing intensities</param>
        /// <param name="owoHelper">(optional) An instance of OWOHelper. If not specified, preview sensations will be sent via the OWOGame SDK directly.</param>
        public MuscleIntensityForm(Dictionary<int, int> intensities, Sensation? sensationForTest = null, string? title = null, OWOHelper? owoHelper = null)
        {
            InitializeComponent();
            this.muscleIntensities = intensities;
            this.testSensation = sensationForTest;
            this.owoHelper = owoHelper;

            if (title != null)
            {
                Text = title;
            }

            selectableMuscles = [
                pectoralLMuscle,
                pectoralRMuscle,
                armLMuscle,
                armRMuscle,
                abdominalLMuscle,
                abdominalRMuscle,
                dorsalLMuscle,
                dorsalRMuscle,
                lumbarRMuscle,
                lumbarLMuscle
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

        private void UnregisterClickEvents()
        {
            foreach (SelectableMuscle muscle in selectableMuscles)
            {
                muscle.MouseClick -= SelectableMuscle_Click;
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
            intensityValueInput.Text = muscleIntensityTrackBar.Value.ToString();
        }

        private void MuscleIntensityTrackBar_Scroll(object sender, EventArgs e)
        {
            intensityValueInput.Text = muscleIntensityTrackBar.Value.ToString();

            muscleIntensities[currentMuscleID] = muscleIntensityTrackBar.Value;
        }

        private void IntensityValueTextBox_Exit(object sender, EventArgs e)
        {
            if (!int.TryParse(intensityValueInput.Text, out int value) || value < 0 || value > 200)
            {
                intensityValueInput.Text = muscleIntensityTrackBar.Value.ToString();
            }

            muscleIntensities[currentMuscleID] = value;

            UpdateActiveMuscleGroup();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MuscleIntensityForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterClickEvents();
        }

        private void SetIntensityForAllButton_Click(object sender, EventArgs e)
        {
            int intensity = 100;
            using (NumberInputDialog dialog = new("Enter intensity for all muscles:", "Input Intensity", 0, 200, 100))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                intensity = dialog.Value;
            }

            // Reset all muscles to 100%
            foreach (int key in muscleIntensities.Keys)
            {
                muscleIntensities[key] = intensity;
            }
            ShowFrontButton_Click(sender, e);
        }

        private void TestSensationButton_Click(object sender, EventArgs e)
        {
            if (testSensation == null)
            {
                return;
            }

            Muscle[] muscles;
            if (Control.ModifierKeys == Keys.Shift)
            {
                muscles = Muscle.All;
            }
            else
            {
                Muscle? currentMuscle = Muscle.All.FirstOrDefault(m => m.id == currentMuscleID);
                if (currentMuscle == null)
                {
                    return;
                }

                muscles = [currentMuscle.Value];
            }

            Muscle[] musclesWithIntensity = new Muscle[muscles.Length];
            for (int i = 0; i < muscles.Length; i++)
            {
                Muscle muscle = muscles[i];
                int intensity = muscleIntensities[muscle.id];
                musclesWithIntensity[i] = muscle.WithIntensity(intensity);
            }

            // Play sensation (use owoHelper if we can)
            if (owoHelper == null)
            {
                OWO.Send(testSensation, musclesWithIntensity);
                return;
            }

            owoHelper.AddSensation(testSensation, musclesWithIntensity, SENSATION_NAME);
        }

        private void MuscleIntensityForm_Shown(object sender, EventArgs e)
        {
            testSensationButton.Visible = testSensation != null;
            testSensationButton.Enabled = OWO.ConnectionState == ConnectionState.Connected;
        }
    }
}
