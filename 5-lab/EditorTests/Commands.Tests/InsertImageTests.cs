using Editor.Commands;
using Editor.Documents;
using Moq;

namespace EditorTests.Commands.Tests;

[TestFixture]
public class InsertImageCommandTests
{
    private Mock<IDocument> _documentMock;

    [SetUp]
    public void Setup()
    {
        _documentMock = new Mock<IDocument>();
    }

    [Test]
    public void Execute_CallsInsertImage()
    {
        // Arrange
        const string path = "test.jpg";
        const int width = 100;
        const int height = 200;
        const int position = 1;

        InsertImageCommand command = new InsertImageCommand( _documentMock.Object, path, width, height, position );

        // Act
        command.Execute();

        // Assert
        _documentMock.Verify( d => d.InsertImage( path, width, height, position ), Times.Once );
    }

    [Test]
    public void Unexecute_AfterExecute_CallsDeleteItem()
    {
        // Arrange
        const string path = "test.jpg";
        const int width = 100;
        const int height = 200;
        const int position = 1;

        InsertImageCommand command = new InsertImageCommand( _documentMock.Object, path, width, height, position );
        command.Execute();

        // Act
        command.Unexecute();

        // Assert
        _documentMock.Verify( d => d.DeleteItem( position ), Times.Once );
    }
}