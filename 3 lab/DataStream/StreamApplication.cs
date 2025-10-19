using DataStream.InputStream;
using DataStream.OutputStream;

namespace DataStream;

public class StreamApplication
{
    private const int _minimalCountArgs = 3;

    public void Run( string[] args )
    {
        if ( args.Length < _minimalCountArgs )
        {
            Console.WriteLine( $"Minimal count of parameters - {_minimalCountArgs}\n" +
                "Usage: <options> <input file> <output file>\n" +
                "Example: --encrypt 3 --encrypt 100500 --compress input.dat output.dat\n" );

            return;
        }

        try
        {
            string fileInputPath = args[ ^2 ];
            string fileOutputPath = args[ ^1 ];

            IInputStream inputStream = new FileInputStream( fileInputPath );
            using IOutputStream outputStream = new FileOutputStream( fileOutputPath );

            string[] options = args.SkipLast( 2 ).ToArray();

            CommandHandler commandHandler = new( inputStream, outputStream );
            commandHandler.HandleOptions( options );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }
}