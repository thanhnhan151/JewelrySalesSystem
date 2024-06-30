using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Gems;
using JewelrySalesSystem.DAL.Interfaces;

namespace JewelrySalesSystem.BAL.Validators.Gems
{
    public class UpdateGemRequestValidator : AbstractValidator<UpdateGemRequest>
    {
        //Initialize for using to method for checking exist
        private readonly IGemService _gemService;
        public UpdateGemRequestValidator(IGemService gemService)
        {
            _gemService = gemService;

            //Validate GemId: Not Empty, is integer, >0, is existed
            RuleFor(g => g.GemId).NotEmpty().WithMessage("Gem Id is required")
                .Must(MustBeAnId).WithMessage("Gem Id is an integer and greater than 0")
                .MustAsync(async (gemId, cancellationToken) => await beValidGem(gemId, cancellationToken)).WithMessage("Gem Id is not exist");

            //Validate GemName: NotEmpty, MaximumLength
            RuleFor(g => g.GemName).NotEmpty().WithMessage("GemName is required");
                //.MaximumLength(20).WithMessage("GemName is at least 20 characters");

            //Validate FeaturedImage: Not Empty
            RuleFor(g => g.FeaturedImage).NotEmpty().WithMessage("FeatureImage is required");

            ////Validate Origin: Not Empty
            //RuleFor(g => g.Origin).NotEmpty().WithMessage("Origin is required");

            ////Validate CaratWeight > 0
            //RuleFor(g => g.CaratWeight).GreaterThan(0).WithMessage("CaraWeight must be greater than 0");

            ////Validate Colour: Not Empty
            //RuleFor(g => g.Color).NotEmpty().WithMessage("Colour is required");

            ////Validate Clarity: Not Empty
            //RuleFor(g => g.Clarity).NotEmpty().WithMessage("Clarity is required");

            ////Validate Cut: Not Empty
            //RuleFor(g => g.Cut).NotEmpty().WithMessage("Cut is required");

            ////Validate CaratWeightPrice in GemPrice: NotEmpty and >0
            //RuleFor(g => g.GemPrice.CaratWeightPrice).NotEmpty().WithMessage("CaratWeightPrice is required").
            //    GreaterThan(0).WithMessage("CaraWeightPrice must be greater than 0");

            ////Validate ColourPrice in GemPrice: NotEmpty and > 0
            //RuleFor(g => g.GemPrice.ColourPrice).NotEmpty().WithMessage("ColourPrice is required").
            //    GreaterThan(0).WithMessage("ColourPrice must be greater than 0");

            ////Validate ClarityPrice in GemPrice: NotEmpty and >0
            //RuleFor(g => g.GemPrice.ClarityPrice).NotEmpty().WithMessage("ClarityPrice is required").
            //    GreaterThan(0).WithMessage("ClarityPrice must be greater than 0");

            ////Validate CutPrice in GemPrice: NotEmpty and >0
            //RuleFor(g => g.GemPrice.CutPrice).NotEmpty().WithMessage("CutPrice is required").
            //    GreaterThan(0).WithMessage("CutPrice must be greater than 0");
        }

        //Check Gem is existed by id
        private async Task<bool> beValidGem(int id, CancellationToken cancellationToken)
        {
            var checkExist = await _gemService.GetGemById(id);
            if(checkExist != null)
            {
                return true;
            }
            return false;
        }

        //Condition for input id
        private static bool MustBeAnId<T>(T value)
        {
            if (value == null)
                return true; 

            if (value is int intValue && intValue > 0)
                return true;

            //if (value is string stringValue)
            //    return int.TryParse(stringValue, out _);

            return false;
        }
    }
}
