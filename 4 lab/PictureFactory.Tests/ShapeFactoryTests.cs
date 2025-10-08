using PictureFactory.Factories;
using PictureFactory.Shapes;
using PictureFactory.Types;

namespace PictureFactory.Tests
{
    public class ShapeFactoryTests
    {
        private ShapeFactory _factory = new();

        [Test]
        public void CreateShape_WithRegularPolygonDescr_WillCreatedRegularPolygon()
        {
            string descr = "reg-polygon red 6 50 50 50";

            Shape shape = _factory.CreateShape( descr );
            RegularPolygon concreteObj = ( RegularPolygon )shape;

            Assert.That( shape, Is.InstanceOf<RegularPolygon>() );
            Assert.That( concreteObj.Color, Is.EqualTo( Color.Red ) );
            Assert.That( concreteObj.VertexCount, Is.EqualTo( 6 ) );
            Assert.That( concreteObj.Center, Is.EqualTo( new Point( 50, 50 ) ) );
            Assert.That( concreteObj.Radius, Is.EqualTo( 50 ) );
        }

        [Test]
        public void CreateShape_WithEllipseDescr_WillCreatedEllipse()
        {
            string descr = "ellipse red 50 50 50 20";

            Shape shape = _factory.CreateShape( descr );
            Ellipse concreteObj = ( Ellipse )shape;

            Assert.That( shape, Is.InstanceOf<Ellipse>() );
            Assert.That( concreteObj.Color, Is.EqualTo( Color.Red ) );
            Assert.That( concreteObj.Center, Is.EqualTo( new Point( 50, 50 ) ) );
            Assert.That( concreteObj.RadiusX, Is.EqualTo( 50 ) );
            Assert.That( concreteObj.RadiusY, Is.EqualTo( 20 ) );
        }

        [Test]
        public void CreateShape_WithTriangleDescr_WillCreatedTriangle()
        {
            string descr = "triangle red 0 0 20 20 30 30";

            Shape shape = _factory.CreateShape( descr );
            Triangle concreteObj = ( Triangle )shape;

            Assert.That( shape, Is.InstanceOf<Triangle>() );
            Assert.That( concreteObj.Color, Is.EqualTo( Color.Red ) );
            Assert.That( concreteObj.FirstVertex, Is.EqualTo( new Point( 0, 0 ) ) );
            Assert.That( concreteObj.SecondVertex, Is.EqualTo( new Point( 20, 20 ) ) );
            Assert.That( concreteObj.ThirdVertex, Is.EqualTo( new Point( 30, 30 ) ) );
        }

        [Test]
        public void CreateShape_WithRectangleDescr_WillCreatedRectangle()
        {
            string descr = "rect red 20 20 50 50";

            Shape shape = _factory.CreateShape( descr );
            Rectangle concreteObj = ( Rectangle )shape;

            Assert.That( shape, Is.InstanceOf<Rectangle>() );
            Assert.That( concreteObj.Color, Is.EqualTo( Color.Red ) );
            Assert.That( concreteObj.LeftTop, Is.EqualTo( new Point( 20, 20 ) ) );
            Assert.That( concreteObj.RightBottom, Is.EqualTo( new Point( 50, 50 ) ) );
        }

