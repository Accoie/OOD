namespace Editor.Image;

public class ImageItem : IImage
{
    public string Path { get; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public ImageItem( string path, int width, int height )
    {
        ValidateSize( width, height );

        Path = path;
        Width = width;
        Height = height;
    }

    public void Resize( int width, int height )
    {
        ValidateSize( width, height );

        Width = width;
        Height = height;
    }

    private void ValidateSize( int width, int height )
    {
        if ( width < 0 || height < 0 )
        {
            throw new ArgumentException( "Width and height can not be negative" );
        }
    }
}