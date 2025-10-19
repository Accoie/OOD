using Editor.Documents.Items;
using Editor.Image;
using Editor.Paragraph;
using Moq;
using NUnit.Framework;

namespace EditorTests.Documents.Items.Tests;

[TestFixture]
public class DocumentItemTests
{
    [Test]
    public void Constructor_WithParagraph_SetsParagraphProperty()
    {
        // Arrange
        Mock<IParagraph> paragraphMock = new Mock<IParagraph>();

        // Act
        DocumentItem documentItem = new DocumentItem( paragraphMock.Object );

        // Assert
        Assert.That( documentItem.Paragraph, Is.EqualTo( paragraphMock.Object ) );
        Assert.That( documentItem.Image, Is.Null );
    }

    [Test]
    public void Constructor_WithImage_SetsImageProperty()
    {
        // Arrange
        Mock<IImage> imageMock = new Mock<IImage>();

        // Act
        DocumentItem documentItem = new DocumentItem( imageMock.Object );

        // Assert
        Assert.That( documentItem.Image, Is.EqualTo( imageMock.Object ) );
        Assert.That( documentItem.Paragraph, Is.Null );
    }

    [Test]
    public void DocumentItem_WithParagraph_HasCorrectType()
    {
        // Arrange
        Mock<IParagraph> paragraphMock = new Mock<IParagraph>();
        DocumentItem documentItem = new DocumentItem( paragraphMock.Object );

        // Act & Assert
        Assert.That( documentItem.Paragraph, Is.Not.Null );
        Assert.That( documentItem.Image, Is.Null );
    }

    [Test]
    public void DocumentItem_WithImage_HasCorrectType()
    {
        // Arrange
        Mock<IImage> imageMock = new Mock<IImage>();
        DocumentItem documentItem = new DocumentItem( imageMock.Object );

        // Act & Assert
        Assert.That( documentItem.Image, Is.Not.Null );
        Assert.That( documentItem.Paragraph, Is.Null );
    }
}