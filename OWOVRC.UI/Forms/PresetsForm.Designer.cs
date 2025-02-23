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
            removePresetButton = new Button();
            oscPathHintLabel = new Label();
            oscPathLabel = new Label();
            presetsHelpLinkLabel = new LinkLabel();
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
            importSensationButton.Location = new Point(679, 12);
            importSensationButton.Name = "importSensationButton";
            importSensationButton.Size = new Size(109, 23);
            importSensationButton.TabIndex = 2;
            importSensationButton.Text = "Import sensation";
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
            dropIndicatorLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dropIndicatorLabel.BackColor = SystemColors.ButtonHighlight;
            dropIndicatorLabel.Enabled = false;
            dropIndicatorLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dropIndicatorLabel.ForeColor = SystemColors.ControlDarkDark;
            dropIndicatorLabel.Location = new Point(202, 194);
            dropIndicatorLabel.Name = "dropIndicatorLabel";
            dropIndicatorLabel.Size = new Size(410, 63);
            dropIndicatorLabel.TabIndex = 5;
            dropIndicatorLabel.Text = "Drop files here to import";
            dropIndicatorLabel.TextAlign = ContentAlignment.MiddleCenter;
            dropIndicatorLabel.Visible = false;
            // 
            // removePresetButton
            // 
            removePresetButton.Location = new Point(12, 12);
            removePresetButton.Name = "removePresetButton";
            removePresetButton.Size = new Size(75, 23);
            removePresetButton.TabIndex = 6;
            removePresetButton.Text = "Remove";
            removePresetButton.UseVisualStyleBackColor = true;
            removePresetButton.Click += RemovePresetButton_Click;
            // 
            // oscPathHintLabel
            // 
            oscPathHintLabel.AutoSize = true;
            oscPathHintLabel.Location = new Point(118, 16);
            oscPathHintLabel.Name = "oscPathHintLabel";
            oscPathHintLabel.Size = new Size(231, 15);
            oscPathHintLabel.TabIndex = 7;
            oscPathHintLabel.Text = "Parameter OSC path for calling sensations:";
            // 
            // oscPathLabel
            // 
            oscPathLabel.AutoSize = true;
            oscPathLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            oscPathLabel.ForeColor = Color.Blue;
            oscPathLabel.Location = new Point(355, 16);
            oscPathLabel.Name = "oscPathLabel";
            oscPathLabel.Size = new Size(257, 15);
            oscPathLabel.TabIndex = 8;
            oscPathLabel.Text = "/OWO/SensationsTrigger/<Sensation Name>";
            // 
            // presetsHelpLinkLabel
            // 
            presetsHelpLinkLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            presetsHelpLinkLabel.AutoSize = true;
            presetsHelpLinkLabel.Location = new Point(12, 423);
            presetsHelpLinkLabel.Name = "presetsHelpLinkLabel";
            presetsHelpLinkLabel.Size = new Size(116, 15);
            presetsHelpLinkLabel.TabIndex = 24;
            presetsHelpLinkLabel.TabStop = true;
            presetsHelpLinkLabel.Text = "Avatar triggers setup";
            presetsHelpLinkLabel.LinkClicked += PresetsHelpLinkLabel_LinkClicked;
            // 
            // PresetsForm
            // 
            AcceptButton = saveButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = closeButton;
            ClientSize = new Size(800, 450);
            Controls.Add(dropIndicatorLabel);
            Controls.Add(presetsHelpLinkLabel);
            Controls.Add(oscPathLabel);
            Controls.Add(oscPathHintLabel);
            Controls.Add(removePresetButton);
            Controls.Add(saveButton);
            Controls.Add(importSensationButton);
            Controls.Add(closeButton);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(765, 461);
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
        private Button removePresetButton;
        private Label oscPathHintLabel;
        private Label oscPathLabel;
        private LinkLabel presetsHelpLinkLabel;
    }
}