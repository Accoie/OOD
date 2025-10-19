using Editor.Commands;
using Editor.Documents;
using Moq;

namespace EditorTests.Commands.Tests;

[TestFixture]
public class InsertParagraphCommandTests
{
    private Mock<IDocument> _documentMock;

    [SetUp]
    public void Setup()
    {
        _documentMock = new Mock<IDocument>();
    }

    [Test]
    public void Execute_CallsInsertParagraph()
    {
        // Arrange
        const string text = "Test paragraph text";
        const int position = 1;

        InsertParagraphCommand command = new InsertParagraphCommand( _documentMock.Object, text, position );

        // Act
        command.Execute();

        // Assert
        _documentMock.Verify( d => d.InsertParagraph( text, position ), Times.Once );
    }

    [Test]
    public void Unexecute_AfterExecute_CallsDeleteItem()
    {
        // Arrange
        const string text = "Test paragraph text";
        const int position = 1;

        InsertParagraphCommand command = new InsertParagraphCommand( _documentMock.Object, text, position );
        command.Execute();

        // Act
        command.Unexecute();

        // Assert
        _documentMock.Verify( d => d.DeleteItem( position ), Times.Once );
    }
}