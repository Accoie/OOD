using Moq;
using ObjectAdapter.Application;
using ObjectAdapter.ModernGraphicsLib;

namespace ObjectAdapter.Tests
{
    [TestFixture]
    public class ModernGraphicsAdapterTests
    {
        private Mock<IModernGraphicsRenderer> _mockRenderer;
        private ModernGraphicsRendererAdapter _adapter;

        [SetUp]
        public void Initialize()
        {
            _mockRenderer = new Mock<IModernGraphicsRenderer>();
            _adapter = new ModernGraphicsRendererAdapter( _mockRenderer.Object );
        }

        [Test]
        public void DrawLine_AfterMovingToNewPosition_ShouldUseCorrectStartPoint()
        {
            // Arrange
            _adapter.MoveTo( 5, 15 );

            // Act
            _adapter.LineTo( 25, 35 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.Is<Point>( p => p.X == 5 && p.Y == 15 ),
                It.Is<Point>( p => p.X == 25 && p.Y == 35 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_WithNegativeCoordinates_ShouldBeProcessedCorrectly()
        {
            // Arrange
            _adapter.MoveTo( -5, -10 );

            // Act
            _adapter.LineTo( -15, -25 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.Is<Point>( p => p.X == -5 && p.Y == -10 ),
                It.Is<Point>( p => p.X == -15 && p.Y == -25 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_FromOrigin_ShouldWorkProperly()
        {
            // Arrange
            _adapter.MoveTo( 0, 0 );

            // Act
            _adapter.LineTo( 100, 150 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.Is<Point>( p => p.X == 0 && p.Y == 0 ),
                It.Is<Point>( p => p.X == 100 && p.Y == 150 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_ToSpecifiedEndPoint_ShouldPassCorrectEndPoint()
        {
            // Arrange
            _adapter.MoveTo( 2, 3 );

            // Act
            _adapter.LineTo( 50, 75 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.IsAny<Point>(),
                It.Is<Point>( p => p.X == 50 && p.Y == 75 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_MultipleCalls_ShouldUpdateCurrentPosition()
        {
            // Arrange
            _adapter.MoveTo( 1, 2 );
            _adapter.LineTo( 10, 20 );

            // Act
            _adapter.LineTo( 30, 40 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.Is<Point>( p => p.X == 10 && p.Y == 20 ),
                It.Is<Point>( p => p.X == 30 && p.Y == 40 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_ToNegativeEndPoint_ShouldHandleNegativeValues()
        {
            // Arrange
            _adapter.MoveTo( 8, 12 );

            // Act
            _adapter.LineTo( -20, -30 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.IsAny<Point>(),
                It.Is<Point>( p => p.X == -20 && p.Y == -30 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_BackToOrigin_ShouldAcceptZeroCoordinates()
        {
            // Arrange
            _adapter.MoveTo( 7, 9 );

            // Act
            _adapter.LineTo( 0, 0 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.IsAny<Point>(),
                It.Is<Point>( p => p.X == 0 && p.Y == 0 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_ShouldInvokeBeginAndEndDrawMethods()
        {
            // Arrange
            _adapter.MoveTo( 3, 4 );

            // Act
            _adapter.LineTo( 6, 8 );

            // Assert
            _mockRenderer.Verify( x => x.BeginDraw(), Times.Once );
            _mockRenderer.Verify( x => x.EndDraw(), Times.Once );
        }

        [Test]
        public void DrawLine_SequenceOfOperations_ShouldMaintainProperState()
        {
            // Arrange
            _adapter.MoveTo( 1, 1 );
            _adapter.LineTo( 2, 2 );

            // Act
            _adapter.LineTo( 4, 4 );
            _adapter.LineTo( 8, 8 );
            _adapter.LineTo( 16, 16 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.Is<Point>( p => p.X == 1 && p.Y == 1 ),
                It.Is<Point>( p => p.X == 2 && p.Y == 2 ) ),
                Times.Once );

            _mockRenderer.Verify( x => x.DrawLine(
                It.Is<Point>( p => p.X == 2 && p.Y == 2 ),
                It.Is<Point>( p => p.X == 4 && p.Y == 4 ) ),
                Times.Once );

            _mockRenderer.Verify( x => x.DrawLine(
                It.Is<Point>( p => p.X == 4 && p.Y == 4 ),
                It.Is<Point>( p => p.X == 8 && p.Y == 8 ) ),
                Times.Once );

            _mockRenderer.Verify( x => x.DrawLine(
                It.Is<Point>( p => p.X == 8 && p.Y == 8 ),
                It.Is<Point>( p => p.X == 16 && p.Y == 16 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_WithLargeCoordinates_ShouldProcessCorrectly()
        {
            // Arrange
            _adapter.MoveTo( 1000, 2000 );

            // Act
            _adapter.LineTo( 3000, 4000 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.Is<Point>( p => p.X == 1000 && p.Y == 2000 ),
                It.Is<Point>( p => p.X == 3000 && p.Y == 4000 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_AfterMultipleMoveToCalls_ShouldUseLastPosition()
        {
            // Arrange
            _adapter.MoveTo( 1, 1 );
            _adapter.MoveTo( 2, 2 );
            _adapter.MoveTo( 3, 3 );

            // Act
            _adapter.LineTo( 4, 4 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.Is<Point>( p => p.X == 3 && p.Y == 3 ),
                It.Is<Point>( p => p.X == 4 && p.Y == 4 ) ),
                Times.Once );
        }
    }
}