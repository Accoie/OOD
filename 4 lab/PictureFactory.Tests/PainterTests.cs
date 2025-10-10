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
        private PictureDraft _pictureDraft;
        private Painter _painter;

        [SetUp]
        public void SetUp()
        {
            _mockCanvas = new Mock<ICanvas>();
            _pictureDraft = new PictureDraft();
            _painter = new Painter();
        }

        [Test]
        public void DrawPicture_WhenDraftIsEmpty_ShouldNotDrawAnyShapes()
        {
            // Arrange

            // Act
            _painter.DrawPicture( _pictureDraft, _mockCanvas.Object );

            // Assert
            _mockCanvas.VerifyNoOtherCalls();
        }

        [Test]
        public void DrawPicture_WhenDraftHasShapes_ShouldDrawAllShapes()
        {
            // Arrange
            var mockShape1 = new Mock<Shape>( Color.Red );
            var mockShape2 = new Mock<Shape>( Color.Red );

            mockShape1.Setup( s => s.GetShapeType() ).Returns( ShapeType.Ellipse );
            mockShape2.Setup( s => s.GetShapeType() ).Returns( ShapeType.Rectangle );

            _pictureDraft.AddShape( mockShape1.Object );
            _pictureDraft.AddShape( mockShape2.Object );

            // Act
            _painter.DrawPicture( _pictureDraft, _mockCanvas.Object );

            // Assert
            mockShape1.Verify( s => s.Draw( _mockCanvas.Object ), Times.Once );
            mockShape2.Verify( s => s.Draw( _mockCanvas.Object ), Times.Once );
        }

        [Test]
        public void DrawPicture_WhenDraftHasShapes_ShouldDrawAllShapesInCorrectOrder()
        {
            // Arrange
            var drawnShapes = new List<Shape>();
            var mockShape1 = new Mock<Shape>( Color.Red );
            var mockShape2 = new Mock<Shape>( Color.Red );
            var mockShape3 = new Mock<Shape>( Color.Red );

            mockShape1.Setup( s => s.Draw( _mockCanvas.Object ) )
                     .Callback( () => drawnShapes.Add( mockShape1.Object ) );
            mockShape2.Setup( s => s.Draw( _mockCanvas.Object ) )
                     .Callback( () => drawnShapes.Add( mockShape2.Object ) );
            mockShape3.Setup( s => s.Draw( _mockCanvas.Object ) )
                     .Callback( () => drawnShapes.Add( mockShape3.Object ) );

            _pictureDraft.AddShape( mockShape1.Object );
            _pictureDraft.AddShape( mockShape2.Object );
            _pictureDraft.AddShape( mockShape3.Object );

            // Act
            _painter.DrawPicture( _pictureDraft, _mockCanvas.Object );

            // Assert
            Assert.That( drawnShapes.Count, Is.EqualTo( 3 ) );
            Assert.That( drawnShapes[ 0 ], Is.EqualTo( mockShape1.Object ) );
            Assert.That( drawnShapes[ 1 ], Is.EqualTo( mockShape2.Object ) );
            Assert.That( drawnShapes[ 2 ], Is.EqualTo( mockShape3.Object ) );
        }

        [Test]
        public void DrawPicture_WithMultipleShapes_ShouldCallDrawForEachShape()
        {
            // Arrange
            var mockShape1 = new Mock<Shape>( Color.Red );
            var mockShape2 = new Mock<Shape>( Color.Blue );
            var mockShape3 = new Mock<Shape>( Color.Green );

            _pictureDraft.AddShape( mockShape1.Object );
            _pictureDraft.AddShape( mockShape2.Object );
            _pictureDraft.AddShape( mockShape3.Object );

            // Act
            _painter.DrawPicture( _pictureDraft, _mockCanvas.Object );

            // Assert
            mockShape1.Verify( s => s.Draw( _mockCanvas.Object ), Times.Once );
            mockShape2.Verify( s => s.Draw( _mockCanvas.Object ), Times.Once );
            mockShape3.Verify( s => s.Draw( _mockCanvas.Object ), Times.Once );
        }

        [Test]
        public void DrawPicture_WithSingleShape_ShouldCallDrawOnce()
        {
            // Arrange
            var mockShape = new Mock<Shape>( Color.Red );
            _pictureDraft.AddShape( mockShape.Object );

            // Act
            _painter.DrawPicture( _pictureDraft, _mockCanvas.Object );

            // Assert
            mockShape.Verify( s => s.Draw( _mockCanvas.Object ), Times.Once );
        }
    }
}