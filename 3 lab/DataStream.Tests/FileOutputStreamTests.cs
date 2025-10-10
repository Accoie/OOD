using DataStream.OutputStream;

namespace DataStream.Tests;

public class FileOutputStreamTests
{
    private string CreateTempFile()
    {
        return Path.GetTempFileName();
    }

    [Test]
    public void Constructor_WithNullPath_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>( () => new FileOutputStream( "" ) );
    }

    [Test]
    public void Constructor_WithEmptyPath_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>( () => new FileOutputStream( "" ) );
    }

    [Test]
    public void WriteByte_WritesDataToFile()
    {
        // Arrange
        string tempFile = CreateTempFile();
        using FileOutputStream fileOutputStream = new FileOutputStream( tempFile );
        byte testByte = 0x41;

        // Act
        fileOutputStream.WriteByte( testByte );
        fileOutputStream.Dispose();

        // Assert
        byte[] fileContent = File.ReadAllBytes( tempFile );
        Assert.That( fileContent.Length, Is.EqualTo( 1 ) );
        Assert.That( fileContent[ 0 ], Is.EqualTo( testByte ) );

        File.Delete( tempFile );
    }

    [Test]
    public void WriteBlock_WritesDataToFile()
    {
        // Arrange
        string tempFile = CreateTempFile();
        using FileOutputStream fileOutputStream = new FileOutputStream( tempFile );
        byte[] testData = { 0x41, 0x42, 0x43, 0x44 };

        // Act
        fileOutputStream.WriteBlock( testData, testData.Length );
        fileOutputStream.Dispose();

        // Assert
        byte[] fileContent = File.ReadAllBytes( tempFile );
        Assert.That( fileContent, Is.EqualTo( testData ) );

        File.Delete( tempFile );
    }

    [Test]
    public void WriteBlock_WithPartialData_WritesOnlySpecifiedSize()
    {
        // Arrange
        string tempFile = CreateTempFile();
        using FileOutputStream fileOutputStream = new FileOutputStream( tempFile );
        byte[] testData = { 0x41, 0x42, 0x43, 0x44, 0x45 };
        int partialSize = 3;

        // Act
        fileOutputStream.WriteBlock( testData, partialSize );
        fileOutputStream.Dispose();

        // Assert
        byte[] fileContent = File.ReadAllBytes( tempFile );
        Assert.That( fileContent.Length, Is.EqualTo( partialSize ) );
        Assert.That( fileContent, Is.EqualTo( testData.Take( partialSize ) ) );

        File.Delete( tempFile );
    }

    [Test]
    public void WriteBlock_WithZeroSize_DoesNotWriteAnything()
    {
        // Arrange
        string tempFile = CreateTempFile();
        using FileOutputStream fileOutputStream = new FileOutputStream( tempFile );
        byte[] testData = { 0x41, 0x42, 0x43 };

        // Act
        fileOutputStream.WriteBlock( testData, 0 );
        fileOutputStream.Dispose();

        // Assert
        byte[] fileContent = File.ReadAllBytes( tempFile );
        Assert.That( fileContent.Length, Is.EqualTo( 0 ) );

        File.Delete( tempFile );
    }

    [Test]
    public void WriteAfterDispose_ThrowsObjectDisposedException()
    {
        // Arrange
        string tempFile = CreateTempFile();
        FileOutputStream fileOutputStream = new FileOutputStream( tempFile );

        // Act
        fileOutputStream.Dispose();

        // Assert
        Assert.Throws<ObjectDisposedException>( () => fileOutputStream.WriteByte( 0x41 ) );
        Assert.Throws<ObjectDisposedException>( () => fileOutputStream.WriteBlock( [ 0x41 ], 1 ) );

        File.Delete( tempFile );
    }

    [Test]
    public void MultipleWriteOperations_WorkCorrectly()
    {
        // Arrange
        string tempFile = CreateTempFile();
        using FileOutputStream fileOutputStream = new FileOutputStream( tempFile );

        // Act
        fileOutputStream.WriteByte( 0x41 );
        fileOutputStream.WriteBlock( [ 0x42, 0x43 ], 2 );
        fileOutputStream.WriteByte( 0x44 );
        fileOutputStream.Dispose();

        // Assert
        byte[] fileContent = File.ReadAllBytes( tempFile );
        Assert.That( fileContent, Is.EqualTo( new byte[] { 0x41, 0x42, 0x43, 0x44 } ) );

        File.Delete( tempFile );
    }

    [Test]
    public void WriteBlock_WithLargeData_WritesCorrectly()
    {
        // Arrange
        string tempFile = CreateTempFile();
        using FileOutputStream fileOutputStream = new FileOutputStream( tempFile );
        byte[] largeData = new byte[ 1000 ];
        for ( int i = 0; i < largeData.Length; i++ )
        {
            largeData[ i ] = ( byte )( i % 256 );
        }

        // Act
        fileOutputStream.WriteBlock( largeData, largeData.Length );
        fileOutputStream.Dispose();

        // Assert
        byte[] fileContent = File.ReadAllBytes( tempFile );
        Assert.That( fileContent.Length, Is.EqualTo( largeData.Length ) );
        Assert.That( fileContent, Is.EqualTo( largeData ) );

        File.Delete( tempFile );
    }
}