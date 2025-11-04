using MultiGumballMachineApp.GumballMachines;
using MultiGumballMachineApp.States;
using Moq;

namespace MultiGumballMachineApp.Tests.StatesTests
{
    [TestFixture]
    public class SoldOutStateTests
    {
        private Mock<IGumballMachine> _gumballMachineMock;
        private SoldOutState _soldOutState;
        private StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _gumballMachineMock = new Mock<IGumballMachine>();
            _soldOutState = new SoldOutState( _gumballMachineMock.Object );

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
            _soldOutState.InsertQuarter();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "You can't insert a quarter, the machine is sold out" ) );
        }

        [Test]
        public void EjectQuarter_WhenCalled_PrintsCorrectMessage()
        {
            // Act
            _soldOutState.EjectQuarter();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "You can't eject, you haven't inserted a quarter yet" ) );
        }

        [Test]
        public void TurnCrank_WhenCalled_PrintsCorrectMessage()
        {
            // Act
            _soldOutState.TurnCrank();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "You turned but there're no gumballs" ) );
        }

        [Test]
        public void Dispense_WhenCalled_PrintsCorrectMessage()
        {
            // Act
            _soldOutState.Dispense();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "No gumball dispensed" ) );
        }

        [Test]
        public void ToString_WhenCalled_ReturnsCorrectString()
        {
            // Act
            string result = _soldOutState.ToString();

            // Assert
            Assert.That( result, Is.EqualTo( "sold out" ) );
        }

        [Test]
        public void Constructor_WhenCalled_SetsGumballMachine()
        {
            // Arrange
            Mock<IGumballMachine> machineMock = new Mock<IGumballMachine>();

            // Act
            SoldOutState state = new SoldOutState( machineMock.Object );

            // Assert
            Assert.That( state, Is.Not.Null );
        }

        [Test]
        public void Methods_WhenCalled_DoNotChangeState()
        {
            // Act & Assert
            _soldOutState.InsertQuarter();
            _gumballMachineMock.VerifyNoOtherCalls();

            _soldOutState.EjectQuarter();
            _gumballMachineMock.Verify( m => m.GetQuartersCount(), Times.Once );
            _gumballMachineMock.VerifyNoOtherCalls();

            _soldOutState.TurnCrank();
            _gumballMachineMock.VerifyNoOtherCalls();

            _soldOutState.Dispense();
            _gumballMachineMock.VerifyNoOtherCalls();
        }
    }
}