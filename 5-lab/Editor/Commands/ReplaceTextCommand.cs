using Editor.Documents;
using Editor.Documents.Items;
using Editor.Paragraph;

namespace Editor.Commands;

public class ReplaceTextCommand : AbstractCommand
{
    public int Position { get; }

    private readonly string _text;
    private string _previousText = "";

    public ReplaceTextCommand( IDocument document, string text, int position ) : base( document )
    {
        _text = text;
        Position = position;
    }

    protected override void DoExecute()
    {
        IParagraph paragraph = GetParagraphAtPosition();
        _previousText = paragraph.Text;
        paragraph.SetText( _text );
    }

    protected override void DoUnexecute()
    {
        IParagraph paragraph = GetParagraphAtPosition();
        paragraph.SetText( _previousText );
    }

    private IParagraph GetParagraphAtPosition()
    {
        if ( _document.GetItemsCount() <= Position )
        {
            throw new ArgumentOutOfRangeException( "Incorrect item position" );
        }

        DocumentItem documentItem = _document.GetItem( Position );

        if ( documentItem.Paragraph is null )
        {
            throw new InvalidOperationException( "Item in the position is not a paragraph" );
        }

        return documentItem.Paragraph;
    }
}
