using AutoMapper;
using FluentValidation;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Users;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserRequest> _createUserValidator;

        public UserService(
            IUnitOfWork unitOfWork
            , IMapper mapper,IValidator<CreateUserRequest> validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createUserValidator = validator;
        }

        public async Task<User?> LoginAsync(string userName, string passWord)
            => await _unitOfWork.Users.LoginAsync(userName, passWord);

        public async Task<PaginatedList<GetUserResponse>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => _mapper.Map<PaginatedList<GetUserResponse>>(await _unitOfWork.Users.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize));

        public async Task<CreateUserRequest> AddAsync(CreateUserRequest createUserRequest)
        {

            var validationResult = await _createUserValidator.ValidateAsync(createUserRequest);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var userEntity = _mapper.Map<User>(createUserRequest);
            _unitOfWork.Users.AddEntity(userEntity);
            await _unitOfWork.CompleteAsync();

            return createUserRequest;
        }

        public async Task UpdateAsync(UpdateUserRequest updateUserRequest)
        {
            _unitOfWork.Users.UpdateEntity(_mapper.Map<User>(updateUserRequest));
            await _unitOfWork.CompleteAsync();
        }

        public async Task<GetUserResponse?> GetByIdWithIncludeAsync(int id) => _mapper.Map<GetUserResponse>(await _unitOfWork.Users.GetByIdWithIncludeAsync(id));

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Users.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<GetUserResponse?> GetByIdAsync(int id) => _mapper.Map<GetUserResponse>(await _unitOfWork.Users.GetEntityByIdAsync(id));
    }
}
