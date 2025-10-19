namespace DataStream.InputStream;

public class FileInputStream : IInputStream
{
    private readonly byte[] _buffer = [];
    private int _position;

    public FileInputStream( string path )
    {
        if ( string.IsNullOrEmpty( path ) )
        {
            throw new ArgumentException( "Path cannot be null or empty", nameof( path ) );
        }

        if ( !File.Exists( path ) )
        {
            throw new FileNotFoundException( $"File not found: {path}" );
        }

        using FileStream file = File.OpenRead( path );

        _buffer = File.ReadAllBytes( path );
    }

    public bool IsEof()
    {
        return _position >= _buffer.Length;
    }

    public int ReadBlock( byte[] dstData, int dataSize )
    {
        int bytesRead = 0;

        for ( ; bytesRead < dataSize; bytesRead++ )
        {
            dstData[ bytesRead ] = ReadByte();
        }

        return bytesRead;
    }

    public byte ReadByte()
    {
        if ( IsEof() )
        {
            throw new EndOfStreamException( "End of file!" );
        }

        return _buffer[ _position++ ];
    }
}