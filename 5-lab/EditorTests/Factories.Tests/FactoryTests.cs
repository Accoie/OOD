using Editor.Commands;
using Editor.Documents;
using Editor.Documents.Items;
using Editor.Factory;
using Editor.Image;
using Editor.Paragraph;
using Moq;

namespace EditorTests.Factory.Tests;

[TestFixture]
public class CommandFactoryTests
{
    private Mock<IDocument> _documentMock;
    private Dictionary<string, string> _helpCommands;
    private CommandFactory _factory;

    [SetUp]
    public void Setup()
    {
        _documentMock = new Mock<IDocument>();
        _helpCommands = new Dictionary<string, string>
        {
            [ "help" ] = "Displays help",
            [ "list" ] = "Lists items"
        };

        _factory = new CommandFactory( _helpCommands, _documentMock.Object );
    }

    [Test]
    public void Constructor_InitializesWithHelpCommand()
    {
        // Arrange & Act
        CommandFactory factory = new CommandFactory( _helpCommands, _documentMock.Object );

        // Assert
        Assert.That( factory, Is.Not.Null );
    }

    [Test]
    public void CreateCommand_WithEmptyString_ThrowsArgumentException()
    {
        // Arrange
        const string emptyCommand = "";

        // Act & Assert
        Assert.Throws<ArgumentException>( () => _factory.CreateCommand( emptyCommand ) );
    }

    [Test]
    public void CreateCommand_WithWhitespace_ThrowsArgumentException()
    {
        // Arrange
        const string whitespaceCommand = "   ";

        // Act & Assert
        Assert.Throws<ArgumentException>( () => _factory.CreateCommand( whitespaceCommand ) );
    }

    [Test]
    public void CreateCommand_HelpCommand_ReturnsHelpCommand()
    {
        // Arrange
        const string command = "help";

        // Act
        ICommand result = _factory.CreateCommand( command );

        // Assert
        Assert.That( result, Is.InstanceOf<HelpCommand>() );
    }

    [Test]
    public void CreateCommand_ListCommand_ReturnsListCommand()
    {
        // Arrange
        const string command = "list";

        // Act
        ICommand result = _factory.CreateCommand( command );

        // Assert
        Assert.That( result, Is.InstanceOf<ListCommand>() );
    }

    [Test]
    public void CreateCommand_UndoCommand_ReturnsUndoCommand()
    {
        // Arrange
        const string command = "undo";

        // Act
        ICommand result = _factory.CreateCommand( command );

        // Assert
        Assert.That( result, Is.InstanceOf<UndoCommand>() );
    }

    [Test]
    public void CreateCommand_RedoCommand_ReturnsRedoCommand()
    {
        // Arrange
        const string command = "redo";

        // Act
        ICommand result = _factory.CreateCommand( command );

        // Assert
        Assert.That( result, Is.InstanceOf<RedoCommand>() );
    }

    [Test]
    public void CreateCommand_InsertParagraphCommand_ReturnsValidCommand()
    {
        // Arrange
        const string command = "insertparagraph end Test paragraph";

        // Act
        ICommand result = _factory.CreateCommand( command );

        // Assert
        Assert.That( result, Is.InstanceOf<InsertParagraphCommand>() );
    }

    [Test]
    public void CreateCommand_InsertImageCommand_ReturnsValidCommand()
    {
        // Arrange
        const string command = "insertimage end 100 200 test.jpg";

        // Act
        ICommand result = _factory.CreateCommand( command );

        // Assert
        Assert.That( result, Is.InstanceOf<InsertImageCommand>() );
    }

    [Test]
    public void CreateCommand_DeleteItemCommand_ReturnsValidCommand()
    {
        // Arrange
        const string command = "deleteitem 0";
        _documentMock.Setup( document => document.GetItemsCount() ).Returns( 1 );

        // Act
        ICommand result = _factory.CreateCommand( command );

        // Assert
        Assert.That( result, Is.InstanceOf<DeleteItemCommand>() );
    }

    [Test]
    public void CreateCommand_SaveCommand_ReturnsValidCommand()
    {
        // Arrange
        const string command = "save test.html";

        // Act
        ICommand result = _factory.CreateCommand( command );

        // Assert
        Assert.That( result, Is.InstanceOf<SaveCommand>() );
    }

    [Test]
    public void CreateCommand_ReplaceTextCommand_ReturnsValidCommand()
    {
        // Arrange
        Mock<IParagraph> paragraphMock = new Mock<IParagraph>();
        DocumentItem documentItem = new DocumentItem( paragraphMock.Object );
        _documentMock.Setup( document => document.GetItemsCount() ).Returns( 1 );
        _documentMock.Setup( document => document.GetItem( 0 ) ).Returns( documentItem );

        const string command = "replacetext 0 New text";

        // Act
        ICommand result = _factory.CreateCommand( command );

        // Assert
        Assert.That( result, Is.InstanceOf<ReplaceTextCommand>() );
    }

