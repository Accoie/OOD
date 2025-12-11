using Editor.Documents;

namespace Editor.Commands;

public abstract class AbstractCommand : ICommand
{
    private bool _execute = false;

    protected readonly IDocument _document;

    protected AbstractCommand( IDocument document )
    {
        _document = document;
    }

    public void Execute()
    {
        if ( _execute )
        {
            return;
        }
        _execute = !_execute;

        DoExecute();
    }

    public void Unexecute()
    {
        if ( _execute )
        {
            DoUnexecute();
            _execute = false;
        }
    }

    protected abstract void DoExecute();
    protected abstract void DoUnexecute();
}
