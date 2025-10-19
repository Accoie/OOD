using Editor.Commands;
using Editor.Documents;
using Moq;
using NUnit.Framework;

namespace EditorTests.Commands.Tests;

[TestFixture]
public class SetTitleCommandTests
{
    private Mock<IDocument> _documentMock;

    [SetUp]
    public void Setup()
    {
        _documentMock = new Mock<IDocument>();
    }

    [Test]
    public void Execute_SetsNewTitle()
    {
        // Arrange
        const string newTitle = "New Title";
        const string previousTitle = "Previous Title";

        _documentMock.Setup( d => d.GetTitle() ).Returns( previousTitle );

        SetTitleCommand command = new SetTitleCommand( _documentMock.Object, newTitle );

        // Act
        command.Execute();

        // Assert
        _documentMock.Verify( d => d.SetTitle( newTitle ), Times.Once );
    }

    [Test]
    public void Unexecute_AfterExecute_RestoresPreviousTitle()
    {
        // Arrange
        const string newTitle = "New Title";
        const string previousTitle = "Previous Title";

        _documentMock.Setup( d => d.GetTitle() ).Returns( previousTitle );

        SetTitleCommand command = new SetTitleCommand( _documentMock.Object, newTitle );
        command.Execute();

        // Act
        command.Unexecute();

        // Assert
        _documentMock.Verify( d => d.SetTitle( previousTitle ), Times.Once );
    }

    [Test]
    public void Execute_Unexecute_Sequence_WorksCorrectly()
    {
        // Arrange
        const string newTitle = "New Title";
        const string previousTitle = "Previous Title";

        _documentMock.Setup( d => d.GetTitle() ).Returns( previousTitle );

        SetTitleCommand command = new SetTitleCommand( _documentMock.Object, newTitle );

        // Act & Assert
        Assert.DoesNotThrow( () =>
        {
            command.Execute();
            command.Unexecute();
        } );

        _documentMock.Verify( d => d.SetTitle( newTitle ), Times.Once );
        _documentMock.Verify( d => d.SetTitle( previousTitle ), Times.Once );
    }
}