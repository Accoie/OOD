using Moq;
using SimUDuck.Ducks;
using SimUDuck.DucksActions.Dance;
using SimUDuck.DucksActions.Fly;
using SimUDuck.DucksActions.Quack;

namespace SimUDuck.Tests;

public class DuckTests
{
    private readonly Mock<IFlyBehavior> _flyBehaviorMock = new();
    private readonly Mock<IQuackBehavior> _quackBehaviorMock = new();
    private readonly Mock<IDanceBehavior> _danceBehaviorMock = new();

    [Test]
    public void Dance_WithDuck_WillDance()
    {
        //Arrange
        bool danced = false;
        Mock<Duck> duck = new Mock<Duck>( _flyBehaviorMock.Object, _quackBehaviorMock.Object, _danceBehaviorMock.Object );
        _danceBehaviorMock.Setup( x => x.Dance() ).Callback( () => danced = true );
        duck.CallBase = true;
        //Act
        duck.Object.Dance();

        //Assert
        Assert.That( danced, Is.True );
    }

    [Test]
    public void Fly_WithDuckOnOddFlight_WillNotQuack()
    {
        //Arrange
        bool quacked = false;
        Mock<Duck> duck = new Mock<Duck>( _flyBehaviorMock.Object, _quackBehaviorMock.Object, _danceBehaviorMock.Object );
        _quackBehaviorMock.Setup( x => x.Quack() ).Callback( () => quacked = true );
        duck.CallBase = true;
        //Act
        duck.Object.Fly();

        //Assert
        Assert.That( quacked, Is.False );
    }

    [Test]
    public void Fly_WithDuckOnEvenFlight_WillQuack()
    {
        //Arrange
        bool isQuacked = false;
        Mock<Duck> duck = new Mock<Duck>( _flyBehaviorMock.Object, _quackBehaviorMock.Object, _danceBehaviorMock.Object );
        _quackBehaviorMock.Setup( x => x.Quack() ).Callback( () => isQuacked = true );
        duck.CallBase = true;
        //Act
        duck.Object.Fly();
        duck.Object.Fly();
        //Assert
        Assert.That( isQuacked, Is.True );
    }
}