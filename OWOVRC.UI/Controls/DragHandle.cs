namespace OWOVRC.UI.Controls
{
    public partial class DragHandle : Control
    {
        private readonly SolidBrush panelBrush = new(SystemColors.ControlDark);
        private readonly SolidBrush panel3DBrush = new(SystemColors.ControlLight);

        public DragHandle()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.CreateControl();
            MaximumSize = new(12, Parent?.Height ?? 0);
            this.Cursor = Cursors.NoMoveVert;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            DrawLine(pe, 0);
            DrawLine(pe, 6);
        }

        private void DrawLine(PaintEventArgs pe, int x)
        {
            Rectangle box1 = new(x, 0, 6, ClientRectangle.Height);
            pe.Graphics.FillRectangle(panel3DBrush, box1);

            Rectangle box2 = new(x + 2, 0, 2, ClientRectangle.Height);
            pe.Graphics.FillRectangle(panelBrush, box2);
        }
    }
}
