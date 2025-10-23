namespace Slides.Types
{
    public struct Frame
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public Frame( double x, double y, double width, double height )
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
