using FluentValidation;
using MiniE_Commorce.Application.Features.Commands.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Validations.Category
{
    public class CreateCategoryCommandValidator:AbstractValidator<CreateCategoryCommandRequest>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(request => request.Name)
    .NotEmpty().WithMessage("Ad boş bırakılamaz.")
    .MaximumLength(50).WithMessage("Ad en fazla 50 karakter uzunluğunda olmalıdır.");
        }
    }
}
