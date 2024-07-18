using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JewelrySalesSystem.BAL.Models.Materials;

namespace JewelrySalesSystem.BAL.Validators.Materials
{
    public class CreateMaterialItemRequestValidation : AbstractValidator<CreateMaterialItemRequest>
    {
        public CreateMaterialItemRequestValidation()
        {
            RuleFor(x => x.Weight)
            .GreaterThan(0)
            .LessThanOrEqualTo(3.75f)
            .WithMessage("Weigt must be greater than 0 and less than or equal to 3.75");
        }
    }
}
