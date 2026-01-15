using System;

namespace ShapesApplication.Models.History
{
    public class UndoCommand : ICommand
    {
        private readonly IHistory _history;
        private ICommand _undoneCommand;

        public UndoCommand( IHistory history )
        {
            _history = history ?? throw new ArgumentNullException( nameof( history ) );
        }

        public void Execute()
        {
            if ( !_history.CanUndo )
            {
                throw new InvalidOperationException( "Cannot undo" );
            }

            _history.Undo();
        }

        public void Unexecute()
        {
            if ( _history.CanRedo )
            {
                _history.Redo();
            }
            else
            {
                _undoneCommand?.Execute();
            }
        }
    }
}