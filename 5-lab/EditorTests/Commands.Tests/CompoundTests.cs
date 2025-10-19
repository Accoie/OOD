using Editor.Commands;
using Moq;

namespace EditorTests.Commands.Tests;

[TestFixture]
public class CompoundCommandTests
{
    private Mock<ICommand> _command1Mock;
    private Mock<ICommand> _command2Mock;
    private Mock<ICommand> _command3Mock;

    [SetUp]
    public void Setup()
    {
        _command1Mock = new Mock<ICommand>();
        _command2Mock = new Mock<ICommand>();
        _command3Mock = new Mock<ICommand>();
    }

    [Test]
    public void Execute_ExecutesAllCommandsInOrder()
    {
        // Arrange
        ICommand[] commands = new ICommand[]
        {
            _command1Mock.Object,
            _command2Mock.Object,
            _command3Mock.Object
        };
        CompoundCommand compoundCommand = new CompoundCommand( commands );

        // Act
        compoundCommand.Execute();

        // Assert
        _command1Mock.Verify( c => c.Execute(), Times.Once );
        _command2Mock.Verify( c => c.Execute(), Times.Once );
        _command3Mock.Verify( c => c.Execute(), Times.Once );
    }
    [Test]
    public void Unexecute_UnexecutesAllCommandsInReverseOrder()
    {
        // Arrange
        var sequence = new MockSequence();

        _command3Mock.InSequence( sequence ).Setup( c => c.Unexecute() );
        _command2Mock.InSequence( sequence ).Setup( c => c.Unexecute() );
        _command1Mock.InSequence( sequence ).Setup( c => c.Unexecute() );

        ICommand[] commands =
        {
            _command1Mock.Object,
            _command2Mock.Object,
            _command3Mock.Object
        };

        var compoundCommand = new CompoundCommand( commands );
        compoundCommand.Execute();

        // Act
        compoundCommand.Unexecute();

        // Assert
        _command3Mock.Verify( c => c.Unexecute(), Times.Once );
        _command2Mock.Verify( c => c.Unexecute(), Times.Once );
        _command1Mock.Verify( c => c.Unexecute(), Times.Once );
    }


    [Test]
    public void CommandsProperty_ReturnsCommands()
    {
        // Arrange
        ICommand[] commands = new ICommand[]
        {
            _command1Mock.Object,
            _command2Mock.Object
        };
        CompoundCommand compoundCommand = new CompoundCommand( commands );

        // Act & Assert
        Assert.That( compoundCommand.Commands.Count, Is.EqualTo( 2 ) );
        Assert.That( compoundCommand.Commands[ 0 ], Is.EqualTo( _command1Mock.Object ) );
        Assert.That( compoundCommand.Commands[ 1 ], Is.EqualTo( _command2Mock.Object ) );
    }

    [Test]
    public void Execute_Unexecute_Sequence_WorksCorrectly()
    {
        // Arrange
        ICommand[] commands =
        [
            _command1Mock.Object,
            _command2Mock.Object
        ];
        CompoundCommand compoundCommand = new CompoundCommand( commands );

        // Act & Assert
        Assert.DoesNotThrow( () =>
        {
            compoundCommand.Execute();
            compoundCommand.Unexecute();
        } );

        _command1Mock.Verify( c => c.Execute(), Times.Once );
        _command2Mock.Verify( c => c.Execute(), Times.Once );
        _command1Mock.Verify( c => c.Unexecute(), Times.Once );
        _command2Mock.Verify( c => c.Unexecute(), Times.Once );
    }
}