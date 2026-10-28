namespace OWOVRC.UI.Forms.Dialogs
{
    partial class AudioCaptureSettingsDialog
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
        protected virtual void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioCaptureSettingsDialog));
            okButton = new Button();
            cancelButton = new Button();
            descriptionLabel = new Label();
            audioDeviceComboBox = new ComboBox();
            vrcOnlyCheckbox = new CheckBox();
            SuspendLayout();
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.DialogResult = DialogResult.OK;
            okButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            okButton.Location = new Point(237, 88);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 2;
            okButton.Text = "Ok";
            okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(156, 88);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // descriptionLabel
            // 
            descriptionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new Point(12, 16);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(79, 15);
            descriptionLabel.TabIndex = 3;
            descriptionLabel.Text = "Audio device:";
            // 
            // comboBox1
            // 
            audioDeviceComboBox.FormattingEnabled = true;
            audioDeviceComboBox.Location = new Point(12, 34);
            audioDeviceComboBox.Name = "comboBox1";
            audioDeviceComboBox.Size = new Size(300, 23);
            audioDeviceComboBox.TabIndex = 4;
            // 
            // vrcOnlyCheckbox
            // 
            vrcOnlyCheckbox.AutoSize = true;
            vrcOnlyCheckbox.Location = new Point(12, 63);
            vrcOnlyCheckbox.Name = "vrcOnlyCheckbox";
            vrcOnlyCheckbox.Size = new Size(261, 19);
            vrcOnlyCheckbox.TabIndex = 5;
            vrcOnlyCheckbox.Text = "Only capture audio from the VRChat process";
            vrcOnlyCheckbox.UseVisualStyleBackColor = true;
            // 
            // AudioCaptureSettingsDialog
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(324, 123);
            Controls.Add(vrcOnlyCheckbox);
            Controls.Add(audioDeviceComboBox);
            Controls.Add(descriptionLabel);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "AudioCaptureSettingsDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Audio Capture Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button okButton;
        private Button cancelButton;
        private Label descriptionLabel;
        protected ComboBox audioDeviceComboBox;
        private CheckBox vrcOnlyCheckbox;
    }
}