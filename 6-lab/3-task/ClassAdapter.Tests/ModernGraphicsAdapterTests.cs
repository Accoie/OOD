using ObjectAdapter.Application;

namespace ObjectAdapter.Tests
{
    [TestFixture]
    public class ModernGraphicsAdapterTests
    {
        private StringWriter _consoleOutput;
        private TextWriter _originalOutput;

        [SetUp]
        public void Initialize()
        {
            _consoleOutput = new StringWriter();
            _originalOutput = Console.Out;
            Console.SetOut( _consoleOutput );
        }

        [TearDown]
        public void Cleanup()
        {
            Console.SetOut( _originalOutput );
            _consoleOutput.Dispose();
        }

        [Test]
        public void MoveToCall_ShouldMoveCurrentPointToMoveToParameters()
        {
            ModernGraphicsRendererAdapter adapter = new ModernGraphicsRendererAdapter();

            adapter.MoveTo( 10, 20 );
            adapter.LineTo( 30, 40 );

            string output = _consoleOutput.ToString();
            StringAssert.Contains( "<draw>", output );
            StringAssert.Contains( "  <line fromX=10 fromY=20 toX=30 toY=40>", output );
            StringAssert.Contains( "</draw>", output );
        }

        [Test]
        public void MoveToCall_WithNegativeArguments_ShouldBeSuccessfullyExecuted()
        {
            ModernGraphicsRendererAdapter adapter = new ModernGraphicsRendererAdapter();

            adapter.MoveTo( -10, -20 );
            adapter.LineTo( 30, 40 );

            string output = _consoleOutput.ToString();
            StringAssert.Contains( "<draw>", output );
            StringAssert.Contains( "  <line fromX=-10 fromY=-20 toX=30 toY=40>", output );
            StringAssert.Contains( "</draw>", output );
        }

        [Test]
        public void MoveToCall_WithSameArguments_ShouldBeSuccessfullyExecuted()
        {
            ModernGraphicsRendererAdapter adapter = new ModernGraphicsRendererAdapter();

            adapter.MoveTo( 0, 0 );
            adapter.LineTo( 30, 40 );

            string output = _consoleOutput.ToString();
            StringAssert.Contains( "<draw>", output );
            StringAssert.Contains( "  <line fromX=0 fromY=0 toX=30 toY=40>", output );
            StringAssert.Contains( "</draw>", output );
        }

        [Test]
        public void LineToCall_ShouldInvokeDrawLineWithRightArguments()
        {
            ModernGraphicsRendererAdapter adapter = new ModernGraphicsRendererAdapter();

            adapter.MoveTo( 10, 20 );
            adapter.LineTo( 30, 40 );

            string output = _consoleOutput.ToString();
            StringAssert.Contains( "<draw>", output );
            StringAssert.Contains( "  <line fromX=10 fromY=20 toX=30 toY=40>", output );
            StringAssert.Contains( "</draw>", output );
        }

        [Test]
        public void LineToCall_ShouldChangeCurrentPoint()
        {
            ModernGraphicsRendererAdapter adapter = new ModernGraphicsRendererAdapter();

            adapter.MoveTo( 10, 20 );
            adapter.LineTo( 30, 40 );
            adapter.LineTo( 50, 60 );

            string output = _consoleOutput.ToString();
            string[] lines = output.Split( '\n', StringSplitOptions.RemoveEmptyEntries );

            Assert.That( lines.Length, Is.EqualTo( 6 ) );
            StringAssert.Contains( "  <line fromX=10 fromY=20 toX=30 toY=40>", output );
            StringAssert.Contains( "  <line fromX=30 fromY=40 toX=50 toY=60>", output );
        }

        [Test]
        public void LineToCall_WithNegativeArguments_ShouldBeSuccessfullyExecuted()
        {
            ModernGraphicsRendererAdapter adapter = new ModernGraphicsRendererAdapter();

            adapter.MoveTo( 10, 20 );
            adapter.LineTo( -30, -40 );

            string output = _consoleOutput.ToString();
            StringAssert.Contains( "<draw>", output );
            StringAssert.Contains( "  <line fromX=10 fromY=20 toX=-30 toY=-40>", output );
            StringAssert.Contains( "</draw>", output );
        }

