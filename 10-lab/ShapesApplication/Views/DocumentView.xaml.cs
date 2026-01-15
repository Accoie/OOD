using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ShapesApplication.Controller;
using ShapesApplication.Models;
using ShapesApplication.Models.Shapes;
using Point = System.Windows.Point;
using Shape = ShapesApplication.Models.Shape;

namespace ShapesApplication.Views
{
    public partial class DocumentView : UserControl, IShapeView
    {
        private readonly ShapeController _controller;
        private readonly DrawingImage _drawingImage;
        private readonly DrawingImage _selectionImage;
        private readonly Rectangle[] _resizeHandles;
        private int _activeResizeHandle = -1;
        private bool _isInitialized = false;
        private bool _isResizing = false;
        private Point _resizeStartPoint;
        private Rect _originalBoundsBeforeResize;
        private Point _fixedAnchorPoint;

        public event MouseButtonEventHandler OnMouseDownEvent;
        public event MouseEventHandler OnMouseMoveEvent;
        public event MouseButtonEventHandler OnMouseUpEvent;
        public event KeyEventHandler OnKeyDownEvent;
        public event EventHandler<ShapeType> RequestAddShape;
        public event EventHandler RequestDeleteShape;
        public event EventHandler RequestUndo;
        public event EventHandler RequestRedo;
        public event EventHandler<ResizeEventArgs> RequestResizeShape;

        public DocumentView( Document document )
        {
            InitializeComponent();

            _drawingImage = new DrawingImage();
            _selectionImage = new DrawingImage();

            RenderImage.Source = _drawingImage;
            SelectionRenderImage.Source = _selectionImage;

            _resizeHandles = new Rectangle[ 4 ];
            for ( int i = 0; i < 4; i++ )
            {
                _resizeHandles[ i ] = new Rectangle
                {
                    Width = 8,
                    Height = 8,
                    Fill = Brushes.White,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    Visibility = Visibility.Collapsed,
                    IsHitTestVisible = true
                };

                MainCanvas.Children.Add( _resizeHandles[ i ] );
            }

            _controller = new ShapeController( document, this );

            Loaded += DocumentView_Loaded;
            SizeChanged += DocumentViewOnSizeChanged;

            Focusable = true;
        }

        public FrameworkElement GetCanvas()
        {
            return MainCanvas;
        }

        public Rect GetCanvasBounds()
        {
            double margin = 10;
            double borderThickness = 1;

            double availableWidth = Math.Max( 0, ActualWidth - ( 2 * margin ) - ( 2 * borderThickness ) - 3 );
            double availableHeight = Math.Max( 0, ActualHeight - ( 2 * margin ) - ( 2 * borderThickness ) - 3 );

            if ( availableWidth <= 0 || double.IsNaN( availableWidth ) )
            {
                availableWidth = 600;
            }

            if ( availableHeight <= 0 || double.IsNaN( availableHeight ) )
            {
                availableHeight = 400;
            }

            return new Rect( 0, 0, availableWidth, availableHeight );
        }

        public void RenderShapes()
        {
            if ( !_isInitialized )
            {
                return;
            }

            Rect canvasBounds = GetCanvasBounds();

            if ( canvasBounds.Width <= 0 || canvasBounds.Height <= 0 ||
                double.IsNaN( canvasBounds.Width ) || double.IsNaN( canvasBounds.Height ) )
            {
                return;
            }

            DrawShapes( canvasBounds );

            UpdateSelection( canvasBounds );

            RenderImage.Width = canvasBounds.Width;
            RenderImage.Height = canvasBounds.Height;
            SelectionRenderImage.Width = canvasBounds.Width;
            SelectionRenderImage.Height = canvasBounds.Height;
        }

        public void AddRectangle()
        {
            RequestAddShape?.Invoke( this, ShapeType.Rectangle );
        }

        public void AddTriangle()
        {
            RequestAddShape?.Invoke( this, ShapeType.Triangle );
        }

        public void AddEllipse()
        {
            RequestAddShape?.Invoke( this, ShapeType.Ellipse );
        }

