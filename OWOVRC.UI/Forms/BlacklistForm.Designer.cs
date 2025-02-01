namespace OWOVRC.UI.Forms
{
    partial class BlacklistForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlacklistForm));
            saveButton = new Button();
            checkedListBox1 = new CheckedListBox();
            SuspendLayout();
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            saveButton.DialogResult = DialogResult.OK;
            saveButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            saveButton.Location = new Point(231, 334);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 1;
            saveButton.Text = "Close";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += CloseButton_Click;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(12, 12);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(294, 310);
            checkedListBox1.TabIndex = 4;
            checkedListBox1.SelectedIndexChanged += CheckedListBox1_SelectedIndexChanged;
            // 
            // BlacklistForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(318, 369);
            Controls.Add(checkedListBox1);
            Controls.Add(saveButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "BlacklistForm";
            Text = "Settings";
            ResumeLayout(false);
        }

        #endregion

        private Button saveButton;
        private CheckedListBox checkedListBox1;
    }
}