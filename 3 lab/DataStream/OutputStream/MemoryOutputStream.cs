namespace DataStream.OutputStream;

public class MemoryOutputStream : IOutputStream
{
    private byte[] _buffer;
    private int _position = 0;
    private bool _disposed = false;

    public MemoryOutputStream( byte[] buffer )
    {
        _buffer = ( byte[] )buffer.Clone();
    }

    public void Dispose()
    {
        _disposed = true;
    }

    public void Flush()
    {
    }

    public void WriteBlock( byte[] srcData, int dataSize )
    {
        if ( _disposed )
        {
            throw new ObjectDisposedException( "Stream is disposed!" );
        }

        for ( int i = 0; i < dataSize; i++ )
        {
            WriteByte( srcData[ i ] );
        }
    }

    public void WriteByte( byte data )
    {
        if ( _disposed )
        {
            throw new ObjectDisposedException( "Stream is disposed!" );
        }

        if ( _position >= _buffer.Length )
        {
            throw new Exception( "Not enought space in memory!" );
        }

        _buffer[ _position++ ] = data;
    }
}
