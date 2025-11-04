using GumballMachineApp.GumballMachines;
using GumballMachineApp.States;
using Moq;

namespace GumballMachineApp.Tests.StatesTests
{
    [TestFixture]
    public class SoldStateTests
    {
        private Mock<IGumballMachine> _gumballMachineMock;
        private SoldState _soldState;
        private StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _gumballMachineMock = new Mock<IGumballMachine>();
            _soldState = new SoldState( _gumballMachineMock.Object );

            _stringWriter = new StringWriter();
            Console.SetOut( _stringWriter );
        }

        [TearDown]
        public void TearDown()
        {
            _stringWriter?.Dispose();
        }

        [Test]
        public void InsertQuarter_WhenCalled_PrintsCorrectMessage()
        {
            // Act
            _soldState.InsertQuarter();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "Please wait, we're already giving you a gumball" ) );
        }

        [Test]
        public void EjectQuarter_WhenCalled_PrintsCorrectMessage()
        {
            // Act
            _soldState.EjectQuarter();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "Sorry, you already turned the crank" ) );
        }

        [Test]
        public void TurnCrank_WhenCalled_PrintsCorrectMessage()
        {
            // Act
            _soldState.TurnCrank();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "Turning twice doesn't get you another gumball" ) );
        }

        [Test]
        public void Dispense_WhenGumballsRemaining_SetsNoQuarterState()
        {
            // Arrange
            _gumballMachineMock.Setup( m => m.GetGumballsCount() ).Returns( 5 );

            // Act
            _soldState.Dispense();

            // Assert
            _gumballMachineMock.Verify( m => m.ReleaseBall(), Times.Once );
            _gumballMachineMock.Verify( m => m.SetNoQuarterState(), Times.Once );
            _gumballMachineMock.Verify( m => m.SetSoldOutState(), Times.Never );
        }

        [Test]
        public void Dispense_WhenNoGumballsLeft_SetsSoldOutState()
        {
            // Arrange
            _gumballMachineMock.Setup( m => m.GetGumballsCount() ).Returns( 0 );

            // Act
            _soldState.Dispense();

            // Assert
            _gumballMachineMock.Verify( m => m.ReleaseBall(), Times.Once );
            _gumballMachineMock.Verify( m => m.SetSoldOutState(), Times.Once );
            _gumballMachineMock.Verify( m => m.SetNoQuarterState(), Times.Never );
        }

        [Test]
        public void Dispense_WhenNoGumballsLeft_PrintsOutOfGumballsMessage()
        {
            // Arrange
            _gumballMachineMock.Setup( m => m.GetGumballsCount() ).Returns( 0 );

            // Act
            _soldState.Dispense();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "Oops, out of gumballs" ) );
        }

        [Test]
        public void Dispense_WhenGumballsRemaining_DoesNotPrintOutOfGumballsMessage()
        {
            // Arrange
            _gumballMachineMock.Setup( m => m.GetGumballsCount() ).Returns( 3 );

            // Act
            _soldState.Dispense();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.Empty );
        }

        [Test]
        public void ToString_WhenCalled_ReturnsCorrectString()
        {
            // Act
            string result = _soldState.ToString();

            // Assert
            Assert.That( result, Is.EqualTo( "delivering a gumball" ) );
        }

        [Test]
        public void Constructor_WhenCalled_SetsGumballMachine()
        {
            // Arrange
            Mock<IGumballMachine> machineMock = new Mock<IGumballMachine>();

            // Act
            SoldState state = new SoldState( machineMock.Object );

            // Assert
            Assert.That( state, Is.Not.Null );
        }
    }
}