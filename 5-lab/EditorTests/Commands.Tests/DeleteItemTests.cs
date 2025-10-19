using Editor.Commands;
using Editor.Documents;
using Editor.Documents.Items;
using Editor.Image;
using Editor.Paragraph;
using Moq;

namespace EditorTests.Commands.Tests;

[TestFixture]
public class DeleteItemTests
{
    private Mock<IDocument> _documentMock;

    [SetUp]
    public void Setup()
    {
        _documentMock = new Mock<IDocument>();
    }

    [Test]
    public void Execute_ValidPosition_DeletesItem()
    {
        // Arrange
        const int position = 1;
        Mock<IParagraph> paragraphMock = new Mock<IParagraph>();
        DocumentItem documentItem = new DocumentItem( paragraphMock.Object );

        _documentMock.Setup( document => document.GetItem( position ) ).Returns( documentItem );
        _documentMock.Setup( document => document.GetItemsCount() ).Returns( 3 );

        DeleteItemCommand command = new DeleteItemCommand( _documentMock.Object, position );

        // Act
        command.Execute();

        // Assert
        _documentMock.Verify( document => document.DeleteItem( position ), Times.Once );
    }

    [Test]
    public void Execute_PositionOutOfRange_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        const int position = 5;
        Mock<IParagraph> paragraphMock = new Mock<IParagraph>();
        DocumentItem documentItem = new DocumentItem( paragraphMock.Object );

