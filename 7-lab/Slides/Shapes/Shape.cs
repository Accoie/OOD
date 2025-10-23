using System.Drawing;
using Slides.Canvas;
using Slides.Styles.FillStyles;
using Slides.Styles.LineStyles;
using Slides.Types;

namespace Slides.Shapes
{
    public abstract class Shape : IShape
    {
        protected Frame _frame;
        protected ILineStyle _lineStyle;
        protected IFillStyle _fillStyle;

        protected Shape( Frame frame, ILineStyle lineStyle, IFillStyle fillStyle )
        {
            _frame = frame;
            _lineStyle = lineStyle;
            _fillStyle = fillStyle;
        }

        public abstract void Draw( ICanvas canvas );

        public Frame GetFrame()
        {
            return _frame;
        }

        public IFillStyle GetFillStyle()
        {
            return _fillStyle;
        }

        public ILineStyle GetLineStyle()
        {
            return _lineStyle;
        }

        public void SetFrame( Frame frame )
        {
            double scaleX = _frame.Width / frame.Width;
            double scaleY = _frame.Height / frame.Height;
            double dx = _frame.X - frame.X;
            double dy = _frame.Y - frame.Y;

            Scale( scaleX, scaleY );
            Move( dx, dy );
        }

        public void SetFillStyle( IFillStyle style )
        {
            _fillStyle = style;
        }

        public void SetLineStyle( ILineStyle style )
        {
            _lineStyle = style;
        }

        protected bool HasFill()
        {
            return _fillStyle.IsEnabled() ?? false;
        }

        protected bool HasLine()
        {
            return _lineStyle.IsEnabled() ?? false;
        }

        protected RGBAColor GetFillColor()
        {
            return _fillStyle.GetColor() ?? new RGBAColor( 0, 0, 0, 1 );
        }

        protected RGBAColor GetLineColor()
        {
            return _lineStyle.GetColor() ?? new RGBAColor( 0, 0, 0, 1 );
        }

        protected int GetLineWidth()
        {
            return _lineStyle.GetLineWidth();
        }

        protected abstract void Scale( double scaleX, double scaleY );
        protected abstract void Move( double dx, double dy );
    }
}