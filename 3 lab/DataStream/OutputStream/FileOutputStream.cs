namespace DataStream.OutputStream;

public class FileOutputStream : IOutputStream
{
    private FileStream _file;
    private bool _disposed;

    public FileOutputStream( string path )
    {
        if ( string.IsNullOrEmpty( path ) )
        {
            throw new ArgumentException( "Path cannot be null or empty", nameof( path ) );
        }

        _file = File.Create( path );
    }

    public void WriteBlock( byte[] srcData, int dataSize )
    {
        ThrowIfDisposed();

        for ( int i = 0; i < dataSize; i++ )
        {
            _file.WriteByte( srcData[ i ] );
        }
    }

    public void WriteByte( byte data )
    {
        ThrowIfDisposed();
        Console.WriteLine( data );
        _file.WriteByte( data );
    }

    public void Dispose()
    {
        _file.Dispose();
        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        if ( _disposed )
        {
            throw new ObjectDisposedException( "Stream is closed!" );
        }
    }

    public void Flush()
    {
        _file.Flush();
    }
}
