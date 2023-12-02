using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;


using CommandAPI.Data;
using CommandAPI.Models;
using CommandAPI.Dtos;
using CommandAPI.Profiles;

namespace CommandAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommandsController : ControllerBase
{
    private readonly ICommandAPIRepo _repository ;
    private readonly IMapper _mapper;

    public CommandsController(ICommandAPIRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
    {
        var commandItems = _repository.GetAllCommands();
        return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
    }

    [HttpGet("{id}",  Name="GetCommandById")]
    public ActionResult<CommandReadDto> GetCommandById(int id)
    {
        var commandItem = _repository.GetCommandById(id);
        if (commandItem == null)
            return NotFound();
        else
            return Ok(_mapper.Map<CommandReadDto>(commandItem));
    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto cmd)
    {
        var commandModel = _mapper.Map<Command>(cmd);
        _repository.CreateCommand(commandModel);
        _repository.SaveChanges();

        var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

        return CreatedAtRoute(nameof(GetCommandById), 
            new {Id = commandReadDto.Id}, commandReadDto);
    } 

    [HttpPut("{id}")]
    public ActionResult UpdateCommand(int id, CommandUpdateDto cmd)
    {
        //existing instance in DB
        var commandModel = _repository.GetCommandById(id);
        if (commandModel == null)
        {
            return NotFound();
        }

        // map exisiting instance in db with passed obj/data i.e. cmd
        // map cmd to commandModel
        _mapper.Map(cmd, commandModel);
        _repository.UpdateCommand(commandModel);
        _repository.SaveChanges();

        return NoContent();

    }

    [HttpPatch("{id}")]
    public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
    {
        var commandModelFromRepo = _repository.GetCommandById(id);
        if (commandModelFromRepo == null)
        {
            return NotFound();
        }

        var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
        patchDoc.ApplyTo(commandToPatch, ModelState);

        if (!TryValidateModel(commandToPatch))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(commandToPatch, commandModelFromRepo);
        _repository.UpdateCommand(commandModelFromRepo);
        _repository.SaveChanges();

        return NoContent();
        
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCommand(int id)
    {
        var commandModelFromRepo = _repository.GetCommandById(id);
        if (commandModelFromRepo == null)
        {
            return NotFound();
        }
        _repository.DeleteCommand(commandModelFromRepo);
        _repository.SaveChanges();

        return NoContent();
    }
}