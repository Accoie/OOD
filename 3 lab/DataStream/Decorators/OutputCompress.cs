using DataStream.Decorators.Common;
using DataStream.OutputStream;

namespace DataStream.Decorators;

public class OutputCompress : OutputStreamDecorator
{
    private readonly CompressedData _compressedData = new( 0, 0 );

    public OutputCompress( IOutputStream outputStream ) : base( outputStream )
    {
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
        if ( _compressedData.Size == 0 )
        {
            _compressedData.Data = data;
            _compressedData.Size = 1;
            return;
        }

        if ( _compressedData.Data == data && _compressedData.Size < byte.MaxValue )
        {
            _compressedData.Size++;
        }
        else
        {
            Flush();

            _compressedData.Data = data;
            _compressedData.Size = 1;
        }
    }

    public override void Flush()
    {
        if ( _compressedData.Size > 0 )
        {
            _outputStream.WriteByte( _compressedData.Size );
            _outputStream.WriteByte( _compressedData.Data );
            _compressedData.Size = 0;
        }
    }
}