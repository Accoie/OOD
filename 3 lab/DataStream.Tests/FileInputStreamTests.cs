using DataStream.InputStream;

namespace DataStream.Tests;

public class FileInputStreamTests
{
    private string CreateTempFile( byte[] content )
    {
        string tempFile = Path.GetTempFileName();
        File.WriteAllBytes( tempFile, content );

        return tempFile;
    }

    [Test]
    public void Constructor_WithValidFile_ReadsFileContent()
    {
        // Arrange
        byte[] fileContent = { 0x41, 0x42, 0x43 };
        string tempFile = CreateTempFile( fileContent );

        // Act
        FileInputStream fileInputStream = new FileInputStream( tempFile );

        // Assert
        Assert.False( fileInputStream.IsEof() );
        File.Delete( tempFile );
    }

    [Test]
    public void Constructor_WithNonExistentFile_ThrowsFileNotFoundException()
    {
        // Act & Assert
        Assert.Throws<FileNotFoundException>( () => new FileInputStream( "nonexistent_file.dat" ) );
    }

    [Test]
    public void ReadByte_ReadsBytesCorrectly()
    {
        // Arrange
        byte[] fileContent = { 0x41, 0x42, 0x43 };
        string tempFile = CreateTempFile( fileContent );
        FileInputStream fileInputStream = new FileInputStream( tempFile );

        // Act & Assert
        Assert.That( fileInputStream.ReadByte(), Is.EqualTo( 0x41 ) );
        Assert.That( fileInputStream.ReadByte(), Is.EqualTo( 0x42 ) );
        Assert.That( fileInputStream.ReadByte(), Is.EqualTo( 0x43 ) );

        File.Delete( tempFile );
    }

    [Test]
    public void ReadByte_AtEndOfFile_ThrowsException()
    {
        // Arrange
        byte[] fileContent = { 0x41 };
        string tempFile = CreateTempFile( fileContent );
         FileInputStream fileInputStream = new FileInputStream( tempFile );

        // Act
        fileInputStream.ReadByte();

        // Assert
        Assert.True( fileInputStream.IsEof() );
        Assert.Throws<EndOfStreamException>( () => fileInputStream.ReadByte() );

        File.Delete( tempFile );
    }

    [Test]
    public void IsEof_WorksCorrectly()
    {
        // Arrange
        byte[] fileContent = { 0x41, 0x42 };
        string tempFile = CreateTempFile( fileContent );
         FileInputStream fileInputStream = new FileInputStream( tempFile );

        // Act & Assert
        Assert.False( fileInputStream.IsEof() );
        fileInputStream.ReadByte();
        Assert.False( fileInputStream.IsEof() );
        fileInputStream.ReadByte();
        Assert.True( fileInputStream.IsEof() );

        File.Delete( tempFile );
    }

    [Test]
    public void ReadBlock_ReadsFullBlock()
    {
        // Arrange
        byte[] fileContent = { 0x41, 0x42, 0x43, 0x44 };
        string tempFile = CreateTempFile( fileContent );
         FileInputStream fileInputStream = new FileInputStream( tempFile );

        byte[] buffer = new byte[ 3 ];

        // Act
        int bytesRead = fileInputStream.ReadBlock( buffer, 3 );

        // Assert
        Assert.That( bytesRead, Is.EqualTo( 3 ) );
        Assert.That( buffer, Is.EqualTo( new byte[] { 0x41, 0x42, 0x43 } ) );

        File.Delete( tempFile );
    }

    [Test]
    public void ReadBlock_WithEmptyFile_ThrowsException()
    {
        // Arrange
        byte[] fileContent = {};
        string tempFile = CreateTempFile( fileContent );
        FileInputStream fileInputStream = new FileInputStream( tempFile );

        byte[] buffer = new byte[ 3 ];

        // Act & Assert
        Assert.Throws<EndOfStreamException>( () => fileInputStream.ReadBlock( buffer, 3 ) );

        File.Delete( tempFile );
    }

    [Test]
    public void MultipleReadOperations_WorkCorrectly()
    {
        // Arrange
        byte[] fileContent = { 0x41, 0x42, 0x43, 0x44 };
        string tempFile = CreateTempFile( fileContent );
        FileInputStream fileInputStream = new FileInputStream( tempFile );

        // Act
        byte firstByte = fileInputStream.ReadByte();
        byte[] buffer = new byte[ 2 ];
        int bytesRead = fileInputStream.ReadBlock( buffer, 2 );
        byte lastByte = fileInputStream.ReadByte();

        // Assert
        Assert.That( firstByte, Is.EqualTo( 0x41 ) );
        Assert.That( bytesRead, Is.EqualTo( 2 ) );
        Assert.That( buffer, Is.EqualTo( new byte[] { 0x42, 0x43 } ) );
        Assert.That( lastByte, Is.EqualTo( 0x44 ) );
        Assert.True( fileInputStream.IsEof() );

        File.Delete( tempFile );
    }
}