namespace OWOVRC.UI.Forms
{
    partial class AppDiscoveryForm
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppDiscoveryForm));
            appListBox = new ListBox();
            selectEntryButton = new Button();
            closeButton = new Button();
            SuspendLayout();
            // 
            // appListBox
            // 
            appListBox.FormattingEnabled = true;
            appListBox.ItemHeight = 15;
            appListBox.Location = new Point(12, 12);
            appListBox.Name = "appListBox";
            appListBox.Size = new Size(294, 319);
            appListBox.TabIndex = 0;
            appListBox.SelectedValueChanged += ListBox1_SelectedValueChanged;
            // 
            // selectEntryButton
            // 
            selectEntryButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            selectEntryButton.Enabled = false;
            selectEntryButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            selectEntryButton.Location = new Point(231, 341);
            selectEntryButton.Name = "selectEntryButton";
            selectEntryButton.Size = new Size(75, 23);
            selectEntryButton.TabIndex = 1;
            selectEntryButton.Text = "Select";
            selectEntryButton.UseVisualStyleBackColor = true;
            selectEntryButton.Click += SelectEntryButton_Click;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.Location = new Point(150, 341);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(76, 23);
            closeButton.TabIndex = 2;
            closeButton.Text = "Cancel";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += CloseButton_Click;
            // 
            // AppDiscoveryForm
            // 
            AcceptButton = selectEntryButton;
            CancelButton = closeButton;
            ClientSize = new Size(318, 376);
            Controls.Add(closeButton);
            Controls.Add(selectEntryButton);
            Controls.Add(appListBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AppDiscoveryForm";
            Text = "Searching for MyOWO...";
            FormClosing += AppDiscoveryForm_FormClosing;
            Shown += AppDiscoveryForm_Shown;
            ResumeLayout(false);
        }

        private Button selectEntryButton;
        private Button closeButton;
        private ListBox appListBox;

        #endregion
    }
}