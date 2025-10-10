using Moq;
using PictureFactory.PictureDrafts;
using PictureFactory.Shapes;
using PictureFactory.Types;

namespace PictureFactory.Tests
{
    public class PictureDraftTests
    {
        [Test]
        public void GetShapesSize_WhenEmpty_ReturnsZero()
        {
            // Arrange
            PictureDraft draft = new PictureDraft();

            // Act
            int size = draft.GetShapesSize();

            // Assert
            Assert.That( size, Is.EqualTo( 0 ) );
        }

        [Test]
        public void GetShapesSize_AfterAddingShapes_ReturnsCorrectCount()
        {
            // Arrange
            PictureDraft draft = new PictureDraft();
            Shape shape1 = new Mock<Shape>( Color.Red ).Object;
            Shape shape2 = new Mock<Shape>( Color.Red ).Object;

            // Act
            draft.AddShape( shape1 );
            draft.AddShape( shape2 );

            // Assert
            Assert.That( draft.GetShapesSize(), Is.EqualTo( 2 ) );
        }

        [Test]
        public void GetShape_WithValidIndex_ReturnsCorrectShape()
        {
            // Arrange
            PictureDraft draft = new PictureDraft();
            Shape shape1 = new Mock<Shape>( Color.Red ).Object;
            Shape shape2 = new Mock<Shape>( Color.Red ).Object;

            draft.AddShape( shape1 );
            draft.AddShape( shape2 );

            // Act
            Shape result1 = draft.GetShape( 0 );
            Shape result2 = draft.GetShape( 1 );

            // Assert
            Assert.That( result1, Is.EqualTo( shape1 ) );
            Assert.That( result2, Is.EqualTo( shape2 ) );
        }

        [Test]
        public void GetShape_WithNegativeIndex_ThrowsArgumentException()
        {
            // Arrange
            PictureDraft draft = new PictureDraft();
            draft.AddShape( new Mock<Shape>( Color.Red ).Object );

            // Act & Assert
            Assert.Throws<ArgumentException>( () => draft.GetShape( -1 ) );
        }

        [Test]
        public void GetShape_WithIndexEqualToCount_ThrowsArgumentException()
        {
            // Arrange
            PictureDraft draft = new PictureDraft();
            draft.AddShape( new Mock<Shape>( Color.Red ).Object );

            // Act & Assert
            Assert.Throws<ArgumentException>( () => draft.GetShape( 1 ) );
        }

        [Test]
        public void GetShape_WithIndexGreaterThanCount_ThrowsArgumentException()
        {
            // Arrange
            PictureDraft draft = new PictureDraft();
            draft.AddShape( new Mock<Shape>( Color.Red ).Object );

            // Act & Assert
            Assert.Throws<ArgumentException>( () => draft.GetShape( 5 ) );
        }

        [Test]
        public void AddShape_AddsShapeToDraft_WillAdded()
        {
            // Arrange
            PictureDraft draft = new PictureDraft();
            Shape shape = new Mock<Shape>( Color.Red ).Object;

            // Act
            draft.AddShape( shape );

            // Assert
            Assert.That( draft.GetShapesSize(), Is.EqualTo( 1 ) );
            Assert.That( draft.GetShape( 0 ), Is.EqualTo( shape ) );
        }

        [Test]
        public void AddShape_MultipleShapes_MaintainsOrder()
        {
            // Arrange
            PictureDraft draft = new PictureDraft();
            Shape[] shapes =
            {
                new Mock<Shape>(Color.Red).Object,
                new Mock<Shape>(Color.Red).Object,
                new Mock<Shape>(Color.Red).Object
            };

            // Act
            foreach ( var shape in shapes )
            {
                draft.AddShape( shape );
            }

            // Assert
            for ( int i = 0; i < shapes.Length; i++ )
            {
                Assert.That( draft.GetShape( i ), Is.EqualTo( shapes[ i ] ) );
            }
        }
    }
}
