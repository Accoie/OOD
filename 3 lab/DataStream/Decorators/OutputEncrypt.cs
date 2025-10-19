using DataStream.Decorators.Common;
using DataStream.OutputStream;

namespace DataStream.Decorators;

public class OutputEncrypt : OutputStreamDecorator
{
    private readonly IReadOnlyDictionary<byte, byte> _encryptTable;

    public OutputEncrypt( IOutputStream outputStream, int key ) : base( outputStream )
    {
        _encryptTable = ReplaceTableCreator.CreateEncryptTable( key );
    }

    public override void Flush()
    {
        _outputStream.Flush();
    }

    public override void WriteBlock( byte[] srcData, int dataSize )
    {
        for ( int i = 0; i < dataSize; i++ )
        {
            WriteByte( srcData[ i ] );
        }

        Flush();
    }

    public override void WriteByte( byte data )
    {
        byte encryptedByte = _encryptTable[ data ];

        _outputStream.WriteByte( encryptedByte );
    }
}