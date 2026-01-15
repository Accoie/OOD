using Slides.Shapes;

namespace Slides.Group
{
    public interface IShapeGroup : IShape
    {
        int GetShapesCount();
        IShape GetShape( int index );
        void InsertShape( IShape shape, int index );
        void RemoveShape( int index );
        IShapeGroup? GetParent();
        void SetParent( IShapeGroup parent );
        public bool CheckExistParent( IShapeGroup parent );
    }
}