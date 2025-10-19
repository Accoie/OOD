using Editor.Commands;
using Editor.Documents;
using Editor.Factory;
using Editor.History;

namespace Editor.Client;

public class EditorApplication
{
    private CommandHistory _history;
    private IDocument _document;
    private CommandFactory _commandFactory;

    private readonly IReadOnlyDictionary<string, string> _helpCommands = new Dictionary<string, string>()
    {
        { "InsertParagraph", "<позиция>|end <текст параграфа>" },
        { "InsertImage", "<позиция>|end <ширинаfu> <высота> <путь к файлу изображения>" },
        { "SetTitle", "<заголовок документа>" },
        { "List", "" },
        { "ReplaceText", "<позиция> <текст параграфа>" },
        { "ResizeImage", "<позиция> <ширина> <высота>" },
        { "DeleteItem", "<позиция>" },
        { "Help", "" },
        { "Undo", "" },
        { "Redo", "" },
        { "Save", "<путь/файл.html>" }
    };

    public EditorApplication()
    {
        _history = new CommandHistory();
        _document = new Document( ( ICommandHistoryUser )_history, "Document" );
        _commandFactory = new CommandFactory( _helpCommands.ToDictionary(), _document );
    }

    public void Run()
    {
        DisplayHelp();
        while ( true )
        {
            try
            {
                string commandStr = Console.ReadLine() ?? "";
                if ( commandStr.ToLower().Trim() == "exit" )
                {
                    return;
                }

                ICommand command = _commandFactory.CreateCommand( commandStr );

                _history.AddAndExecuteCommand( command );

                Console.WriteLine( "Success!\n" );
            }
            catch ( Exception ex )
            {
                Console.WriteLine( ex.Message );
            }
        }
    }

    private void DisplayHelp()
    {
        new HelpCommand( _helpCommands.ToDictionary() ).Execute();
        Console.WriteLine( "\n" );
    }
}
