using Editor.Image;

namespace Editor.Html;

public class HtmlLayout
{
    private List<string> _layoutItems;
    private string _title;

    public HtmlLayout( string title )
    {
        _title = title;
        _layoutItems = [];
    }

    public void AddParagraph( string text )
    {
        string paragraphText = $"<p>{text}</p>";

        _layoutItems.Add( paragraphText );
    }

    public void AddImage( string path, int width, int height )
    {
        string imageText = $"<img src=\"{path}\" width={width} height={height}></img>";

        _layoutItems.Add( imageText );
    }

    public string CreateLayout()
    {
        string layoutText = "";

        layoutText += "<!DOCTYPE html>\n";
        layoutText += "<html lang=\"ru\">\n";

        layoutText += CreateHead();
        layoutText += CreateBody();

        layoutText += "</html>";

        return layoutText;
    }

    private string CreateBody()
    {
        string bodyText = "<body>\n";

        foreach ( string item in _layoutItems )
        {
            bodyText += $"  {item}\n";
        }

        bodyText += "</body>\n";

        return bodyText;
    }

    private string CreateHead()
    {
        return "<head>\n" +
       "  <meta charset=\"utf-8\">\n" +
       "  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n" +
       $"  <title>{_title}</title>\n" +
       "</head>";
    }
}