        [Test]
        public void CreateShape_WithUnknownShapeType_ThrowsArgumentException()
        {
            string descr = "unknown red 50 50 50";

            Assert.Throws<ArgumentException>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithUnknownColor_ThrowsArgumentException()
        {
            string descr = "rect purpledSDF 20 20 50 50";

            Assert.Throws<ArgumentException>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithInvalidNumberFormat_ThrowsException()
        {
            string descr = "rect red abc 20 50 50";

            Assert.Throws<Exception>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithRegularPolygonLessThan3Vertices_ThrowsArgumentException()
        {
            string descr = "reg-polygon red 2 50 50 50";

            Assert.Throws<ArgumentException>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithRegularPolygonNegativeRadius_ThrowsArgumentException()
        {
            string descr = "reg-polygon red 6 50 50 -10";

            Assert.Throws<ArgumentException>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithRegularPolygonInsufficientParams_ThrowsArgumentException()
        {
            string descr = "reg-polygon red 6 50";

            Assert.Throws<ArgumentException>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithEllipseNegativeRadiusX_ThrowsArgumentException()
        {
            string descr = "ellipse red 50 50 -10 20";

            Assert.Throws<ArgumentException>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithEllipseNegativeRadiusY_ThrowsArgumentException()
        {
            string descr = "ellipse red 50 50 50 -5";

            Assert.Throws<ArgumentException>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithEllipseZeroRadius_ThrowsArgumentException()
        {
            string descr = "ellipse red 50 50 0 20";

            Assert.Throws<ArgumentException>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithEllipseInsufficientParams_ThrowsArgumentException()
        {
            string descr = "ellipse red 50 50";

            Assert.Throws<ArgumentException>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithTriangleInsufficientParams_ThrowsArgumentException()
        {
            string descr = "triangle red 0 0 20 20";

            Assert.Throws<ArgumentException>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithTriangleInvalidPoints_ThrowsException()
        {
            string descr = "triangle red 0 invalid 20 20 30 30";

            Assert.Throws<Exception>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithRectangleInsufficientParams_ThrowsArgumentException()
        {
            string descr = "rect red 20 20";

            Assert.Throws<ArgumentException>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithRectangleInvalidCoordinates_ThrowsException()
        {
            string descr = "rect red 20 abc 50 50";

            Assert.Throws<Exception>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithRectangleInvalidLeftTopRightBottom_ThrowsException()
        {
            string descr = "rect red 60 60 20 20";

            Assert.Throws<ArgumentException>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithEmptyString_ThrowsException()
        {
            string descr = "";

            Assert.Throws<ArgumentNullException>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithOnlyShapeType_ThrowsException()
        {
            string descr = "rect";

            Assert.Throws<ArgumentException>( () => _factory.CreateShape( descr ) );
        }

        [Test]
        public void CreateShape_WithExtraParameters_ShouldIgnoreExtraParams()
        {
            string descr = "rect red 20 20 50 50 extra param 100";

            Shape shape = _factory.CreateShape( descr );
            Rectangle concreteObj = ( Rectangle )shape;

            Assert.That( shape, Is.InstanceOf<Rectangle>() );
            Assert.That( concreteObj.Color, Is.EqualTo( Color.Red ) );
            Assert.That( concreteObj.LeftTop, Is.EqualTo( new Point( 20, 20 ) ) );
            Assert.That( concreteObj.RightBottom, Is.EqualTo( new Point( 50, 50 ) ) );
        }

        [Test]
        public void CreateShape_WithDecimalNumbers_ShouldParseCorrectly()
        {
            string descr = "ellipse red 50.5 50.5 25.75 15.25";

            Shape shape = _factory.CreateShape( descr );
            Ellipse concreteObj = ( Ellipse )shape;

            Assert.That( shape, Is.InstanceOf<Ellipse>() );
            Assert.That( concreteObj.Center.X, Is.EqualTo( 50.5 ).Within( 0.001 ) );
            Assert.That( concreteObj.Center.Y, Is.EqualTo( 50.5 ).Within( 0.001 ) );
            Assert.That( concreteObj.RadiusX, Is.EqualTo( 25.75 ).Within( 0.001 ) );
            Assert.That( concreteObj.RadiusY, Is.EqualTo( 15.25 ).Within( 0.001 ) );
        }

        [Test]
        public void CreateShape_WithNegativeCoordinates_ShouldWork()
        {
            string descr = "rect red -20 -20 50 50";

            Shape shape = _factory.CreateShape( descr );
            Rectangle concreteObj = ( Rectangle )shape;

            Assert.That( shape, Is.InstanceOf<Rectangle>() );
            Assert.That( concreteObj.LeftTop, Is.EqualTo( new Point( -20, -20 ) ) );
            Assert.That( concreteObj.RightBottom, Is.EqualTo( new Point( 50, 50 ) ) );
        }
    }
}