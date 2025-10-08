using PictureFactory.Shapes;

namespace PictureFactory.Factories
{
    public interface IShapeFactory
    {
        Shape CreateShape( string descr );
    }
}
