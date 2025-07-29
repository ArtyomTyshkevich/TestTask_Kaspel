using FluentValidation;
using TestTask.Application.DTOs;

namespace TestTask.Application.Validators
{
    public class OrderDTOValidator : AbstractValidator<OrderDTO>
    {
        public OrderDTOValidator()
        {
            RuleFor(x => x.CreatedDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Дата создания не может быть в будущем.");

            RuleFor(x => x.TotalPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Общая цена должна быть положительной.");

            RuleFor(x => x.Books)
                .NotNull().WithMessage("Список книг обязателен.")
                .Must(books => books.Count > 0)
                .WithMessage("Список книг не может быть пустым.");

            RuleForEach(x => x.Books)
                .SetValidator(new BookDTOValidator());
        }
    }
}
