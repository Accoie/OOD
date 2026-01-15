using TiledImage.Types;

namespace TiledImage
{
    public static class ImageController
    {
        public static void Print( Image img, TextWriter output )
        {
            ValidatePrintParameters( img, output );
            PrintImageContent( img, output );
        }

        public static Image LoadImage( string pixels )
        {
            ValidateLoadImageParameters( pixels );
            string[] lines = pixels.Split( '\n' );
            Size size = CalculateImageSize( lines );

            return CreateImageFromLines( lines, size );
        }

        public static void SaveImageToPPM( Image image, string filename )
        {
            ValidateSaveImageParameters( image, filename );
            WritePPMFile( image, filename );
        }

        private static void ValidatePrintParameters( Image img, TextWriter output )
        {
            if ( img is null )
            {
                throw new ArgumentNullException( nameof( img ) );
            }
            if ( output is null )
            {
                throw new ArgumentNullException( nameof( output ) );
            }
        }

        private static void PrintImageContent( Image img, TextWriter output )
        {
            Size size = img.Size;
            for ( int y = 0; y < size.Height; y++ )
            {
                for ( int x = 0; x < size.Width; x++ )
                {
                    uint pixel = img.GetPixel( new Point( x, y ) );
                    output.Write( ( char )( pixel ) );
                }
                output.WriteLine();
            }
        }

        private static void ValidateLoadImageParameters( string pixels )
        {
            if ( pixels is null )
            {
                throw new ArgumentNullException( nameof( pixels ) );
            }
            if ( string.IsNullOrEmpty( pixels ) )
            {
                throw new ArgumentException( "Pixels string cannot be null or empty", nameof( pixels ) );
            }
        }

        private static Size CalculateImageSize( string[] lines )
        {
            uint maxWidth = 0;
            foreach ( string line in lines )
            {
                if ( line.Length > maxWidth )
                {
                    maxWidth = ( uint )line.Length;
                }
            }
            uint height = ( uint )lines.Length;

            return new Size( maxWidth, height );
        }

        private static Image CreateImageFromLines( string[] lines, Size size )
        {
            Image img = new Image( size, 0 );
            for ( int y = 0; y < size.Height; y++ )
            {
                FillImageLine( img, lines[ y ], y, size.Width );
            }

            return img;
        }

        private static void FillImageLine( Image img, string line, int y, uint maxWidth )
        {
            for ( int x = 0; x < line.Length; x++ )
            {
                char ch = line[ x ];
                img.SetPixel( new Point( x, y ), ch );
            }
            for ( int x = line.Length; x < maxWidth; x++ )
            {
                img.SetPixel( new Point( x, y ), 0 );
            }
        }

        private static void ValidateSaveImageParameters( Image image, string filename )
        {
            if ( image is null )
            {
                throw new ArgumentNullException( nameof( image ) );
            }

            ValidateFilename( filename );
        }

        private static void ValidateFilename( string filename )
        {
            if ( filename is null )
            {
                throw new ArgumentNullException( nameof( filename ) );
            }
            if ( string.IsNullOrEmpty( filename ) )
            {
                throw new ArgumentException( "Filename cannot be null or empty", nameof( filename ) );
            }
        }

        private static void WritePPMFile( Image image, string filename )
        {
            using ( StreamWriter file = new StreamWriter( filename ) )
            {
                WritePPMHeader( file, image.Size );
                WritePPMContent( file, image );
            }
        }

        private static void WritePPMHeader( StreamWriter file, Size size )
        {
            file.WriteLine( "P3" );
            file.WriteLine( $"{size.Width} {size.Height}" );
            file.WriteLine( "255" );
        }

        private static void WritePPMContent( StreamWriter file, Image image )
        {
            Size size = image.Size;
            for ( int y = 0; y < size.Height; y++ )
            {
                for ( int x = 0; x < size.Width; x++ )
                {
                    uint color = image.GetPixel( new Point( x, y ) );
                    WriteColorComponents( file, color );
                }

                file.WriteLine();
            }
        }

        private static void WriteColorComponents( StreamWriter file, uint color )
        {
            byte r = ( byte )( ( color >> 16 ) & 0xFF );
            byte g = ( byte )( ( color >> 8 ) & 0xFF );
            byte b = ( byte )( color & 0xFF );
            file.Write( $"{( int )r} {( int )g} {( int )b} " );
        }
    }
}