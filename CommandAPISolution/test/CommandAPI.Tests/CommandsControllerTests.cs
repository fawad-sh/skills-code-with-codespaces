using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AutoMapper;

using CommandAPI.Controllers;
using CommandAPI.Models;
using CommandAPI.Data;
using CommandAPI.Profiles;
using CommandAPI.Dtos;

namespace CommandAPI.Tests;

public class CommandsControllerTests : IDisposable
{
    Mock<ICommandAPIRepo> mockRepo;
    CommandsProfile realProfile;
    MapperConfiguration config;
    IMapper mapper; 

    public CommandsControllerTests()
    {
        mockRepo = new Mock<ICommandAPIRepo>();
        realProfile = new CommandsProfile();
        config = new MapperConfiguration( cfg => cfg.AddProfile(realProfile));
        mapper = new Mapper(config);
    }

    public void Dispose()
    {
        mockRepo = null;
        realProfile = null;
        config = null;
        mapper = null;
    }

    [Fact]
    public void GetCommandItems_ReturnZeroItems_WhenDBIsEmpty()
    {
        //Arrange
        mockRepo.Setup( repo =>
            repo.GetAllCommands()).Returns(GetCommands(0));

        var controller = new CommandsController(mockRepo.Object, mapper);

        //Act
        var result = controller.GetAllCommands();

        //Asserrt
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetAllCommands_ReturnOneItem_WhenDBHasOneResource()
    {
        //Arrange
        mockRepo.Setup( repo => 
            repo.GetAllCommands()).Returns(GetCommands(1));

        var controller = new CommandsController(mockRepo.Object, mapper);

        //Act
        var result = controller.GetAllCommands();

        //Assert
        var okResult = result.Result as OkObjectResult;
        var commands = okResult.Value as List<CommandReadDto>;

        Assert.Single(commands);
    }

    [Fact]
    public void GetAllCommands_Returns200Ok_WhenDBHasOneResource()
    {
        //Arrrange
        mockRepo.Setup( repo =>
            repo.GetAllCommands()).Returns(GetCommands(1));
        
        var controller = new CommandsController(mockRepo.Object, mapper);

        //Act
        var result = controller.GetAllCommands();

        //Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetAllCommands_ReturnsCorrectType_WhenDBHasOneResource()
    {
        //Arrange
        mockRepo.Setup( repo =>
            repo.GetAllCommands()).Returns(GetCommands(1));

        var controller = new CommandsController(mockRepo.Object, mapper);  

        //Act
        var result = controller.GetAllCommands();

        //Assert
        Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);

    }

    [Fact]
    public void GetCommandByID_Returns404NotFound_WhenNonExistentIdProvided()
    {
        //Arrange
        mockRepo.Setup( repo => repo.GetCommandById(0)).Returns(() => null);

        var controller = new CommandsController(mockRepo.Object, mapper);

        //Act
        var result = controller.GetCommandById(1);

        //Assert
        Assert.IsType<NotFoundResult>(result.Result);

    }

    [Fact]
    public void GetCommandById_Returns200Ok_WhenValidIdProvided()
    {
        //Arrange
        mockRepo.Setup( repo => 
            repo.GetCommandById(1)).Returns(new Command { Id=1,
            HowTo="mock",Line="Mock",Platform="Moq"});
        
        var controller = new CommandsController(mockRepo.Object, mapper);

        //Act
        var result = controller.GetCommandById(1);

        //Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetCommandById_ReturnsCorrectType_WhenValidIdProvided()
    {
        //Arrange
        mockRepo.Setup( repo => 
            repo.GetCommandById(1)).Returns(new Command { Id=1,
            HowTo="mock",Line="Mock",Platform="Moq"});
        
        var controller = new CommandsController(mockRepo.Object, mapper);

        //Act
        var result = controller.GetCommandById(1);

        //Assert
        Assert.IsType<ActionResult<CommandReadDto>>(result);
    }

    [Fact]
    public void CreateCommand_ReturnsCorrectType_WhenValidObjectSubmitted()
    {
        //Arrange
        mockRepo.Setup( repo => 
            repo.GetCommandById(1)).Returns(new Command{
                Id=1,HowTo="mock",Line="Mock",Platform="Moq"
            });

        var controller = new CommandsController(mockRepo.Object, mapper);

        //Act
        var result = controller.CreateCommand(new CommandCreateDto {});

        //Assert
        Assert.IsType<ActionResult<CommandReadDto>>(result);

    }

    [Fact]
    public void CreateCommand_Returns201Created_WhenValidObjectSubmitted()
    {
        //Arrange
        mockRepo.Setup( repo => 
            repo.GetCommandById(1)).Returns(new Command{
                Id=1,HowTo="mock",Line="Mock",Platform="Moq"
            });

        var controller = new CommandsController(mockRepo.Object, mapper);

        //Act
        var result = controller.CreateCommand(new CommandCreateDto {});

        //Assert
        Assert.IsType<CreatedAtRouteResult>(result.Result);

    }

    [Fact]
    public void UpdateCommand_Returns204NoContent_WhenValidObjectSubmitted()
    {
        //Arrange
        mockRepo.Setup( repo =>
            repo.GetCommandById(1)).Returns(new Command{Id=1,HowTo="mock",Line="Mock",Platform="Moq"});

        var controller = new CommandsController(mockRepo.Object, mapper);

        //Act
        var result = controller.UpdateCommand(1, new CommandUpdateDto{});

        //Assert
        Assert.IsType<NoContentResult>(result);

    }

    [Fact]
    public void UpdateCommand_Returns404NotFound_WhenNonExistentResourceIdSubmitted()
    {
        //Arrange
        mockRepo.Setup( repo =>
            repo.GetCommandById(1)).Returns( () => null);

        var controller = new CommandsController(mockRepo.Object, mapper);

        //Act
        var result = controller.UpdateCommand(1, new CommandUpdateDto{});

        //Assert
        Assert.IsType<NotFoundResult>(result);

    }

    [Fact]
    public void DeleteCommand_Returns204_WhenValidResourceIdSubmitted()
    {
        //Arrange
        mockRepo.Setup( repo =>
            repo.GetCommandById(1)).Returns(new Command{Id=1,HowTo="mock",Line="Mock",Platform="Moq"});
        
        var controller = new CommandsController(mockRepo.Object, mapper);

        //Act
        var result = controller.DeleteCommand(1);

        //Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void DeleteCommand_Returns404_WhenNonExistentResourceIdSubmitted()
    {
        //Arrange
        mockRepo.Setup( repo =>
            repo.GetCommandById(1)).Returns(() => null);
        
        var controller = new CommandsController(mockRepo.Object, mapper);

        //Act
        var result = controller.DeleteCommand(1);

        //Assert
        Assert.IsType<NotFoundResult>(result);
    }

    private List<Command> GetCommands(int num)
    {
        var commands = new List<Command>();
        if (num > 0)
        {
            commands.Add(new Command {
                Id =0,
                HowTo = "How to build a .Net app",
                Line = "dotnet build",
                Platform = ".Net CLI"
            });
        }
        return commands;
    }

}