using Moq;
using MultiGumballMachineApp.GumballMachines;
using MultiGumballMachineApp.States;

namespace MultiGumballMachineApp.Tests
{
    [TestFixture]
    public class MultiGumballMachineTests
    {
        private StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _stringWriter = new StringWriter();
            Console.SetOut( _stringWriter );
        }

        [TearDown]
        public void TearDown()
        {
            _stringWriter?.Dispose();
        }

        [Test]
        public void Constructor_WithNegativeBallsCount_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>( () => new GumballMachine( -1 ) );
        }

        [Test]
        public void RefillGumballs_WithNegativeGumballs_WillThrow()
        {
            // Arrange
            IGumballMachineClient machine = new GumballMachine( 5 );

            // Act & Assert
            Assert.Throws<ArgumentException>( () => machine.RefillGumballs( -1 ) );
        }

        [Test]
        public void TurnCrank_WhenNoBalls_ReturnsCorrectString()
        {
            // Arrange
            IGumballMachineClient machine = new GumballMachine( 0 );

            // Act
            machine.TurnCrank();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Contains.Substring( "You turned but there're no gumballs" ) );
            Assert.That( output, Contains.Substring( "No gumball dispensed" ) );
        }

        [Test]
        public void TurnCrank_WhenBallsAreEnded_CannotTurnCrankAgain()
        {
            // Arrange
            IGumballMachineClient machine = new GumballMachine( 3 );

            // Act
            machine.InsertQuarter();
            machine.InsertQuarter();
            machine.InsertQuarter();
            machine.InsertQuarter();
            machine.TurnCrank();
            machine.TurnCrank();
            machine.TurnCrank();

            machine.TurnCrank();

            string output = _stringWriter.ToString().Trim();
            string result = machine.ToString() ?? "";
            // Assert
            Assert.That( output, Contains.Substring( "You turned but there're no gumballs" ) );
            Assert.That( output, Contains.Substring( "No gumball dispensed" ) );
            Assert.That( result, Contains.Substring( "Inventory: 0 gumballs" ) );
            Assert.That( result, Contains.Substring( "Quarters: 1" ) );
            Assert.That( result, Contains.Substring( "Machine is sold out" ) );
        }

        [Test]
        public void ToString_WithMultipleGumballsAndQuarters_ReturnsCorrectString()
        {
            // Arrange
            IGumballMachineClient machine = new GumballMachine( 3 );

            // Act
            machine.InsertQuarter();
            machine.InsertQuarter();
            string result = machine.ToString() ?? "";

            // Assert
            Assert.That( result, Contains.Substring( "Inventory: 3 gumballs" ) );
            Assert.That( result, Contains.Substring( "Quarters: 2" ) );
            Assert.That( result, Contains.Substring( "Machine is waiting for turning the crank" ) );
        }

        [Test]
        public void ToString_WithSingleGumball_ReturnsCorrectString()
        {
            // Arrange
            IGumballMachineClient machine = new GumballMachine( 1 );

            // Act
            string result = machine.ToString() ?? "";

            // Assert
            Assert.That( result, Contains.Substring( "Inventory: 1 gumball" ) );
            Assert.That( result, Contains.Substring( "Quarters: 0" ) );
        }
    }
}