using Editor.Documents;

namespace Editor.Commands;

public class UndoCommand : AbstractCommand
{
    public UndoCommand( IDocument document ) : base( document )
    {
    }

    protected override void DoExecute()
    {
        if ( _document.CanUndo() )
        {
            _document.Undo();
        }
        else
        {
            throw new InvalidOperationException( "Cannot undo command from document" );
        }

    }

    protected override void DoUnexecute()
    {
        if ( _document.CanRedo() )
        {
            _document.Redo();
        }
        else
        {
            throw new InvalidOperationException( "Cannot redo command from document" );
        }
    }
}
