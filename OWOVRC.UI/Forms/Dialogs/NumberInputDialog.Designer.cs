namespace OWOVRC.UI.Forms.Dialogs
{
    partial class NumberInputDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumberInputDialog));
            okButton = new Button();
            cancelButton = new Button();
            inputBox = new NumericUpDown();
            descriptionLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)inputBox).BeginInit();
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
            // 
            // inputBox
            // 
            inputBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            inputBox.Location = new Point(12, 34);
            inputBox.Name = "inputBox";
            inputBox.Size = new Size(300, 23);
            inputBox.TabIndex = 1;
            inputBox.KeyDown += InputBox_KeyDown;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new Point(12, 16);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(129, 15);
            descriptionLabel.TabIndex = 3;
            descriptionLabel.Text = "Please enter a Number:";
            // 
            // NumberInputDialog
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(324, 98);
            Controls.Add(descriptionLabel);
            Controls.Add(inputBox);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "NumberInputDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Input";
            Shown += NumberInputDialog_Shown;
            ((System.ComponentModel.ISupportInitialize)inputBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button okButton;
        private Button cancelButton;
        private NumericUpDown inputBox;
        private Label descriptionLabel;
    }
}