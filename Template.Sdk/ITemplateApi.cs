using Refit;
using System.Threading.Tasks;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;

namespace Template.Sdk
{
    public interface ITemplateApi
    {
        [Get("/api/User")]
        Task<ApiResponse<PagedResponse<UserResponse>>> GetUsersAsync(UserSearchRequest request = default, PaginationQuery pagination = default);
        
        [Get("/api/User/{id}")]
        Task<ApiResponse<UserResponse>> GetUserByIdAsync(int id);

        [Put("/api/User/{id}")]
        Task<ApiResponse<UserResponse>> UpdateUserAsync(int id, UserUpdateRequest request);

        [Delete("/api/User/{id}")]
        Task<ApiResponse<bool>> DeleteKorisnikAsync(int id);

        [Get("/api/Role")]
        Task<ApiResponse<PagedResponse<RoleResponse>>> GetRolesAsync();
    }
}
