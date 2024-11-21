namespace OWOVRC.UI.Forms
{
    partial class PresetsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PresetsForm));
            dataGridView1 = new DataGridView();
            closeButton = new Button();
            importSensationButton = new Button();
            saveButton = new Button();
            dropIndicatorLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowDrop = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 41);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ShowEditingIcon = false;
            dataGridView1.Size = new Size(776, 368);
            dataGridView1.TabIndex = 0;
            dataGridView1.DragDrop += DataGridView1_DragDrop;
            dataGridView1.DragEnter += DataGridView1_DragEnter;
            dataGridView1.DragLeave += DataGridView1_DragLeave;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            closeButton.Location = new Point(632, 415);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 23);
            closeButton.TabIndex = 1;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += CloseButton_Click;
            // 
            // importSensationButton
            // 
            importSensationButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            importSensationButton.Location = new Point(695, 12);
            importSensationButton.Name = "importSensationButton";
            importSensationButton.Size = new Size(93, 23);
            importSensationButton.TabIndex = 2;
            importSensationButton.Text = "Import .owo";
            importSensationButton.UseVisualStyleBackColor = true;
            importSensationButton.Click += ImportSensationButton_Click;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            saveButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            saveButton.Location = new Point(713, 415);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 4;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;
            // 
            // dropIndicatorLabel
            // 
            dropIndicatorLabel.AutoSize = true;
            dropIndicatorLabel.BackColor = Color.Transparent;
            dropIndicatorLabel.Enabled = false;
            dropIndicatorLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dropIndicatorLabel.ForeColor = SystemColors.ControlDarkDark;
            dropIndicatorLabel.Location = new Point(193, 188);
            dropIndicatorLabel.Name = "dropIndicatorLabel";
            dropIndicatorLabel.Size = new Size(396, 45);
            dropIndicatorLabel.TabIndex = 5;
            dropIndicatorLabel.Text = "Drop files here to import";
            dropIndicatorLabel.Visible = false;
            // 
            // PresetsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dropIndicatorLabel);
            Controls.Add(saveButton);
            Controls.Add(importSensationButton);
            Controls.Add(closeButton);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PresetsForm";
            Text = "PresetsForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button closeButton;
        private Button importSensationButton;
        private Button saveButton;
        private Label dropIndicatorLabel;
    }
}