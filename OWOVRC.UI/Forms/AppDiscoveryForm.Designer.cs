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
            resolveHostsCheckbox = new CheckBox();
            SuspendLayout();
            // 
            // appListBox
            // 
            appListBox.FormattingEnabled = true;
            appListBox.Location = new Point(12, 12);
            appListBox.Name = "appListBox";
            appListBox.Size = new Size(294, 319);
            appListBox.TabIndex = 0;
            appListBox.SelectedValueChanged += ListBox1_SelectedValueChanged;
            appListBox.DoubleClick += AppListBox_DoubleClick;
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
            closeButton.DialogResult = DialogResult.Cancel;
            closeButton.Location = new Point(150, 341);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(76, 23);
            closeButton.TabIndex = 2;
            closeButton.Text = "Cancel";
            closeButton.UseVisualStyleBackColor = true;
            // 
            // resolveHostsCheckbox
            // 
            resolveHostsCheckbox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            resolveHostsCheckbox.AutoSize = true;
            resolveHostsCheckbox.Location = new Point(12, 344);
            resolveHostsCheckbox.Name = "resolveHostsCheckbox";
            resolveHostsCheckbox.Size = new Size(127, 19);
            resolveHostsCheckbox.TabIndex = 3;
            resolveHostsCheckbox.Text = "Resolve hostnames";
            resolveHostsCheckbox.UseVisualStyleBackColor = true;
            // 
            // AppDiscoveryForm
            // 
            AcceptButton = selectEntryButton;
            CancelButton = closeButton;
            ClientSize = new Size(318, 376);
            Controls.Add(resolveHostsCheckbox);
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
            PerformLayout();
        }

        private Button selectEntryButton;
        private Button closeButton;
        private ListBox appListBox;

        #endregion

        private CheckBox resolveHostsCheckbox;
    }
}