    [Test]
    public void CreateCommand_ResizeImageCommand_ReturnsValidCommand()
    {
        // Arrange
        Mock<IImage> imageMock = new Mock<IImage>();
        DocumentItem documentItem = new DocumentItem( imageMock.Object );
        _documentMock.Setup( document => document.GetItemsCount() ).Returns( 1 );
        _documentMock.Setup( document => document.GetItem( 0 ) ).Returns( documentItem );

        const string command = "resizeimage 0 300 400";

        // Act
        ICommand result = _factory.CreateCommand( command );

        // Assert
        Assert.That( result, Is.InstanceOf<ResizeImageCommand>() );
    }

    [Test]
    public void CreateCommand_SetTitleCommand_ReturnsValidCommand()
    {
        // Arrange
        const string command = "settitle New Title";

        // Act
        ICommand result = _factory.CreateCommand( command );

        // Assert
        Assert.That( result, Is.InstanceOf<SetTitleCommand>() );
    }

    [Test]
    public void CreateCommand_UnknownCommand_ThrowsArgumentException()
    {
        // Arrange
        const string unknownCommand = "unknowncommand";

        // Act & Assert
        Assert.Throws<ArgumentException>( () => _factory.CreateCommand( unknownCommand ) );
    }

    [Test]
    public void CreateCommand_SetTitleCommand_WithSameLastCommand_CreatesCompoundCommand()
    {
        // Arrange
        const string firstCommand = "settitle Title1";
        const string secondCommand = "settitle Title2";

        // Act
        ICommand firstResult = _factory.CreateCommand( firstCommand );
        ICommand secondResult = _factory.CreateCommand( secondCommand );

        // Assert
        Assert.That( firstResult, Is.InstanceOf<SetTitleCommand>() );
        Assert.That( secondResult, Is.InstanceOf<CompoundCommand>() );

        CompoundCommand compoundCommand = ( CompoundCommand )secondResult;
        Assert.That( compoundCommand.Commands.Count, Is.EqualTo( 2 ) );
        Assert.That( compoundCommand.Commands[ 0 ], Is.InstanceOf<SetTitleCommand>() );
        Assert.That( compoundCommand.Commands[ 1 ], Is.InstanceOf<SetTitleCommand>() );
    }

    [Test]
    public void CreateCommand_ReplaceTextCommand_WithSamePosition_CreatesCompoundCommand()
    {
        // Arrange
        Mock<IParagraph> paragraphMock = new Mock<IParagraph>();
        DocumentItem documentItem = new DocumentItem( paragraphMock.Object );
        _documentMock.Setup( document => document.GetItemsCount() ).Returns( 1 );
        _documentMock.Setup( document => document.GetItem( 0 ) ).Returns( documentItem );

        const string firstCommand = "replacetext 0 newtext";
        const string secondCommand = "replacetext 0 othertext";
        const string thirdCommand = "replacetext 0 othertext";
        const string fourthCommand = "replacetext 0 othertext";

        // Act
        ICommand firstResult = _factory.CreateCommand( firstCommand );
        ICommand secondResult = _factory.CreateCommand( secondCommand );
        ICommand thirdResult = _factory.CreateCommand( thirdCommand );
        ICommand fourthResult = _factory.CreateCommand( fourthCommand );

        // Assert
        Assert.That( firstResult, Is.InstanceOf<ReplaceTextCommand>() );
        Assert.That( secondResult, Is.InstanceOf<CompoundCommand>() );
        Assert.That( thirdResult, Is.InstanceOf<CompoundCommand>() );
        Assert.That( fourthResult, Is.InstanceOf<CompoundCommand>() );
    }

    [Test]
    public void CreateCommand_ReplaceTextCommand_WithDifferentPosition_DoesNotCreateCompoundCommand()
    {
        // Arrange
        Mock<IParagraph> paragraphMock1 = new Mock<IParagraph>();
        Mock<IParagraph> paragraphMock2 = new Mock<IParagraph>();
        DocumentItem documentItem1 = new DocumentItem( paragraphMock1.Object );
        DocumentItem documentItem2 = new DocumentItem( paragraphMock2.Object );

        _documentMock.Setup( document => document.GetItemsCount() ).Returns( 2 );
        _documentMock.Setup( document => document.GetItem( 0 ) ).Returns( documentItem1 );
        _documentMock.Setup( document => document.GetItem( 1 ) ).Returns( documentItem2 );

        const string firstCommand = "replacetext 0 newtext";
        const string secondCommand = "replacetext 1 othertext";

        // Act
        ICommand firstResult = _factory.CreateCommand( firstCommand );
        ICommand secondResult = _factory.CreateCommand( secondCommand );

        // Assert
        Assert.That( firstResult, Is.InstanceOf<ReplaceTextCommand>() );
        Assert.That( secondResult, Is.InstanceOf<ReplaceTextCommand>() );
    }

