using System;
using System.Windows;

namespace ShapesApplication.Views
{
    public class ResizeEventArgs : EventArgs
    {
        public Point AnchorPoint { get; }
        public Point NewPoint { get; }
        public bool IsResizing { get; }

        public ResizeEventArgs( Point anchorPoint, Point newPoint, bool isResizing )
        {
            AnchorPoint = anchorPoint;
            NewPoint = newPoint;
            IsResizing = isResizing;
        }
    }
}
