using DataStream.Decorators;
using DataStream.Decorators.Common;
using DataStream.OutputStream;
using Moq;

namespace DataStream.Tests;

public class EncryptTests
{
    [Test]
    public void WriteByte_EncryptsDataCorrectly_WillEncrypted()
    {
        // Arrange
        Mock<IOutputStream> mockOutputStream = new Mock<IOutputStream>();
        int key = 12345;
        Dictionary<byte, byte> encryptTable = ReplaceTableCreator.CreateEncryptTable( key );
        OutputEncrypt outputEncrypt = new OutputEncrypt( mockOutputStream.Object, key );
        byte testByte = 0x41;
        byte expectedEncryptedByte = encryptTable[ testByte ];

        // Act
        outputEncrypt.WriteByte( testByte );

        // Assert
        mockOutputStream.Verify( x => x.WriteByte( expectedEncryptedByte ), Times.Once );
    }

    [Test]
    public void WriteBlock_EncryptsAllBytesInArray_WillEncrypted()
    {
        // Arrange
        Mock<IOutputStream> mockOutputStream = new Mock<IOutputStream>();
        int key = 12345;
        Dictionary<byte, byte> encryptTable = ReplaceTableCreator.CreateEncryptTable( key );
        OutputEncrypt outputEncrypt = new OutputEncrypt( mockOutputStream.Object, key );

        byte[] testData = { 0x41, 0x42, 0x43, 0x44 };
        int dataSize = testData.Length;

        byte[] expectedEncryptedData = new byte[ dataSize ];
        for ( int i = 0; i < dataSize; i++ )
        {
            expectedEncryptedData[ i ] = encryptTable[ testData[ i ] ];
        }

        // Act
        outputEncrypt.WriteBlock( testData, dataSize );

        // Assert
        mockOutputStream.Verify( x => x.WriteByte( It.IsAny<byte>() ), Times.Exactly( dataSize ) );

        foreach ( byte expectedByte in expectedEncryptedData )
        {
            mockOutputStream.Verify( x => x.WriteByte( expectedByte ), Times.Once );
        }
    }

    [Test]
    public void WriteBlock_WithPartialData_EncryptsOnlySpecifiedSize()
    {
        // Arrange
        Mock<IOutputStream> mockOutputStream = new Mock<IOutputStream>();
        int key = 12345;
        Dictionary<byte, byte> encryptTable = ReplaceTableCreator.CreateEncryptTable( key );
        OutputEncrypt outputEncrypt = new OutputEncrypt( mockOutputStream.Object, key );

        byte[] testData = { 0x41, 0x42, 0x43, 0x44, 0x45 };
        int partialSize = 3;

        // Act
        outputEncrypt.WriteBlock( testData, partialSize );

        // Assert
        mockOutputStream.Verify( x => x.WriteByte( It.IsAny<byte>() ), Times.Exactly( partialSize ) );

        for ( int i = 0; i < partialSize; i++ )
        {
            byte expectedEncryptedByte = encryptTable[ testData[ i ] ];
            mockOutputStream.Verify( x => x.WriteByte( expectedEncryptedByte ), Times.Once );
        }
    }

    [Test]
    public void WriteBlock_WithZeroSize_WillNotWriteAnything()
    {
        // Arrange
        Mock<IOutputStream> mockOutputStream = new Mock<IOutputStream>();
        int key = 12345;
        OutputEncrypt outputEncrypt = new OutputEncrypt( mockOutputStream.Object, key );

        byte[] testData = { 0x41, 0x42, 0x43 };

        // Act
        outputEncrypt.WriteBlock( testData, 0 );

        // Assert
        mockOutputStream.Verify( x => x.WriteByte( It.IsAny<byte>() ), Times.Never );
    }
}