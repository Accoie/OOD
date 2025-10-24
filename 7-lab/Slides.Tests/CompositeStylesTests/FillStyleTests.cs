using Moq;
using Slides.Styles.FillStyles;
using Slides.Types;

namespace Slides.Tests.CompositeStylesTests
{
    [TestFixture]
    public class CompositeFillStyleTests
    {
        [Test]
        public void GetColor_EmptyCollection_ReturnsNull()
        {
            // Arrange
            Func<List<IFillStyle>> getStyles = () => new List<IFillStyle>();
            CompositeFillStyle composite = new CompositeFillStyle( getStyles );

            // Act
            RGBAColor? result = composite.GetColor();

            // Assert
            Assert.IsNull( result );
        }

        [Test]
        public void GetColor_AllStylesSameColor_ReturnsThatColor()
        {
            // Arrange
            RGBAColor expectedColor = new RGBAColor( 255, 0, 0, 255 );
            List<IFillStyle> mockStyles = CreateMockStylesWithSameColor( expectedColor, 3 );
            Func<List<IFillStyle>> getStyles = () => mockStyles;
            CompositeFillStyle composite = new CompositeFillStyle( getStyles );

            // Act
            RGBAColor? result = composite.GetColor();

            // Assert
            Assert.That( result, Is.EqualTo( expectedColor ) );
        }

        [Test]
        public void GetColor_DifferentColors_ReturnsNull()
        {
            // Arrange
            List<IFillStyle> mockStyles = new List<IFillStyle>
            {
                CreateMockFillStyle(new RGBAColor(255, 0, 0, 255)),
                CreateMockFillStyle(new RGBAColor(0, 255, 0, 255)),
                CreateMockFillStyle(new RGBAColor(255, 0, 0, 255)) // Один отличается
            };

            Func<List<IFillStyle>> getStyles = () => mockStyles;
            CompositeFillStyle composite = new CompositeFillStyle( getStyles );

            // Act
            RGBAColor? result = composite.GetColor();

            // Assert
            Assert.IsNull( result );
        }

        [Test]
        public void GetColor_SomeStylesReturnNull_ReturnsNull()
        {
            // Arrange
            List<IFillStyle> mockStyles = new List<IFillStyle>
            {
                CreateMockFillStyle(new RGBAColor(255, 0, 0, 255)),
                CreateMockFillStyle(null),
                CreateMockFillStyle(new RGBAColor(255, 0, 0, 255))
            };

            Func<List<IFillStyle>> getStyles = () => mockStyles;
            CompositeFillStyle composite = new CompositeFillStyle( getStyles );

            // Act
            RGBAColor? result = composite.GetColor();

            // Assert
            Assert.IsNull( result );
        }

        [Test]
        public void IsEnabled_EmptyCollection_ReturnsNull()
        {
            // Arrange
            Func<List<IFillStyle>> getStyles = () => new List<IFillStyle>();
            CompositeFillStyle composite = new CompositeFillStyle( getStyles );

            // Act
            bool? result = composite.IsEnabled();

            // Assert
            Assert.IsNull( result );
        }

        [Test]
        public void IsEnabled_AllStylesEnabled_ReturnsTrue()
        {
            // Arrange
            List<IFillStyle> mockStyles = CreateMockStylesWithSameEnabled( true, 3 );
            Func<List<IFillStyle>> getStyles = () => mockStyles;
            CompositeFillStyle composite = new CompositeFillStyle( getStyles );

            // Act
            bool? result = composite.IsEnabled();

            // Assert
            Assert.IsTrue( result );
        }

        [Test]
        public void IsEnabled_AllStylesDisabled_ReturnsFalse()
        {
            // Arrange
            List<IFillStyle> mockStyles = CreateMockStylesWithSameEnabled( false, 3 );
            Func<List<IFillStyle>> getStyles = () => mockStyles;
            CompositeFillStyle composite = new CompositeFillStyle( getStyles );

            // Act
            bool? result = composite.IsEnabled();

            // Assert
            Assert.IsFalse( result );
        }

        [Test]
        public void IsEnabled_MixedEnabledStates_ReturnsNull()
        {
            // Arrange
            List<IFillStyle> mockStyles = new List<IFillStyle>
            {
                CreateMockFillStyle(enabled: true),
                CreateMockFillStyle(enabled: false),
                CreateMockFillStyle(enabled: true)
            };

            Func<List<IFillStyle>> getStyles = () => mockStyles;
            CompositeFillStyle composite = new CompositeFillStyle( getStyles );

            // Act
            bool? result = composite.IsEnabled();

            // Assert
            Assert.IsNull( result );
        }

        [Test]
        public void IsEnabled_SomeStylesReturnNull_ReturnsNull()
        {
            // Arrange
            List<IFillStyle> mockStyles = new List<IFillStyle>
            {
                CreateMockFillStyle(enabled: true),
                CreateMockFillStyle(enabled: null),
                CreateMockFillStyle(enabled: true)
            };

            Func<List<IFillStyle>> getStyles = () => mockStyles;
            CompositeFillStyle composite = new CompositeFillStyle( getStyles );

            // Act
            bool? result = composite.IsEnabled();

            // Assert
            Assert.IsNull( result );
        }

