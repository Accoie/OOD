using Slides.Canvas;
using Slides.Group;
using Slides.Shapes;
using Slides.Slides;
using Slides.Styles.FillStyles;
using Slides.Styles.LineStyles;
using Slides.Types;

namespace Slides
{
    public class Application
    {
        public void Run()
        {
            ICanvas canvas = new SvgCanvas();
            ISlide slide = new Slide();

            Rectangle houseBody = new Rectangle(
                new Frame( 200, 300, 400, 300 ),
                new LineStyle( new RGBAColor( 0, 0, 0, 1 ), 4, true ),
                new FillStyle( new RGBAColor( 240, 200, 160, 1 ), true ),
                new Point( 200, 300 ),
                400,
                300
            );

            Triangle roof = new Triangle(
                new LineStyle( new RGBAColor( 0, 0, 0, 1 ), 4, true ),
                new FillStyle( new RGBAColor( 180, 50, 50, 1 ), true ),
                new Frame( 200, 150, 400, 150 ),
                new Point( 200, 300 ),
                new Point( 400, 150 ),
                new Point( 600, 300 )
            );

            Rectangle door = new Rectangle(
                new Frame( 360, 450, 80, 150 ),
                new LineStyle( new RGBAColor( 0, 0, 0, 1 ), 3, true ),
                new FillStyle( new RGBAColor( 120, 70, 30, 1 ), true ),
                new Point( 360, 450 ),
                80,
                150
            );

            Rectangle leftWindow = new Rectangle(
                new Frame( 250, 370, 70, 70 ),
                new LineStyle( new RGBAColor( 0, 0, 0, 1 ), 2, true ),
                new FillStyle( new RGBAColor( 100, 180, 255, 1 ), true ),
                new Point( 250, 370 ),
                70,
                70
            );

            Rectangle rightWindow = new Rectangle(
                new Frame( 480, 370, 70, 70 ),
                new LineStyle( new RGBAColor( 0, 0, 0, 1 ), 2, true ),
                new FillStyle( new RGBAColor( 100, 180, 255, 1 ), true ),
                new Point( 480, 370 ),
                70,
                70
            );

            Ellipse sun = new Ellipse(
                new Frame( 100, 100, 100, 100 ),
                new LineStyle( new RGBAColor( 255, 200, 0, 1 ), 3, true ),
                new FillStyle( new RGBAColor( 255, 230, 0, 1 ), true ),
                new Point( 100, 100 ),
                50,
                50
            );

            ShapeGroup house = new ShapeGroup( [ houseBody, roof, door, leftWindow, rightWindow ] );

            ShapeGroup scene = new ShapeGroup( [ sun, house ] );

            slide.AddShape( scene );
            slide.Draw( canvas );

            string path = Console.ReadLine() ?? "";
            canvas.Save( path );
        }
    }
}