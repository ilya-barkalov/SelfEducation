using Application.Features.GetLevels;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

public class LevelController : Controller
{
    private readonly IMediator _mediator;

    public LevelController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Получить список уровней.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get(GetLevelsQuery query) => Ok(await _mediator.Send(query));
}