        [Test]
        public void SetColor_AppliesToAllStyles()
        {
            // Arrange
            RGBAColor newColor = new RGBAColor( 0, 255, 0, 128 );
            List<Mock<IFillStyle>> mockStyles = new List<Mock<IFillStyle>>
            {
                new Mock<IFillStyle>(),
                new Mock<IFillStyle>(),
                new Mock<IFillStyle>()
            };

            List<IFillStyle> styles = mockStyles.Select( m => m.Object ).ToList();
            Func<List<IFillStyle>> getStyles = () => styles;
            CompositeFillStyle composite = new CompositeFillStyle( getStyles );

            // Act
            composite.SetColor( newColor );

            // Assert
            foreach ( Mock<IFillStyle> mockStyle in mockStyles )
            {
                mockStyle.Verify( m => m.SetColor( newColor ), Times.Once() );
            }
        }

        [Test]
        public void SetColor_EmptyCollection_NoException()
        {
            // Arrange
            Func<List<IFillStyle>> getStyles = () => new List<IFillStyle>();
            CompositeFillStyle composite = new CompositeFillStyle( getStyles );
            RGBAColor color = new RGBAColor( 255, 0, 0, 255 );

            // Act & Assert (не должно быть исключения)
            Assert.DoesNotThrow( () => composite.SetColor( color ) );
        }

        [Test]
        public void GetColor_SingleStyle_ReturnsThatStyleColor()
        {
            // Arrange
            RGBAColor expectedColor = new RGBAColor( 128, 128, 128, 255 );
            IFillStyle mockStyle = CreateMockFillStyle( expectedColor, true );
            Func<List<IFillStyle>> getStyles = () => new List<IFillStyle> { mockStyle };
            CompositeFillStyle composite = new CompositeFillStyle( getStyles );

            // Act
            RGBAColor? result = composite.GetColor();

            // Assert
            Assert.That( result, Is.EqualTo( expectedColor ) );
        }

        [Test]
        public void IsEnabled_SingleStyle_ReturnsThatStyleEnabled()
        {
            // Arrange
            IFillStyle mockStyle = CreateMockFillStyle( enabled: false );
            Func<List<IFillStyle>> getStyles = () => new List<IFillStyle> { mockStyle };
            CompositeFillStyle composite = new CompositeFillStyle( getStyles );

            // Act
            bool? result = composite.IsEnabled();

            // Assert
            Assert.IsFalse( result );
        }

        [Test]
        public void GetColor_AllNullColors_ReturnsNull()
        {
            // Arrange
            List<IFillStyle> mockStyles = new List<IFillStyle>
            {
                CreateMockFillStyle(null),
                CreateMockFillStyle(null),
                CreateMockFillStyle(null)
            };

            Func<List<IFillStyle>> getStyles = () => mockStyles;
            CompositeFillStyle composite = new CompositeFillStyle( getStyles );

            // Act
            RGBAColor? result = composite.GetColor();

            // Assert
            Assert.IsNull( result );
        }

        [Test]
        public void SetColor_MultipleCalls_AlwaysGetsFreshStyles()
        {
            // Arrange
            int callCount = 0;
            List<IFillStyle> firstCallStyles = CreateMockStylesWithSameColor( new RGBAColor( 255, 0, 0, 255 ), 2 );
            List<IFillStyle> secondCallStyles = CreateMockStylesWithSameColor( new RGBAColor( 0, 255, 0, 255 ), 2 );

            Func<List<IFillStyle>> getStyles = () =>
            {
                callCount++;
                return callCount == 1 ? firstCallStyles : secondCallStyles;
            };

            CompositeFillStyle composite = new CompositeFillStyle( getStyles );

            // Act & Assert
            RGBAColor? firstResult = composite.GetColor();
            RGBAColor? secondResult = composite.GetColor();

            // Assert
            Assert.That( firstResult, Is.EqualTo( new RGBAColor( 255, 0, 0, 255 ) ) );
            Assert.That( secondResult, Is.EqualTo( new RGBAColor( 0, 255, 0, 255 ) ) );
        }

        private List<IFillStyle> CreateMockStylesWithSameColor( RGBAColor color, int count )
        {
            List<IFillStyle> styles = new List<IFillStyle>();
            for ( int i = 0; i < count; i++ )
            {
                styles.Add( CreateMockFillStyle( color ) );
            }
            return styles;
        }

        private List<IFillStyle> CreateMockStylesWithSameEnabled( bool? enabled, int count )
        {
            List<IFillStyle> styles = new List<IFillStyle>();
            for ( int i = 0; i < count; i++ )
            {
                styles.Add( CreateMockFillStyle( enabled: enabled ) );
            }
            return styles;
        }

        private IFillStyle CreateMockFillStyle( RGBAColor? color = null, bool? enabled = null )
        {
            Mock<IFillStyle> mock = new Mock<IFillStyle>();
            mock.Setup( m => m.GetColor() ).Returns( color );
            mock.Setup( m => m.IsEnabled() ).Returns( enabled );
            return mock.Object;
        }
    }
}