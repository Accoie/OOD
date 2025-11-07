namespace TiledImage
{
    public class DrawingApp
    {
        public static void DrawHouse()
        {
            const uint SkyColor = 0x87CEEB;
            const int ImageWidth = 40;
            const int ImageHeight = 30;

            Image img = new Image( new Size( ImageWidth, ImageHeight ), SkyColor );

            DrawGround( img );
            DrawHouseBase( img );
            DrawRoof( img );
            DrawDoor( img );
            DrawWindows( img );
            DrawWindowFrames( img );
            DrawSun( img );
            DrawTree( img );
            DrawPath( img );

            ImageController.SaveImageToPPM( img, "house.ppm" );
        }

        public static void DrawCity()
        {
            Image img = ImageController.LoadImage(
                "    ##    ####    ##  \n" +
                "   ####  ######  #### \n" +
                "  ################### \n" +
                "  ##  ##   ##   ##  ##\n" +
                "  ##  ##   ##   ##  ##\n" +
                "  ################### \n" +
                "   ##  ##  ##  ##  ## \n"
            );
            Console.WriteLine( "Drawing City Skyline:" );
            ImageController.Print( img, Console.Out );
            Console.WriteLine();
        }

        private static void DrawGround( Image img )
        {
            const uint GroundColor = 0x228B22;
            const int GroundStartY = 20;
            const int ImageWidth = 40;
            const int ImageHeight = 30;

            for ( int y = GroundStartY; y < ImageHeight; y++ )
            {
                for ( int x = 0; x < ImageWidth; x++ )
                {
                    img.SetPixel( new Point( x, y ), GroundColor );
                }
            }
        }

        private static void DrawHouseBase( Image img )
        {
            const uint HouseColor = 0x8B4513;
            const int HouseStartX = 10;
            const int HouseEndX = 30;
            const int HouseStartY = 10;
            const int HouseEndY = 20;

            for ( int y = HouseStartY; y < HouseEndY; y++ )
            {
                for ( int x = HouseStartX; x < HouseEndX; x++ )
                {
                    img.SetPixel( new Point( x, y ), HouseColor );
                }
            }
        }

        private static void DrawRoof( Image img )
        {
            const uint RoofColor = 0x8B0000;
            const int RoofStartX = 8;
            const int RoofPeakX = 20;
            const int RoofPeakY = 2;
            const int RoofEndX = 32;
            const int RoofBaseY = 10;
            const int HouseStartX = 10;
            const int HouseEndX = 30;

            Drawer.DrawLine( img, new Point( RoofStartX, RoofBaseY ), new Point( RoofPeakX, RoofPeakY ), RoofColor );
            Drawer.DrawLine( img, new Point( RoofPeakX, RoofPeakY ), new Point( RoofEndX, RoofBaseY ), RoofColor );
            Drawer.DrawLine( img, new Point( RoofStartX, RoofBaseY ), new Point( RoofEndX, RoofBaseY ), RoofColor );

            for ( int y = 3; y < RoofBaseY; y++ )
            {
                for ( int x = 9 + ( RoofBaseY - y ); x < 31 - ( RoofBaseY - y ); x++ )
                {
                    if ( x >= HouseStartX && x < HouseEndX )
                        img.SetPixel( new Point( x, y ), RoofColor );
                }
            }
        }

        private static void DrawDoor( Image img )
        {
            const uint DoorColor = 0x654321;
            const int DoorStartX = 17;
            const int DoorEndX = 23;
            const int DoorStartY = 12;
            const int HouseEndY = 20;

            for ( int y = DoorStartY; y < HouseEndY; y++ )
            {
                for ( int x = DoorStartX; x < DoorEndX; x++ )
                {
                    img.SetPixel( new Point( x, y ), DoorColor );
                }
            }
        }

        private static void DrawWindows( Image img )
        {
            const uint WindowColor = 0xADD8E6;
            const int WindowStartY = 12;
            const int WindowEndY = 16;
            const int LeftWindowStartX = 12;
            const int LeftWindowEndX = 16;
            const int RightWindowStartX = 24;
            const int RightWindowEndX = 28;

            for ( int y = WindowStartY; y < WindowEndY; y++ )
            {
                for ( int x = LeftWindowStartX; x < LeftWindowEndX; x++ )
                {
                    img.SetPixel( new Point( x, y ), WindowColor );
                }
                for ( int x = RightWindowStartX; x < RightWindowEndX; x++ )
                {
                    img.SetPixel( new Point( x, y ), WindowColor );
                }
            }
        }

        private static void DrawWindowFrames( Image img )
        {
            const uint FrameColor = 0x000000;
            const int LeftWindowCenterX = 14;
            const int RightWindowCenterX = 26;
            const int WindowMiddleY = 14;
            const int WindowStartY = 12;
            const int WindowEndY = 16;
            const int LeftWindowStartX = 12;
            const int LeftWindowEndX = 16;
            const int RightWindowStartX = 24;
            const int RightWindowEndX = 28;

            Drawer.DrawLine( img, new Point( LeftWindowCenterX, WindowStartY ), new Point( LeftWindowCenterX, WindowEndY - 1 ), FrameColor );
            Drawer.DrawLine( img, new Point( LeftWindowStartX, WindowMiddleY ), new Point( LeftWindowEndX - 1, WindowMiddleY ), FrameColor );
            Drawer.DrawLine( img, new Point( RightWindowCenterX, WindowStartY ), new Point( RightWindowCenterX, WindowEndY - 1 ), FrameColor );
            Drawer.DrawLine( img, new Point( RightWindowStartX, WindowMiddleY ), new Point( RightWindowEndX - 1, WindowMiddleY ), FrameColor );
        }

        private static void DrawSun( Image img )
        {
            const uint SunColor = 0xFFFF00;
            const int SunCenterX = 33;
            const int SunCenterY = 5;
            const int SunRadius = 3;

            Drawer.FillCircle( img, new Point( SunCenterX, SunCenterY ), SunRadius, SunColor );
        }

        private static void DrawTree( Image img )
        {
            const uint TreeTrunkColor = 0x8B4513;
            const uint TreeLeavesColor = 0x306010;
            const int TreeTrunkX = 5;
            const int TreeTrunkStartY = 15;
            const int TreeTrunkEndY = 20;
            const int TreeLeavesX = 5;
            const int TreeLeavesY = 12;
            const int TreeLeavesRadius = 3;

            Drawer.DrawLine( img, new Point( TreeTrunkX, TreeTrunkStartY ), new Point( TreeTrunkX, TreeTrunkEndY ), TreeTrunkColor );
            Drawer.FillCircle( img, new Point( TreeLeavesX, TreeLeavesY ), TreeLeavesRadius, TreeLeavesColor );
        }

        private static void DrawPath( Image img )
        {
            const uint PathColor = 0xC0C0C0;
            const int PathStartX = 18;
            const int PathEndX = 22;
            const int PathStartY = 20;
            const int ImageHeight = 30;

            for ( int y = PathStartY; y < ImageHeight; y++ )
            {
                for ( int x = PathStartX; x < PathEndX; x++ )
                {
                    img.SetPixel( new Point( x, y ), PathColor );
                }
            }
        }
    }
}