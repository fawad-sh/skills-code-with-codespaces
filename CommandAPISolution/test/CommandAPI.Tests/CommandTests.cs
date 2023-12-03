using CommandAPI.Models;

namespace CommandAPI.Tests;

public class CommandTests : IDisposable
{
    Command testCommand;

    public CommandTests()
    {
        testCommand = new Command 
        {
            HowTo = "Do something awesome",
            Line = "dotnet test",
            Platform = "xUnit"
        };
    }

    public void Dispose()
    {
        testCommand = null;
    }

    [Fact]
    public void CanChangeHowTo()
    {
        //Arrange

        //Act
        testCommand.HowTo = "Execute unit tests";

        //Assert
        Assert.Equal("Execute unit tests", testCommand.HowTo);

    }

    [Fact]
    public void CanChangeLine()
    {
        //Arrange

        //Act
        testCommand.Platform = "NUnit";

        //Assert
        Assert.Equal("xUnit", testCommand.Platform);

    }
}