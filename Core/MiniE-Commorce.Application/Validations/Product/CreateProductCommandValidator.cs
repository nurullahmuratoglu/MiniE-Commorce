using FluentValidation;
using MiniE_Commorce.Application.Features.Commands.Category;
using MiniE_Commorce.Application.Features.Commands.Product.CreateProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Validations.Product
{
    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty().WithMessage("Ad boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter uzunluğunda olmalıdır.");
        }
    }
}
