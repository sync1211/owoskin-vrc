namespace OWOVRC.UI.Forms.Dialogs
{
    partial class NameCollisionDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NameCollisionDialog));
            okButton = new Button();
            cancelButton = new Button();
            descriptionLabel = new Label();
            newNameInput = new TextBox();
            autoRenameButton = new Button();
            SuspendLayout();
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.DialogResult = DialogResult.OK;
            okButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            okButton.Location = new Point(237, 63);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 2;
            okButton.Text = "Ok";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OkButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(156, 63);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += ControlButton_Click;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new Point(12, 16);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(140, 15);
            descriptionLabel.TabIndex = 3;
            descriptionLabel.Text = "Please enter a new name:";
            // 
            // newNameInput
            // 
            newNameInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            newNameInput.Location = new Point(12, 34);
            newNameInput.Name = "newNameInput";
            newNameInput.Size = new Size(300, 23);
            newNameInput.TabIndex = 4;
            newNameInput.TextChanged += NewNameInput_TextChanged;
            // 
            // autoRenameButton
            // 
            autoRenameButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            autoRenameButton.DialogResult = DialogResult.Continue;
            autoRenameButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            autoRenameButton.Location = new Point(12, 63);
            autoRenameButton.Name = "autoRenameButton";
            autoRenameButton.Size = new Size(92, 23);
            autoRenameButton.TabIndex = 5;
            autoRenameButton.Text = "Auto Rename";
            autoRenameButton.UseVisualStyleBackColor = true;
            // 
            // NameCollisionDialog
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(324, 98);
            Controls.Add(autoRenameButton);
            Controls.Add(newNameInput);
            Controls.Add(descriptionLabel);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "NameCollisionDialog";
            Text = "Name already in use";
            Shown += NameCollisionDialog_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button okButton;
        private Button cancelButton;
        private Label descriptionLabel;
        private TextBox newNameInput;
        private Button autoRenameButton;
    }
}