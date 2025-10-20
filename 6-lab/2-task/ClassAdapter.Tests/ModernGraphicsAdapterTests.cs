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
            StringAssert.Contains( "  <line fromX=10 fromY=20 toX=30 toY=40 />", output );
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
            StringAssert.Contains( "  <line fromX=-10 fromY=-20 toX=30 toY=40 />", output );
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
            StringAssert.Contains( "  <line fromX=0 fromY=0 toX=30 toY=40 />", output );
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
            StringAssert.Contains( "  <line fromX=10 fromY=20 toX=30 toY=40 />", output );
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
            StringAssert.Contains( "  <line fromX=10 fromY=20 toX=30 toY=40 />", output );
            StringAssert.Contains( "  <line fromX=30 fromY=40 toX=50 toY=60 />", output );
        }

        [Test]
        public void LineToCall_WithNegativeArguments_ShouldBeSuccessfullyExecuted()
        {
            ModernGraphicsRendererAdapter adapter = new ModernGraphicsRendererAdapter();

            adapter.MoveTo( 10, 20 );
            adapter.LineTo( -30, -40 );

            string output = _consoleOutput.ToString();
            StringAssert.Contains( "<draw>", output );
            StringAssert.Contains( "  <line fromX=10 fromY=20 toX=-30 toY=-40 />", output );
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
            StringAssert.Contains( "  <line fromX=10 fromY=20 toX=0 toY=0 />", output );
            StringAssert.Contains( "</draw>", output );
        }
    }
}