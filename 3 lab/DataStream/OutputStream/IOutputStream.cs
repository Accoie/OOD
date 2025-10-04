namespace DataStream.OutputStream;

public interface IOutputStream : IDisposable
{
    void WriteByte( byte data );
    void WriteBlock( byte[] srcData, int dataSize );
    void Flush();
}