        public void DeleteSelectedShape()
        {
            RequestDeleteShape?.Invoke( this, EventArgs.Empty );
        }

        public void Undo()
        {
            RequestUndo?.Invoke( this, EventArgs.Empty );
        }

        public void Redo()
        {
            RequestRedo?.Invoke( this, EventArgs.Empty );
        }

        private void DrawShapes( Rect canvasBounds )
        {
            DrawingGroup shapesDrawingGroup = new DrawingGroup();

            using ( DrawingContext dc = shapesDrawingGroup.Open() )
            {
                dc.DrawRectangle( Brushes.White, null, canvasBounds );

                foreach ( Shape shape in _controller.GetShapes() )
                {
                    DrawShape( dc, shape );
                }
            }

            _drawingImage.Drawing = shapesDrawingGroup;
        }

        private void DocumentView_Loaded( object sender, RoutedEventArgs e )
        {
            _isInitialized = true;
            RenderShapes();
            MainCanvas.Focus();
        }

        private void DocumentViewOnSizeChanged( object sender, SizeChangedEventArgs e )
        {
            if ( _isInitialized && e.NewSize != e.PreviousSize )
            {
                Dispatcher.BeginInvoke( new Action( () =>
                {
                    if ( ActualWidth > 0 && ActualHeight > 0 )
                    {
                        RenderShapes();
                    }
                } ), System.Windows.Threading.DispatcherPriority.Background );
            }
        }

        private void DrawShape( DrawingContext context, IShape shape )
        {
            Geometry geometry = shape.GetGeometry();
            if ( geometry == null )
            {
                return;
            }

            Pen pen = new Pen( shape.Style.StrokeColor, shape.Style.StrokeWidth );
            context.DrawGeometry( shape.Style.FillColor, pen, geometry );
        }

        private void UpdateSelection( Rect canvasBounds )
        {
            IShape selectedShape = _controller.GetShapes().FirstOrDefault( s => s.IsSelected );

            if ( selectedShape != null )
            {
                DrawingGroup selectionDrawingGroup = new DrawingGroup();
                using ( DrawingContext context = selectionDrawingGroup.Open() )
                {
                    context.DrawRectangle( Brushes.Transparent, null, canvasBounds );

                    DrawSelection( context, selectedShape );
                }

                _selectionImage.Drawing = selectionDrawingGroup;
                SelectionRenderImage.Visibility = Visibility.Visible;

                UpdateResizeHandles( selectedShape, _isResizing );
            }
            else
            {
                SelectionRenderImage.Visibility = Visibility.Collapsed;
                _selectionImage.Drawing = null;
                HideResizeHandles();
            }
        }

        private void DrawSelection( DrawingContext dc, IShape shape )
        {
            Rect bounds = shape.Bounds;
            Pen dashPen = new Pen( Brushes.Black, 1 )
            {
                DashStyle = DashStyles.Dash,
                DashCap = PenLineCap.Flat
            };

            dc.DrawRectangle( null, dashPen, bounds );
        }

        private void UpdateResizeHandles( IShape shape, bool isResizing )
        {
            Rect b = shape.Bounds;

            if ( !isResizing )
            {
                const double minSize = 20;
                if ( b.Width < minSize || b.Height < minSize )
                {
                    HideResizeHandles();
                    return;
                }
            }

            Point[] handles = new[]
            {
                b.TopLeft,
                b.TopRight,
                b.BottomRight,
                b.BottomLeft
            };

            Cursor[] cursors = new[]
            {
                Cursors.SizeNWSE,
                Cursors.SizeNESW,
                Cursors.SizeNWSE,
                Cursors.SizeNESW
            };

            for ( int i = 0; i < 4; i++ )
            {
                Rectangle handle = _resizeHandles[ i ];
                Canvas.SetLeft( handle, handles[ i ].X - 4 );
                Canvas.SetTop( handle, handles[ i ].Y - 4 );
                handle.Cursor = cursors[ i ];
                handle.Visibility = Visibility.Visible;

                if ( !handle.IsMouseCaptured && !_isResizing )
                {
                    handle.MouseDown += ResizeHandleOnMouseDown;
                    handle.MouseMove += ResizeHandleOnMouseMove;
                    handle.MouseUp += ResizeHandleOnMouseUp;
                }
            }
        }

