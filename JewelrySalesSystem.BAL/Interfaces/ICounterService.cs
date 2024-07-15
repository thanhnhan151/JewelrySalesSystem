using JewelrySalesSystem.BAL.Models.Counters;
using JewelrySalesSystem.DAL.Common;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface ICounterService
    {
        Task<PaginatedList<GetCounterResponse>> PaginationAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , bool isActive
            , int page
            , int pageSize);

        Task AddAsync(CreateCounterRequest request);

        Task UpdateAsync(UpdateCounterRequest request);

        Task ChangeStatusAsync(int id);

        Task AssignStaffToCounterAsync(int counterId, int userId);
    }
}
