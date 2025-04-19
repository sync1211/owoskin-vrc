namespace OWOVRC.UI.Forms
{
    partial class AdvancedPresetsForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedPresetsForm));
            interruptableCheckBox = new CheckBox();
            loopCheckBox = new CheckBox();
            priorityInput = new NumericUpDown();
            priorityLabel = new Label();
            intensityInput = new NumericUpDown();
            intensityLabel = new Label();
            pathLabel = new Label();
            pathInput = new TextBox();
            maxValueInput = new NumericUpDown();
            maxLabel = new Label();
            minValueInput = new NumericUpDown();
            minLabel = new Label();
            nameInput = new TextBox();
            enabledCheckBox = new CheckBox();
            saveButton = new Button();
            cancelButton = new Button();
            listBox1 = new ListBox();
            deleteEntryButton = new Button();
            copyEntryButton = new Button();
            addEntryButton = new Button();
            nameLabel = new Label();
            presetGroupBox = new GroupBox();
            toolTip1 = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)priorityInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)intensityInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxValueInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)minValueInput).BeginInit();
            presetGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // interruptableCheckBox
            // 
            interruptableCheckBox.AutoSize = true;
            interruptableCheckBox.Location = new Point(68, 192);
            interruptableCheckBox.Name = "interruptableCheckBox";
            interruptableCheckBox.Size = new Size(94, 19);
            interruptableCheckBox.TabIndex = 13;
            interruptableCheckBox.Text = "Interruptable";
            interruptableCheckBox.UseVisualStyleBackColor = true;
            // 
            // loopCheckBox
            // 
            loopCheckBox.AutoSize = true;
            loopCheckBox.Location = new Point(9, 192);
            loopCheckBox.Name = "loopCheckBox";
            loopCheckBox.Size = new Size(53, 19);
            loopCheckBox.TabIndex = 12;
            loopCheckBox.Text = "Loop";
            loopCheckBox.UseVisualStyleBackColor = true;
            // 
            // priorityInput
            // 
            priorityInput.Location = new Point(64, 163);
            priorityInput.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            priorityInput.Name = "priorityInput";
            priorityInput.Size = new Size(98, 23);
            priorityInput.TabIndex = 11;
            // 
            // priorityLabel
            // 
            priorityLabel.AutoSize = true;
            priorityLabel.Location = new Point(9, 165);
            priorityLabel.Name = "priorityLabel";
            priorityLabel.Size = new Size(45, 15);
            priorityLabel.TabIndex = 10;
            priorityLabel.Text = "Priority";
            // 
            // intensityInput
            // 
            intensityInput.Location = new Point(64, 134);
            intensityInput.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            intensityInput.Name = "intensityInput";
            intensityInput.Size = new Size(98, 23);
            intensityInput.TabIndex = 9;
            // 
            // intensityLabel
            // 
            intensityLabel.AutoSize = true;
            intensityLabel.Location = new Point(9, 136);
            intensityLabel.Name = "intensityLabel";
            intensityLabel.Size = new Size(52, 15);
            intensityLabel.TabIndex = 8;
            intensityLabel.Text = "Intensity";
            // 
            // pathLabel
            // 
            pathLabel.AutoSize = true;
            pathLabel.Location = new Point(9, 79);
            pathLabel.Name = "pathLabel";
            pathLabel.Size = new Size(31, 15);
            pathLabel.TabIndex = 7;
            pathLabel.Text = "Path";
            // 
            // pathInput
            // 
            pathInput.Location = new Point(64, 76);
            pathInput.Name = "pathInput";
            pathInput.Size = new Size(271, 23);
            pathInput.TabIndex = 6;
            // 
            // maxValueInput
            // 
            maxValueInput.DecimalPlaces = 2;
            maxValueInput.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            maxValueInput.Location = new Point(64, 105);
            maxValueInput.Name = "maxValueInput";
            maxValueInput.Size = new Size(98, 23);
            maxValueInput.TabIndex = 5;
            // 
            // maxLabel
            // 
            maxLabel.AutoSize = true;
            maxLabel.Location = new Point(203, 109);
            maxLabel.Name = "maxLabel";
            maxLabel.Size = new Size(30, 15);
            maxLabel.TabIndex = 4;
            maxLabel.Text = "Max";
            // 
            // minValueInput
            // 
            minValueInput.DecimalPlaces = 2;
            minValueInput.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            minValueInput.Location = new Point(237, 105);
            minValueInput.Name = "minValueInput";
            minValueInput.Size = new Size(98, 23);
            minValueInput.TabIndex = 3;
            // 
            // minLabel
            // 
            minLabel.AutoSize = true;
            minLabel.Location = new Point(9, 109);
            minLabel.Name = "minLabel";
            minLabel.Size = new Size(28, 15);
            minLabel.TabIndex = 2;
            minLabel.Text = "Min";
            // 
            // nameInput
            // 
            nameInput.Location = new Point(62, 47);
            nameInput.Name = "nameInput";
            nameInput.Size = new Size(273, 23);
            nameInput.TabIndex = 1;
            // 
            // enabledCheckBox
            // 
            enabledCheckBox.AutoSize = true;
            enabledCheckBox.Location = new Point(9, 22);
            enabledCheckBox.Name = "enabledCheckBox";
            enabledCheckBox.Size = new Size(68, 19);
            enabledCheckBox.TabIndex = 0;
            enabledCheckBox.Text = "Enabled";
            enabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            saveButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            saveButton.Location = new Point(455, 276);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 1;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(374, 276);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += CancelButton_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(12, 12);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(171, 259);
            listBox1.TabIndex = 3;
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            // 
            // deleteEntryButton
            // 
            deleteEntryButton.Image = (Image)resources.GetObject("deleteEntryButton.Image");
            deleteEntryButton.Location = new Point(130, 272);
            deleteEntryButton.Name = "deleteEntryButton";
            deleteEntryButton.Size = new Size(53, 27);
            deleteEntryButton.TabIndex = 19;
            toolTip1.SetToolTip(deleteEntryButton, "Delete preset");
            deleteEntryButton.UseVisualStyleBackColor = true;
            deleteEntryButton.Click += DeleteEntryButton_Click;
            // 
            // copyEntryButton
            // 
            copyEntryButton.Image = (Image)resources.GetObject("copyEntryButton.Image");
            copyEntryButton.Location = new Point(71, 272);
            copyEntryButton.Name = "copyEntryButton";
            copyEntryButton.Size = new Size(53, 27);
            copyEntryButton.TabIndex = 18;
            toolTip1.SetToolTip(copyEntryButton, "Duplicate preset");
            copyEntryButton.UseVisualStyleBackColor = true;
            copyEntryButton.Click += CopyEntryButton_Click;
            // 
            // addEntryButton
            // 
            addEntryButton.Image = (Image)resources.GetObject("addEntryButton.Image");
            addEntryButton.Location = new Point(12, 272);
            addEntryButton.Name = "addEntryButton";
            addEntryButton.Size = new Size(53, 27);
            addEntryButton.TabIndex = 17;
            toolTip1.SetToolTip(addEntryButton, "Add new preset");
            addEntryButton.UseVisualStyleBackColor = true;
            addEntryButton.Click += AddEntryButton_Click;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(9, 50);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(39, 15);
            nameLabel.TabIndex = 20;
            nameLabel.Text = "Name";
            // 
            // presetGroupBox
            // 
            presetGroupBox.Controls.Add(priorityInput);
            presetGroupBox.Controls.Add(nameLabel);
            presetGroupBox.Controls.Add(maxLabel);
            presetGroupBox.Controls.Add(maxValueInput);
            presetGroupBox.Controls.Add(minValueInput);
            presetGroupBox.Controls.Add(pathInput);
            presetGroupBox.Controls.Add(interruptableCheckBox);
            presetGroupBox.Controls.Add(minLabel);
            presetGroupBox.Controls.Add(pathLabel);
            presetGroupBox.Controls.Add(loopCheckBox);
            presetGroupBox.Controls.Add(enabledCheckBox);
            presetGroupBox.Controls.Add(intensityLabel);
            presetGroupBox.Controls.Add(nameInput);
            presetGroupBox.Controls.Add(intensityInput);
            presetGroupBox.Controls.Add(priorityLabel);
            presetGroupBox.Location = new Point(189, 12);
            presetGroupBox.Name = "presetGroupBox";
            presetGroupBox.Size = new Size(341, 259);
            presetGroupBox.TabIndex = 21;
            presetGroupBox.TabStop = false;
            presetGroupBox.Text = "Preset";
            // 
            // AdvancedPresetsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(542, 311);
            Controls.Add(presetGroupBox);
            Controls.Add(deleteEntryButton);
            Controls.Add(copyEntryButton);
            Controls.Add(addEntryButton);
            Controls.Add(listBox1);
            Controls.Add(cancelButton);
            Controls.Add(saveButton);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AdvancedPresetsForm";
            Text = "Advanced Presets";
            ((System.ComponentModel.ISupportInitialize)priorityInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)intensityInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxValueInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)minValueInput).EndInit();
            presetGroupBox.ResumeLayout(false);
            presetGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private NumericUpDown intensityInput;
        private Label intensityLabel;
        private Label pathLabel;
        private TextBox pathInput;
        private NumericUpDown maxValueInput;
        private Label maxLabel;
        private NumericUpDown minValueInput;
        private Label minLabel;
        private TextBox nameInput;
        private CheckBox enabledCheckBox;
        private Button saveButton;
        private Button cancelButton;
        private CheckBox loopCheckBox;
        private NumericUpDown priorityInput;
        private Label priorityLabel;
        private CheckBox interruptableCheckBox;
        private ListBox listBox1;
        private Button deleteEntryButton;
        private Button copyEntryButton;
        private Button addEntryButton;
        private Label nameLabel;
        private GroupBox presetGroupBox;
        private ToolTip toolTip1;
    }
}