using Editor.Documents;
using Editor.Documents.Items;
using Editor.Image;
using Editor.Paragraph;

namespace Editor.Commands;

public class ListCommand : AbstractCommand
{
    public ListCommand( IDocument document ) : base( document )
    {
    }

    protected override void DoExecute()
    {
        Console.WriteLine( $"Title: {_document.GetTitle()}" );

        for ( int i = 0; i < _document.GetItemsCount(); i++ )
        {
            DocumentItem item = _document.GetItem( i );

            if ( item?.Image is not null )
            {
                IImage image = item.Image;
                Console.WriteLine( $"{i}. Image: {image.Width} {image.Height} {image.Path}" );
            }

            if ( item?.Paragraph is not null )
            {
                IParagraph paragraph = item.Paragraph;
                Console.WriteLine( $"{i}. Paragraph: {paragraph.Text}" );
            }
        }
    }

    protected override void DoUnexecute()
    {
    }
}