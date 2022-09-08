using Application.Features.CreateTopic;
using Application.Features.GetTopic;
using Application.Features.GetTopics;
using Application.Features.UpdateTopic;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebUI.Controllers;

public class TopicController : Controller
{
    private readonly IMediator _mediator;

    public TopicController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public ActionResult Index() => View();

    /// <summary>
    /// Страница темы. 
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Display(GetTopicQuery query) => View(await _mediator.Send(query));

    [HttpGet]
    public IActionResult GetTopics(GetTopicsQuery query) => ViewComponent("Topics", query);

    /// <summary>
    /// Страница создания темы.
    /// </summary>
    [HttpGet]
    public IActionResult Create() => View();

    /// <summary>
    /// Создать тему.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateTopicCommand command) => Ok(await _mediator.Send(command));

    /// <summary>
    /// Страница редактирования темы. 
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Edit(GetTopicQuery query) => View(await _mediator.Send(query));

    /// <summary>
    /// Отредактировать тему.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Edit(UpdateTopicCommand command) => Ok(await _mediator.Send(command));
}
