using NUnit.Framework;
using System;
using SimUDuck.Ducks;
using SimUDuck.DucksActions;

namespace SimUDuck.Tests;

public class DuckTests
{
    [Test]
    public void Dance_WithDuck_WillDance()
    {
        //Arrange
        bool wasDanced = false;
        Action danceBehavior = () => wasDanced = true;

        IDuck duck = new Duck(
            (() => 0, false), // flyBehavior
            () => { },         // quakBehavior
            danceBehavior     // danceBehavior
        );

        //Act
        duck.Dance();

        //Assert
        Assert.That( wasDanced, Is.True );
    }

    [Test]
    public void OnFly_WithDuckOnOddFlight_WillNotQuack()
    {
        //Arrange
        int quackCount = 0;
        Action quackBehavior = () => ++quackCount;

        IDuck duck = new Duck(
            FlyBehavior.FlyWithWings(),
            quackBehavior,
            () => { }
        );

        //Act
        duck.Fly();

        //Assert
        Assert.That( quackCount, Is.EqualTo( 0 ) );
    }

    [Test]
    public void OnFly_WithDuckOnEvenFlight_WillQuack()
    {
        //Arrange
        int quackCount = 0;
        Action quackBehavior = () => ++quackCount;

        IDuck duck = new Duck( FlyBehavior.FlyWithWings(), quackBehavior,() => { } );

        //Act
        duck.Fly();
        duck.Fly();

        //Assert
        Assert.That( quackCount, Is.EqualTo( 1 ) );
    }
}