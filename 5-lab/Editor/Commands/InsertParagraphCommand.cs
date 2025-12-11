using Editor.Documents;

namespace Editor.Commands;

public class InsertParagraphCommand : AbstractCommand
{
    private readonly string _text;
    private readonly int? _position;

    public InsertParagraphCommand( IDocument document, string text, int? position = null ) : base( document )
    {
        _text = text;
        _position = position;
    }

    protected override void DoExecute()
    {
        _document.InsertParagraph( _text, _position );
    }

    protected override void DoUnexecute()
    {
        _document.DeleteItem( _position );
    }
}