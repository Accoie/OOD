using System.Collections.ObjectModel;

namespace ShapesApplication.Models
{
    public class AddShapeCommand : ICommand
    {
        private readonly ObservableCollection<Shape> _shapes;
        private readonly Shape _shape;

        public AddShapeCommand( ObservableCollection<Shape> shapes, Shape shape )
        {
            _shapes = shapes;
            _shape = shape;
        }

        public void Execute()
        {
            _shapes.Add( _shape );
        }

        public void Unexecute()
        {
            _shapes.Remove( _shape );
        }
    }
}