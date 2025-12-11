using Editor.Commands;
using Editor.Documents;
using Editor.Documents.Items;
using Editor.Paragraph;
using Moq;
using NUnit.Framework;
using System;

namespace EditorTests.Commands.Tests;

[TestFixture]
public class ReplaceTextCommandTests
{
    private Mock<IDocument> _documentMock;
    private Mock<IParagraph> _paragraphMock;

    [SetUp]
    public void Setup()
    {
        _documentMock = new Mock<IDocument>();
        _paragraphMock = new Mock<IParagraph>();
    }

    [Test]
    public void Execute_ValidPositionAndParagraph_ReplacesText()
    {
        // Arrange
        const int position = 1;
        const string newText = "New text";
        const string previousText = "Previous text";

        var documentItem = new DocumentItem( _paragraphMock.Object );
        _paragraphMock.Setup( p => p.Text ).Returns( previousText );

        _documentMock.Setup( d => d.GetItemsCount() ).Returns( 2 );
        _documentMock.Setup( d => d.GetItem( position ) ).Returns( documentItem );

        ReplaceTextCommand command = new ReplaceTextCommand( _documentMock.Object, newText, position );

        // Act
        command.Execute();

        // Assert
        _paragraphMock.Verify( p => p.SetText( newText ), Times.Once );
    }

    [Test]
    public void Execute_PositionOutOfRange_ThrowsArgumentException()
    {
        // Arrange
        const int position = 5;
        const string newText = "New text";

        _documentMock.Setup( d => d.GetItemsCount() ).Returns( 2 );

        ReplaceTextCommand command = new ReplaceTextCommand( _documentMock.Object, newText, position );

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>( () => command.Execute() );
    }

    [Test]
    public void Execute_ItemIsNotParagraph_ThrowsInvalidOperationException()
    {
        // Arrange
        const int position = 1;
        const string newText = "New text";

        var imageMock = new Mock<Editor.Image.IImage>();
        var documentItem = new DocumentItem( imageMock.Object );

        _documentMock.Setup( d => d.GetItemsCount() ).Returns( 2 );
        _documentMock.Setup( d => d.GetItem( position ) ).Returns( documentItem );

        ReplaceTextCommand command = new ReplaceTextCommand( _documentMock.Object, newText, position );

        // Act & Assert
        Assert.Throws<InvalidOperationException>( () => command.Execute() );
    }

    [Test]
    public void Unexecute_AfterExecute_RestoresPreviousText()
    {
        // Arrange
        const int position = 1;
        const string newText = "New text";
        const string previousText = "Previous text";

        var documentItem = new DocumentItem( _paragraphMock.Object );
        _paragraphMock.Setup( p => p.Text ).Returns( previousText );

        _documentMock.Setup( d => d.GetItemsCount() ).Returns( 2 );
        _documentMock.Setup( d => d.GetItem( position ) ).Returns( documentItem );

        ReplaceTextCommand command = new ReplaceTextCommand( _documentMock.Object, newText, position );
        command.Execute();

        // Act
        command.Unexecute();

        // Assert
        _paragraphMock.Verify( p => p.SetText( previousText ), Times.Once );
    }

    [Test]
    public void Execute_Unexecute_Sequence_WorksCorrectly()
    {
        // Arrange
        const int position = 1;
        const string newText = "New text";
        const string previousText = "Previous text";

        var documentItem = new DocumentItem( _paragraphMock.Object );
        _paragraphMock.Setup( p => p.Text ).Returns( previousText );

        _documentMock.Setup( d => d.GetItemsCount() ).Returns( 2 );
        _documentMock.Setup( d => d.GetItem( position ) ).Returns( documentItem );

        ReplaceTextCommand command = new ReplaceTextCommand( _documentMock.Object, newText, position );

        // Act & Assert
        Assert.DoesNotThrow( () =>
        {
            command.Execute();
            command.Unexecute();
        } );

        _paragraphMock.Verify( p => p.SetText( newText ), Times.Once );
        _paragraphMock.Verify( p => p.SetText( previousText ), Times.Once );
    }

    [Test]
    public void PositionProperty_ReturnsCorrectValue()
    {
        // Arrange
        const int position = 2;
        const string text = "Test text";

        ReplaceTextCommand command = new ReplaceTextCommand( _documentMock.Object, text, position );

        // Act & Assert
        Assert.That( command.Position, Is.EqualTo( position ) );
    }
}