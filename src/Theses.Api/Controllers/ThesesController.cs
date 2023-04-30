using MediatR;
using Microsoft.AspNetCore.Mvc;
using Theses.Api.Filters;
using Theses.Api.Mappings.Create;
using Theses.Api.Mappings.Get;
using Theses.Api.Mappings.Thesis;
using Theses.Api.Mappings.Update;
using Theses.Application.Common.Models;
using Theses.Application.Theses.Commands.Delete;
using Theses.Application.Theses.Queries.Get;
using Theses.Application.Theses.Queries.GetAll;

namespace Theses.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExceptionFilter]
public class ThesesController : ControllerBase
{
    private readonly ISender _sender;

    public ThesesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<ThesisDto>>> GetPaginatedList([FromQuery] GetPaginatedListDto dto)
    {
        var commandMapper = new GetPaginatedListDtoMapper();
        var command = commandMapper.GetPaginatedListDtoToGetPaginatedListQuery(dto);
        var result = await _sender.Send(command);
        var thesisMapper = new ThesisMapper();
        var theses = thesisMapper.PaginatedThesesToPaginatedThesesDto(result);
        return Ok(theses);
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

    [HttpGet("all")]
    public async Task<ActionResult<IReadOnlyCollection<ThesisDto>>> GetAll()
    {
        var result = await _sender.Send(GetAllThesesQuery.Instance);
        var mapper = new ThesisMapper();
        var theses = mapper.ThesesToThesesDto(result);

        return Ok(theses);
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
