using System.Globalization;
using System.Text;
using Slides.Types;

namespace Slides.Canvas
{
    public class SvgCanvas : ICanvas
    {
        private RGBAColor _fillColor = new( 0, 0, 0, 0 );
        private RGBAColor _lineColor = new( 0, 0, 0, 1 );
        private double _lineWidth = 1.0;
        private readonly StringBuilder _svg = new();

        private readonly CultureInfo _culture = CultureInfo.InvariantCulture;

        public SvgCanvas()
        {
            _svg.AppendLine( """<svg xmlns="http://www.w3.org/2000/svg" width="1000" height="1000">""" );
        }

        public void DrawLine( Point from, Point to )
        {
            _svg.AppendLine(
                $"  <line x1=\"{FormatDouble( from.X )}\" y1=\"{FormatDouble( from.Y )}\" " +
                $"x2=\"{FormatDouble( to.X )}\" y2=\"{FormatDouble( to.Y )}\" " +
                $"stroke=\"{_lineColor}\" stroke-width=\"{FormatDouble( _lineWidth )}\" />" );
        }

        public void DrawEllipse( Point center, double radiusX, double radiusY )
        {
            _svg.AppendLine(
                $"  <ellipse cx=\"{FormatDouble( center.X )}\" cy=\"{FormatDouble( center.Y )}\" " +
                $"rx=\"{FormatDouble( radiusX )}\" ry=\"{FormatDouble( radiusY )}\" " +
                $"fill=\"none\" stroke=\"{_lineColor}\" stroke-width=\"{FormatDouble( _lineWidth )}\" />" );
        }

        public void FillEllipse( Point center, double radiusX, double radiusY )
        {
            _svg.AppendLine(
                $"  <ellipse cx=\"{FormatDouble( center.X )}\" cy=\"{FormatDouble( center.Y )}\" " +
                $"rx=\"{FormatDouble( radiusX )}\" ry=\"{FormatDouble( radiusY )}\" " +
                $"fill=\"{_fillColor}\" stroke=\"{_lineColor}\" stroke-width=\"{FormatDouble( _lineWidth )}\" />" );
        }

        public void FillPolygon( Point[] points )
        {
            string pointsAttr = string.Join( " ", points.Select( p => $"{FormatDouble( p.X )},{FormatDouble( p.Y )}" ) );

            _svg.AppendLine(
                $"  <polygon points=\"{pointsAttr}\" " +
                $"fill=\"{_fillColor}\" stroke=\"{_lineColor}\" stroke-width=\"{FormatDouble( _lineWidth )}\" />" );
        }

        public void SetFillColor( RGBAColor color ) => _fillColor = color;
        public void SetLineColor( RGBAColor color ) => _lineColor = color;

        public void SetLineWidth( double width ) => _lineWidth = width;

        public void Save( string path )
        {
            _svg.AppendLine( "</svg>" );
            File.WriteAllText( path, _svg.ToString() );
        }

        private string FormatDouble( double value )
        {
            return value.ToString( "0.##", _culture );
        }
    }
}