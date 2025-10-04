using System.Text;
using DataStream;
using DataStream.InputStream;
using DataStream.OutputStream;
using NUnit.Framework;

namespace DataStream.Tests;

[TestFixture]
public class CommandHandlerTests
{
    private string CreateTempFile( byte[]? content = null )
    {
        string tempFile = Path.GetTempFileName();
        if ( content != null )
        {
            File.WriteAllBytes( tempFile, content );
        }
        return tempFile;
    }

    private string CreateTempFileWithText( string content )
    {
        string tempFile = Path.GetTempFileName();
        File.WriteAllText( tempFile, content );
        return tempFile;
    }

    [Test]
    public void HandleOptions_WithCompress_CompressesData()
    {
        // Arrange
        string testData = "AAAABBBBCCCC";
        string inputFile = CreateTempFileWithText( testData );
        string outputFile = CreateTempFile();


        var inputStream = new FileInputStream( inputFile );
        using var outputStream = new FileOutputStream( outputFile );
        var commandHandler = new CommandHandler( inputStream, outputStream );
        string[] options = { "--compress" };

        // Act
        commandHandler.HandleOptions( options );

        // Assert
        outputStream.Dispose();
        byte[] outputBytes = File.ReadAllBytes( outputFile );
        Assert.That( outputBytes.Length, Is.LessThan( testData.Length ) );

    }

    [Test]
    public void HandleOptions_WithDecompress_DecompressesData()
    {
        // Arrange
        byte[] compressedData = { 4, ( byte )'A', 4, ( byte )'B', 4, ( byte )'C' };
        string inputFile = CreateTempFile( compressedData );
        string outputFile = CreateTempFile();


        var inputStream = new FileInputStream( inputFile );
        using var outputStream = new FileOutputStream( outputFile );
        var commandHandler = new CommandHandler( inputStream, outputStream );
        string[] options = { "--decompress" };

        // Act
        commandHandler.HandleOptions( options );

        // Assert
        outputStream.Dispose();
        string result = File.ReadAllText( outputFile );
        Assert.That( result, Is.EqualTo( "AAAABBBBCCCC" ) );

    }

    [Test]
    public void HandleOptions_WithEncryptAndDecrypt_RoundTripWorks()
    {
        // Arrange
        string originalData = "Hello World!";
        string inputFile = CreateTempFileWithText( originalData );
        string outputFile1 = CreateTempFile();
        string outputFile2 = CreateTempFile();
        int key = 42;

        var inputStream = new FileInputStream( inputFile );
        using ( var outputStream = new FileOutputStream( outputFile1 ) )
        {
            var encryptHandler = new CommandHandler( inputStream, outputStream );
            string[] encryptOptions = { "--encrypt", key.ToString() };
            encryptHandler.HandleOptions( encryptOptions );
        }


        byte[] encryptedData = File.ReadAllBytes( outputFile1 );

        File.WriteAllBytes( inputFile, encryptedData );

        inputStream = new FileInputStream( inputFile );
        using ( var outputStream = new FileOutputStream( outputFile2 ) )
        {
            var decryptHandler = new CommandHandler( inputStream, outputStream );
            string[] decryptOptions = { "--decrypt", key.ToString() };

            // Act
            decryptHandler.HandleOptions( decryptOptions );
        }

        // Assert
        string decryptedResult = File.ReadAllText( outputFile2 );
        Assert.That( decryptedResult, Is.EqualTo( originalData ) );
    }

    [Test]
    public void HandleOptions_WithCompressAndDecompress_RoundTripWorks()
    {
        // Arrange
        string originalData = "AAAAABBBBBCCCCC";
        string inputFile = CreateTempFileWithText( originalData );
        string outputFile1 = CreateTempFile();
        string outputFile2 = CreateTempFile();

        var inputStream = new FileInputStream( inputFile );
        using ( var outputStream = new FileOutputStream( outputFile1 ) )
        {
            var compressHandler = new CommandHandler( inputStream, outputStream );
            string[] compressOptions = { "--compress" };
            compressHandler.HandleOptions( compressOptions );
        }

        byte[] compressedData = File.ReadAllBytes( outputFile1 );

        File.WriteAllBytes( inputFile, compressedData );

        inputStream = new FileInputStream( inputFile );
        using ( var outputStream = new FileOutputStream( outputFile2 ) )
        {
            var decompressHandler = new CommandHandler( inputStream, outputStream );
            string[] decompressOptions = { "--decompress" };

            // Act
            decompressHandler.HandleOptions( decompressOptions );
        }

        // Assert
        string decompressedResult = File.ReadAllText( outputFile2 );
        Assert.That( decompressedResult, Is.EqualTo( originalData ) );

    }

