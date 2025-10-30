using GumballMachineApp.GumballMachines;

namespace GumballMachineApp.Tests
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
        public void Constructor_WithPositiveBallsCount_SetsNoQuarterState()
        {
            // Act
            GumballMachine machine = new GumballMachine( 5 );

            // Assert
            Assert.That( machine.GetGumballsCount(), Is.EqualTo( 5 ) );
            Assert.That( machine.GetQuarters(), Is.EqualTo( 0 ) );
        }

        [Test]
        public void Constructor_WithZeroBallsCount_SetsSoldOutState()
        {
            // Act
            GumballMachine machine = new GumballMachine( 0 );

            // Assert
            Assert.That( machine.GetGumballsCount(), Is.EqualTo( 0 ) );
            Assert.That( machine.GetQuarters(), Is.EqualTo( 0 ) );
        }

        [Test]
        public void Constructor_WithNegativeBallsCount_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>( () => new GumballMachine( -1 ) );
        }

        [Test]
        public void ReleaseBall_WhenGumballsAvailable_DecreasesCountAndPrintsMessage()
        {
            // Arrange
            GumballMachine machine = new GumballMachine( 3 );

            // Act
            machine.ReleaseBall();

            // Assert
            Assert.That( machine.GetGumballsCount(), Is.EqualTo( 2 ) );
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.EqualTo( "A gumball comes rolling out the slot..." ) );
        }

        [Test]
        public void ReleaseBall_WhenNoGumballs_DoesNothing()
        {
            // Arrange
            GumballMachine machine = new GumballMachine( 0 );

            // Act
            machine.ReleaseBall();

            // Assert
            Assert.That( machine.GetGumballsCount(), Is.EqualTo( 0 ) );
            string output = _stringWriter.ToString().Trim();
            Assert.That( output, Is.Empty );
        }

        [Test]
        public void GetGumballsCount_ReturnsCorrectCount()
        {
            // Arrange
            GumballMachine machine = new GumballMachine( 7 );

            // Act
            int count = machine.GetGumballsCount();

            // Assert
            Assert.That( count, Is.EqualTo( 7 ) );
        }

        [Test]
        public void ToString_WithMultipleGumballs_ReturnsCorrectString()
        {
            // Arrange
            GumballMachine machine = new GumballMachine( 3 );

            // Act
            string result = machine.ToString();

            // Assert
            Assert.That( result, Contains.Substring( "Inventory: 3 gumballs" ) );
            Assert.That( result, Contains.Substring( "Quarters: 0" ) );
            Assert.That( result, Contains.Substring( "Machine is waiting for quarter" ) );
        }

        [Test]
        public void ToString_WithSingleGumball_ReturnsCorrectString()
        {
            // Arrange
            GumballMachine machine = new GumballMachine( 1 );

            // Act
            string result = machine.ToString();

            // Assert
            Assert.That( result, Contains.Substring( "Inventory: 1 gumball" ) );
            Assert.That( result, Contains.Substring( "Quarters: 0" ) );
        }

        [Test]
        public void SetStateMethods_ChangeCurrentState()
        {
            // Arrange
            GumballMachine machine = new GumballMachine( 3 );

            // Act & Assert
            machine.SetSoldOutState();
            string soldOutString = machine.ToString();

            Assert.That( soldOutString, Contains.Substring( "Machine is sold out" ) );

            machine.SetNoQuarterState();
            string noQuarterString = machine.ToString();

            Assert.That( noQuarterString, Contains.Substring( "Machine is waiting for quarter" ) );

            machine.SetHasQuarterState();
            string hasQuarterString = machine.ToString();

            Assert.That( hasQuarterString, Contains.Substring( "Machine is waiting for turning the crank" ) );

            machine.SetSoldState();
            string soldString = machine.ToString();

            Assert.That( soldString, Contains.Substring( "Machine is delivering a gumball" ) );
        }

        [Test]
        public void AddQuarter_WhenUnderLimit_IncreasesQuarterCount()
        {
            // Arrange
            GumballMachine machine = new GumballMachine( 3 );

            // Act
            machine.AddQuarter();

            // Assert
            Assert.That( machine.GetQuarters(), Is.EqualTo( 1 ) );
        }

        [Test]
        public void AddQuarter_WhenAtLimit_PrintsErrorMessage()
        {
            // Arrange
            GumballMachine machine = new GumballMachine( 3 );
            for ( int i = 0; i < machine.GetQuartersLimit(); i++ )
            {
                machine.AddQuarter();
            }

            // Act
            machine.AddQuarter();

            // Assert
            string output = _stringWriter.ToString().Trim();

            Assert.That( output, Contains.Substring( "Can't add quarter, bcs its too more" ) );
            Assert.That( output, Contains.Substring( $"Limit is {machine.GetQuartersLimit()}" ) );
            Assert.That( machine.GetQuarters(), Is.EqualTo( machine.GetQuartersLimit() ) );
        }

        [Test]
        public void DeleteQuarter_WhenQuartersAvailable_DecreasesQuarterCount()
        {
            // Arrange
            GumballMachine machine = new GumballMachine( 3 );
            machine.AddQuarter();
            machine.AddQuarter();

            // Act
            machine.DeleteQuarter();

            // Assert
            Assert.That( machine.GetQuarters(), Is.EqualTo( 1 ) );
        }

        [Test]
        public void DeleteQuarter_WhenNoQuarters_PrintsErrorMessage()
        {
            // Arrange
            GumballMachine machine = new GumballMachine( 3 );

            // Act
            machine.DeleteQuarter();

            // Assert
            string output = _stringWriter.ToString().Trim();

            Assert.That( output, Is.EqualTo( "There is no quarters" ) );
            Assert.That( machine.GetQuarters(), Is.EqualTo( 0 ) );
        }

        [Test]
        public void ResetQuarters_WhenQuartersAvailable_SetsQuartersToZero()
        {
            // Arrange
            GumballMachine machine = new GumballMachine( 3 );
            machine.AddQuarter();
            machine.AddQuarter();
            machine.AddQuarter();

            // Act
            machine.ResetQuarters();

            // Assert
            Assert.That( machine.GetQuarters(), Is.EqualTo( 0 ) );
        }

        [Test]
        public void ResetQuarters_WhenNoQuarters_DoesNothing()
        {
            // Arrange
            GumballMachine machine = new GumballMachine( 3 );

            // Act
            machine.ResetQuarters();

            // Assert
            Assert.That( machine.GetQuarters(), Is.EqualTo( 0 ) );
        }

        [Test]
        public void GetQuartersLimit_ReturnsCorrectLimit()
        {
            // Arrange
            GumballMachine machine = new GumballMachine( 3 );

            // Act
            int limit = machine.GetQuartersLimit();

            // Assert
            Assert.That( limit, Is.EqualTo( 5 ) );
        }

        [Test]
        public void ToString_WithQuarters_ShowsCorrectQuarterCount()
        {
            // Arrange
            GumballMachine machine = new GumballMachine( 3 );
            machine.AddQuarter();
            machine.AddQuarter();

            // Act
            string result = machine.ToString();

            // Assert
            Assert.That( result, Contains.Substring( "Quarters: 2" ) );
        }

        [Test]
        public void MultipleOperations_WithQuarters_WorkCorrectly()
        {
            // Arrange
            GumballMachine machine = new GumballMachine( 5 );

            // Act & Assert
            machine.AddQuarter();
            Assert.That( machine.GetQuarters(), Is.EqualTo( 1 ) );

            machine.AddQuarter();
            Assert.That( machine.GetQuarters(), Is.EqualTo( 2 ) );

            machine.DeleteQuarter();
            Assert.That( machine.GetQuarters(), Is.EqualTo( 1 ) );

            machine.ResetQuarters();
            Assert.That( machine.GetQuarters(), Is.EqualTo( 0 ) );

            for ( int i = 0; i < machine.GetQuartersLimit(); i++ )
            {
                machine.AddQuarter();
            }
            Assert.That( machine.GetQuarters(), Is.EqualTo( machine.GetQuartersLimit() ) );
        }
    }
}