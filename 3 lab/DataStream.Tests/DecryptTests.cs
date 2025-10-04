using DataStream.Decorators;
using DataStream.Decorators.Common;
using DataStream.InputStream;
using Moq;

namespace DataStream.Tests;

public class DecryptTests
{
    [Test]
    public void ReadByte_DecryptsDataCorrectly()
    {
        // Arrange
        var mockInputStream = new Mock<IInputStream>();
        int key = 12345;
        var decryptTable = ReplaceTableCreator.CreateDecryptTable( key );
        var inputDecrypt = new InputDecrypt( mockInputStream.Object, key );

        byte encryptedByte = 0x87;
        byte expectedDecryptedByte = decryptTable[ encryptedByte ];

        mockInputStream.Setup( x => x.ReadByte() ).Returns( encryptedByte );

        // Act
        byte result = inputDecrypt.ReadByte();

        // Assert
        Assert.That( result, Is.EqualTo( expectedDecryptedByte ) );
        mockInputStream.Verify( x => x.ReadByte(), Times.Once );
    }

    [Test]
    public void ReadBlock_DecryptsAllBytesInArray()
    {
        // Arrange
        var mockInputStream = new Mock<IInputStream>();
        int key = 12345;
        var decryptTable = ReplaceTableCreator.CreateDecryptTable( key );
        var inputDecrypt = new InputDecrypt( mockInputStream.Object, key );

        byte[] encryptedData = { 0x87, 0xAB, 0xCD, 0xEF };
        byte[] expectedDecryptedData = new byte[ encryptedData.Length ];
        for ( int i = 0; i < encryptedData.Length; i++ )
        {
            expectedDecryptedData[ i ] = decryptTable[ encryptedData[ i ] ];
        }

        var sequence = new MockSequence();
        foreach ( byte encryptedByte in encryptedData )
        {
            mockInputStream.InSequence( sequence ).Setup( x => x.ReadByte() ).Returns( encryptedByte );
        }

        byte[] destinationBuffer = new byte[ encryptedData.Length ];

        // Act
        int bytesRead = inputDecrypt.ReadBlock( destinationBuffer, encryptedData.Length );

        // Assert
        Assert.That( bytesRead, Is.EqualTo( encryptedData.Length ) );
        Assert.That( destinationBuffer, Is.EqualTo( expectedDecryptedData ) );
        mockInputStream.Verify( x => x.ReadByte(), Times.Exactly( encryptedData.Length ) );
    }

    [Test]
    public void ReadBlock_WithPartialData_DecryptsOnlySpecifiedSize()
    {
        // Arrange
        var mockInputStream = new Mock<IInputStream>();
        int key = 12345;
        var decryptTable = ReplaceTableCreator.CreateDecryptTable( key );
        var inputDecrypt = new InputDecrypt( mockInputStream.Object, key );

        byte[] encryptedData = { 0x87, 0xAB, 0xCD, 0xEF, 0x12 };
        int requestedSize = 3;

        var sequence = new MockSequence();
        for ( int i = 0; i < requestedSize; i++ )
        {
            mockInputStream.InSequence( sequence ).Setup( x => x.ReadByte() ).Returns( encryptedData[ i ] );
        }

        byte[] destinationBuffer = new byte[ requestedSize ];

        // Act
        int bytesRead = inputDecrypt.ReadBlock( destinationBuffer, requestedSize );

        // Assert
        Assert.That( bytesRead, Is.EqualTo( requestedSize ) );

        for ( int i = 0; i < requestedSize; i++ )
        {
            byte expectedDecryptedByte = decryptTable[ encryptedData[ i ] ];
            Assert.That( destinationBuffer[ i ], Is.EqualTo( expectedDecryptedByte ) );
        }

        mockInputStream.Verify( x => x.ReadByte(), Times.Exactly( requestedSize ) );
    }

    [Test]
    public void ReadBlock_WithZeroSize_DoesNotReadAnything()
    {
        // Arrange
        var mockInputStream = new Mock<IInputStream>();
        int key = 12345;
        var inputDecrypt = new InputDecrypt( mockInputStream.Object, key );

        byte[] destinationBuffer = new byte[ 10 ];

        // Act
        int bytesRead = inputDecrypt.ReadBlock( destinationBuffer, 0 );

        // Assert
        Assert.That( bytesRead, Is.EqualTo( 0 ) );
        mockInputStream.Verify( x => x.ReadByte(), Times.Never );
    }
}