namespace Editor.Paragraph;

public class ParagraphItem : IParagraph
{
    public string Text { get; private set; }

    public ParagraphItem( string text )
    {
        Text = text;
    }

    public void SetText( string text )
    {
        Text = text;
    }
}
