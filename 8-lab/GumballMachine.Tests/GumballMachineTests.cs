using GumballMachineApp.GumballMachines;

namespace GumballMachineApp.Tests
{
    [TestFixture]
    public class GumballMachineTests
    {
        [Test]
        public void Constructor_WithPositiveBallsCount_SetsNoQuarterState()
        {
            // Act
            GumballMachine machine = new GumballMachine( 5 );

            // Assert
            Assert.That( machine.GetGumballsCount(), Is.EqualTo( 5 ) );
        }

        [Test]
        public void Constructor_WithZeroBallsCount_SetsSoldOutState()
        {
            // Act
            GumballMachine machine = new GumballMachine( 0 );

            // Assert
            Assert.That( machine.GetGumballsCount(), Is.EqualTo( 0 ) );
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
            using StringWriter sw = new StringWriter();
            Console.SetOut( sw );

            // Act
            machine.ReleaseBall();

            // Assert
            Assert.That( machine.GetGumballsCount(), Is.EqualTo( 2 ) );
            string output = sw.ToString().Trim();
            Assert.That( output, Is.EqualTo( "A gumball comes rolling out the slot..." ) );
        }

        [Test]
        public void ReleaseBall_WhenNoGumballs_DoesNothing()
        {
            // Arrange
            GumballMachine machine = new GumballMachine( 0 );
            using StringWriter sw = new StringWriter();
            Console.SetOut( sw );

            // Act
            machine.ReleaseBall();

            // Assert
            Assert.That( machine.GetGumballsCount(), Is.EqualTo( 0 ) );
            string output = sw.ToString().Trim();
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
    }
}