    [Test]
    public void HandleOptions_FullCycle_CompressEncryptDecryptDecompress()
    {
        // Arrange
        string originalData = "XXXYYYZZZ";
        string inputFile = CreateTempFileWithText( originalData );
        string encryptedFile = CreateTempFile();
        string decryptedFile = CreateTempFile();

        int encryptKey = 7;

        // Act 
        var inputStream1 = new FileInputStream( inputFile );
        using ( var outputStream1 = new FileOutputStream( encryptedFile) )
        {
            var commandHandler1 = new CommandHandler( inputStream1, outputStream1 );
            commandHandler1.HandleOptions([ "--compress", "--encrypt", encryptKey.ToString() ]);
        }

        var inputStream2 = new FileInputStream( encryptedFile );
        using ( var outputStream2 = new FileOutputStream( decryptedFile ) )
        {
            var commandHandler2 = new CommandHandler( inputStream2, outputStream2 );
            commandHandler2.HandleOptions( [  "--decrypt", encryptKey.ToString(), "--decompress" ] );
        }

        // Assert
        string result = File.ReadAllText( decryptedFile );
        Assert.That( result, Is.EqualTo( originalData ) );
    }

    [Test]
    public void HandleOptions_EncryptWithoutKey_ThrowsException()
    {
        // Arrange
        string inputFile = CreateTempFileWithText( "test" );
        string outputFile = CreateTempFile();

        var inputStream = new FileInputStream( inputFile );
        using var outputStream = new FileOutputStream( outputFile );
        var commandHandler = new CommandHandler( inputStream, outputStream );
        string[] options = { "--encrypt" };

        // Act & Assert
        Assert.Throws<Exception>( () => commandHandler.HandleOptions( options ) );

    }

    [Test]
    public void HandleOptions_DecryptWithoutKey_ThrowsException()
    {
        // Arrange
        string inputFile = CreateTempFileWithText( "test" );
        string outputFile = CreateTempFile();


        var inputStream = new FileInputStream( inputFile );
        using var outputStream = new FileOutputStream( outputFile );
        var commandHandler = new CommandHandler( inputStream, outputStream );
        string[] options = { "--decrypt" };

        // Act & Assert
        Assert.Throws<Exception>( () => commandHandler.HandleOptions( options ) );

    }

    [Test]
    public void HandleOptions_UnknownOption_ThrowsException()
    {
        // Arrange
        string inputFile = CreateTempFileWithText( "test" );
        string outputFile = CreateTempFile();


        var inputStream = new FileInputStream( inputFile );
        using var outputStream = new FileOutputStream( outputFile );
        var commandHandler = new CommandHandler( inputStream, outputStream );
        string[] options = { "--unknown-option" };

        // Act & Assert
        Assert.Throws<ArgumentException>( () => commandHandler.HandleOptions( options ) );

    }

    [Test]
    public void HandleOptions_EmptyOptions_ProcessesWithoutModification()
    {
        // Arrange
        string testData = "Test data";
        string inputFile = CreateTempFileWithText( testData );
        string outputFile = CreateTempFile();


        var inputStream = new FileInputStream( inputFile );
        using var outputStream = new FileOutputStream( outputFile );
        var commandHandler = new CommandHandler( inputStream, outputStream );
        string[] options = { };

        // Act
        commandHandler.HandleOptions( options );

        // Assert
        outputStream.Dispose();
        string result = File.ReadAllText( outputFile );
        Assert.That( result, Is.EqualTo( testData ) );

    }

    [Test]
    public void HandleOptions_EmptyInput_ProducesEmptyOutput()
    {
        // Arrange
        string inputFile = CreateTempFileWithText( "" );
        string outputFile = CreateTempFile();


        var inputStream = new FileInputStream( inputFile );
        using var outputStream = new FileOutputStream( outputFile );
        var commandHandler = new CommandHandler( inputStream, outputStream );
        string[] options = { "--compress" };

        // Act
        commandHandler.HandleOptions( options );

        // Assert
        outputStream.Dispose();
        byte[] result = File.ReadAllBytes( outputFile );
        Assert.That( result.Length, Is.EqualTo( 0 ) );
    }

    [Test]
    public void HandleOptions_LargeData_CompressesCorrectly()
    {
        // Arrange
        StringBuilder largeData = new StringBuilder();
        for ( int i = 0; i < 1000; i++ )
        {
            largeData.Append( 'A' );
        }
        string inputFile = CreateTempFileWithText( largeData.ToString() );
        string outputFile = CreateTempFile();


        var inputStream = new FileInputStream( inputFile );
        using var outputStream = new FileOutputStream( outputFile );
        var commandHandler = new CommandHandler( inputStream, outputStream );
        string[] options = { "--compress" };

        // Act
        commandHandler.HandleOptions( options );

        // Assert
        outputStream.Dispose();
        byte[] result = File.ReadAllBytes( outputFile );
        Assert.That( result.Length, Is.LessThan( largeData.Length ) );
        Assert.That( result.Length, Is.EqualTo( 8 ) );
    }

    [Test]
    public void HandleOptions_InvalidKeyFormat_ThrowsException()
    {
        // Arrange
        string inputFile = CreateTempFileWithText( "test" );
        string outputFile = CreateTempFile();

        var inputStream = new FileInputStream( inputFile );
        using var outputStream = new FileOutputStream( outputFile );
        var commandHandler = new CommandHandler( inputStream, outputStream );
        string[] options = { "--encrypt", "not-a-number" };

        // Act & Assert
        Assert.Throws<FormatException>( () => commandHandler.HandleOptions( options ) );
    }
}