using Moq;
using PictureFactory.Canvases;
using PictureFactory.Painters;
using PictureFactory.PictureDrafts;
using PictureFactory.Shapes;
using PictureFactory.Shapes.Params;
using PictureFactory.Types;

namespace PictureFactory.Tests
{
    [TestFixture]
    public class PainterTests
    {
        private Mock<ICanvas> _mockCanvas;
        private Mock<IPictureDraft> _mockDraft;
        private Painter _painter;

        [SetUp]
        public void SetUp()
        {
            _mockCanvas = new Mock<ICanvas>();
            _mockDraft = new Mock<IPictureDraft>();
            _painter = new Painter();
        }

        [Test]
        public void DrawPicture_WhenDraftIsEmpty_ShouldNotDrawAnyShapes()
        {
            // Arrange
            _mockDraft.Setup( d => d.GetShapesSize() ).Returns( 0 );

            // Act
            _painter.DrawPicture( _mockDraft.Object, _mockCanvas.Object );

            // Assert
            _mockDraft.Verify( d => d.GetShape( It.IsAny<int>() ), Times.Never );
        }

        [Test]
        public void DrawPicture_WhenDraftHasShapes_ShouldDrawAllShapes()
        {
            // Arrange
            Mock<Shape> mockShape1 = new Mock<Shape>( Color.Red );
            Mock<Shape> mockShape2 = new Mock<Shape>( Color.Red );

            mockShape1.Setup( s => s.GetShapeType() ).Returns( ShapeType.Ellipse );
            mockShape2.Setup( s => s.GetShapeType() ).Returns( ShapeType.Rectangle );

            _mockDraft.Setup( d => d.GetShapesSize() ).Returns( 2 );
            _mockDraft.Setup( d => d.GetShape( 0 ) ).Returns( mockShape1.Object );
            _mockDraft.Setup( d => d.GetShape( 1 ) ).Returns( mockShape2.Object );

            // Act
            _painter.DrawPicture( _mockDraft.Object, _mockCanvas.Object );

            // Assert
            mockShape1.Verify( s => s.Draw( _mockCanvas.Object ), Times.Once );
            mockShape2.Verify( s => s.Draw( _mockCanvas.Object ), Times.Once );
        }

        [Test]
        public void DrawPicture_WhenDraftHasShapes_ShouldCallGetShapeWithCorrectIndices()
        {
            // Arrange
            Mock<Shape> mockShape1 = new Mock<Shape>( Color.Red );
            Mock<Shape> mockShape2 = new Mock<Shape>( Color.Red );
            Mock<Shape> mockShape3 = new Mock<Shape>( Color.Red );

            _mockDraft.Setup( d => d.GetShapesSize() ).Returns( 3 );
            _mockDraft.Setup( d => d.GetShape( 0 ) ).Returns( mockShape1.Object );
            _mockDraft.Setup( d => d.GetShape( 1 ) ).Returns( mockShape2.Object );
            _mockDraft.Setup( d => d.GetShape( 2 ) ).Returns( mockShape3.Object );

            // Act
            _painter.DrawPicture( _mockDraft.Object, _mockCanvas.Object );

            // Assert
            _mockDraft.Verify( d => d.GetShape( 0 ), Times.Once );
            _mockDraft.Verify( d => d.GetShape( 1 ), Times.Once );
            _mockDraft.Verify( d => d.GetShape( 2 ), Times.Once );
        }
    }
}