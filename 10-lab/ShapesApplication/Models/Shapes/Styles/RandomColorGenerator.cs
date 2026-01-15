using System;
using System.Windows.Media;

namespace ShapesApplication.Models.Shapes.Styles
{
    public static class RandomColorGenerator
    {
        private static readonly Random _random = new Random();

        public static Brush GetRandomColor()
        {
            byte r = ( byte )_random.Next( 256 );
            byte g = ( byte )_random.Next( 256 );
            byte b = ( byte )_random.Next( 256 );

            return new SolidColorBrush( Color.FromRgb( r, g, b ) );
        }
    }
}