using Editor.Image;
using Editor.Paragraph;

namespace Editor.Documents.Items;

public class DocumentItem
{
    public IParagraph? Paragraph { get; }
    public IImage? Image { get; }

    public DocumentItem( IParagraph paragraph )
    {
        Paragraph = paragraph;
    }

    public DocumentItem( IImage image )
    {
        Image = image;
    }
}
