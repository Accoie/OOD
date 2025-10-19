using Editor.Documents;
using Editor.Documents.Items;
using Editor.History;
using Editor.Image;
using Editor.Paragraph;
using Moq;

namespace EditorTests.Documents.Tests;

[TestFixture]
public class DocumentTests
{
    private Mock<ICommandHistory> _historyMock;
    private Document _document;

    [SetUp]
    public void Setup()
    {
        _historyMock = new Mock<ICommandHistory>();
        _document = new Document( _historyMock.Object, "Test Document" );
    }

    [Test]
    public void Constructor_InitializesTitle()
    {
        // Arrange & Act
        Document document = new Document( _historyMock.Object, "Test Title" );

        // Assert
        Assert.That( document.GetTitle(), Is.EqualTo( "Test Title" ) );
    }

    [Test]
    public void InsertParagraph_WithNullPosition_AddsAtEnd()
    {
        // Arrange
        const string text = "Test paragraph";

        // Act
        IParagraph paragraph = _document.InsertParagraph( text, null );

        // Assert
        Assert.That( paragraph, Is.Not.Null );
        Assert.That( _document.GetItemsCount(), Is.EqualTo( 1 ) );
        Assert.That( _document.GetItem( 0 ).Paragraph, Is.EqualTo( paragraph ) );
    }

    [Test]
    public void InsertParagraph_WithValidPosition_AddsAtPosition()
    {
        // Arrange
        const string text1 = "First paragraph";
        const string text2 = "Second paragraph";
        const int position = 0;

        // Act
        IParagraph firstParagraph = _document.InsertParagraph( text1 );
        IParagraph secondParagraph = _document.InsertParagraph( text2, position );

        // Assert
        Assert.That( _document.GetItemsCount(), Is.EqualTo( 2 ) );
        Assert.That( _document.GetItem( 0 ).Paragraph, Is.EqualTo( secondParagraph ) );
        Assert.That( _document.GetItem( 1 ).Paragraph, Is.EqualTo( firstParagraph ) );
    }

    [Test]
    public void InsertParagraph_WithPositionGreaterThanCount_AddsAtEnd()
    {
        // Arrange
        const string text = "Test paragraph";
        const int largePosition = 10;

        // Act
        IParagraph paragraph = _document.InsertParagraph( text, largePosition );

        // Assert
        Assert.That( _document.GetItemsCount(), Is.EqualTo( 1 ) );
        Assert.That( _document.GetItem( 0 ).Paragraph, Is.EqualTo( paragraph ) );
    }

    [Test]
    public void InsertImage_WithNullPosition_AddsAtEnd()
    {
        // Arrange
        const string path = "test.jpg";
        const int width = 100;
        const int height = 200;

        // Act
        IImage image = _document.InsertImage( path, width, height, null );

        // Assert
        Assert.That( image, Is.Not.Null );
        Assert.That( _document.GetItemsCount(), Is.EqualTo( 1 ) );
        Assert.That( _document.GetItem( 0 ).Image, Is.EqualTo( image ) );
    }

    [Test]
    public void InsertImage_WithValidPosition_AddsAtPosition()
    {
        // Arrange
        const string path1 = "test1.jpg";
        const string path2 = "test2.jpg";
        const int width = 100;
        const int height = 200;
        const int position = 0;

        // Act
        IImage firstImage = _document.InsertImage( path1, width, height );
        IImage secondImage = _document.InsertImage( path2, width, height, position );

        // Assert
        Assert.That( _document.GetItemsCount(), Is.EqualTo( 2 ) );
        Assert.That( _document.GetItem( 0 ).Image, Is.EqualTo( secondImage ) );
        Assert.That( _document.GetItem( 1 ).Image, Is.EqualTo( firstImage ) );
    }

    [Test]
    public void InsertImage_WithPositionGreaterThanCount_AddsAtEnd()
    {
        // Arrange
        const string path = "test.jpg";
        const int width = 100;
        const int height = 200;
        const int largePosition = 10;

        // Act
        IImage image = _document.InsertImage( path, width, height, largePosition );

        // Assert
        Assert.That( _document.GetItemsCount(), Is.EqualTo( 1 ) );
        Assert.That( _document.GetItem( 0 ).Image, Is.EqualTo( image ) );
    }

    [Test]
    public void GetItemsCount_ReturnsCorrectCount()
    {
        // Arrange
        _document.InsertParagraph( "Paragraph 1" );
        _document.InsertParagraph( "Paragraph 2" );

        // Act
        int count = _document.GetItemsCount();

        // Assert
        Assert.That( count, Is.EqualTo( 2 ) );
    }

    [Test]
    public void GetItem_WithValidIndex_ReturnsItem()
    {
        // Arrange
        IParagraph paragraph = _document.InsertParagraph( "Test paragraph" );

        // Act
        DocumentItem item = _document.GetItem( 0 );

        // Assert
        Assert.That( item.Paragraph, Is.EqualTo( paragraph ) );
    }

