using Editor.Commands;

namespace Editor.History
{
    public class CommandHistory : ICommandHistory
    {
        private readonly List<ICommand> _commands = [];
        private int _currentActionIndex = -1;

        public void AddAndExecuteCommand( ICommand command )
        {
            if ( _currentActionIndex < _commands.Count - 1 )
            {
                _commands.RemoveRange( _currentActionIndex + 1, _commands.Count - _currentActionIndex - 1 );
            }

            _commands.Add( command );
            _currentActionIndex = _commands.Count - 1;

            command.Execute();
        }

        public bool CanRedo()
        {
            return _currentActionIndex < _commands.Count - 1;
        }

        public bool CanUndo()
        {
            return _currentActionIndex >= 0;
        }

        public void Redo()
        {
            if ( !CanRedo() )
            {
                throw new InvalidOperationException( "Can not execute redo action" );
            }

            _currentActionIndex++;
            _commands[ _currentActionIndex ].Execute();
        }

        public void Undo()
        {
            if ( !CanUndo() )
            {
                throw new InvalidOperationException( "Can not execute undo action" );
            }

            _commands[ _currentActionIndex ].Unexecute();
            _currentActionIndex--;
        }
    }
}