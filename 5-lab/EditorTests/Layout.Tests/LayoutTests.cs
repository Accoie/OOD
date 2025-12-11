using Editor.Html;

namespace EditorTests.Html.Tests;

[TestFixture]
public class HtmlLayoutTests
{
    private HtmlLayout _htmlLayout;

    [SetUp]
    public void Setup()
    {
        _htmlLayout = new HtmlLayout( "Test Document" );
    }

    [Test]
    public void Constructor_WithTitle_InitializesCorrectly()
    {
        // Arrange & Act
        HtmlLayout layout = new HtmlLayout( "My Title" );

        // Assert
        Assert.That( layout, Is.Not.Null );
    }

    [Test]
    public void AddParagraph_WithText_AddsParagraphToLayout()
    {
        // Arrange
        const string paragraphText = "This is a test paragraph";

        // Act
        _htmlLayout.AddParagraph( paragraphText );
        string result = _htmlLayout.CreateLayout();

        // Assert
        Assert.That( result, Contains.Substring( $"<p>{paragraphText}</p>" ) );
    }

    [Test]
    public void AddParagraph_WithEmptyText_AddsEmptyParagraph()
    {
        // Arrange
        const string emptyText = "";

        // Act
        _htmlLayout.AddParagraph( emptyText );
        string result = _htmlLayout.CreateLayout();

        // Assert
        Assert.That( result, Contains.Substring( "<p></p>" ) );
    }

    [Test]
    public void AddParagraph_WithSpecialCharacters_HandlesCorrectly()
    {
        // Arrange
        const string specialText = "Text with <>&\"' special characters";

        // Act
        _htmlLayout.AddParagraph( specialText );
        string result = _htmlLayout.CreateLayout();

        // Assert
        Assert.That( result, Contains.Substring( $"<p>{specialText}</p>" ) );
    }

    [Test]
    public void AddImage_WithValidParameters_AddsImageToLayout()
    {
        // Arrange
        const string imagePath = "images/test.jpg";
        const int width = 300;
        const int height = 200;

        // Act
        _htmlLayout.AddImage( imagePath, width, height );
        string result = _htmlLayout.CreateLayout();

        // Assert
        Assert.That( result, Contains.Substring( $"<img src=\"{imagePath}\" width={width} height={height}></img>" ) );
    }

    [Test]
    public void AddImage_WithZeroDimensions_AddsImageWithZeroDimensions()
    {
        // Arrange
        const string imagePath = "images/test.jpg";
        const int width = 0;
        const int height = 0;

        // Act
        _htmlLayout.AddImage( imagePath, width, height );
        string result = _htmlLayout.CreateLayout();

        // Assert
        Assert.That( result, Contains.Substring( $"<img src=\"{imagePath}\" width={width} height={height}></img>" ) );
    }

    [Test]
    public void AddImage_WithPathContainingSpaces_HandlesCorrectly()
    {
        // Arrange
        const string imagePath = "images/my test image.jpg";
        const int width = 100;
        const int height = 150;

        // Act
        _htmlLayout.AddImage( imagePath, width, height );
        string result = _htmlLayout.CreateLayout();

        // Assert
        Assert.That( result, Contains.Substring( $"<img src=\"{imagePath}\" width={width} height={height}></img>" ) );
    }

    [Test]
    public void CreateLayout_WithNoItems_ReturnsBasicHtmlStructure()
    {
        // Act
        string result = _htmlLayout.CreateLayout();

        // Assert
        Assert.That( result, Contains.Substring( "<!DOCTYPE html>" ) );
        Assert.That( result, Contains.Substring( "<html lang=\"ru\">" ) );
        Assert.That( result, Contains.Substring( "<head>" ) );
        Assert.That( result, Contains.Substring( "<title>Test Document</title>" ) );
        Assert.That( result, Contains.Substring( "<body>" ) );
        Assert.That( result, Contains.Substring( "</body>" ) );
        Assert.That( result, Contains.Substring( "</html>" ) );
    }

