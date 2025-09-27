using System.ComponentModel;

namespace OWOVRC.UI.Controls
{
    public partial class DirectionSpeedIndicator : Control
    {
        [Localizable(true)]
        [Description("The value of the element on the X-Axis"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float ValueX
        {
            get
            {
                return valueX;
            }
            set
            {
                // Don't redraw if the value hasn't changed
                if (this.valueX == value)
                {
                    return;
                }
                this.valueX = value;
                UpdateIndicator();
            }
        }
        private float valueX;
        [Localizable(true)]
        [Description("The value of the element on the Y-Axis"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float ValueY
        {
            get
            {
                return valueY;
            }
            set
            {
                // Don't redraw if the value hasn't changed
                if (this.valueY == value)
                {
                    return;
                }
                this.valueY = value;
                UpdateIndicator();
            }
        }
        private float valueY;
        [Localizable(true)]
        [Description("The minimum value of the element on the X-Axis"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float MaxX { get; set; } = 100f;
        [Localizable(true)]
        [Description("The maximum value of the element on the Y-Axis"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float MaxY { get; set; } = 100f;

        // Indicator line
        [Localizable(true)]
        [Description("The color of indicator line"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color DirectionIndicatorColor
        {
            get
            {
                return indicatorColor;
            }
            set
            {
                indicatorColor = value;
                indicatorPen = new Pen(value);
            }
        }
        private Color indicatorColor = Color.Red;
        private Pen indicatorPen = new(Color.Red);
        private Point indicatorStart = new();
        private Point indicatorEnd = new();

        // Threshold lines
        [Localizable(true)]
        [Description("Threshold for values on the X-Axis"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float ThresholdX
        {
            get
            {
                return thresholdX;
            }
            set
            {
                if (thresholdX == value)
                {
                    return;
                }
                thresholdX = value;
                UpdateThresholdIndicator();
            }
        }
        private float thresholdX = 100f;
        [Localizable(true)]
        [Description("Threshold for values on the Y-Axis"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float ThresholdY
        {
            get
            {
                return thresholdY;
            }
            set
            {
                if (thresholdY == value)
                {
                    return;
                }
                thresholdY = value;
                UpdateThresholdIndicator();
            }
        }
        private float thresholdY = 100f;

        [Localizable(true)]
        [Description("Color of the threshold indicator"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ThresholdColor
        {
            get
            {
                return thresholdColor;
            }
            set
            {
                thresholdColor = value;
                thresholdPen = new Pen(value);
                Invalidate();
            }
        }
        private Color thresholdColor = Color.Orange;
        private Pen thresholdPen = new(Color.Orange);
        private Rectangle thresholdRectangle = new();

        // Panel
        private readonly SolidBrush panelBGBrush = new(Color.White);

        // Axis line
        private readonly Pen axisPen = new(Color.LightGray);
        private Point axisStartY = new();
        private Point axisEndY = new();
        private Point axisStartX = new();
        private Point axisEndX = new();



        public DirectionSpeedIndicator()
        {
            InitializeComponent();

            // Enable double buffering to reduce flickering
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint,
                true
            );

            CalculateContentScale();

            UpdateIndicator();
        }

        public void ForceUpdate()
        {
            CalculateContentScale();
            UpdateIndicator();
        }

        private void UpdateIndicator()
        {
            int centerX = Width / 2;
            int centerY = Height / 2;

            float valueXScaled = (int) (Math.Clamp(valueX / MaxX, -1f, 1f) * Width);
            float valueYScaled = (int) (Math.Clamp(valueY / MaxY, -1f, 1f) * Height);

            int indicatorX = (int) (centerX + valueXScaled);
            int indicatorY = (int) (centerY - valueYScaled);

            indicatorEnd = new Point(indicatorX, indicatorY);

            Invalidate();
        }

        private void UpdateThresholdIndicator()
        {
            int centerX = Width / 2;
            int centerY = Height / 2;

            int thresholdWidth = (int)(Math.Clamp(thresholdX / MaxX, -1f, 1f) * Width);
            int thresholdHeight = (int)(Math.Clamp(thresholdY / MaxY, -1f, 1f) * Height);

            int thresholdOffsetX = centerX - thresholdWidth;
            int thresholdOffsetY = centerY - thresholdHeight;

            thresholdRectangle = new Rectangle(thresholdOffsetX, thresholdOffsetY, thresholdWidth * 2, thresholdHeight * 2);

            Invalidate();
        }

        private void CalculateContentScale()
        {
            axisStartX = new Point(0, Height / 2);
            axisEndX = new Point(Width, Height / 2);

            axisStartY = new Point(Width / 2, 0);
            axisEndY = new Point(Width / 2, Height);

            indicatorStart = new Point(Width / 2, Height / 2);;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // Draw background
            pe.Graphics.FillRectangle(panelBGBrush, ClientRectangle);

            // Draw axis lines
            pe.Graphics.DrawLine(axisPen, axisStartX, axisEndX);
            pe.Graphics.DrawLine(axisPen, axisStartY, axisEndY);

            // Draw diretion line
            pe.Graphics.DrawLine(indicatorPen, indicatorStart, indicatorEnd);

            // Draw threshold lines
            pe.Graphics.DrawRectangle(thresholdPen, thresholdRectangle);
        }
    }
}
