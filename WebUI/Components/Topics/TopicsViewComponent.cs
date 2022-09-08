using Application.Features.GetTopics;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace WebUI.Components.Topics;

public class TopicsViewComponent : ViewComponent
{
    private readonly IMediator _mediator;

    public TopicsViewComponent(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Получить компонент "Таблица отчетов"
    /// </summary>
    /// <param name="companyId">Уникальный идентификатор компании.</param>
    /// <param name="creditBureau">Кредитное бюро</param>
    public async Task<IViewComponentResult> InvokeAsync(GetTopicsQuery query) => View("~/Components/Topics/Default.cshtml", await _mediator.Send(query));
}
