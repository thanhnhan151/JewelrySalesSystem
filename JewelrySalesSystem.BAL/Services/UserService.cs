using AutoMapper;
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

        public UserService(
            IUnitOfWork unitOfWork
            , IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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

            var result = _unitOfWork.Users.AddEntity(_mapper.Map<User>(createUserRequest));

            await _unitOfWork.CompleteAsync();

            return createUserRequest;
        }

        public async Task UpdateAsync(UpdateUserRequest updateUserRequest)
        {
            _unitOfWork.Users.UpdateEntity(_mapper.Map<User>(updateUserRequest));
            await _unitOfWork.CompleteAsync();
        }

        public async Task<GetUserResponse?> GetByIdAsync(int id) => _mapper.Map<GetUserResponse>(await _unitOfWork.Users.GetByIdAsync(id));
    }
}
