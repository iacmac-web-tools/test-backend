using System.Net;
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

    /// <summary>
    /// Получение постраничного списка тезисов
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Справка по фильтрации: https://alirezanet.github.io/Gridify/guide/filtering.html.
    /// Справка по сортировке: https://alirezanet.github.io/Gridify/guide/ordering.html.
    /// </remarks>
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

    /// <summary>
    /// Добавить новый тезис
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ThesisDto>> Create([FromBody] CreateThesisDto dto)
    {
        var commandMapper = new CreateThesisDtoMapper();
        var command = commandMapper.CreateThesisDtoToCreateThesisCommand(dto);
        var result = await _sender.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        var thesisMapper = new ThesisMapper();
        var thesis = thesisMapper.ThesisToThesisDto(result.Value);

        return Ok(thesis);
    }

    /// <summary>
    /// Получение полного списка тезисов
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<ActionResult<IReadOnlyCollection<ThesisDto>>> GetAll()
    {
        var result = await _sender.Send(GetAllThesesQuery.Instance);
        var mapper = new ThesisMapper();
        var theses = mapper.ThesesToThesesDto(result);

        return Ok(theses);
    }

    /// <summary>
    /// Получить полную информацию по одному тезису
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    public async Task<ActionResult<ThesisDto>> Get(long id)
    {
        var result = await _sender.Send(new GetThesisQuery(id));
        if (result.IsFailed) return NotFound();

        var mapper = new ThesisMapper();
        var thesis = mapper.ThesisToThesisDto(result.Value);

        return Ok(thesis);
    }

    /// <summary>
    /// Изменить тезис
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Удалить тезис
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
