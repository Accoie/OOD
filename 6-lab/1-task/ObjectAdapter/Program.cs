using ObjectAdapter.Application;

class Program
{
    public static int Main()
    {
        Console.Write( "Should we use new API (y)?" );
        string userInput = Console.ReadLine() ?? "";

        if ( !string.IsNullOrEmpty( userInput ) && ( userInput == "y" || userInput == "Y" ) )
        {
            PicturePainter.PaintPictureOnModernGraphicsRenderer();
        }
        else
        {
            PicturePainter.PaintPictureOnCanvas();
        }

        return 0;
    }
}