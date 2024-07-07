﻿using JewelrySalesSystem.BAL.Models.Users;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IUserService
    {
        Task<User?> LoginAsync(string userName, string passWord);

        Task<PaginatedList<GetUserResponse>> PaginationAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , bool isActive
            , int page
            , int pageSize);

        Task<CreateUserRequest> AddAsync(CreateUserRequest createUserRequest);

        Task UpdateAsync(UpdateUserRequest updateUserRequest);

        Task<GetUserResponse?> GetByIdAsync(int id);

        Task<GetUserResponse?> GetByIdWithIncludeAsync(int id);

        Task DeleteAsync(int id);
    }
}
