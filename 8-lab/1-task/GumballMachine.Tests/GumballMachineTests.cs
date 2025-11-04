using GumballMachineApp.GumballMachines;

namespace GumballMachineApp.Tests
{
    [TestFixture]
    public class GumballMachineTests
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
        public void Constructor_WithPositiveBallsCount_InitializesInNoQuarterState()
        {
            // Arrange
            IGumballMachineClient machine = new GumballMachine( 5 );

            // Act
            string result = machine.ToString();

            // Assert
            Assert.That( result, Contains.Substring( "Machine is waiting for quarter" ) );
        }

        [Test]
        public void Constructor_WithZeroBallsCount_InitializesInSoldOutState()
        {
            // Arrange
            IGumballMachineClient machine = new GumballMachine( 0 );

            // Act
            string result = machine.ToString();

            // Assert
            Assert.That( result, Contains.Substring( "Machine is sold out" ) );
        }

        [Test]
        public void Constructor_WithNegativeBallsCount_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>( () => new GumballMachine( -1 ) );
        }

        [Test]
        public void InsertQuarter_WhenInNoQuarterState_ChangesState()
        {
            // Arrange
            IGumballMachineClient machine = new GumballMachine( 3 );

            // Act
            machine.InsertQuarter();

            // Assert
            string result = machine.ToString();
            Assert.That( result, Contains.Substring( "Machine is waiting for turning the crank" ) );
        }

        [Test]
        public void EjectQuarter_WhenHasQuarter_ReturnsToNoQuarterState()
        {
            // Arrange
            IGumballMachineClient machine = new GumballMachine( 3 );
            machine.InsertQuarter();

            // Act
            machine.EjectQuarter();

            // Assert
            string result = machine.ToString();
            Assert.That( result, Contains.Substring( "Machine is waiting for quarter" ) );
        }

        [Test]
        public void TurnCrank_WhenHasQuarter_DispensesGumball()
        {
            // Arrange
            IGumballMachineClient machine = new GumballMachine( 3 );
            machine.InsertQuarter();

            // Act
            machine.TurnCrank();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Contains.Substring( "A gumball comes rolling out the slot..." ) );

            string machineState = machine.ToString();
            Assert.That( machineState, Contains.Substring( "Inventory: 2 gumballs" ) );
        }

        [Test]
        public void TurnCrank_WhenNoQuarter_DoesNothing()
        {
            // Arrange
            IGumballMachineClient machine = new GumballMachine( 3 );

            // Act
            machine.TurnCrank();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Does.Not.Contain( "A gumball comes rolling out the slot..." ) );
        }

        [Test]
        public void TurnCrank_WhenSoldOut_DoesNothing()
        {
            // Arrange
            IGumballMachineClient machine = new GumballMachine( 0 );

            // Act
            machine.TurnCrank();

            // Assert
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Does.Not.Contain( "A gumball comes rolling out the slot..." ) );
        }

        [Test]
        public void ToString_WithMultipleGumballs_ReturnsCorrectString()
        {
            // Arrange
            IGumballMachineClient machine = new GumballMachine( 3 );

            // Act
            string result = machine.ToString();

            // Assert
            Assert.That( result, Contains.Substring( "Inventory: 3 gumballs" ) );
            Assert.That( result, Contains.Substring( "Machine is waiting for quarter" ) );
        }

        [Test]
        public void ToString_WithSingleGumball_ReturnsCorrectString()
        {
            // Arrange
            IGumballMachineClient machine = new GumballMachine( 1 );

            // Act
            string result = machine.ToString();

            // Assert
            Assert.That( result, Contains.Substring( "Inventory: 1 gumball" ) );
        }

        [Test]
        public void ToString_AfterGumballDispensed_ShowsUpdatedCount()
        {
            // Arrange
            IGumballMachineClient machine = new GumballMachine( 2 );
            machine.InsertQuarter();

            // Act
            machine.TurnCrank();

            // Assert
            string result = machine.ToString();
            Assert.That( result, Contains.Substring( "Inventory: 1 gumball" ) );
        }

        [Test]
        public void CompleteWorkflow_FromInsertToDispense_WorksCorrectly()
        {
            // Arrange
            IGumballMachineClient machine = new GumballMachine( 2 );

            // Act & Assert
            machine.InsertQuarter();
            string afterInsert = machine.ToString();
            Assert.That( afterInsert, Contains.Substring( "Machine is waiting for turning the crank" ) );

            // Act & Assert
            machine.TurnCrank();
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Contains.Substring( "A gumball comes rolling out the slot..." ) );

            // Act & Assert
            string finalState = machine.ToString();
            Assert.That( finalState, Contains.Substring( "Inventory: 1 gumball" ) );
            Assert.That( finalState, Contains.Substring( "Machine is waiting for quarter" ) );
        }
    }
}