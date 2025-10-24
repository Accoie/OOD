using Moq;
using Slides.Canvas;
using Slides.Group;
using Slides.Shapes;
using Slides.Styles.FillStyles;
using Slides.Styles.LineStyles;
using Slides.Types;

namespace Slides.Tests
{
    public class ShapeGroupTests
    {
        private ShapeGroup _shapeGroup;
        private Mock<IShape> _shapeMock1;
        private Mock<IShape> _shapeMock2;
        private Mock<IShapeGroup> _shapeGroupMock;

        [SetUp]
        public void Setup()
        {
            _shapeMock1 = new Mock<IShape>();
            _shapeMock2 = new Mock<IShape>();
            _shapeGroupMock = new Mock<IShapeGroup>();
            _shapeMock1.Setup( s => s.GetFrame() ).Returns( new Frame( 0, 0, 10, 10 ) );
            _shapeMock2.Setup( s => s.GetFrame() ).Returns( new Frame( 10, 10, 10, 10 ) );

            _shapeGroup = new ShapeGroup( new List<IShape> { _shapeMock1.Object, _shapeMock2.Object } );
        }

        [Test]
        public void Draw_WithCanvas_WillDrawAllShapes()
        {
            // Arrange
            Mock<ICanvas> canvasMock = new Mock<ICanvas>();

            // Act
            _shapeGroup.Draw( canvasMock.Object );

            // Assert
            _shapeMock1.Verify( s => s.Draw( canvasMock.Object ), Times.Once );
            _shapeMock2.Verify( s => s.Draw( canvasMock.Object ), Times.Once );
        }

        [Test]
        public void GetShape_WithValidIndex_ReturnsExpectedShape()
        {
            // Arrange & Act & Assert
            Assert.That( _shapeGroup.GetShape( 0 ), Is.EqualTo( _shapeMock1.Object ) );
            Assert.That( _shapeGroup.GetShape( 1 ), Is.EqualTo( _shapeMock2.Object ) );
        }

        [Test]
        public void GetShape_WithInvalidIndex_ThrowsException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>( () => _shapeGroup.GetShape( -1 ) );
            Assert.Throws<ArgumentOutOfRangeException>( () => _shapeGroup.GetShape( 2 ) );
        }

        [Test]
        public void GetShapesCount_ReturnsCorrectCount()
        {
            // Arrange & Act & Assert
            Assert.That( _shapeGroup.GetShapesCount(), Is.EqualTo( 2 ) );
        }

        [Test]
        public void InsertShape_AddsShapeAndUpdatesFrame()
        {
            // Arrange
            Mock<IShape> newShape = new Mock<IShape>();
            newShape.Setup( s => s.GetFrame() ).Returns( new Frame( 20, 20, 10, 10 ) );

            // Act
            _shapeGroup.InsertShape( newShape.Object, 1 );

            // Assert
            Assert.That( _shapeGroup.GetShapesCount(), Is.EqualTo( 3 ) );
            Assert.That( _shapeGroup.GetShape( 1 ), Is.EqualTo( newShape.Object ) );
        }

        [Test]
        public void RemoveShape_RemovesShapeAndUpdatesFrame()
        {
            // Arrange & Act
            _shapeGroup.RemoveShape( 0 );

            // Assert
            Assert.That( _shapeGroup.GetShapesCount(), Is.EqualTo( 1 ) );
            Assert.That( _shapeGroup.GetShape( 0 ), Is.EqualTo( _shapeMock2.Object ) );
        }

        [Test]
        public void SetLineStyle_PropagatesToAllShapes()
        {
            // Arrange
            Mock<ILineStyle> lineStyle = new Mock<ILineStyle>();

            // Act
            _shapeGroup.SetLineStyle( lineStyle.Object );

            // Assert
            _shapeMock1.Verify( s => s.SetLineStyle( lineStyle.Object ), Times.Once );
            _shapeMock2.Verify( s => s.SetLineStyle( lineStyle.Object ), Times.Once );
        }

        [Test]
        public void SetFillStyle_PropagatesToAllShapes()
        {
            // Arrange
            Mock<IFillStyle> fillStyle = new Mock<IFillStyle>();

            // Act
            _shapeGroup.SetFillStyle( fillStyle.Object );

            // Assert
            _shapeMock1.Verify( s => s.SetFillStyle( fillStyle.Object ), Times.Once );
            _shapeMock2.Verify( s => s.SetFillStyle( fillStyle.Object ), Times.Once );
        }

