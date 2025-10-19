using Editor.Commands;
using Editor.Documents;
using Moq;

namespace EditorTests.Commands.Tests;

[TestFixture]
public class SaveCommandTests
{
    private Mock<IDocument> _documentMock;

    [SetUp]
    public void Setup()
    {
        _documentMock = new Mock<IDocument>();
    }

    [Test]
    public void Execute_CallsSaveWithPath()
    {
        // Arrange
        const string path = "test.html";
        SaveCommand command = new SaveCommand( _documentMock.Object, path );

        // Act
        command.Execute();

        // Assert
        _documentMock.Verify( d => d.Save( path ), Times.Once );
    }

    [Test]
    public void Unexecute_DoesNothing()
    {
        // Arrange
        const string path = "test.html";
        SaveCommand command = new SaveCommand( _documentMock.Object, path );
        command.Execute();

        // Act
        command.Unexecute();

        // Assert
        _documentMock.Verify( d => d.Save( path ), Times.Once );
    }
}