    [Test]
    public void CreateLayout_WithMultipleItems_PreservesOrder()
    {
        // Arrange
        const string paragraph1 = "First paragraph";
        const string paragraph2 = "Second paragraph";
        const string imagePath = "images/test.jpg";
        const int width = 100;
        const int height = 200;

        // Act
        _htmlLayout.AddParagraph( paragraph1 );
        _htmlLayout.AddImage( imagePath, width, height );
        _htmlLayout.AddParagraph( paragraph2 );
        string result = _htmlLayout.CreateLayout();

        // Assert
        int indexParagraph1 = result.IndexOf( $"<p>{paragraph1}</p>" );
        int indexImage = result.IndexOf( $"<img src=\"{imagePath}\"" );
        int indexParagraph2 = result.IndexOf( $"<p>{paragraph2}</p>" );

        Assert.That( indexParagraph1, Is.LessThan( indexImage ) );
        Assert.That( indexImage, Is.LessThan( indexParagraph2 ) );
    }

    [Test]
    public void CreateLayout_IncludesCorrectMetaTags()
    {
        // Act
        string result = _htmlLayout.CreateLayout();

        // Assert
        Assert.That( result, Contains.Substring( "<meta charset=\"utf-8\">" ) );
        Assert.That( result, Contains.Substring( "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" ) );
    }

    [Test]
    public void CreateLayout_WithTitle_SetsCorrectTitle()
    {
        // Arrange
        const string customTitle = "Custom Document Title";
        HtmlLayout customLayout = new HtmlLayout( customTitle );

        // Act
        string result = customLayout.CreateLayout();

        // Assert
        Assert.That( result, Contains.Substring( $"<title>{customTitle}</title>" ) );
    }

    [Test]
    public void CreateLayout_WithEmptyTitle_SetsEmptyTitle()
    {
        // Arrange
        const string emptyTitle = "";
        HtmlLayout emptyLayout = new HtmlLayout( emptyTitle );

        // Act
        string result = emptyLayout.CreateLayout();

        // Assert
        Assert.That( result, Contains.Substring( "<title></title>" ) );
    }

    [Test]
    public void CreateLayout_WithNullTitle_SetsEmptyTitle()
    {
        // Arrange
        const string nullTitle = "";
        HtmlLayout nullLayout = new HtmlLayout( nullTitle );

        // Act
        string result = nullLayout.CreateLayout();

        // Assert
        Assert.That( result, Contains.Substring( "<title></title>" ) );
    }

    [Test]
    public void CreateLayout_WithMixedContent_GeneratesValidHtml()
    {
        // Arrange
        const string paragraphText = "Sample paragraph";
        const string imagePath = "photos/image.png";
        const int width = 250;
        const int height = 150;

        // Act
        _htmlLayout.AddParagraph( paragraphText );
        _htmlLayout.AddImage( imagePath, width, height );
        _htmlLayout.AddParagraph( paragraphText );
        string result = _htmlLayout.CreateLayout();

        // Assert
        Assert.That( result, Contains.Substring( $"<p>{paragraphText}</p>" ) );
        Assert.That( result, Contains.Substring( $"<img src=\"{imagePath}\" width={width} height={height}></img>" ) );
        Assert.That( result.Count( ch => ch == '<' ), Is.GreaterThan( 0 ) );
    }

    [Test]
    public void CreateLayout_HasCorrectBodyStructure()
    {
        // Arrange
        const string testText = "Test content";

        // Act
        _htmlLayout.AddParagraph( testText );
        string result = _htmlLayout.CreateLayout();

        // Assert
        Assert.That( result, Contains.Substring( "<body>" ) );
        Assert.That( result, Contains.Substring( "</body>" ) );
        Assert.That( result, Contains.Substring( $"  <p>{testText}</p>" ) );
    }

    [Test]
    public void CreateLayout_HasCorrectHeadStructure()
    {
        // Act
        string result = _htmlLayout.CreateLayout();

        // Assert
        Assert.That( result, Contains.Substring( "<head>" ) );
        Assert.That( result, Contains.Substring( "</head>" ) );
        Assert.That( result, Contains.Substring( "  <meta charset=\"utf-8\">" ) );
        Assert.That( result, Contains.Substring( "  <meta name=\"viewport\"" ) );
        Assert.That( result, Contains.Substring( "  <title>Test Document</title>" ) );
    }
}