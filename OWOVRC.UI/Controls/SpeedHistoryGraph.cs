using System.ComponentModel;

namespace OWOVRC.UI.Controls
{
    public partial class SpeedHistoryGraph : Control
    {
        private readonly List<float> values = [];
        private readonly List<float> valueDelta = [];

        [Localizable(true)]
        [Description("Maximum value on the Y-Axis"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float MaxY
        {
            get
            {
                return maxY;
            }
            set
            {
                if (maxY == value)
                {
                    return;
                }
                maxY = value;
            }
        }
        private float maxY = 100f;


        // Indicator line
        [Localizable(true)]
        [Description("The color of indicator line"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color LineColor
        {
            get
            {
                return lineColor;
            }
            set
            {
                lineColor = value;
                linePen = new Pen(value);
            }
        }
        private Color lineColor = Color.Black;
        private Pen linePen = new(Color.Black);

        [Localizable(true)]
        [Description("Threshold for values on the Y-Axis"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float DeltaThreshold
        {
            get
            {
                return deltaThreshold;
            }
            set
            {
                if (deltaThreshold == value)
                {
                    return;
                }
                deltaThreshold = value;
                Invalidate();
            }
        }
        private float deltaThreshold = 30f;

        [Localizable(true)]
        [Description("Color of the graph line when the delta exceeds the delta threshold"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color DeltaThresholdColor
        {
            get
            {
                return deltaThresholdColor;
            }
            set
            {
                deltaThresholdColor = value;
                deltaThresholdPen = new Pen(value);
                deltaThresholdBrush = new SolidBrush(value);
                Invalidate();
            }
        }
        private Color deltaThresholdColor = Color.Orange;
        private Pen deltaThresholdPen = new(Color.Orange);
        private SolidBrush deltaThresholdBrush = new(Color.Orange);


        [Localizable(true)]
        [Description("Number of values to display"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int SegmentCount
        {
            get
            {
                return segmentCount;
            }
            set
            {
                if (segmentCount == value)
                {
                    return;
                }
                segmentCount = value;
                Invalidate();
            }
        }
        private int segmentCount = 5;

        // Panel
        private readonly SolidBrush panelBGBrush = new(Color.White);



        public SpeedHistoryGraph()
        {
            InitializeComponent();

            // Enable double buffering to reduce flickering
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint,
                true
            );

            AddValue(0);
        }

        public void AddValue(float value)
        {
            float oldValue = values.FirstOrDefault();
            valueDelta.Insert(0, Math.Abs(oldValue - value));
            values.Insert(0, value);

            if (values.Count > (segmentCount + 1))
            {
                values.RemoveAt(values.Count - 1);
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // Draw background
            pe.Graphics.FillRectangle(panelBGBrush, ClientRectangle);

            // Draw values
            int segmentWidth = ClientRectangle.Width / segmentCount;
            Point? lastPoint = null;
            int segmentLimit = Math.Min(values.Count - 1, segmentCount);

            for (int i = 0; i <= segmentLimit; i++)
            {
                float delta = (i > 0) ? valueDelta[i - 1] : 0;
                float value = values[i];
                float scaledValue = Math.Max(Math.Min(value / maxY, 1), 0.01f);
                int calHeight = ClientRectangle.Height - (int) (ClientRectangle.Height * scaledValue);

                Point currentPoint = new(i * segmentWidth, calHeight);

                Pen pen = (delta >= deltaThreshold) ? deltaThresholdPen : linePen;
                pe.Graphics.DrawLine(pen, lastPoint ?? currentPoint, currentPoint);

                if (i > 0)
                {
                    Brush textBrush = (delta >= deltaThreshold) ? deltaThresholdBrush : Brushes.Black;
                    Point midpoint = GetMidpoint(lastPoint ?? currentPoint, currentPoint);
                    pe.Graphics.DrawString(delta.ToString("0.00"), DefaultFont, textBrush, midpoint);
                }

                lastPoint = currentPoint;
            }
        }

        public static Point GetMidpoint(Point point1, Point point2)
        {
            float differenceX = (float)point1.X - (float)point2.X;
            float differenceY = (float)point1.Y - (float)point2.Y;

            int newX = (int) (point1.X - (differenceX / 2));
            int newY = (int) (point1.Y - (differenceY / 2));

            //FIXME: Calculation is incorrect

            return new Point(newX, Math.Max(newY - 20, 5)); // Offset of 20 to properly show the value above the line
        }
    }
}
