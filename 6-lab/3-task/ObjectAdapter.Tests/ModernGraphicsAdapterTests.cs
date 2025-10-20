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
        public void DrawLine_AfterMovingToNewPosition_ShouldUseCorrectStartPointAndDefaultColor()
        {
            // Arrange
            _adapter.MoveTo( 5, 15 );

            // Act
            _adapter.LineTo( 25, 35 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.Is<Point>( p => p.X == 5 && p.Y == 15 ),
                It.Is<Point>( p => p.X == 25 && p.Y == 35 ),
                It.Is<RGBAColor>( c => c.R == 0 && c.G == 0 && c.B == 0 && c.A == 1.0 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_WithSetColor_ShouldUseSpecifiedColor()
        {
            // Arrange
            _adapter.SetColor( 0xFF0000 );
            _adapter.MoveTo( 0, 0 );

            // Act
            _adapter.LineTo( 10, 10 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.IsAny<Point>(),
                It.IsAny<Point>(),
                It.Is<RGBAColor>( c => c.R == 1.0 && c.G == 0 && c.B == 0 && c.A == 1.0 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_WithGreenColor_ShouldConvertColorCorrectly()
        {
            // Arrange
            _adapter.SetColor( 0x00FF00 );
            _adapter.MoveTo( 1, 1 );

            // Act
            _adapter.LineTo( 2, 2 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.IsAny<Point>(),
                It.IsAny<Point>(),
                It.Is<RGBAColor>( c => c.R == 0 && c.G == 1.0 && c.B == 0 && c.A == 1.0 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_WithBlueColor_ShouldConvertColorCorrectly()
        {
            // Arrange
            _adapter.SetColor( 0x0000FF );
            _adapter.MoveTo( 3, 3 );

            // Act
            _adapter.LineTo( 4, 4 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.IsAny<Point>(),
                It.IsAny<Point>(),
                It.Is<RGBAColor>( c => c.R == 0 && c.G == 0 && c.B == 1.0 && c.A == 1.0 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_WithCustomColor_ShouldConvertColorCorrectly()
        {
            // Arrange
            _adapter.SetColor( 0x3A78F2 );
            _adapter.MoveTo( 5, 5 );

            // Act
            _adapter.LineTo( 6, 6 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.IsAny<Point>(),
                It.IsAny<Point>(),
                It.Is<RGBAColor>( c =>
                    Math.Abs( c.R - 0.227 ) < 0.01 &&
                    Math.Abs( c.G - 0.471 ) < 0.01 &&
                    Math.Abs( c.B - 0.949 ) < 0.01 &&
                    c.A == 1.0 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_MultipleCallsWithDifferentColors_ShouldUseCorrectColors()
        {
            // Arrange
            _adapter.MoveTo( 0, 0 );
            _adapter.SetColor( 0xFF0000 );
            _adapter.LineTo( 10, 10 );

            // Act
            _adapter.SetColor( 0x00FF00 );
            _adapter.LineTo( 20, 20 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.IsAny<Point>(),
                It.IsAny<Point>(),
                It.Is<RGBAColor>( c => c.R == 1.0 && c.G == 0 && c.B == 0 ) ),
                Times.Once );

            _mockRenderer.Verify( x => x.DrawLine(
                It.IsAny<Point>(),
                It.IsAny<Point>(),
                It.Is<RGBAColor>( c => c.R == 0 && c.G == 1.0 && c.B == 0 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_AfterColorChange_ShouldUseNewColor()
        {
            // Arrange
            _adapter.SetColor( 0xFF0000 );
            _adapter.MoveTo( 1, 1 );
            _adapter.LineTo( 2, 2 );

            // Act
            _adapter.SetColor( 0x0000FF );
            _adapter.LineTo( 3, 3 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.IsAny<Point>(),
                It.IsAny<Point>(),
                It.Is<RGBAColor>( c => c.B == 1.0 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_WithoutSetColor_ShouldUseDefaultBlackColor()
        {
            // Arrange
            _adapter.MoveTo( 0, 0 );

            // Act
            _adapter.LineTo( 1, 1 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.IsAny<Point>(),
                It.IsAny<Point>(),
                It.Is<RGBAColor>( c => c.R == 0 && c.G == 0 && c.B == 0 && c.A == 1.0 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_WithWhiteColor_ShouldConvertCorrectly()
        {
            // Arrange
            _adapter.SetColor( 0xFFFFFF );
            _adapter.MoveTo( 7, 7 );

            // Act
            _adapter.LineTo( 8, 8 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.IsAny<Point>(),
                It.IsAny<Point>(),
                It.Is<RGBAColor>( c => c.R == 1.0 && c.G == 1.0 && c.B == 1.0 && c.A == 1.0 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_WithGrayColor_ShouldConvertCorrectly()
        {
            // Arrange
            _adapter.SetColor( 0x808080 );
            _adapter.MoveTo( 9, 9 );

            // Act
            _adapter.LineTo( 10, 10 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.IsAny<Point>(),
                It.IsAny<Point>(),
                It.Is<RGBAColor>( c =>
                    Math.Abs( c.R - 0.5 ) < 0.01 &&
                    Math.Abs( c.G - 0.5 ) < 0.01 &&
                    Math.Abs( c.B - 0.5 ) < 0.01 &&
                    c.A == 1.0 ) ),
                Times.Once );
        }

        [Test]
        public void DrawLine_SequenceWithColorChanges_ShouldMaintainProperState()
        {
            // Arrange
            _adapter.MoveTo( 1, 1 );
            _adapter.SetColor( 0xFF0000 );
            _adapter.LineTo( 2, 2 );

            // Act
            _adapter.SetColor( 0x00FF00 );
            _adapter.LineTo( 4, 4 );
            _adapter.SetColor( 0x0000FF );
            _adapter.LineTo( 8, 8 );

            // Assert
            _mockRenderer.Verify( x => x.DrawLine(
                It.IsAny<Point>(),
                It.IsAny<Point>(),
                It.Is<RGBAColor>( c => c.R == 1.0 ) ),
                Times.Once );

            _mockRenderer.Verify( x => x.DrawLine(
                It.IsAny<Point>(),
                It.IsAny<Point>(),
                It.Is<RGBAColor>( c => c.G == 1.0 ) ),
                Times.Once );

            _mockRenderer.Verify( x => x.DrawLine(
                It.IsAny<Point>(),
                It.IsAny<Point>(),
                It.Is<RGBAColor>( c => c.B == 1.0 ) ),
                Times.Once );
        }
    }
}