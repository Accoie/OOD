using System;
using System.Collections.Generic;

namespace ShapesApplication.Models.History
{
    public class CommandHistory : IHistory
    {
        private readonly LinkedList<ICommand> _undoDeque = new LinkedList<ICommand>();
        private readonly LinkedList<ICommand> _redoDeque = new LinkedList<ICommand>();
        private readonly int _maxSize;

        public bool CanUndo => _undoDeque.Count > 0;
        public bool CanRedo => _redoDeque.Count > 0;

        public CommandHistory( int maxSize = 10 )
        {
            if ( maxSize <= 0 )
            {
                throw new ArgumentException( "Размер должен быть положительным числом", nameof( maxSize ) );
            }

            _maxSize = maxSize;
        }

        public void AddAndExecuteCommand( ICommand command )
        {
            if ( command is UndoCommand || command is RedoCommand )
            {
                command.Execute();
                return;
            }

            command.Execute();

            _undoDeque.AddLast( command );

            if ( _undoDeque.Count > _maxSize )
            {
                _undoDeque.RemoveFirst();
            }

            _redoDeque.Clear();
        }

        public void Undo()
        {
            if ( !CanUndo )
            {
                throw new InvalidOperationException( "Cannot execute undo action" );
            }

            ICommand lastCommand = _undoDeque.Last.Value;
            _undoDeque.RemoveLast();

            lastCommand.Unexecute();

            _redoDeque.AddLast( lastCommand );

            if ( _redoDeque.Count > _maxSize )
            {
                _redoDeque.RemoveFirst();
            }
        }

        public void Redo()
        {
            if ( !CanRedo )
            {
                throw new InvalidOperationException( "Cannot execute redo action" );
            }

            ICommand lastCommand = _redoDeque.Last.Value;
            _redoDeque.RemoveLast();

            lastCommand.Execute();

            _undoDeque.AddLast( lastCommand );

            if ( _undoDeque.Count > _maxSize )
            {
                _undoDeque.RemoveFirst();
            }
        }
    }
}