        [Test]
        public void SetParent_WithValidParent_SetsSuccessfully()
        {
            // Arrange
            ShapeGroup parentGroup = new ShapeGroup( new List<IShape>() );

            // Act
            _shapeGroup.SetParent( parentGroup );

            // Assert
            Assert.That( _shapeGroup.GetParent(), Is.EqualTo( parentGroup ) );
        }

        [Test]
        public void SetParent_WhenParentAlreadyExists_Throws()
        {
            // Arrange
            ShapeGroup parentGroup1 = new ShapeGroup( [ _shapeGroup ] );
            ShapeGroup parentGroup2 = new ShapeGroup( [ parentGroup1 ] );

            // Act & Assert
            Assert.Throws<ArgumentException>( () => _shapeGroup.SetParent( parentGroup2 ) );
        }

        [Test]
        public void SetParent_WhenParentInParent_Throws()
        {
            // Arrange
            ShapeGroup parentGroup1 = new ShapeGroup( [ _shapeGroup ] );
            ShapeGroup parentGroup2 = new ShapeGroup( [ parentGroup1 ] );
            ShapeGroup parentGroup3 = new ShapeGroup( [ parentGroup2 ] );
            ShapeGroup parentGroup4 = new ShapeGroup( [ parentGroup3 ] );

            // Act & Assert
            Assert.Throws<ArgumentException>( () => _shapeGroup.SetParent( parentGroup4 ) );
        }

        [Test]
        public void CheckExistParent_ReturnsFalseIfNotExists()
        {
            // Arrange
            ShapeGroup parentGroup = new ShapeGroup( new List<IShape>() );

            // Act & Assert
            Assert.That( _shapeGroup.CheckExistParent( parentGroup ), Is.False );
        }

        [Test]
        public void SetFrame_RescalesShapesCorrectly()
        {
            // Arrange
            Frame initialFrame = new Frame( 0, 0, 20, 20 );
            _shapeGroup.SetFrame( initialFrame );
            Frame newFrame = new Frame( 0, 0, 10, 10 );

            _shapeMock1.Setup( s => s.GetFrame() ).Returns( new Frame( 0, 0, 10, 10 ) );
            _shapeMock2.Setup( s => s.GetFrame() ).Returns( new Frame( 10, 10, 10, 10 ) );

            // Act
            _shapeGroup.SetFrame( newFrame );

            // Assert
            _shapeMock1.Verify( s => s.SetFrame( It.IsAny<Frame>() ), Times.Exactly( 2 ) );
            _shapeMock2.Verify( s => s.SetFrame( It.IsAny<Frame>() ), Times.Exactly( 2 ) );
        }

        [Test]
        public void InsertShape_WithShapeGroup_SetParentWillCalled()
        {
            // Arrange

            // Act
            _shapeGroup.InsertShape( _shapeGroupMock.Object, 1 );

            // Assert
            _shapeGroupMock.Verify( s => s.SetParent( _shapeGroup ), Times.Once() );
        }

        [Test]
        public void InsertShape_WithIndexOutOfRange_WillThrow()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>( () => _shapeGroup.InsertShape( _shapeGroupMock.Object, 3 ) );
            Assert.Throws<ArgumentOutOfRangeException>( () => _shapeGroup.InsertShape( _shapeGroupMock.Object, -1 ) );
        }

        [Test]
        public void InsertShape_WithIndexEqualCount_WillNotThrow()
        {
            // Arrange & Act & Assert
            Assert.DoesNotThrow( () => _shapeGroup.InsertShape( _shapeGroupMock.Object, 2 ) );
        }

        [Test]
        public void Constructor_WithShapes_WillSetParentToGroups()
        {
            // Arrange & Act
            ShapeGroup _shapeGroup1 = new ShapeGroup( new List<IShape> { _shapeMock1.Object, _shapeGroupMock.Object } );

            // Assert
            _shapeGroupMock.Verify( s => s.SetParent( _shapeGroup1 ), Times.Once );
        }
    }
}