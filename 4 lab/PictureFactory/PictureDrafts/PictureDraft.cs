using PictureFactory.Shapes;

namespace PictureFactory.PictureDrafts
{
    public class PictureDraft : IPictureDraft
    {
        private List<Shape> _shapes = new();

        public int GetShapesSize()
        {
            return _shapes.Count;
        }

        public Shape GetShape( int index )
        {
            if ( index >= _shapes.Count || index < 0 )
            {
                throw new ArgumentException( "Invalid index" );
            }

            return _shapes[ index ];
        }

        public void AddShape( Shape shape )
        {
            _shapes.Add( shape );
        }
    }
}