using DataStream.InputStream;

namespace DataStream.Decorators;

public class InputDecompress : InputStreamDecorator
{
    private byte _currentByte;
    private int _remainingCount = 0;

    public InputDecompress( IInputStream inputStream ) : base( inputStream )
    {
    }

    public override bool IsEof()
    {
        if ( _remainingCount == 0 )
        {
            return base.IsEof();
        }

        return false;
    }

    public override int ReadBlock( byte[] dstData, int dataSize )
    {
        int bytesRead = 0;

        for ( ; bytesRead < dataSize; bytesRead++ )
        {
            dstData[ bytesRead ] = ReadByte();
        }

        return bytesRead;
    }

    public override byte ReadByte()
    {
        if ( _remainingCount > 0 )
        {
            _remainingCount--;

            return _currentByte;
        }

        _remainingCount = _inputStream.ReadByte() - 1;
        _currentByte = _inputStream.ReadByte();

        return _currentByte;
    }
}