using DataStream.Decorators;
using DataStream.InputStream;
using DataStream.OutputStream;

namespace DataStream;

public class CommandHandler
{
    private IInputStream _inputStream;
    private IOutputStream _outputStream;

    public CommandHandler( IInputStream inputStream, IOutputStream outputStream )
    {
        _inputStream = inputStream;
        _outputStream = outputStream;
    }

    public void HandleOptions( string[] options )
    {
        List<Func<IInputStream, IInputStream>> inputDecorators = new();
        List<Func<IOutputStream, IOutputStream>> outputDecorators = new();

        int optionsRead = 0;

        while ( optionsRead < options.Length )
        {
            switch ( options[ optionsRead++ ] )
            {
                case "--encrypt":
                    {
                        if ( optionsRead >= options.Length )
                        {
                            throw new Exception( "There is not key for encrypt" );
                        }

                        int key = TryParseKey( options[ optionsRead++ ] );
                        outputDecorators.Add( stream => new OutputEncrypt( stream, key ) );
                        break;
                    }
                case "--decrypt":
                    {
                        if ( optionsRead >= options.Length )
                        {
                            throw new Exception( "There is not key for decrypt" );
                        }

                        int key = TryParseKey( options[ optionsRead++ ] );
                        inputDecorators.Add( stream => new InputDecrypt( stream, key ) );
                        break;
                    }
                case "--compress":
                    {
                        outputDecorators.Add( stream => new OutputCompress( stream ) );
                        break;
                    }
                case "--decompress":
                    {
                        inputDecorators.Add( stream => new InputDecompress( stream ) );
                        break;
                    }
                default:
                    throw new ArgumentException( "Unknown option!" );
            }
        }

        for ( int i = inputDecorators.Count - 1; i >= 0; i-- )
        {
            _inputStream = inputDecorators[ i ]( _inputStream );
        }

        foreach ( Func<IOutputStream, IOutputStream> decorator in outputDecorators )
        {
            _outputStream = decorator( _outputStream );
        }

        WriteOutput();
    }

    private void WriteOutput() // сделатть по байтам
    {
        while ( !_inputStream.IsEof() )
        {
            byte data = _inputStream.ReadByte();
            _outputStream.WriteByte( data );
        }

        _outputStream.Flush();
    }

    private int TryParseKey( string keyStr )
    {
        if ( !int.TryParse( keyStr, out int key ) )
        {
            throw new FormatException( "Invalid key!" );
        }

        return key;
    }
}