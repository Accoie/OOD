using System.Windows;

namespace ShapesApplication.Models
{
    public class ResizeShapeCommand : ICommand
    {
        private readonly Shape _shape;
        private readonly Point _anchorPoint;
        private readonly Point _newPoint;
        private Rect _originalBounds;

        public ResizeShapeCommand( Shape shape, Point anchorPoint, Point newPoint )
        {
            _shape = shape;
            _anchorPoint = anchorPoint;
            _newPoint = newPoint;
            _originalBounds = shape.Bounds;
        }

        public void Execute()
        {
            _shape.Resize( _anchorPoint, _newPoint );
        }

        public void Unexecute()
        {
            _shape.Bounds = _originalBounds;
        }
    }
}