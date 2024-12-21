namespace OWOVRC.UI.Controls
{
    partial class AudioSettingsPriorityPanel
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

            foreach (AudioSettingsEntry item in Items.OrderByDescending((entry) => entry.Priority))
            {
                item.OnDragStart -= HandleItemDragStart;
                item.OnDragStop -= HandleItemDragStop;
                item.OnPriorityChanged -= HandlePriorityChanged;
            }

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
            audioSettingsEntry1 = new AudioSettingsEntry();
            SuspendLayout();
            // 
            // audioSettingsEntry1
            // 
            audioSettingsEntry1.BackColor = SystemColors.Control;
            audioSettingsEntry1.Location = new Point(3, 3);
            audioSettingsEntry1.MinimumSize = new Size(464, 31);
            audioSettingsEntry1.Name = "audioSettingsEntry1";
            audioSettingsEntry1.Priority = 0;
            audioSettingsEntry1.Size = new Size(464, 31);
            audioSettingsEntry1.TabIndex = 0;
            // 
            // AudioSettingsPriorityPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(audioSettingsEntry1);
            Name = "AudioSettingsPriorityPanel";
            Size = new Size(470, 217);
            ResumeLayout(false);
        }

        #endregion

        private AudioSettingsEntry audioSettingsEntry1;
    }
}
