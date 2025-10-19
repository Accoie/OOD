using Editor.Commands;
using Editor.Documents;
using Moq;
using NUnit.Framework;
using System;

namespace EditorTests.Commands.Tests;

[TestFixture]
public class RedoCommandTests
{
    private Mock<IDocument> _documentMock;

    [SetUp]
    public void Setup()
    {
        _documentMock = new Mock<IDocument>();
    }

    [Test]
    public void Execute_WhenCanRedo_CallsRedo()
    {
        // Arrange
        _documentMock.Setup( d => d.CanRedo() ).Returns( true );
        RedoCommand command = new RedoCommand( _documentMock.Object );

        // Act
        command.Execute();

        // Assert
        _documentMock.Verify( d => d.Redo(), Times.Once );
    }

    [Test]
    public void Execute_WhenCannotRedo_ThrowsInvalidOperationException()
    {
        // Arrange
        _documentMock.Setup( d => d.CanRedo() ).Returns( false );
        RedoCommand command = new RedoCommand( _documentMock.Object );

        // Act & Assert
        Assert.Throws<InvalidOperationException>( () => command.Execute() );
        _documentMock.Verify( d => d.Redo(), Times.Never );
    }

    [Test]
    public void Unexecute_WhenCanUndo_CallsUndo()
    {
        // Arrange
        _documentMock.Setup( d => d.CanUndo() ).Returns( true );
        _documentMock.Setup( d => d.CanRedo() ).Returns( true );
        RedoCommand command = new RedoCommand( _documentMock.Object );

        // Act
        command.Execute();
        command.Unexecute();

        // Assert
        _documentMock.Verify( d => d.Undo(), Times.Once );
    }

    [Test]
    public void Unexecute_WhenCannotUndo_ThrowsInvalidOperationException()
    {
        // Arrange
        _documentMock.Setup( d => d.CanUndo() ).Returns( false );
        _documentMock.Setup( d => d.CanRedo() ).Returns( true );
        RedoCommand command = new RedoCommand( _documentMock.Object );

        // Act
        command.Execute();

        // Assert
        Assert.Throws<InvalidOperationException>( () => command.Unexecute() );
        _documentMock.Verify( d => d.Undo(), Times.Never );
    }
}