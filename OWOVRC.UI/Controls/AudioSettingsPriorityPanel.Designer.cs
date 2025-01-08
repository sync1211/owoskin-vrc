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
            SuspendLayout();
            // 
            // AudioSettingsPriorityPanel
            // 
            AutoScroll = true;
            BackColor = Color.White;
            Name = "AudioSettingsPriorityPanel";
            Size = new Size(466, 192);
            ResumeLayout(false);
        }

        #endregion

        private AudioSettingsEntry audioSettingsEntry1;
    }
}
