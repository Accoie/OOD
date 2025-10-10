using DataStream.Decorators;
using DataStream.OutputStream;
using Moq;

namespace DataStream.Tests;

public class CompressTests
{
    [Test]
    public void WriteBlock_WithDefaultData_WillCompressed()
    {
        // Arrange
        byte[] originalData = new byte[] { 1, 1, 1, 2, 2, 3 };

        Mock<IOutputStream> outputMock = new Mock<IOutputStream>();

        List<byte> compressedData = new();

        outputMock.Setup( m => m.WriteByte( It.IsAny<byte>() ) ).Callback<byte>( compressedData.Add );
        OutputCompress compressor = new OutputCompress( outputMock.Object );

        //Act
        compressor.WriteBlock( originalData, originalData.Length );

        //Assert
        Assert.That( compressedData.ToArray().SequenceEqual( new byte[] { 3, 1, 2, 2, 1, 3 } ) );
    }
}