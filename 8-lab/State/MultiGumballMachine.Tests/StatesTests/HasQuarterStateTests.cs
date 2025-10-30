using GumballMachineApp.GumballMachines;
using GumballMachineApp.States;
using Moq;

namespace GumballMachineApp.Tests.StatesTests
{
    [TestFixture]
    public class HasQuarterStateTests
    {
        private Mock<IGumballMachine> _gumballMachineMock;
        private HasQuarterState _hasQuarterState;
        private StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _gumballMachineMock = new Mock<IGumballMachine>();
            _hasQuarterState = new HasQuarterState( _gumballMachineMock.Object );

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
            _hasQuarterState.InsertQuarter();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "You can't insert another one quarter" ) );
            _gumballMachineMock.Verify( g => g.GetGumballsCount(), Times.Once );
            _gumballMachineMock.Verify( g => g.GetQuartersLimit(), Times.Once );
            _gumballMachineMock.VerifyNoOtherCalls();
        }

        [Test]
        public void EjectQuarter_WhenCalled_EjectsQuarterAndSetsNoQuarterState()
        {
            // Act
            _hasQuarterState.EjectQuarter();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "Quarters ejected successfully" ) );
            _gumballMachineMock.Verify( m => m.SetNoQuarterState(), Times.Once );
            _gumballMachineMock.Verify( m => m.ResetQuarters(), Times.Once );
            _gumballMachineMock.VerifyNoOtherCalls();
        }

        [Test]
        public void TurnCrank_WhenCalled_TurnsCrankAndSetsSoldState()
        {
            // Act
            _hasQuarterState.TurnCrank();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "You turned the crank" ) );
            _gumballMachineMock.Verify( m => m.SetSoldState(), Times.Once );
            _gumballMachineMock.Verify( m => m.DeleteQuarter(), Times.Once );
            _gumballMachineMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Dispense_WhenCalled_PrintsCorrectMessage()
        {
            // Act
            _hasQuarterState.Dispense();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "You need to insert a quarter first" ) );
            _gumballMachineMock.VerifyNoOtherCalls();
        }

        [Test]
        public void ToString_WhenCalled_ReturnsCorrectString()
        {
            // Act
            string result = _hasQuarterState.ToString();

            // Assert
            Assert.That( result, Is.EqualTo( "waiting for turning the crank" ) );
        }

        [Test]
        public void Constructor_WhenCalled_SetsGumballMachine()
        {
            // Arrange
            Mock<IGumballMachine> machineMock = new Mock<IGumballMachine>();

            // Act
            HasQuarterState state = new HasQuarterState( machineMock.Object );

            // Assert
            Assert.That( state, Is.Not.Null );
        }

        [Test]
        public void EjectQuarter_WhenCalled_DoesNotCallOtherStateChanges()
        {
            // Act
            _hasQuarterState.EjectQuarter();

            // Assert
            _gumballMachineMock.Verify( m => m.SetNoQuarterState(), Times.Once );
            _gumballMachineMock.Verify( m => m.SetSoldState(), Times.Never );
            _gumballMachineMock.Verify( m => m.ResetQuarters(), Times.Once );
            _gumballMachineMock.VerifyNoOtherCalls();
        }

        [Test]
        public void TurnCrank_WhenCalled_DoesNotCallOtherStateChanges()
        {
            // Act
            _hasQuarterState.TurnCrank();

            // Assert
            _gumballMachineMock.Verify( m => m.SetSoldState(), Times.Once );
            _gumballMachineMock.Verify( m => m.DeleteQuarter(), Times.Once );
            _gumballMachineMock.VerifyNoOtherCalls();
        }
    }
}