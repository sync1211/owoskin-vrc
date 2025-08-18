namespace OWOVRC.UI.Forms.Dialogs
{
    partial class PresetsImportDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PresetsImportDialog));
            dataGridView1 = new DataGridView();
            importButton = new Button();
            cancelButton = new Button();
            autoRenameButton = new Button();
            descriptionLabel = new Label();
            collisionIndicatorLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 27);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Size = new Size(511, 266);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            dataGridView1.DataError += DataGridView1_DataError;
            // 
            // importButton
            // 
            importButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            importButton.DialogResult = DialogResult.OK;
            importButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            importButton.Location = new Point(448, 299);
            importButton.Name = "importButton";
            importButton.Size = new Size(75, 23);
            importButton.TabIndex = 1;
            importButton.Text = "Import";
            importButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(367, 299);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // autoRenameButton
            // 
            autoRenameButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            autoRenameButton.Location = new Point(12, 299);
            autoRenameButton.Name = "autoRenameButton";
            autoRenameButton.Size = new Size(87, 23);
            autoRenameButton.TabIndex = 3;
            autoRenameButton.Text = "Auto Rename";
            autoRenameButton.UseVisualStyleBackColor = true;
            autoRenameButton.Click += AutoRenameButton_Click;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new Point(12, 9);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(211, 15);
            descriptionLabel.TabIndex = 4;
            descriptionLabel.Text = "The following persets will be imported:";
            // 
            // collisionIndicatorLabel
            // 
            collisionIndicatorLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            collisionIndicatorLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            collisionIndicatorLabel.ForeColor = Color.Red;
            collisionIndicatorLabel.Location = new Point(105, 299);
            collisionIndicatorLabel.Name = "collisionIndicatorLabel";
            collisionIndicatorLabel.Size = new Size(256, 23);
            collisionIndicatorLabel.TabIndex = 5;
            collisionIndicatorLabel.Text = "Name conflicts found!";
            collisionIndicatorLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PresetsImportDialog
            // 
            AcceptButton = importButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(535, 334);
            Controls.Add(collisionIndicatorLabel);
            Controls.Add(descriptionLabel);
            Controls.Add(autoRenameButton);
            Controls.Add(cancelButton);
            Controls.Add(importButton);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PresetsImportDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Import presets";
            Shown += PresetsImportDialog_Shown;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button importButton;
        private Button cancelButton;
        private Button autoRenameButton;
        private Label descriptionLabel;
        private Label collisionIndicatorLabel;
    }
}