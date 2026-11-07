namespace OWOVRC.UI.Controls
{
    public struct IndicatorMouseEventValues
    {
        public readonly int X;
        public readonly int Y;
        public readonly float PercentX;
        public readonly float PercentY;
        public readonly float ValueX;
        public readonly float ValueY;

        public IndicatorMouseEventValues(int x, int y, float percentX, float percentY, float valueX, float valueY)
        {
            X = x;
            Y = y;

            PercentX = percentX;
            PercentY = percentY;

            ValueX = valueX;
            ValueY = valueY;
        }
    }
}
