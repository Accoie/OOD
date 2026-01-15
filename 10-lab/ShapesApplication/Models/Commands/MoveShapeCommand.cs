using System.Windows;

namespace ShapesApplication.Models
{
    public class MoveShapeCommand : ICommand
    {
        private readonly Shape _shape;
        private readonly Vector _offset;
        private Vector _previousOffset;

        public MoveShapeCommand( Shape shape, Vector offset )
        {
            _shape = shape;
            _offset = offset;
        }

        public void Execute()
        {
            _previousOffset = new Vector( -_offset.X, -_offset.Y );
            _shape.Move( _offset );
        }

        public void Unexecute()
        {
            _shape.Move( _previousOffset );
        }
    }
}