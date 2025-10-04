namespace DataStream.InputStream;

public class MemoryInputStream : IInputStream
{
    private readonly byte[] memory;
    private int currentIndex = 0;

    public MemoryInputStream( byte[] memory )
    {
        this.memory = ( byte[] )memory.Clone();
    }

    public bool IsEof()
    {
        return currentIndex >= memory.Length;
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
            throw new EndOfStreamException( "End of stream!" );
        }

        return memory[ currentIndex++ ];
    }
}