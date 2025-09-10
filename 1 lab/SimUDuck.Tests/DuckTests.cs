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
        bool wasDanced = false;
        IDuck duck = new Duck( _flyBehaviorMock.Object, _quackBehaviorMock.Object, _danceBehaviorMock.Object );
        _danceBehaviorMock.Setup( x => x.Dance() ).Callback( () => wasDanced = true );

        //Act
        duck.Dance();

        //Assert
        Assert.That( wasDanced, Is.True );
    }

    [Test]
    public void OnFly_WithDuckOnOddFlight_WillNotQuack()
    {
        //Arrange
        bool isQuacked = false;
        IDuck duck = new Duck( _flyBehaviorMock.Object, _quackBehaviorMock.Object, _danceBehaviorMock.Object );
        _quackBehaviorMock.Setup( x => x.Quack() ).Callback( () => isQuacked = true );

        //Act
        duck.Fly();

        //Assert
        Assert.That( isQuacked, Is.False );
    }

    [Test]
    public void OnFly_WithDuckOnEvenFlight_WillQuack()
    {
        //Arrange
        bool isQuacked = false;
        IDuck duck = new Duck( _flyBehaviorMock.Object, _quackBehaviorMock.Object, _danceBehaviorMock.Object );
        _quackBehaviorMock.Setup( x => x.Quack() ).Callback( () => isQuacked = true );

        //Act
        duck.Fly();
        duck.Fly();
        //Assert
        Assert.That( isQuacked, Is.True );
    }
}