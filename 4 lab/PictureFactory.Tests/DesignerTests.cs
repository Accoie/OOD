using System.Text;
using Moq;
using PictureFactory.Designers;
using PictureFactory.Factories;
using PictureFactory.PictureDrafts;

namespace PictureFactory.Tests
{
    public class DesignerTests
    {
        private Mock<IShapeFactory> _mockShapeFactory;
        private Designer _designer;

        [SetUp]
        public void SetUp()
        {
            _mockShapeFactory = new Mock<IShapeFactory>();
            _designer = new Designer( _mockShapeFactory.Object );
        }

        [Test]
        public void CreateDraft_WhenExitCommandImmediately_ReturnsEmptyDraft()
        {
            // Arrange
            Stream stream = CreateStreamWithLines( "exit" );

            // Act
            PictureDraft result = _designer.CreateDraft( stream );

            // Assert
            Assert.That( result.GetShapesSize(), Is.EqualTo( 0 ) );
        }

        [Test]
        public void CreateDraft_StreamIsLeftOpenAfterProcessing()
        {
            // Arrange
            Stream stream = CreateStreamWithLines( "exit" );
            stream.Position = 0;
            using StreamReader reader = new StreamReader( stream, Encoding.UTF8, leaveOpen: true );
            // Act
            _designer.CreateDraft( stream );

            // Assert 
            Assert.DoesNotThrow( () => { string content = reader.ReadToEnd(); } );
        }

        private Stream CreateStreamWithLines( params string[] lines )
        {
            string text = string.Join( Environment.NewLine, lines );
            byte[] bytes = Encoding.UTF8.GetBytes( text );
            return new MemoryStream( bytes );
        }
    }
}

