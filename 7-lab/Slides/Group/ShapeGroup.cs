using Slides.Canvas;
using Slides.Shapes;
using Slides.Styles.FillStyles;
using Slides.Styles.LineStyles;
using Slides.Types;

namespace Slides.Group
{
    public class ShapeGroup : IShapeGroup
    {
        private IShapeGroup? _parent;
        private List<IShape> _shapes;
        private CompositeLineStyle _lineStyle;
        private CompositeFillStyle _fillStyle;
        private Frame _frame;

        public ShapeGroup( List<IShape> shapes )
        {
            _shapes = shapes;
            _lineStyle = new CompositeLineStyle( _shapes.Select( s => s.GetLineStyle() ).ToList );
            _fillStyle = new CompositeFillStyle( _shapes.Select( s => s.GetFillStyle() ).ToList );
            _parent = null;

            foreach ( IShape shape in _shapes )
            {
                if ( shape is IShapeGroup group )
                {
                    group.SetParent( this );
                }
            }

            ActualizeFrame();
        }

        public void Draw( ICanvas canvas )
        {
            _shapes.ForEach( s => s.Draw( canvas ) );
        }

        public IFillStyle GetFillStyle()
        {
            return _fillStyle;
        }

        public Frame GetFrame()
        {
            return _frame;
        }

        public ILineStyle GetLineStyle()
        {
            return _lineStyle;
        }

        public IShape GetShape( int index )
        {
            if ( _shapes.Count <= index || index < 0 )
            {
                throw new ArgumentOutOfRangeException( "Index is out of range" );
            }

            return _shapes.ElementAt( index );
        }

        public int GetShapesCount()
        {
            return _shapes.Count;
        }

        public void InsertShape( IShape shape, int index )
        {
            if ( index > _shapes.Count || index < 0 )
            {
                throw new ArgumentOutOfRangeException( "Index is out of range" );
            }

            if ( shape is IShapeGroup group )
            {
                group.SetParent( this );
            }

            _shapes.Insert( index, shape );
            ActualizeFrame();
        }

        public void RemoveShape( int index )
        {
            _shapes.RemoveAt( index );
            ActualizeFrame();
        }

        public void SetFillStyle( IFillStyle style )
        {
            _shapes.ForEach( s => s.SetFillStyle( style ) );
        }

        public void SetFrame( Frame frame )
        {
            if ( _frame.Width == 0 && _frame.Height == 0 )
            {
                _frame = frame;
                return;
            }

            double scaleX = frame.Width / _frame.Width;
            double scaleY = frame.Height / _frame.Height;

            double dx = frame.X - _frame.X;
            double dy = frame.Y - _frame.Y;

            foreach ( IShape shape in _shapes )
            {
                Frame shapeFrame = shape.GetFrame();

                double newWidth = shapeFrame.Width * scaleX;
                double newHeight = shapeFrame.Height * scaleY;

                shape.SetFrame( new Frame( shapeFrame.X + dx, shapeFrame.Y + dy, newWidth, newHeight ) );
            }

            _frame = frame;
        }

        public void SetParent( IShapeGroup parent )
        {
            foreach ( IShape shape in _shapes )
            {
                if ( shape == parent )
                {
                    throw new ArgumentException( "Parent is child in this group" );
                }
            }

            if ( CheckExistParent( parent ) )
            {
                throw new ArgumentException( "This parent already exists" );
            }

            _parent = parent;
        }

        public IShapeGroup? GetParent()
        {
            return _parent;
        }

        public bool CheckExistParent( IShapeGroup parent )
        {
            if ( _parent == this )
            {
                return false;
            }

            if ( parent == _parent )
            {
                return true;
            }

            if ( _parent is null )
            {
                return false;
            }

            return _parent.CheckExistParent( parent );
        }

        public void SetLineStyle( ILineStyle style )
        {
            _shapes.ForEach( s => s.SetLineStyle( style ) );
        }

        private void ActualizeFrame()
        {
            if ( _shapes.Count == 0 )
            {
                _frame = new Frame( 0, 0, 0, 0 );
                return;
            }

            double minX = double.MaxValue;
            double minY = double.MaxValue;
            double maxX = double.MinValue;
            double maxY = double.MinValue;

            foreach ( IShape shape in _shapes )
            {
                Frame frame = shape.GetFrame();

                minX = Math.Min( minX, frame.X );
                minY = Math.Min( minY, frame.Y );
                maxX = Math.Max( maxX, frame.X + frame.Width );
                maxY = Math.Max( maxY, frame.Y + frame.Height );
            }

            _frame = new Frame(
                minX,
                minY,
                maxX - minX,
                maxY - minY
            );
        }
    }
}