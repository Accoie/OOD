using Editor.Documents;

namespace Editor.Commands;

public class SaveCommand : AbstractCommand
{
    private readonly string _path;

    public SaveCommand( IDocument document, string path ) : base( document )
    {
        _path = path;
    }

    protected override void DoExecute()
    {
        _document.Save( _path );
    }

    protected override void DoUnexecute()
    {
    }
}
