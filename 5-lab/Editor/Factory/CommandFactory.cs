using System.Drawing;
using Editor.Commands;
using Editor.Documents;
using Editor.Parser;

namespace Editor.Factory;

public class CommandFactory
{
    private CommandFactoryParser _parser;
    private Dictionary<string, string> _helpCommands;
    private IDocument _document;
    private ICommand _lastCommand;

    public CommandFactory( Dictionary<string, string> helpCommands, IDocument document )
    {
        _helpCommands = helpCommands;
        _document = document;
        _parser = new CommandFactoryParser( document );
        _lastCommand = new HelpCommand( _helpCommands );
    }

    public ICommand CreateCommand( string descr )
    {
        string[] options = descr.Split( ' ', StringSplitOptions.RemoveEmptyEntries );
        if ( options.Length <= 0 )
        {
            throw new ArgumentException( "Command options length cannot be less than 0" );
        }

        _lastCommand = HandleCreateCommand( options );

        return _lastCommand;
    }

    private ICommand HandleCreateCommand( string[] options )
    {
        string cmdName = options[ 0 ];

        string[] optionsToParse = options.Skip( 1 ).ToArray();

        return cmdName.ToLower() switch
        {
            "insertparagraph" => _parser.ParseInsertParagraphCommand( optionsToParse ),
            "insertimage" => _parser.ParseInsertImageCommand( optionsToParse ),
            "settitle" => CreateSetTitleOrCompoundCommand( options ),
            "list" => new ListCommand( _document ),
            "replacetext" => CreateReplaceTextOrCompoundCommand( optionsToParse ),
            "resizeimage" => CreateResizeImageOrCompoundCommand( optionsToParse ),
            "deleteitem" => _parser.ParseDeleteItemCommand( optionsToParse ),
            "help" => new HelpCommand( _helpCommands ),
            "undo" => new UndoCommand( _document ),
            "redo" => new RedoCommand( _document ),
            "save" => _parser.ParseSaveCommand( optionsToParse ),
            _ => throw new ArgumentException( $"Unknown command: {cmdName}" ),
        };
    }

    private ICommand CreateSetTitleOrCompoundCommand( string[] options )
    {
        SetTitleCommand command = _parser.ParseSetTitleCommand( options );

        bool canCompound = _lastCommand is CompoundCommand lastCmd && lastCmd.Commands.Last() is SetTitleCommand;

        if ( _lastCommand is SetTitleCommand || canCompound )
        {
            return new CompoundCommand( [ _lastCommand, command ] );
        }

        return command;
    }

    private ICommand CreateReplaceTextOrCompoundCommand( string[] options )
    {
        ReplaceTextCommand command = _parser.ParseReplaceTextCommand( options );

        if ( _lastCommand is ReplaceTextCommand lastCommand )
        {
            return EqualReplaceTextCommands( lastCommand, command ) ?
                new CompoundCommand( [ lastCommand, command ] )
                : command;
        }
        else if ( _lastCommand is CompoundCommand lastCmd && lastCmd.Commands.Last() is ReplaceTextCommand cmd )
        {
            return EqualReplaceTextCommands( cmd, command ) ?
            new CompoundCommand( [ _lastCommand, command ] )
            : command;

        }

        return command;
    }

    private ICommand CreateResizeImageOrCompoundCommand( string[] options )
    {
        ResizeImageCommand command = _parser.ParseResizeImageCommand( options );

        if ( _lastCommand is ResizeImageCommand lastCommand )
        {
            return EqualResizeImageCommands( lastCommand, command ) ?
                new CompoundCommand( [ lastCommand, command ] )
                : command;
        }
        else if ( _lastCommand is CompoundCommand lastCmd && lastCmd.Commands.Last() is ResizeImageCommand cmd )
        {
            return EqualResizeImageCommands( cmd, command ) ?
            new CompoundCommand( [ _lastCommand, command ] )
            : command;
        }

        return command;
    }

    private bool EqualReplaceTextCommands( ReplaceTextCommand cmd1, ReplaceTextCommand cmd2 )
    {
        return cmd1.Position == cmd2.Position;

    }

    private bool EqualResizeImageCommands( ResizeImageCommand cmd1, ResizeImageCommand cmd2 )
    {
        return cmd1.Position == cmd2.Position;
    }
}
