﻿using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Genders;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class GenderService : IGenderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenderService(IUnitOfWork unitOfWork , IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateGenderRequest> AddGender(CreateGenderRequest createGenderRequest)
        {

            var result = _unitOfWork.Genders.AddEntity(_mapper.Map<Gender>(createGenderRequest));

            await _unitOfWork.CompleteAsync();

            return createGenderRequest;
        }

    }
}