        [Test]
        public void LineToCall_WithSameArguments_ShouldBeSuccessfullyExecuted()
        {
            ModernGraphicsRendererAdapter adapter = new ModernGraphicsRendererAdapter();

            adapter.MoveTo( 10, 20 );
            adapter.LineTo( 0, 0 );

            string output = _consoleOutput.ToString();
            StringAssert.Contains( "<draw>", output );
            StringAssert.Contains( "  <line fromX=10 fromY=20 toX=0 toY=0>", output );
            StringAssert.Contains( "</draw>", output );
        }

        [Test]
        public void LineToCall_WithSetColor_ShouldIncludeColorInOutput()
        {
            ModernGraphicsRendererAdapter adapter = new ModernGraphicsRendererAdapter();

            adapter.SetColor( 0xFF0000 );
            adapter.MoveTo( 0, 0 );
            adapter.LineTo( 10, 10 );

            string output = _consoleOutput.ToString();
            StringAssert.Contains( "<color r=1,00 g=0,00 b=0,00 a=1,00>", output );
            StringAssert.Contains( "  <line fromX=0 fromY=0 toX=10 toY=10>", output );
        }

        [Test]
        public void LineToCall_WithGreenColor_ShouldConvertColorCorrectly()
        {
            ModernGraphicsRendererAdapter adapter = new ModernGraphicsRendererAdapter();

            adapter.SetColor( 0x00FF00 );
            adapter.MoveTo( 5, 5 );
            adapter.LineTo( 15, 15 );

            string output = _consoleOutput.ToString();
            StringAssert.Contains( "<color r=0,00 g=1,00 b=0,00 a=1,00>", output );
        }

        [Test]
        public void LineToCall_WithBlueColor_ShouldConvertColorCorrectly()
        {
            ModernGraphicsRendererAdapter adapter = new ModernGraphicsRendererAdapter();

            adapter.SetColor( 0x0000FF );
            adapter.MoveTo( 3, 3 );
            adapter.LineTo( 13, 13 );

            string output = _consoleOutput.ToString();
            StringAssert.Contains( "<color r=0,00 g=0,00 b=1,00 a=1,00>", output );
        }

        [Test]
        public void LineToCall_WithCustomColor_ShouldConvertColorCorrectly()
        {
            ModernGraphicsRendererAdapter adapter = new ModernGraphicsRendererAdapter();

            adapter.SetColor( 0x3A78F2 );
            adapter.MoveTo( 1, 1 );
            adapter.LineTo( 11, 11 );

            string output = _consoleOutput.ToString();
            StringAssert.Contains( "<color r=0,23 g=0,47 b=0,95 a=1,00>", output );
        }

        [Test]
        public void LineToCall_WithoutSetColor_ShouldUseDefaultBlackColor()
        {
            ModernGraphicsRendererAdapter adapter = new ModernGraphicsRendererAdapter();

            adapter.MoveTo( 0, 0 );
            adapter.LineTo( 5, 5 );

            string output = _consoleOutput.ToString();
            StringAssert.Contains( "<color r=0,00 g=0,00 b=0,00 a=1,00>", output );
        }

        [Test]
        public void LineToCall_WithMultipleColorChanges_ShouldUseCorrectColors()
        {
            ModernGraphicsRendererAdapter adapter = new ModernGraphicsRendererAdapter();

            adapter.SetColor( 0xFF0000 );
            adapter.MoveTo( 0, 0 );
            adapter.LineTo( 10, 10 );

            adapter.SetColor( 0x00FF00 );
            adapter.LineTo( 20, 20 );

            string output = _consoleOutput.ToString();
            StringAssert.Contains( "<color r=1,00 g=0,00 b=0,00 a=1,00>", output );
            StringAssert.Contains( "<color r=0,00 g=1,00 b=0,00 a=1,00>", output );
        }
    }
}