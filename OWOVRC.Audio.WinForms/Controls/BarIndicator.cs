using System.ComponentModel;

namespace OWOVRC.Audio.WinForms.Controls
{
    public partial class BarIndicator : Control
    {
        [Localizable(true)]
        [Description("The value of the element"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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
                UpdateValueRectangle();
                Invalidate();
            }
        }
        private int value;
        [Localizable(true)]
        [Description("The minimum value of the element"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Min { get; set; }
        [Localizable(true)]
        [Description("The maximum value of the element"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Max { get; set; } = 100;

        [Localizable(true)]
        [Description("The color of the filled bar"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

        [Localizable(true)]
        [Description("The value at which an indicator line should be shown of the element"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int IndicatorValue
        {
            get
            {
                return indicatorValue;
            }
            set
            {
                // Don't redraw if the value hasn't changed
                if (indicatorValue == value)
                {
                    return;
                }

                this.indicatorValue = value;
                UpdateIndicatorRectangle();
                Invalidate();
            }
        }
        private int indicatorValue;
        [Localizable(true)]
        [Description("The color of the indicator bar"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color IndicatorColor
        {
            get
            {
                return indicatorColor;
            }
            set
            {
                indicatorColor = value;
                indicatorBrush = new SolidBrush(indicatorColor);
            }
        }
        private Color indicatorColor = Color.Orange;

        private readonly SolidBrush panelBGBrush = new(Color.White);
        private SolidBrush panelFGBrush = new(Color.Orange);
        private SolidBrush indicatorBrush = new(Color.Black);
        private Rectangle barRectangle = new(0, 0, 0, 0);
        private Rectangle indicatorLine = new(0, 0, 0, 0);

        // Re-calculate values on changes only to save time when redrawing
        private void UpdateValueRectangle()
        {
            float percentage = (Value - Min) / (float)(Max - Min);
            int height = (int)(percentage * Height);
            barRectangle = new(0, Height - height, Width, height);
        }

        private void UpdateIndicatorRectangle()
        {
            float indicatorPercentage = (IndicatorValue - Min) / (float)(Max - Min);
            int indicatorHeight = (int)(indicatorPercentage * Height);
            indicatorLine = new(0, Height - indicatorHeight, Width, 1);
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

            // Draw indicator line
            pe.Graphics.FillRectangle(indicatorBrush, indicatorLine);
        }
    }
}
