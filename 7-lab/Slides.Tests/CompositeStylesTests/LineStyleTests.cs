using Moq;
using Slides.Styles.LineStyles;
using Slides.Types;

namespace Slides.Tests.CompositeStylesTests
{
    [TestFixture]
    public class CompositeLineStyleTests
    {
        [Test]
        public void GetColor_EmptyCollection_ReturnsNull()
        {
            // Arrange
            Func<List<ILineStyle>> getStyles = () => new List<ILineStyle>();
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

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
            List<ILineStyle> mockStyles = CreateMockStylesWithSameColor( expectedColor, 3 );
            Func<List<ILineStyle>> getStyles = () => mockStyles;
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act
            RGBAColor? result = composite.GetColor();

            // Assert
            Assert.That( result, Is.EqualTo( expectedColor ) );
        }

        [Test]
        public void GetColor_DifferentColors_ReturnsNull()
        {
            // Arrange
            List<ILineStyle> mockStyles = new List<ILineStyle>
            {
                CreateMockLineStyle(new RGBAColor(255, 0, 0, 255)),
                CreateMockLineStyle(new RGBAColor(0, 255, 0, 255)),
                CreateMockLineStyle(new RGBAColor(255, 0, 0, 255))
            };

            Func<List<ILineStyle>> getStyles = () => mockStyles;
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act
            RGBAColor? result = composite.GetColor();

            // Assert
            Assert.IsNull( result );
        }

        [Test]
        public void GetColor_SomeStylesReturnNull_ReturnsNull()
        {
            // Arrange
            List<ILineStyle> mockStyles = new List<ILineStyle>
            {
                CreateMockLineStyle(new RGBAColor(255, 0, 0, 255)),
                CreateMockLineStyle(null),
                CreateMockLineStyle(new RGBAColor(255, 0, 0, 255))
            };

            Func<List<ILineStyle>> getStyles = () => mockStyles;
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act
            RGBAColor? result = composite.GetColor();

            // Assert
            Assert.IsNull( result );
        }

        [Test]
        public void GetLineWidth_EmptyCollection_ReturnsZero()
        {
            // Arrange
            Func<List<ILineStyle>> getStyles = () => new List<ILineStyle>();
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act
            int result = composite.GetLineWidth();

            // Assert
            Assert.That( result, Is.EqualTo( 0 ) );
        }

        [Test]
        public void GetLineWidth_AllStylesSameWidth_ReturnsThatWidth()
        {
            // Arrange
            int expectedWidth = 5;
            List<ILineStyle> mockStyles = CreateMockStylesWithSameWidth( expectedWidth, 3 );
            Func<List<ILineStyle>> getStyles = () => mockStyles;
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act
            int result = composite.GetLineWidth();

            // Assert
            Assert.That( result, Is.EqualTo( expectedWidth ) );
        }

        [Test]
        public void GetLineWidth_DifferentWidths_ReturnsZero()
        {
            // Arrange
            List<ILineStyle> mockStyles = new List<ILineStyle>
            {
                CreateMockLineStyle(lineWidth: 2),
                CreateMockLineStyle(lineWidth: 5),
                CreateMockLineStyle(lineWidth: 2)
            };

            Func<List<ILineStyle>> getStyles = () => mockStyles;
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act
            int result = composite.GetLineWidth();

            // Assert
            Assert.That( result, Is.EqualTo( 0 ) );
        }

        [Test]
        public void IsEnabled_EmptyCollection_ReturnsNull()
        {
            // Arrange
            Func<List<ILineStyle>> getStyles = () => new List<ILineStyle>();
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act
            bool? result = composite.IsEnabled();

            // Assert
            Assert.IsNull( result );
        }

        [Test]
        public void IsEnabled_AllStylesEnabled_ReturnsTrue()
        {
            // Arrange
            List<ILineStyle> mockStyles = CreateMockStylesWithSameEnabled( true, 3 );
            Func<List<ILineStyle>> getStyles = () => mockStyles;
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act
            bool? result = composite.IsEnabled();

            // Assert
            Assert.IsTrue( result );
        }

