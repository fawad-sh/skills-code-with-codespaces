using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AutoMapper;

using CommandAPI.Controllers;
using CommandAPI.Models;
using CommandAPI.Data;
using CommandAPI.Profiles;


namespace CommandAPI.Tests;

public class CommandsControlleorTests
{
    [Fact]
    public void GetCommandItems_ReturnZeroItems_WhenDBIsEmpty()
    {
        //Arrange
        var mockRepo = new Mock<ICommandAPIRepo>();
        mockRepo.Setup( repo =>
            repo.GetAllCommands()).Returns(GetCommands(0));

        var realProfile = new CommandsProfile();
        var config = new MapperConfiguration( cfg => 
            cfg.AddProfile(realProfile));
        IMapper mapper = new Mapper(config);    

        var controller = new CommandsController(mockRepo.Object, mapper);
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