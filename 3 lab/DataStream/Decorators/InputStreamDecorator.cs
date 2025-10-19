using DataStream.InputStream;

namespace DataStream.Decorators;

public abstract class InputStreamDecorator : IInputStream
{
    protected IInputStream _inputStream;

    public InputStreamDecorator( IInputStream inputStream )
    {
        _inputStream = inputStream;
    }

    public virtual bool IsEof()
    {
        return _inputStream.IsEof();
    }

    public abstract int ReadBlock( byte[] dstData, int dataSize );

    public abstract byte ReadByte();
}