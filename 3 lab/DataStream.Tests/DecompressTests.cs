using DataStream.Decorators;
using DataStream.Decorators.Common;
using DataStream.InputStream;

namespace DataStream.Tests;

public class DecompressTests
{
    [Test]
    public void ReadBlock_WithCompressedData_WillDecompressed()
    {
        byte[] data = [ 3, 1, 2, 2, 1, 3 ];
        IInputStream inputStream = new MemoryInputStream( data );
        InputDecompress decompressor = new InputDecompress( inputStream );

        decompressor.ReadBlock( data, data.Length );

        Assert.That( data.SequenceEqual( new byte[] { 1, 1, 1, 2, 2, 3 } ) );
    }
}