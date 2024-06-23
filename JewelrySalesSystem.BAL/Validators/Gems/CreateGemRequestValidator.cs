using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using JewelrySalesSystem.BAL.Models.Gems;

namespace JewelrySalesSystem.BAL.Validators.Gems
{
    public class CreateGemRequestValidator : AbstractValidator<CreateGemRequest>
    {
        public CreateGemRequestValidator()
        {

            //Validate GemName: NotEmpty, MaximumLength
            RuleFor(g => g.GemName).NotEmpty().WithMessage("GemName is required");
                //.MaximumLength(20).WithMessage("GemName is at least 20 characters");

            //Validate FeaturedImage: Not Empty
            RuleFor(g => g.FeaturedImage).NotEmpty().WithMessage("FeatureImage is required");

            //Validate Origin: Not Empty
            RuleFor(g => g.Origin).NotEmpty().WithMessage("Origin is required");

            //Validate CaratWeight > 0
            RuleFor(g => g.CaratWeight).GreaterThan(0).WithMessage("CaraWeight must be greater than 0");

            //Validate Colour: Not Empty
            RuleFor(g => g.Colour).NotEmpty().WithMessage("Colour is required");
            //.Must(BeAValidColor)
            //.WithMessage("'{PropertyValue}' is not a valid colour");

            //Validate Clarity: Not Empty
            RuleFor(g => g.Clarity).NotEmpty().WithMessage("Clarity is required");

            //Validate Cut: Not Empty
            RuleFor(g => g.Cut).NotEmpty().WithMessage("Cut is required");

            //Validate CaratWeightPrice in GemPrice: NotEmpty and >0
            RuleFor(g => g.GemPrice.CaratWeightPrice).NotEmpty().WithMessage("CaratWeightPrice is required").
                GreaterThan(0).WithMessage("CaraWeightPrice must be greater than 0");

            //Validate ColourPrice in GemPrice: NotEmpty and >0
            RuleFor(g => g.GemPrice.ColourPrice).NotEmpty().WithMessage("ColourPrice is required").
                GreaterThan(0).WithMessage("ColourPrice must be greater than 0");

            //Validate ClarityPrice in GemPrice: NotEmpty and >0
            RuleFor(g => g.GemPrice.ClarityPrice).NotEmpty().WithMessage("ClarityPrice is required").
                GreaterThan(0).WithMessage("ClarityPrice must be greater than 0");

            //Validate CutPrice in GemPrice: NotEmpty and >0
            RuleFor(g => g.GemPrice.CutPrice).NotEmpty().WithMessage("CutPrice is required").
                GreaterThan(0).WithMessage("CutPrice must be greater than 0");

        }

        //private bool BeAValidColor(string color)
        //{
        //    return Regex.IsMatch(color, @"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$");
        //}

    }
}
