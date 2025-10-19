using Editor.Commands;
using Editor.History;
using Moq;

namespace EditorTests.History.Tests;

[TestFixture]
public class CommandHistoryTests
{
    private CommandHistory _commandHistory;
    private Mock<ICommand> _commandMock1;
    private Mock<ICommand> _commandMock2;
    private Mock<ICommand> _commandMock3;

    [SetUp]
    public void Setup()
    {
        _commandHistory = new CommandHistory();
        _commandMock1 = new Mock<ICommand>();
        _commandMock2 = new Mock<ICommand>();
        _commandMock3 = new Mock<ICommand>();
    }

    [Test]
    public void AddAndExecuteCommand_AddsFirstCommand_ExecutesIt()
    {
        // Act
        _commandHistory.AddAndExecuteCommand( _commandMock1.Object );

        // Assert
        _commandMock1.Verify( command => command.Execute(), Times.Once );
    }

    [Test]
    public void AddAndExecuteCommand_AddsMultipleCommands_ExecutesThem()
    {
        // Act
        _commandHistory.AddAndExecuteCommand( _commandMock1.Object );
        _commandHistory.AddAndExecuteCommand( _commandMock2.Object );
        _commandHistory.AddAndExecuteCommand( _commandMock3.Object );

        // Assert
        _commandMock1.Verify( command => command.Execute(), Times.Once );
        _commandMock2.Verify( command => command.Execute(), Times.Once );
        _commandMock3.Verify( command => command.Execute(), Times.Once );
    }

    [Test]
    public void CanUndo_WithNoCommands_ReturnsFalse()
    {
        // Act & Assert
        Assert.That( _commandHistory.CanUndo(), Is.False );
    }

    [Test]
    public void CanUndo_WithCommands_ReturnsTrue()
    {
        // Arrange
        _commandHistory.AddAndExecuteCommand( _commandMock1.Object );

        // Act & Assert
        Assert.That( _commandHistory.CanUndo(), Is.True );
    }

    [Test]
    public void CanRedo_WithNoCommands_ReturnsFalse()
    {
        // Act & Assert
        Assert.That( _commandHistory.CanRedo(), Is.False );
    }

    [Test]
    public void CanRedo_AfterUndo_ReturnsTrue()
    {
        // Arrange
        _commandHistory.AddAndExecuteCommand( _commandMock1.Object );
        _commandHistory.Undo();

        // Act & Assert
        Assert.That( _commandHistory.CanRedo(), Is.True );
    }

    [Test]
    public void CanRedo_WithNoUndoneCommands_ReturnsFalse()
    {
        // Arrange
        _commandHistory.AddAndExecuteCommand( _commandMock1.Object );

        // Act & Assert
        Assert.That( _commandHistory.CanRedo(), Is.False );
    }

    [Test]
    public void Undo_WithCommands_UnexecutesCommand()
    {
        // Arrange
        _commandHistory.AddAndExecuteCommand( _commandMock1.Object );

        // Act
        _commandHistory.Undo();

        // Assert
        _commandMock1.Verify( command => command.Unexecute(), Times.Once );
    }

    [Test]
    public void Undo_WithNoCommands_ThrowsInvalidOperationException()
    {
        // Act & Assert
        Assert.Throws<InvalidOperationException>( () => _commandHistory.Undo() );
    }

    [Test]
    public void Redo_AfterUndo_ExecutesCommand()
    {
        // Arrange
        _commandHistory.AddAndExecuteCommand( _commandMock1.Object );
        _commandHistory.Undo();

        // Act
        _commandHistory.Redo();

        // Assert
        _commandMock1.Verify( command => command.Execute(), Times.Exactly( 2 ) );
    }

    [Test]
    public void Redo_WithNoRedoCommands_ThrowsInvalidOperationException()
    {
        // Act & Assert
        Assert.Throws<InvalidOperationException>( () => _commandHistory.Redo() );
    }

    [Test]
    public void AddAndExecuteCommand_AfterUndo_RemovesRedoCommands()
    {
        // Arrange
        _commandHistory.AddAndExecuteCommand( _commandMock1.Object );
        _commandHistory.AddAndExecuteCommand( _commandMock2.Object );
        _commandHistory.Undo();

        // Act
        _commandHistory.AddAndExecuteCommand( _commandMock3.Object );

        // Assert
        Assert.That( _commandHistory.CanRedo(), Is.False );
    }

    [Test]
    public void Undo_Redo_Sequence_WorksCorrectly()
    {
        // Arrange
        _commandHistory.AddAndExecuteCommand( _commandMock1.Object );
        _commandHistory.AddAndExecuteCommand( _commandMock2.Object );

        // Act & Assert
        Assert.DoesNotThrow( () =>
        {
            _commandHistory.Undo();
            _commandHistory.Undo();
            _commandHistory.Redo();
            _commandHistory.Redo();
        } );

        _commandMock1.Verify( command => command.Execute(), Times.Exactly( 2 ) );
        _commandMock1.Verify( command => command.Unexecute(), Times.Once );
        _commandMock2.Verify( command => command.Execute(), Times.Exactly( 2 ) );
        _commandMock2.Verify( command => command.Unexecute(), Times.Once );
    }

    [Test]
    public void AddAndExecuteCommand_ClearsRedoStack()
    {
        // Arrange
        _commandHistory.AddAndExecuteCommand( _commandMock1.Object );
        _commandHistory.AddAndExecuteCommand( _commandMock2.Object );
        _commandHistory.Undo();
        _commandHistory.Undo();

        // Act
        _commandHistory.AddAndExecuteCommand( _commandMock3.Object );

        // Assert
        Assert.That( _commandHistory.CanRedo(), Is.False );
        Assert.That( _commandHistory.CanUndo(), Is.True );
    }

    [Test]
    public void MultipleUndoRedo_Operations_WorkCorrectly()
    {
        // Arrange
        _commandHistory.AddAndExecuteCommand( _commandMock1.Object );
        _commandHistory.AddAndExecuteCommand( _commandMock2.Object );
        _commandHistory.AddAndExecuteCommand( _commandMock3.Object );

        // Act & Assert
        // Undo all
        _commandHistory.Undo();
        _commandHistory.Undo();
        _commandHistory.Undo();

        Assert.That( _commandHistory.CanUndo(), Is.False );
        Assert.That( _commandHistory.CanRedo(), Is.True );

        // Redo all
        _commandHistory.Redo();
        _commandHistory.Redo();
        _commandHistory.Redo();

        Assert.That( _commandHistory.CanUndo(), Is.True );
        Assert.That( _commandHistory.CanRedo(), Is.False );
    }

    [Test]
    public void AddCommand_AfterMultipleUndo_ClearsFutureCommands()
    {
        // Arrange
        _commandHistory.AddAndExecuteCommand( _commandMock1.Object );
        _commandHistory.AddAndExecuteCommand( _commandMock2.Object );
        _commandHistory.AddAndExecuteCommand( _commandMock3.Object );

        _commandHistory.Undo();
        _commandHistory.Undo();

        // Act
        _commandHistory.AddAndExecuteCommand( _commandMock1.Object );

        // Assert
        Assert.That( _commandHistory.CanRedo(), Is.False );
        Assert.That( _commandHistory.CanUndo(), Is.True );
    }
}