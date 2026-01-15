using System;
using System.Windows;
using System.Windows.Input;
using ShapesApplication.Models;

namespace ShapesApplication.Views
{
    public interface IShapeView
    {
        event MouseButtonEventHandler OnMouseDownEvent;
        event MouseEventHandler OnMouseMoveEvent;
        event MouseButtonEventHandler OnMouseUpEvent;
        event KeyEventHandler OnKeyDownEvent;
        event EventHandler<ShapeType> RequestAddShape;
        event EventHandler RequestDeleteShape;
        event EventHandler<ResizeEventArgs> RequestResizeShape;
        event EventHandler RequestUndo;
        event EventHandler RequestRedo;

        FrameworkElement GetCanvas();
        Rect GetCanvasBounds();
        void RenderShapes();
    }
}