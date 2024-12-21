namespace OWOVRC.UI.Controls
{
    partial class AudioSettingsEntry
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            DragHandle1 = new DragHandle();
            priorityInput = new NumericUpDown();
            configureButton = new Button();
            prioLabel = new Label();
            nameLabel = new Label();
            minLabel = new Label();
            minInput = new NumericUpDown();
            maxLabel = new Label();
            maxInput = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)priorityInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)minInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxInput).BeginInit();
            SuspendLayout();
            // 
            // DragHandle1
            // 
            DragHandle1.Location = new Point(3, 3);
            DragHandle1.MaximumSize = new Size(12, 150);
            DragHandle1.Name = "DragHandle1";
            DragHandle1.Size = new Size(12, 49);
            DragHandle1.TabIndex = 0;
            DragHandle1.Text = "dragHandle1";
            // 
            // priorityInput
            // 
            priorityInput.Location = new Point(78, 29);
            priorityInput.Name = "priorityInput";
            priorityInput.Size = new Size(48, 23);
            priorityInput.TabIndex = 1;
            priorityInput.Leave += PriorityInput_Leave;
            // 
            // configureButton
            // 
            configureButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            configureButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            configureButton.Location = new Point(240, 3);
            configureButton.Name = "configureButton";
            configureButton.Size = new Size(75, 51);
            configureButton.TabIndex = 4;
            configureButton.Text = "Configure";
            configureButton.UseVisualStyleBackColor = true;
            configureButton.Click += ConfigureButton_Click;
            // 
            // prioLabel
            // 
            prioLabel.Location = new Point(78, 3);
            prioLabel.Name = "prioLabel";
            prioLabel.Size = new Size(48, 23);
            prioLabel.TabIndex = 5;
            prioLabel.Text = "Priority:";
            prioLabel.TextAlign = ContentAlignment.BottomLeft;
            // 
            // nameLabel
            // 
            nameLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nameLabel.ForeColor = SystemColors.ControlDark;
            nameLabel.Location = new Point(21, 3);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(51, 49);
            nameLabel.TabIndex = 6;
            nameLabel.Text = "Entry";
            nameLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // minLabel
            // 
            minLabel.Location = new Point(132, 3);
            minLabel.Name = "minLabel";
            minLabel.Size = new Size(48, 23);
            minLabel.TabIndex = 8;
            minLabel.Text = "Min:";
            minLabel.TextAlign = ContentAlignment.BottomLeft;
            // 
            // minInput
            // 
            minInput.Location = new Point(132, 29);
            minInput.Name = "minInput";
            minInput.Size = new Size(48, 23);
            minInput.TabIndex = 2;
            // 
            // maxLabel
            // 
            maxLabel.Location = new Point(186, 3);
            maxLabel.Name = "maxLabel";
            maxLabel.Size = new Size(48, 23);
            maxLabel.TabIndex = 10;
            maxLabel.Text = "Max:";
            maxLabel.TextAlign = ContentAlignment.BottomLeft;
            // 
            // maxInput
            // 
            maxInput.Location = new Point(186, 29);
            maxInput.Name = "maxInput";
            maxInput.Size = new Size(48, 23);
            maxInput.TabIndex = 3;
            // 
            // AudioSettingsEntry
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(maxLabel);
            Controls.Add(maxInput);
            Controls.Add(minLabel);
            Controls.Add(minInput);
            Controls.Add(nameLabel);
            Controls.Add(prioLabel);
            Controls.Add(configureButton);
            Controls.Add(priorityInput);
            Controls.Add(DragHandle1);
            Name = "AudioSettingsEntry";
            Size = new Size(321, 57);
            ((System.ComponentModel.ISupportInitialize)priorityInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)minInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxInput).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public DragHandle DragHandle1;
        private NumericUpDown priorityInput;
        private Button configureButton;
        private Label prioLabel;
        private Label nameLabel;
        private Label minLabel;
        private NumericUpDown minInput;
        private Label maxLabel;
        private NumericUpDown maxInput;
    }
}
