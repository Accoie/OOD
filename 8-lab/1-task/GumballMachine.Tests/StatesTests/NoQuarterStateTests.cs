using GumballMachineApp.GumballMachines;
using GumballMachineApp.States;
using Moq;

namespace GumballMachineApp.Tests.StatesTests
{
    [TestFixture]
    public class NoQuarterStateTests
    {
        private Mock<IGumballMachine> _gumballMachineMock;
        private NoQuarterState _noQuarterState;
        private StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _gumballMachineMock = new Mock<IGumballMachine>();
            _noQuarterState = new NoQuarterState( _gumballMachineMock.Object );

            _stringWriter = new StringWriter();
            Console.SetOut( _stringWriter );
        }

        [TearDown]
        public void TearDown()
        {
            _stringWriter?.Dispose();
        }

        [Test]
        public void InsertQuarter_WhenCalled_SetsHasQuarterState()
        {
            // Act
            _noQuarterState.InsertQuarter();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "You inserted a quarter" ) );
            _gumballMachineMock.Verify( m => m.SetHasQuarterState(), Times.Once );
        }

        [Test]
        public void EjectQuarter_WhenCalled_PrintsCorrectMessage()
        {
            // Act
            _noQuarterState.EjectQuarter();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "You haven't inserted a quarter" ) );
            _gumballMachineMock.VerifyNoOtherCalls();
        }

        [Test]
        public void TurnCrank_WhenCalled_PrintsCorrectMessage()
        {
            // Act
            _noQuarterState.TurnCrank();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "You turned but there's no quarter" ) );
            _gumballMachineMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Dispense_WhenCalled_PrintsCorrectMessage()
        {
            // Act
            _noQuarterState.Dispense();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "You need to pay first" ) );
            _gumballMachineMock.VerifyNoOtherCalls();
        }

        [Test]
        public void ToString_WhenCalled_ReturnsCorrectString()
        {
            // Act
            string result = _noQuarterState.ToString();

            // Assert
            Assert.That( result, Is.EqualTo( "waiting for quarter" ) );
        }

        [Test]
        public void Constructor_WhenCalled_SetsGumballMachine()
        {
            // Arrange
            Mock<IGumballMachine> machineMock = new Mock<IGumballMachine>();

            // Act
            NoQuarterState state = new NoQuarterState( machineMock.Object );

            // Assert
            Assert.That( state, Is.Not.Null );
        }

        [Test]
        public void InsertQuarter_WhenCalled_DoesNotCallOtherStateChanges()
        {
            // Act
            _noQuarterState.InsertQuarter();

            // Assert
            _gumballMachineMock.Verify( m => m.SetHasQuarterState(), Times.Once );
            _gumballMachineMock.VerifyNoOtherCalls();
        }
    }
}