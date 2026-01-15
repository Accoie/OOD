using System;

namespace ShapesApplication.Models.History
{
    public class RedoCommand : ICommand
    {
        private readonly IHistory _history;

        public RedoCommand( IHistory history )
        {
            _history = history ?? throw new ArgumentNullException( nameof( history ) );
        }

        public void Execute()
        {
            if ( !_history.CanRedo )
            {
                throw new InvalidOperationException( "Cannot redo" );
            }

            _history.Redo();
        }

        public void Unexecute()
        {
            if ( _history.CanUndo )
            {
                _history.Undo();
            }
        }
    }
}