    [Test]
    public void GetItem_WithInvalidIndex_ThrowsException()
    {
        // Arrange
        const int invalidIndex = 5;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>( () => _document.GetItem( invalidIndex ) );
    }

    [Test]
    public void DeleteItem_WithNullIndex_DeletesLastItem()
    {
        // Arrange
        _document.InsertParagraph( "Paragraph 1" );
        _document.InsertParagraph( "Paragraph 2" );

        // Act
        _document.DeleteItem( null );

        // Assert
        Assert.That( _document.GetItemsCount(), Is.EqualTo( 1 ) );
        Assert.That( _document.GetItem( 0 ).Paragraph.Text, Is.EqualTo( "Paragraph 1" ) );
    }

    [Test]
    public void DeleteItem_WithValidIndex_DeletesItemAtPosition()
    {
        // Arrange
        _document.InsertParagraph( "Paragraph 1" );
        _document.InsertParagraph( "Paragraph 2" );

        // Act
        _document.DeleteItem( 0 );

        // Assert
        Assert.That( _document.GetItemsCount(), Is.EqualTo( 1 ) );
        Assert.That( _document.GetItem( 0 ).Paragraph.Text, Is.EqualTo( "Paragraph 2" ) );
    }

    [Test]
    public void SetTitle_ChangesTitle()
    {
        // Arrange
        const string newTitle = "New Title";

        // Act
        _document.SetTitle( newTitle );

        // Assert
        Assert.That( _document.GetTitle(), Is.EqualTo( newTitle ) );
    }

    [Test]
    public void CanUndo_ReturnsHistoryCanUndo()
    {
        // Arrange
        _historyMock.Setup( h => h.CanUndo() ).Returns( true );

        // Act
        bool canUndo = _document.CanUndo();

        // Assert
        Assert.That( canUndo, Is.True );
        _historyMock.Verify( h => h.CanUndo(), Times.Once );
    }

    [Test]
    public void CanRedo_ReturnsHistoryCanRedo()
    {
        // Arrange
        _historyMock.Setup( h => h.CanRedo() ).Returns( true );

        // Act
        bool canRedo = _document.CanRedo();

        // Assert
        Assert.That( canRedo, Is.True );
        _historyMock.Verify( h => h.CanRedo(), Times.Once );
    }

    [Test]
    public void Undo_CallsHistoryUndo()
    {
        // Act
        _document.Undo();

        // Assert
        _historyMock.Verify( h => h.Undo(), Times.Once );
    }

    [Test]
    public void Redo_CallsHistoryRedo()
    {
        // Act
        _document.Redo();

        // Assert
        _historyMock.Verify( h => h.Redo(), Times.Once );
    }

    [Test]
    public void Save_WithValidPath_CreatesHtmlFile()
    {
        // Arrange
        const string testPath = "test_output.html";
        const string paragraphText = "Test paragraph";

        _document.InsertParagraph( paragraphText );

        if ( File.Exists( testPath ) )
        {
            File.Delete( testPath );
        }
        // Act
        _document.Save( testPath );

        // Assert
        Assert.That( File.Exists( testPath ), Is.True );

        string content = File.ReadAllText( testPath );
        Assert.That( content, Contains.Substring( paragraphText ) );
        Assert.That( content, Contains.Substring( "Test Document" ) );
    }

    [Test]
    public void Save_WithInvalidPath_ThrowsArgumentException()
    {
        // Arrange
        const string invalidPath = "";

        // Act & Assert
        Assert.Throws<ArgumentException>( () => _document.Save( invalidPath ) );
    }

    [Test]
    public void Save_WithMixedContent_CreatesProperHtml()
    {
        // Arrange
        const string testPath = "test_mixed.html";
        const string paragraphText = "Test paragraph";
        const string imagePath = "test_image.jpg";

        File.WriteAllText( imagePath, "fake image content" );


        _document.InsertParagraph( paragraphText );
        _document.InsertImage( imagePath, 100, 200 );

        // Act
        _document.Save( testPath );

        // Assert
        Assert.That( File.Exists( testPath ), Is.True );

        string content = File.ReadAllText( testPath );
        Assert.That( content, Contains.Substring( paragraphText ) );
        Assert.That( content, Contains.Substring( "img" ) );
        Assert.That( content, Contains.Substring( "width=100" ) );
        Assert.That( content, Contains.Substring( "height=200" ) );
    }

    [Test]
    public void Save_WithNonExistentImage_ThrowsFileNotFoundException()
    {
        // Arrange
        const string testPath = "test_output.html";
        const string nonExistentImagePath = "non_existent.jpg";

        _document.InsertImage( nonExistentImagePath, 100, 200 );

        // Act & Assert
        Assert.Throws<FileNotFoundException>( () => _document.Save( testPath ) );
    }
}