    [Test]
    public void CreateCommand_ResizeImageCommand_WithSamePosition_CreatesCompoundCommand()
    {
        // Arrange
        Mock<IImage> imageMock = new Mock<IImage>();
        DocumentItem documentItem = new DocumentItem( imageMock.Object );
        _documentMock.Setup( document => document.GetItemsCount() ).Returns( 1 );
        _documentMock.Setup( document => document.GetItem( 0 ) ).Returns( documentItem );

        const string firstCommand = "resizeimage 0 300 400";
        const string secondCommand = "resizeimage 0 500 600";

        // Act
        ICommand firstResult = _factory.CreateCommand( firstCommand );
        ICommand secondResult = _factory.CreateCommand( secondCommand );

        // Assert
        Assert.That( firstResult, Is.InstanceOf<ResizeImageCommand>() );
        Assert.That( secondResult, Is.InstanceOf<CompoundCommand>() );
    }

    [Test]
    public void CreateCommand_ResizeImageCommand_WithDifferentPosition_DoesNotCreateCompoundCommand()
    {
        // Arrange
        Mock<IImage> imageMock1 = new Mock<IImage>();
        Mock<IImage> imageMock2 = new Mock<IImage>();
        DocumentItem documentItem1 = new DocumentItem( imageMock1.Object );
        DocumentItem documentItem2 = new DocumentItem( imageMock2.Object );

        _documentMock.Setup( document => document.GetItemsCount() ).Returns( 2 );
        _documentMock.Setup( document => document.GetItem( 0 ) ).Returns( documentItem1 );
        _documentMock.Setup( document => document.GetItem( 1 ) ).Returns( documentItem2 );

        const string firstCommand = "resizeimage 0 300 400";
        const string secondCommand = "resizeimage 1 500 600";

        // Act
        ICommand firstResult = _factory.CreateCommand( firstCommand );
        ICommand secondResult = _factory.CreateCommand( secondCommand );

        // Assert
        Assert.That( firstResult, Is.InstanceOf<ResizeImageCommand>() );
        Assert.That( secondResult, Is.InstanceOf<ResizeImageCommand>() );
    }

    [Test]
    public void CreateCommand_CommandIsCaseInsensitive()
    {
        // Arrange
        const string upperCaseCommand = "HELP";
        const string mixedCaseCommand = "LiSt";
        const string lowerCaseCommand = "undo";

        // Act & Assert
        Assert.DoesNotThrow( () => _factory.CreateCommand( upperCaseCommand ) );
        Assert.DoesNotThrow( () => _factory.CreateCommand( mixedCaseCommand ) );
        Assert.DoesNotThrow( () => _factory.CreateCommand( lowerCaseCommand ) );
    }

    [Test]
    public void CreateCommand_UpdatesLastCommand()
    {
        // Arrange
        const string firstCommand = "help";
        const string secondCommand = "list";

        // Act
        ICommand firstResult = _factory.CreateCommand( firstCommand );
        ICommand secondResult = _factory.CreateCommand( secondCommand );

        // Assert
        Assert.That( firstResult, Is.InstanceOf<HelpCommand>() );
        Assert.That( secondResult, Is.InstanceOf<ListCommand>() );
    }

    [Test]
    public void CreateCommand_WithInvalidImageSize_ThrowsArgumentException()
    {
        // Arrange
        const string invalidImageCommand = "insertimage end 0 0 test.jpg";

        // Act & Assert
        Assert.Throws<ArgumentException>( () => _factory.CreateCommand( invalidImageCommand ) );
    }

    [Test]
    public void CreateCommand_WithInvalidPosition_ThrowsArgumentException()
    {
        // Arrange
        const string invalidPositionCommand = "deleteitem invalid";

        // Act & Assert
        Assert.Throws<ArgumentException>( () => _factory.CreateCommand( invalidPositionCommand ) );
    }

    [Test]
    public void CreateCommand_WithEmptyText_ThrowsArgumentException()
    {
        // Arrange
        const string emptyTextCommand = "insertparagraph end ";

        // Act & Assert
        Assert.Throws<ArgumentException>( () => _factory.CreateCommand( emptyTextCommand ) );
    }
}