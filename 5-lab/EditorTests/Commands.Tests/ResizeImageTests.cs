using Editor.Commands;
using Editor.Documents;
using Editor.Documents.Items;
using Editor.Image;
using Moq;

namespace EditorTests.Commands.Tests;

[TestFixture]
public class ResizeImageTests
{
    private Mock<IDocument> _documentMock;
    private Mock<IImage> _imageMock;

    [SetUp]
    public void Setup()
    {
        _documentMock = new Mock<IDocument>();
        _imageMock = new Mock<IImage>();
    }

    [Test]
    public void Execute_ValidPositionAndImage_ResizesImage()
    {
        // Arrange
        const int position = 1;
        const int newWidth = 200;
        const int newHeight = 300;
        const int previousWidth = 100;
        const int previousHeight = 150;

        var documentItem = new DocumentItem( _imageMock.Object );
        _imageMock.Setup( i => i.Width ).Returns( previousWidth );
        _imageMock.Setup( i => i.Height ).Returns( previousHeight );

        _documentMock.Setup( d => d.GetItemsCount() ).Returns( 2 );
        _documentMock.Setup( d => d.GetItem( position ) ).Returns( documentItem );

        ResizeImageCommand command = new ResizeImageCommand( _documentMock.Object, newWidth, newHeight, position );

        // Act
        command.Execute();

        // Assert
        _imageMock.Verify( i => i.Resize( newWidth, newHeight ), Times.Once );
    }

    [Test]
    public void Execute_PositionOutOfRange_ThrowsArgumentException()
    {
        // Arrange
        const int position = 5;
        const int width = 200;
        const int height = 300;

        _documentMock.Setup( d => d.GetItemsCount() ).Returns( 2 );

        ResizeImageCommand command = new ResizeImageCommand( _documentMock.Object, width, height, position );

        // Act & Assert
        Assert.Throws<ArgumentException>( () => command.Execute() );
    }

    [Test]
    public void Execute_ItemIsNotImage_ThrowsInvalidOperationException()
    {
        // Arrange
        const int position = 1;
        const int width = 200;
        const int height = 300;

        var paragraphMock = new Mock<Editor.Paragraph.IParagraph>();
        var documentItem = new DocumentItem( paragraphMock.Object );

        _documentMock.Setup( d => d.GetItemsCount() ).Returns( 2 );
        _documentMock.Setup( d => d.GetItem( position ) ).Returns( documentItem );

        ResizeImageCommand command = new ResizeImageCommand( _documentMock.Object, width, height, position );

        // Act & Assert
        Assert.Throws<InvalidOperationException>( () => command.Execute() );
    }

    [Test]
    public void Unexecute_AfterExecute_RestoresPreviousSize()
    {
        // Arrange
        const int position = 1;
        const int newWidth = 200;
        const int newHeight = 300;
        const int previousWidth = 100;
        const int previousHeight = 150;

        var documentItem = new DocumentItem( _imageMock.Object );
        _imageMock.Setup( i => i.Width ).Returns( previousWidth );
        _imageMock.Setup( i => i.Height ).Returns( previousHeight );

        _documentMock.Setup( d => d.GetItemsCount() ).Returns( 2 );
        _documentMock.Setup( d => d.GetItem( position ) ).Returns( documentItem );

        ResizeImageCommand command = new ResizeImageCommand( _documentMock.Object, newWidth, newHeight, position );
        command.Execute();

        // Act
        command.Unexecute();

        // Assert
        _imageMock.Verify( i => i.Resize( previousWidth, previousHeight ), Times.Once );
    }

    [Test]
    public void Execute_Unexecute_Sequence_WorksCorrectly()
    {
        // Arrange
        const int position = 1;
        const int newWidth = 200;
        const int newHeight = 300;
        const int previousWidth = 100;
        const int previousHeight = 150;

        var documentItem = new DocumentItem( _imageMock.Object );
        _imageMock.Setup( i => i.Width ).Returns( previousWidth );
        _imageMock.Setup( i => i.Height ).Returns( previousHeight );

        _documentMock.Setup( d => d.GetItemsCount() ).Returns( 2 );
        _documentMock.Setup( d => d.GetItem( position ) ).Returns( documentItem );

        ResizeImageCommand command = new ResizeImageCommand( _documentMock.Object, newWidth, newHeight, position );

        // Act & Assert
        Assert.DoesNotThrow( () =>
        {
            command.Execute();
            command.Unexecute();
        } );

        _imageMock.Verify( i => i.Resize( newWidth, newHeight ), Times.Once );
        _imageMock.Verify( i => i.Resize( previousWidth, previousHeight ), Times.Once );
    }

    [Test]
    public void PositionProperty_ReturnsCorrectValue()
    {
        // Arrange
        const int position = 2;
        const int width = 200;
        const int height = 300;

        ResizeImageCommand command = new ResizeImageCommand( _documentMock.Object, width, height, position );

        // Act & Assert
        Assert.That( command.Position, Is.EqualTo( position ) );
    }
}