using System.Collections.ObjectModel;

namespace ShapesApplication.Models
{
    public class RemoveShapeCommand : ICommand
    {
        private readonly ObservableCollection<Shape> _shapes;
        private readonly Shape _shape;
        private int _index;

        public RemoveShapeCommand( ObservableCollection<Shape> shapes, Shape shape )
        {
            _shapes = shapes;
            _shape = shape;
        }

        public void Execute()
        {
            _index = _shapes.IndexOf( _shape );
            _shapes.Remove( _shape );
        }

        public void Unexecute()
        {
            _shapes.Insert( _index, _shape );
        }
    }
}