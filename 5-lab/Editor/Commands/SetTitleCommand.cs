using Editor.Documents;

namespace Editor.Commands;

public class SetTitleCommand : AbstractCommand
{
    private readonly string _title;
    private string _previousTitle = "";

    public SetTitleCommand( IDocument document, string title ) : base( document )
    {
        _title = title;
    }

    protected override void DoExecute()
    {
        _previousTitle = _document.GetTitle();
        _document.SetTitle( _title );
    }

    protected override void DoUnexecute()
    {
        _document.SetTitle( _previousTitle );
    }
}
