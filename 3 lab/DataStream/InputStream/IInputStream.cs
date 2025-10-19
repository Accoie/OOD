namespace DataStream.InputStream;

public interface IInputStream
{
    bool IsEof();
    byte ReadByte();
    int ReadBlock( byte[] dstData, int dataSize );
}