        [Test]
        public void IsEnabled_AllStylesDisabled_ReturnsFalse()
        {
            // Arrange
            List<ILineStyle> mockStyles = CreateMockStylesWithSameEnabled( false, 3 );
            Func<List<ILineStyle>> getStyles = () => mockStyles;
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act
            bool? result = composite.IsEnabled();

            // Assert
            Assert.IsFalse( result );
        }

        [Test]
        public void IsEnabled_MixedEnabledStates_ReturnsNull()
        {
            // Arrange
            List<ILineStyle> mockStyles = new List<ILineStyle>
            {
                CreateMockLineStyle(enabled: true),
                CreateMockLineStyle(enabled: false),
                CreateMockLineStyle(enabled: true)
            };

            Func<List<ILineStyle>> getStyles = () => mockStyles;
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act
            bool? result = composite.IsEnabled();

            // Assert
            Assert.IsNull( result );
        }

        [Test]
        public void IsEnabled_SomeStylesReturnNull_ReturnsNull()
        {
            // Arrange
            List<ILineStyle> mockStyles = new List<ILineStyle>
            {
                CreateMockLineStyle(enabled: true),
                CreateMockLineStyle(enabled: null),
                CreateMockLineStyle(enabled: true)
            };

            Func<List<ILineStyle>> getStyles = () => mockStyles;
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

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
            List<Mock<ILineStyle>> mockStyles = new List<Mock<ILineStyle>>
            {
                new Mock<ILineStyle>(),
                new Mock<ILineStyle>(),
                new Mock<ILineStyle>()
            };

            List<ILineStyle> styles = mockStyles.Select( m => m.Object ).ToList();
            Func<List<ILineStyle>> getStyles = () => styles;
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act
            composite.SetColor( newColor );

            // Assert
            foreach ( Mock<ILineStyle> mockStyle in mockStyles )
            {
                mockStyle.Verify( m => m.SetColor( newColor ), Times.Once() );
            }
        }

        [Test]
        public void SetLineWidth_AppliesToAllStyles()
        {
            // Arrange
            int newWidth = 5;
            List<Mock<ILineStyle>> mockStyles = new List<Mock<ILineStyle>>
            {
                new Mock<ILineStyle>(),
                new Mock<ILineStyle>(),
                new Mock<ILineStyle>()
            };

            List<ILineStyle> styles = mockStyles.Select( m => m.Object ).ToList();
            Func<List<ILineStyle>> getStyles = () => styles;
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act
            composite.SetLineWidth( newWidth );

            // Assert
            foreach ( Mock<ILineStyle> mockStyle in mockStyles )
            {
                mockStyle.Verify( m => m.SetLineWidth( newWidth ), Times.Once() );
            }
        }

        [Test]
        public void SetColor_EmptyCollection_NoException()
        {
            // Arrange
            Func<List<ILineStyle>> getStyles = () => new List<ILineStyle>();
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );
            RGBAColor color = new RGBAColor( 255, 0, 0, 255 );

            // Act & Assert
            Assert.DoesNotThrow( () => composite.SetColor( color ) );
        }

        [Test]
        public void SetLineWidth_EmptyCollection_NoException()
        {
            // Arrange
            Func<List<ILineStyle>> getStyles = () => new List<ILineStyle>();
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act & Assert
            Assert.DoesNotThrow( () => composite.SetLineWidth( 5 ) );
        }

        [Test]
        public void SetLineWidth_WithNegativeValue_ThrowsOnIndividualStyles()
        {
            // Arrange
            Mock<ILineStyle> mockStyle1 = new Mock<ILineStyle>();
            Mock<ILineStyle> mockStyle2 = new Mock<ILineStyle>();
            mockStyle2.Setup( m => m.SetLineWidth( -1 ) ).Throws<ArgumentOutOfRangeException>();

            List<ILineStyle> styles = new List<ILineStyle> { mockStyle1.Object, mockStyle2.Object };
            Func<List<ILineStyle>> getStyles = () => styles;
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>( () => composite.SetLineWidth( -1 ) );
            mockStyle1.Verify( m => m.SetLineWidth( -1 ), Times.Once() );
        }

