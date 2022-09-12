using Application.Features.Tags.CreateTag;
using Application.Features.Tags.DeleteTag;
using Application.Features.Tags.GetTag;
using Application.Features.Tags.GetTags;
using Application.Features.Tags.UpdateTag;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

public class TagController : Controller
{
    private readonly IMediator _mediator;

    public TagController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Страница тэгов
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index() => View(await _mediator.Send(new GetTagsQuery()));

    /// <summary>
    /// Получить список тэгов.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get(GetTagsQuery query) => Ok(await _mediator.Send(query));

    /// <summary>
    /// Создать новый тэг.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateTagCommand command) => Ok(await _mediator.Send(command));

    /// <summary>
    /// Страница редактирования тэга.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Update(GetTagQuery query) => View(await _mediator.Send(query));

    /// <summary>
    /// Обновить тэг.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Update(UpdateTagCommand command) => Ok(await _mediator.Send(command));

    /// <summary>
    /// Удалить тэг.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Delete(DeleteTagCommand command) => Ok(await _mediator.Send(command));
}
