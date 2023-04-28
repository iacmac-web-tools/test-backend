using MediatR;
using Microsoft.AspNetCore.Mvc;
using Theses.Api.Mappings.Create;
using Theses.Api.Mappings.Thesis;
using Theses.Api.Mappings.Update;
using Theses.Application.Theses.Commands.Delete;
using Theses.Application.Theses.Queries.Get;
using Theses.Application.Theses.Queries.GetAll;

namespace Theses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ThesesController : ControllerBase
{
    private readonly ISender _sender;

    public ThesesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateThesisDto dto)
    {
        var commandMapper = new CreateThesisDtoMapper();
        var command = commandMapper.CreateThesisDtoToCreateThesisCommand(dto);
        var result = await _sender.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        var thesisMapper = new ThesisMapper();
        var thesis = thesisMapper.ThesisToThesisDto(result.Value);
        
        return Ok(thesis);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ThesisDto>> Get(long id)
    {
        var result = await _sender.Send(new GetThesisQuery(id));
        if (result.IsFailed) return NotFound();

        var mapper = new ThesisMapper();
        var thesis = mapper.ThesisToThesisDto(result.Value);
        
        return Ok(thesis);
    }

    [HttpGet("all")]
    public async Task<ActionResult<IReadOnlyCollection<ThesisDto>>> GetAll()
    {
        var result = await _sender.Send(GetAllThesesQuery.Instance);
        var mapper = new ThesisMapper();
        var theses = result.Select(x => mapper.ThesisToThesisDto(x));
        
        return Ok(theses);
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<ThesisDto>> Update(long id, [FromBody] UpdateThesisDto dto)
    {
        var commandMapper = new UpdateThesisDtoMapper();
        var command = commandMapper.UpdateThesisDtoToUpdateThesisCommand(id, dto);
        var result = await _sender.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        var thesisMapper = new ThesisMapper();
        var thesis = thesisMapper.ThesisToThesisDto(result.Value);
        
        return Ok(thesis);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var command = new DeleteThesisCommand(id);
        var result = await _sender.Send(command);
        
        return result.IsSuccess
            ? Ok()
            : BadRequest(result.Errors);
    }
}
