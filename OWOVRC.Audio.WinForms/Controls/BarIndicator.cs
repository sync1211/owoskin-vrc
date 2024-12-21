using System.ComponentModel;

namespace OWOVRC.Audio.WinForms.Controls
{
    public partial class BarIndicator : Control
    {
        [Localizable(true)]
        [Description("The value of the element"), Category("Data")]
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                // Don't redraw if the value hasn't changed
                if (this.value == value)
                {
                    return;
                }

                this.value = value;
                UpdateDrawingParameters();
                Invalidate();
            }
        }
        private int value;
        [Localizable(true)]
        [Description("The minimum value of the element"), Category("Data")]
        public int Min { get; set; }
        [Localizable(true)]
        [Description("The maximum value of the element"), Category("Data")]
        public int Max { get; set; } = 100;

        [Localizable(true)]
        [Description("The color of the filled bar"), Category("Data")]
        public Color BarColor
        {
            get
            {
                return barColor;
            }
            set
            {
                barColor = value;
                panelFGBrush = new SolidBrush(value);
            }
        }
        private Color barColor = Color.Orange;

        private readonly SolidBrush panelBGBrush = new(Color.White);
        private SolidBrush panelFGBrush = new(Color.Orange);
        private Rectangle barRectangle = new(0, 0, 0, 0);

        // Re-calculate values on changes only to save time when redrawing
        private void UpdateDrawingParameters()
        {
            float percentage = (Value - Min) / (float)(Max - Min);
            int height = (int)(percentage * Height);
            barRectangle = new(0, Height - height, Width, height);
        }

        public BarIndicator()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            // Draw outline
            pe.Graphics.FillRectangle(panelBGBrush, ClientRectangle);

            // Draw bar
            pe.Graphics.FillRectangle(panelFGBrush, barRectangle);
        }
    }
}
