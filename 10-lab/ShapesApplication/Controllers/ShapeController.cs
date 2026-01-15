using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ShapesApplication.Models;
using ShapesApplication.Models.Shapes;
using ShapesApplication.Models.Shapes.Styles;
using ShapesApplication.Views;

namespace ShapesApplication.Controller
{
    public class ShapeController
    {
        private readonly Document _doc;
        private readonly IShapeView _view;

        private Shape _dragged;
        private Point _dragStart;
        private Rect _originalBounds;

        private bool _isResizing = false;
        private Point _resizeStart;
        private Rect _resizeOriginalBounds;
        private Shape _resizedShape;

        public ShapeController( Document doc, IShapeView view )
        {
            _doc = doc;
            _view = view;

            view.OnMouseDownEvent += OnDown;
            view.OnMouseMoveEvent += OnMove;
            view.OnMouseUpEvent += OnUp;
            view.RequestAddShape += AddShape;
            view.RequestDeleteShape += Delete;
            view.RequestUndo += ( sender, args ) => _doc.Undo();
            view.RequestRedo += ( sender, args ) => _doc.Redo();
            view.RequestResizeShape += OnResizeShape;

            _doc.ShapesChanged += _view.RenderShapes;
        }

        public IReadOnlyList<IShape> GetShapes()
        {
            return _doc.Shapes;
        }

        private void OnDown( object s, MouseButtonEventArgs e )
        {
            Point pos = e.GetPosition( _view.GetCanvas() );
            _dragged = _doc.SelectShapeAtPoint( pos );
            _dragStart = pos;

            if ( _dragged != null )
            {
                _originalBounds = _dragged.Bounds;
            }
        }

        private void OnMove( object s, MouseEventArgs e )
        {
            if ( _dragged == null || e.LeftButton != MouseButtonState.Pressed )
            {
                return;
            }

            Point pos = e.GetPosition( _view.GetCanvas() );
            Rect canvasBounds = _view.GetCanvasBounds();

            Vector delta = pos - _dragStart;
            Point desiredPosition = _originalBounds.Location + delta;

            double constrainedX = desiredPosition.X;
            double constrainedY = desiredPosition.Y;

            if ( constrainedX < 0 )
            {
                constrainedX = 0;
            }

            if ( constrainedY < 0 )
            {
                constrainedY = 0;
            }

            if ( constrainedX + _originalBounds.Width > canvasBounds.Width )
            {
                constrainedX = canvasBounds.Width - _originalBounds.Width;
            }

            if ( constrainedY + _originalBounds.Height > canvasBounds.Height )
            {
                constrainedY = canvasBounds.Height - _originalBounds.Height;
            }

            _dragged.Bounds = new Rect(
                new Point( constrainedX, constrainedY ),
                _originalBounds.Size
            );

            _view.RenderShapes();
        }

        private void OnUp( object s, MouseButtonEventArgs e )
        {
            if ( _dragged != null )
            {
                Vector finalOffset = _dragged.Bounds.Location - _originalBounds.Location;
                _dragged.Bounds = _originalBounds;

                if ( finalOffset.Length > 1 )
                {
                    _doc.MoveSelected( finalOffset );
                }

                _dragged = null;
            }
        }

        private void OnResizeShape( object sender, ResizeEventArgs e )
        {
            Shape shape = _doc.Shapes.FirstOrDefault( s => s.IsSelected );
            if ( shape == null )
            {
                return;
            }

            if ( e.IsResizing )
            {
                if ( !_isResizing )
                {
                    _isResizing = true;
                    _resizedShape = shape;
                    _resizeStart = e.AnchorPoint;
                    _resizeOriginalBounds = shape.Bounds;

                    ResizeShapeTemporarily( shape, _resizeStart, e.NewPoint );
                }
                else
                {
                    ResizeShapeTemporarily( _resizedShape, _resizeStart, e.NewPoint );
                }
            }
            else
            {
                if ( _isResizing )
                {
                    _isResizing = false;

                    _resizedShape.Bounds = _resizeOriginalBounds;

                    if ( _resizeStart != e.NewPoint )
                    {
                        _doc.ResizeSelected( _resizeStart, e.NewPoint );
                    }

                    _resizedShape = null;
                }
            }
        }

        private void ResizeShapeTemporarily( Shape shape, Point anchorPoint, Point newPoint )
        {
            double x = Math.Min( anchorPoint.X, newPoint.X );
            double y = Math.Min( anchorPoint.Y, newPoint.Y );
            double width = Math.Abs( newPoint.X - anchorPoint.X );
            double height = Math.Abs( newPoint.Y - anchorPoint.Y );

            width = Math.Max( width, 10 );
            height = Math.Max( height, 10 );

            shape.Bounds = new Rect( x, y, width, height );
            _view.RenderShapes();
        }

        private void AddShape( object s, ShapeType type )
        {
            Rect canvasBounds = _view.GetCanvasBounds();
            Point shapeLocation = new Point( canvasBounds.Width / 2 - 50, canvasBounds.Height / 2 - 50 );
            Rect frame = new Rect( shapeLocation.X, shapeLocation.Y, 100, 100 );

            Shape shape;
            switch ( type )
            {
                case ShapeType.Rectangle:
                    shape = new RectangleShape( frame, ShapeStyle.UseDefault() );
                    break;
                case ShapeType.Triangle:
                    shape = new TriangleShape( frame, ShapeStyle.UseDefault() );
                    break;
                case ShapeType.Ellipse:
                    shape = new EllipseShape( frame, ShapeStyle.UseDefault() );
                    break;
                default:
                    throw new ArgumentOutOfRangeException( nameof( type ), type, null );
            }

            _doc.AddShape( shape );
        }

        private void Delete( object s, EventArgs e )
        {
            Shape sel = _doc.Shapes.FirstOrDefault( x => x.IsSelected );
            if ( sel != null )
            {
                _doc.RemoveShape( sel );
            }
        }
    }
}