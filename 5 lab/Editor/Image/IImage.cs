namespace Editor.Image
{
    public interface IImage
    {
        string Path { get; }
        int Width { get; }
        int Height { get; }

        void Resize( int width, int height );
    }
}