        _documentMock.Setup( document => document.GetItem( position ) ).Returns( documentItem );
        _documentMock.Setup( document => document.GetItemsCount() ).Returns( 3 );

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>( () => new DeleteItemCommand( _documentMock.Object, position ) );
    }

    [Test]
    public void Unexecute_AfterDeletingParagraph_RestoresParagraph()
    {
        // Arrange
        const int position = 1;
        const string paragraphText = "Test paragraph";
        Mock<IParagraph> paragraphMock = new Mock<IParagraph>();
        paragraphMock.Setup( paragraph => paragraph.Text ).Returns( paragraphText );

        DocumentItem documentItem = new DocumentItem( paragraphMock.Object );

        _documentMock.Setup( document => document.GetItem( position ) ).Returns( documentItem );
        _documentMock.Setup( document => document.GetItemsCount() ).Returns( 3 );
        _documentMock.Setup( document => document.InsertParagraph( paragraphText, position ) )
                    .Returns( new Mock<IParagraph>().Object );

        DeleteItemCommand command = new DeleteItemCommand( _documentMock.Object, position );
        command.Execute();

        // Act
        command.Unexecute();

        // Assert
        _documentMock.Verify( document => document.InsertParagraph( paragraphText, position ), Times.Once );
    }

    [Test]
    public void Unexecute_AfterDeletingImage_RestoresImage()
    {
        // Arrange
        const int position = 1;
        const string imagePath = "test.jpg";
        const int width = 100;
        const int height = 200;

        Mock<IImage> imageMock = new Mock<IImage>();
        imageMock.Setup( image => image.Path ).Returns( imagePath );
        imageMock.Setup( image => image.Width ).Returns( width );
        imageMock.Setup( image => image.Height ).Returns( height );

        DocumentItem documentItem = new DocumentItem( imageMock.Object );

        _documentMock.Setup( document => document.GetItem( position ) ).Returns( documentItem );
        _documentMock.Setup( document => document.GetItemsCount() ).Returns( 3 );
        _documentMock.Setup( document => document.InsertImage( imagePath, width, height, position ) )
                    .Returns( new Mock<IImage>().Object );

        DeleteItemCommand command = new DeleteItemCommand( _documentMock.Object, position );
        command.Execute();

        // Act
        command.Unexecute();

        // Assert
        _documentMock.Verify( document => document.InsertImage( imagePath, width, height, position ), Times.Once );
    }

    [Test]
    public void ExecuteAndUnexecute_MultipleTimes_WorksCorrectly()
    {
        // Arrange
        const int position = 1;
        const string paragraphText = "Test paragraph";
        Mock<IParagraph> paragraphMock = new Mock<IParagraph>();
        paragraphMock.Setup( paragraph => paragraph.Text ).Returns( paragraphText );

        DocumentItem documentItem = new DocumentItem( paragraphMock.Object );

        _documentMock.Setup( document => document.GetItem( position ) ).Returns( documentItem );
        _documentMock.SetupSequence( document => document.GetItemsCount() )
                    .Returns( 3 )
                    .Returns( 2 )
                    .Returns( 3 )
                    .Returns( 2 );

        _documentMock.Setup( document => document.InsertParagraph( paragraphText, position ) )
                    .Returns( new Mock<IParagraph>().Object );

        DeleteItemCommand command = new DeleteItemCommand( _documentMock.Object, position );

        // Act & Assert
        Assert.DoesNotThrow( () =>
        {
            command.Execute();
            command.Unexecute();
            command.Execute();
        } );

        _documentMock.Verify( document => document.DeleteItem( position ), Times.Exactly( 2 ) );
        _documentMock.Verify( document => document.InsertParagraph( paragraphText, position ), Times.Once );
    }

    [Test]
    public void Constructor_EmptyDocumentWithNullPosition_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        _documentMock.Setup( document => document.GetItemsCount() ).Returns( 0 );

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>( () =>
            new DeleteItemCommand( _documentMock.Object, null ) );
    }

    [Test]
    public void Unexecute_WithoutExecute_DoesNothing()
    {
        // Arrange
        const int position = 1;
        Mock<IParagraph> paragraphMock = new Mock<IParagraph>();
        DocumentItem documentItem = new DocumentItem( paragraphMock.Object );

        _documentMock.Setup( document => document.GetItem( position ) ).Returns( documentItem );
        _documentMock.Setup( document => document.GetItemsCount() ).Returns( 3 );

        DeleteItemCommand command = new DeleteItemCommand( _documentMock.Object, position );

        // Act
        command.Unexecute();

        // Assert
        _documentMock.Verify( document => document.DeleteItem( It.IsAny<int>() ), Times.Never );
        _documentMock.Verify( document => document.InsertParagraph( It.IsAny<string>(), It.IsAny<int>() ), Times.Never );
        _documentMock.Verify( document => document.InsertImage( It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>() ), Times.Never );
    }

    [Test]
    public void Execute_TwiceWithoutUnexecute_WillNotDeleteInSecond()
    {
        // Arrange
        const int position = 1;
        Mock<IParagraph> paragraphMock = new Mock<IParagraph>();
        DocumentItem documentItem = new DocumentItem( paragraphMock.Object );

        _documentMock.Setup( document => document.GetItem( position ) ).Returns( documentItem );
        _documentMock.Setup( document => document.GetItemsCount() ).Returns( 3 );
        _documentMock.Setup( document => document.InsertImage( It.IsAny<string>(), 33, 33, It.IsAny<int?>() ) );

        DeleteItemCommand command = new DeleteItemCommand( _documentMock.Object, position );

        // Act
        command.Execute();
        command.Execute();

        // Assert
        _documentMock.Verify( document => document.DeleteItem( It.IsAny<int>() ), Times.Once );
    }

    [Test]
    public void Unexecute_TwiceWithoutExecute_DoesNothingOnSecondUnexecution()
    {
        // Arrange
        const int position = 1;
        Mock<IParagraph> paragraphMock = new Mock<IParagraph>();
        DocumentItem documentItem = new DocumentItem( paragraphMock.Object );

        _documentMock.Setup( document => document.GetItem( position ) ).Returns( documentItem );
        _documentMock.Setup( document => document.GetItemsCount() ).Returns( 3 );
        _documentMock.Setup( document => document.InsertImage( It.IsAny<string>(), 33, 33, It.IsAny<int?>() ) );

        DeleteItemCommand command = new DeleteItemCommand( _documentMock.Object, position );

        // Act
        command.Unexecute();
        command.Unexecute();

        // Assert
        _documentMock.Verify( document => document.InsertParagraph( It.IsAny<string>(), It.IsAny<int>() ), Times.Never );
        _documentMock.Verify( document => document.InsertImage( It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>() ), Times.Never );
    }

    [Test]
    public void Constructor_NegativePosition_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        const int negativePosition = -1;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>( () =>
            new DeleteItemCommand( _documentMock.Object, negativePosition ) );
    }

    [Test]
    public void Constructor_PositionGreaterThanItemsCount_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        const int invalidPosition = 5;
        Mock<IParagraph> paragraphMock = new Mock<IParagraph>();
        DocumentItem documentItem = new DocumentItem( paragraphMock.Object );

        _documentMock.Setup( document => document.GetItem( invalidPosition ) ).Returns( documentItem );
        _documentMock.Setup( document => document.GetItemsCount() ).Returns( 3 );

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>( () =>
            new DeleteItemCommand( _documentMock.Object, invalidPosition ) );
    }
}