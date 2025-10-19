using DataStream.OutputStream;

namespace DataStream.Decorators;

public abstract class OutputStreamDecorator : IOutputStream
{
    protected IOutputStream _outputStream;

    public OutputStreamDecorator( IOutputStream outputStream )
    {
        _outputStream = outputStream;
    }

    public void Dispose()
    {
        _outputStream.Dispose();
    }

    public abstract void Flush();

    public abstract void WriteBlock( byte[] srcData, int dataSize );

    public abstract void WriteByte( byte data );
}
