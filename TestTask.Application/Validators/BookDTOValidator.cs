using FluentValidation;
using TestTask.Application.DTOs;

namespace TestTask.Application.Validators
{
    public class BookDTOValidator : AbstractValidator<BookDTO>
    {
        public BookDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название книги обязательно.")
                .MaximumLength(20).WithMessage("Название не должно превышать 20 символов.");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Автор обязателен.")
                .MaximumLength(30).WithMessage("Имя автора не должно превышать 30 символов.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Описание обязательно.")
                .MaximumLength(1000).WithMessage("Описание не должно превышать 1000 символов.");

            RuleFor(x => x.ReleaseDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Дата выпуска не может быть в будущем.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Цена должна быть положительной.");
        }
    }
}