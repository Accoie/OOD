using TiledImage.Types;

namespace TiledImage;

public static class DrawService // 3js
{
    private const int initialMultiplier = 3;
    private const int radiusMultiplier = 2;

    public static void DrawLine( Image image, Point from, Point to, uint color )
    {
        if ( image is null )
        {
            throw new ArgumentNullException( nameof( image ) );
        }

        int deltaX = Math.Abs( to.X - from.X );
        int deltaY = Math.Abs( to.Y - from.Y );

        if ( deltaY > deltaX )
        {
            DrawSteepLine( image, from, to, color );
        }
        else
        {
            DrawSlopeLine( image, from, to, color );
        }
    }

    public static void DrawCircle( Image image, Point center, int radius, uint color )
    {
        if ( image is null )
        {
            throw new ArgumentNullException( nameof( image ) );
        }

        DrawCirclePoints( image, center, radius, color );
    }

    public static void FillCircle( Image image, Point center, int radius, uint color )
    {
        if ( image is null )
        {
            throw new ArgumentNullException( nameof( image ) );
        }

        FillCircleWithLines( image, center, radius, color );
    }

    private static void DrawCirclePoints( Image image, Point center, int radius, uint color )
    {
        int currentX = 0;
        int currentY = radius;


        int decision = initialMultiplier - radiusMultiplier * radius;

        while ( currentX <= currentY )
        {
            DrawCircleSymmetricalPoints( image, center, currentX, currentY, color );
            CircleParameters updated = UpdateCircleParameters( currentX, currentY, decision );
            currentX = updated.X;
            currentY = updated.Y;
            decision = updated.D;
        }
    }

    private static void DrawCircleSymmetricalPoints( Image image, Point center, int x, int y, uint color )
    {
        image.SetPixel( new Point( center.X + x, center.Y + y ), color );
        image.SetPixel( new Point( center.X - x, center.Y + y ), color );
        image.SetPixel( new Point( center.X + x, center.Y - y ), color );
        image.SetPixel( new Point( center.X - x, center.Y - y ), color );
        image.SetPixel( new Point( center.X + y, center.Y + x ), color );
        image.SetPixel( new Point( center.X - y, center.Y + x ), color );
        image.SetPixel( new Point( center.X + y, center.Y - x ), color );
        image.SetPixel( new Point( center.X - y, center.Y - x ), color );
    }

    private static CircleParameters UpdateCircleParameters( int x, int y, int d )
    {
        int newX = x + 1;
        int newY = y;
        int newD = d;

        if ( d < 0 )
        {
            newD = d + 4 * x + 6;
        }
        else
        {
            newD = d + 4 * ( x - y ) + 10;
            newY = y - 1;
        }

        return new CircleParameters( newX, newY, newD );
    }

    private static void FillCircleWithLines( Image image, Point center, int radius, uint color )
    {
        int x = 0;
        int y = radius;
        int d = initialMultiplier - radiusMultiplier * radius;

        while ( x <= y )
        {
            DrawHorizontalCircleLines( image, center, x, y, color );
            CircleParameters updated = UpdateCircleParameters( x, y, d );
            x = updated.X;
            y = updated.Y;
            d = updated.D;
        }
    }

    private static void DrawHorizontalCircleLines( Image image, Point center, int x, int y, uint color )
    {
        if ( y != x )
        {
            DrawLine( image, new Point( center.X - y, center.Y + x ), new Point( center.X + y, center.Y + x ), color );
            if ( x != 0 )
            {
                DrawLine( image, new Point( center.X - y, center.Y - x ), new Point( center.X + y, center.Y - x ), color );
            }
        }

        DrawLine( image, new Point( center.X - x, center.Y + y ), new Point( center.X + x, center.Y + y ), color );
        if ( y != 0 )
        {
            DrawLine( image, new Point( center.X - x, center.Y - y ), new Point( center.X + x, center.Y - y ), color );
        }
    }

    private static void DrawSteepLine( Image image, Point from, Point to, uint color )
    {
        int deltaX = Math.Abs( to.X - from.X );
        int deltaY = Math.Abs( to.Y - from.Y );

        if ( from.Y > to.Y )
        {
            Point temp = from;
            from = to;
            to = temp;
        }

        int stepX = Math.Sign( to.X - from.X );
        int errorThreshold = deltaY + 1;
        int deltaErr = deltaX + 1;
        int error = deltaErr / 2;

        Point p = from;
        while ( p.Y <= to.Y )
        {
            image.SetPixel( new Point( p.X, p.Y ), color );

            error += deltaErr;

            if ( error >= errorThreshold )
            {
                p.X += stepX;
                error -= errorThreshold;
            }

            p.Y++;
        }
    }

    private static void DrawSlopeLine( Image image, Point from, Point to, uint color )
    {
        int deltaX = Math.Abs( to.X - from.X );
        int deltaY = Math.Abs( to.Y - from.Y );

        if ( from.X > to.X )
        {
            Point temp = from;
            from = to;
            to = temp;
        }

        int stepY = Math.Sign( to.Y - from.Y );
        int errorThreshold = deltaX + 1;
        int deltaErr = deltaY + 1;
        int error = deltaErr / 2;

        Point p = from;
        while ( p.X <= to.X )
        {
            image.SetPixel( new Point( p.X, p.Y ), color );

            error += deltaErr;

            if ( error >= errorThreshold )
            {
                p.Y += stepY;
                error -= errorThreshold;
            }

            p.X++;
        }
    }

    private struct CircleParameters
    {
        public int X { get; }
        public int Y { get; }
        public int D { get; }

        public CircleParameters( int x, int y, int d )
        {
            X = x;
            Y = y;
            D = d;
        }
    }
}