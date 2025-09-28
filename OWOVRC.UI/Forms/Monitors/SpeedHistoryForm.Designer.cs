namespace OWOVRC.UI.Forms.Monitors
{
    partial class SpeedHistoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpeedHistoryForm));
            speedHistoryGraph = new OWOVRC.UI.Controls.SpeedHistoryGraph();
            notRunningIndicator = new Label();
            closeButton = new Button();
            resumeGraphButton = new Button();
            pauseGraphButton = new Button();
            SuspendLayout();
            // 
            // speedHistoryGraph
            // 
            speedHistoryGraph.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            speedHistoryGraph.DeltaThreshold = 30F;
            speedHistoryGraph.DeltaThresholdColor = Color.Orange;
            speedHistoryGraph.LineColor = Color.Black;
            speedHistoryGraph.Location = new Point(12, 40);
            speedHistoryGraph.MaxY = 100F;
            speedHistoryGraph.Name = "speedHistoryGraph";
            speedHistoryGraph.SegmentCount = 20;
            speedHistoryGraph.Size = new Size(1135, 240);
            speedHistoryGraph.TabIndex = 0;
            // 
            // notRunningIndicator
            // 
            notRunningIndicator.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            notRunningIndicator.AutoSize = true;
            notRunningIndicator.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            notRunningIndicator.Location = new Point(12, 286);
            notRunningIndicator.Name = "notRunningIndicator";
            notRunningIndicator.Size = new Size(166, 30);
            notRunningIndicator.TabIndex = 9;
            notRunningIndicator.Text = "Indicators will not be updated \r\nwhile OWOVRC is stopped.";
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.DialogResult = DialogResult.Cancel;
            closeButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            closeButton.Location = new Point(1072, 288);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 23);
            closeButton.TabIndex = 8;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += CloseButton_Click;
            // 
            // resumeGraphButton
            // 
            resumeGraphButton.Image = Properties.Resources.Play;
            resumeGraphButton.Location = new Point(1125, 12);
            resumeGraphButton.Name = "resumeGraphButton";
            resumeGraphButton.Size = new Size(22, 22);
            resumeGraphButton.TabIndex = 10;
            resumeGraphButton.UseVisualStyleBackColor = true;
            resumeGraphButton.Click += ResumeGraphButton_Click;
            // 
            // pauseGraphButton
            // 
            pauseGraphButton.Image = (Image)resources.GetObject("pauseGraphButton.Image");
            pauseGraphButton.Location = new Point(1125, 12);
            pauseGraphButton.Name = "pauseGraphButton";
            pauseGraphButton.Size = new Size(22, 22);
            pauseGraphButton.TabIndex = 11;
            pauseGraphButton.UseVisualStyleBackColor = true;
            pauseGraphButton.Click += PauseGraphButton_Click;
            // 
            // SpeedHistoryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = closeButton;
            ClientSize = new Size(1159, 323);
            Controls.Add(pauseGraphButton);
            Controls.Add(resumeGraphButton);
            Controls.Add(notRunningIndicator);
            Controls.Add(closeButton);
            Controls.Add(speedHistoryGraph);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "SpeedHistoryForm";
            Text = "SpeedHistoryForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.SpeedHistoryGraph speedHistoryGraph;
        private Label notRunningIndicator;
        private Button closeButton;
        private Button resumeGraphButton;
        private Button pauseGraphButton;
    }
}