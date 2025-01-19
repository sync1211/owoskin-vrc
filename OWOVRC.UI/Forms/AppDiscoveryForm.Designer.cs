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
            resources.ApplyResources(appListBox, "appListBox");
            appListBox.FormattingEnabled = true;
            appListBox.Name = "appListBox";
            appListBox.SelectedValueChanged += ListBox1_SelectedValueChanged;
            appListBox.Size = new System.Drawing.Size(294, 319);
            // 
            // selectEntryButton
            // 
            resources.ApplyResources(selectEntryButton, "selectEntryButton");
            selectEntryButton.Name = "selectEntryButton";
            selectEntryButton.UseVisualStyleBackColor = true;
            selectEntryButton.Click += SelectEntryButton_Click;
            // 
            // closeButton
            // 
            resources.ApplyResources(closeButton, "closeButton");
            closeButton.Name = "closeButton";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += CloseButton_Click;
            // 
            // AppDiscoveryForm
            // 
            AcceptButton = selectEntryButton;
            resources.ApplyResources(this, "$this");
            CancelButton = closeButton;
            Controls.Add(closeButton);
            Controls.Add(selectEntryButton);
            Controls.Add(appListBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "AppDiscoveryForm";
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