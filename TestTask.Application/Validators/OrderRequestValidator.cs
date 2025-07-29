using FluentValidation;
using TestTask.Application.DTOs;

namespace TestTask.Application.Validators
{
    public class OrderRequestValidator : AbstractValidator<OrderRequest>
    {
        public OrderRequestValidator()
        {
            RuleFor(x => x.BooksIds)
                .NotEmpty().WithMessage("Список книг не должен быть пустым.")
                .Must(ids => ids.Distinct().Count() == ids.Count)
                .WithMessage("Список книг содержит повторяющиеся идентификаторы.");
        }
    }
}
