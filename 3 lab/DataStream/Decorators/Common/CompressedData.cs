namespace DataStream.Decorators.Common;

public class CompressedData
{
    public byte Data { get; set; }
    public byte Size { get; set; }

    public CompressedData( byte data, byte size )
    {
        Data = data;
        Size = size;
    }
}