        [Test]
        public void GetColor_SingleStyle_ReturnsThatStyleColor()
        {
            // Arrange
            RGBAColor expectedColor = new RGBAColor( 128, 128, 128, 255 );
            ILineStyle mockStyle = CreateMockLineStyle( expectedColor, 2, true );
            Func<List<ILineStyle>> getStyles = () => new List<ILineStyle> { mockStyle };
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act
            RGBAColor? result = composite.GetColor();

            // Assert
            Assert.That( result, Is.EqualTo( expectedColor ) );
        }

        [Test]
        public void GetLineWidth_SingleStyle_ReturnsThatStyleWidth()
        {
            // Arrange
            int expectedWidth = 3;
            ILineStyle mockStyle = CreateMockLineStyle( lineWidth: expectedWidth );
            Func<List<ILineStyle>> getStyles = () => new List<ILineStyle> { mockStyle };
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act
            int result = composite.GetLineWidth();

            // Assert
            Assert.That( result, Is.EqualTo( expectedWidth ) );
        }

        [Test]
        public void IsEnabled_SingleStyle_ReturnsThatStyleEnabled()
        {
            // Arrange
            ILineStyle mockStyle = CreateMockLineStyle( enabled: false );
            Func<List<ILineStyle>> getStyles = () => new List<ILineStyle> { mockStyle };
            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act
            bool? result = composite.IsEnabled();

            // Assert
            Assert.IsFalse( result );
        }

        [Test]
        public void MultipleCalls_AlwaysGetsFreshStyles()
        {
            // Arrange
            int callCount = 0;
            List<ILineStyle> firstCallStyles = CreateMockStylesWithSameColor( new RGBAColor( 255, 0, 0, 255 ), 2 );
            List<ILineStyle> secondCallStyles = CreateMockStylesWithSameColor( new RGBAColor( 0, 255, 0, 255 ), 2 );

            Func<List<ILineStyle>> getStyles = () =>
            {
                callCount++;
                return callCount == 1 ? firstCallStyles : secondCallStyles;
            };

            CompositeLineStyle composite = new CompositeLineStyle( getStyles );

            // Act & Assert
            RGBAColor? firstResult = composite.GetColor();
            RGBAColor? secondResult = composite.GetColor();

            // Assert
            Assert.That( firstResult, Is.EqualTo( new RGBAColor( 255, 0, 0, 255 ) ) );
            Assert.That( secondResult, Is.EqualTo( new RGBAColor( 0, 255, 0, 255 ) ) );
        }

        private List<ILineStyle> CreateMockStylesWithSameColor( RGBAColor color, int count )
        {
            List<ILineStyle> styles = new List<ILineStyle>();
            for ( int i = 0; i < count; i++ )
            {
                styles.Add( CreateMockLineStyle( color ) );
            }
            return styles;
        }

        private List<ILineStyle> CreateMockStylesWithSameWidth( int width, int count )
        {
            List<ILineStyle> styles = new List<ILineStyle>();
            for ( int i = 0; i < count; i++ )
            {
                styles.Add( CreateMockLineStyle( lineWidth: width ) );
            }
            return styles;
        }

        private List<ILineStyle> CreateMockStylesWithSameEnabled( bool? enabled, int count )
        {
            List<ILineStyle> styles = new List<ILineStyle>();
            for ( int i = 0; i < count; i++ )
            {
                styles.Add( CreateMockLineStyle( enabled: enabled ) );
            }
            return styles;
        }

        private ILineStyle CreateMockLineStyle( RGBAColor? color = null, int lineWidth = 0, bool? enabled = null )
        {
            Mock<ILineStyle> mock = new Mock<ILineStyle>();
            mock.Setup( m => m.GetColor() ).Returns( color );
            mock.Setup( m => m.GetLineWidth() ).Returns( lineWidth );
            mock.Setup( m => m.IsEnabled() ).Returns( enabled );
            return mock.Object;
        }
    }
}