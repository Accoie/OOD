using DataStream.Decorators.Common;
using DataStream.InputStream;

namespace DataStream.Decorators;

public class InputDecrypt : InputStreamDecorator
{
    private readonly IReadOnlyDictionary<byte, byte> _decryptTable;

    public InputDecrypt( IInputStream inputStream, int key ) : base( inputStream )
    {
        _decryptTable = ReplaceTableCreator.CreateDecryptTable( key );
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
        return _decryptTable[ _inputStream.ReadByte() ];
    }
}