        private void HideResizeHandles()
        {
            foreach ( Rectangle handle in _resizeHandles )
            {
                handle.Visibility = Visibility.Collapsed;
            }
        }

        private void ResizeHandleOnMouseDown( object sender, MouseButtonEventArgs e )
        {
            if ( e.ChangedButton == MouseButton.Left )
            {
                Rectangle handle = sender as Rectangle;
                if ( handle != null )
                {
                    _activeResizeHandle = Array.IndexOf( _resizeHandles, handle );
                    if ( _activeResizeHandle >= 0 )
                    {
                        IShape shape = _controller.GetShapes().FirstOrDefault( s => s.IsSelected );
                        if ( shape != null )
                        {
                            _isResizing = true;
                            _originalBoundsBeforeResize = shape.Bounds;
                            _resizeStartPoint = e.GetPosition( MainCanvas );

                            _fixedAnchorPoint = GetFixedAnchorPoint( _originalBoundsBeforeResize, _activeResizeHandle );

                            RequestResizeShape?.Invoke( this, new ResizeEventArgs( _fixedAnchorPoint, _resizeStartPoint, true ) );

                            handle.CaptureMouse();
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        private void ResizeHandleOnMouseMove( object sender, MouseEventArgs e )
        {
            if ( _isResizing && _activeResizeHandle >= 0 && e.LeftButton == MouseButtonState.Pressed )
            {
                Point mousePos = e.GetPosition( MainCanvas );
                Point newPoint = ConstrainPointToCanvas( mousePos );

                Rect newBounds = CalculateBoundsFromFixedAnchor( _fixedAnchorPoint, newPoint, _activeResizeHandle );


                RequestResizeShape?.Invoke( this, new ResizeEventArgs( _fixedAnchorPoint, newPoint, true ) );

                e.Handled = true;

                RenderShapes();
            }
        }

        private void ResizeHandleOnMouseUp( object sender, MouseButtonEventArgs e )
        {
            if ( e.ChangedButton == MouseButton.Left && _isResizing )
            {
                Point mousePos = e.GetPosition( MainCanvas );
                Point newPoint = ConstrainPointToCanvas( mousePos );
                RequestResizeShape?.Invoke( this, new ResizeEventArgs( _fixedAnchorPoint, newPoint, false ) );

                _isResizing = false;
                _activeResizeHandle = -1;

                Rectangle handle = sender as Rectangle;
                handle?.ReleaseMouseCapture();

                RenderShapes();
                e.Handled = true;
            }
        }

        private Point GetFixedAnchorPoint( Rect bounds, int handleIndex )
        {
            switch ( handleIndex )
            {
                case 0: return bounds.BottomRight;
                case 1: return bounds.BottomLeft;
                case 2: return bounds.TopLeft;
                case 3: return bounds.TopRight;
                default: return bounds.TopLeft;
            }
        }

        private Rect CalculateBoundsFromFixedAnchor( Point fixedAnchor, Point mousePos, int handleIndex )
        {
            const double minSize = 20;

            double left, top, width, height;

            switch ( handleIndex )
            {
                case 0:
                    left = Math.Min( fixedAnchor.X, mousePos.X );
                    top = Math.Min( fixedAnchor.Y, mousePos.Y );
                    width = Math.Max( minSize, Math.Abs( fixedAnchor.X - mousePos.X ) );
                    height = Math.Max( minSize, Math.Abs( fixedAnchor.Y - mousePos.Y ) );
                    break;

                case 1:
                    left = Math.Min( mousePos.X, fixedAnchor.X );
                    top = Math.Min( fixedAnchor.Y, mousePos.Y );
                    width = Math.Max( minSize, Math.Abs( fixedAnchor.X - mousePos.X ) );
                    height = Math.Max( minSize, Math.Abs( fixedAnchor.Y - mousePos.Y ) );
                    break;

                case 2:
                    left = Math.Min( fixedAnchor.X, mousePos.X );
                    top = Math.Min( mousePos.Y, fixedAnchor.Y );
                    width = Math.Max( minSize, Math.Abs( fixedAnchor.X - mousePos.X ) );
                    height = Math.Max( minSize, Math.Abs( fixedAnchor.Y - mousePos.Y ) );
                    break;

                case 3:
                    left = Math.Min( mousePos.X, fixedAnchor.X );
                    top = Math.Min( mousePos.Y, fixedAnchor.Y );
                    width = Math.Max( minSize, Math.Abs( fixedAnchor.X - mousePos.X ) );
                    height = Math.Max( minSize, Math.Abs( fixedAnchor.Y - mousePos.Y ) );
                    break;

                default:
                    left = fixedAnchor.X;
                    top = fixedAnchor.Y;
                    width = minSize;
                    height = minSize;
                    break;
            }

            return new Rect( left, top, width, height );
        }

        private Point ConstrainPointToCanvas( Point point )
        {
            Rect canvasBounds = GetCanvasBounds();
            return new Point(
                Math.Max( 0, Math.Min( point.X, canvasBounds.Width ) ),
                Math.Max( 0, Math.Min( point.Y, canvasBounds.Height ) )
            );
        }

        private void CanvasOnMouseDown( object sender, MouseButtonEventArgs e )
        {
            MainCanvas.Focus();
            OnMouseDownEvent?.Invoke( this, e );
        }

        private void CanvasOnMouseMove( object sender, MouseEventArgs e )
        {
            OnMouseMoveEvent?.Invoke( this, e );

            if ( !_isResizing )
            {
                IShape shape = _controller.GetShapes().FirstOrDefault( s => s.IsSelected );
                if ( shape != null )
                {
                    Point pos = e.GetPosition( MainCanvas );
                    Cursor cursor = GetResizeCursor( shape.Bounds, pos );
                    if ( cursor != null )
                    {
                        Cursor = cursor;
                        e.Handled = true;
                        return;
                    }
                }
            }

            Cursor = Cursors.Arrow;
        }

        private void CanvasOnMouseUp( object sender, MouseButtonEventArgs e )
        {
            OnMouseUpEvent?.Invoke( this, e );
        }

        private Cursor GetResizeCursor( Rect bounds, Point mousePos )
        {
            const double hitSize = 12;

            Point[] positions = new[]
            {
                bounds.TopLeft,
                bounds.TopRight,
                bounds.BottomRight,
                bounds.BottomLeft
            };

            Cursor[] cursors = new[]
            {
                Cursors.SizeNWSE,
                Cursors.SizeNESW,
                Cursors.SizeNWSE,
                Cursors.SizeNESW
            };

            for ( int i = 0; i < 4; i++ )
            {
                if ( Math.Abs( positions[ i ].X - mousePos.X ) <= hitSize &&
                    Math.Abs( positions[ i ].Y - mousePos.Y ) <= hitSize )
                {
                    return cursors[ i ];
                }
            }

            return null;
        }

        private void DocumentViewOnKeyDown( object sender, KeyEventArgs e )
        {
            if ( e.Key == Key.Delete )
            {
                DeleteSelectedShape();
                e.Handled = true;
            }
            else if ( e.Key == Key.Z && ( Keyboard.Modifiers & ModifierKeys.Control ) != 0 )
            {
                if ( ( Keyboard.Modifiers & ModifierKeys.Shift ) != 0 )
                {
                    Redo();
                }
                else
                {
                    Undo();
                }

                e.Handled = true;
            }
            else if ( e.Key == Key.Y && ( Keyboard.Modifiers & ModifierKeys.Control ) != 0 )
            {
                Redo();
                e.Handled = true;
            }

            OnKeyDownEvent?.Invoke( this, e );
        }
    }
}