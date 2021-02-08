using Refit;
using System.Threading.Tasks;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;

namespace Template.Sdk
{
    public interface ITemplateApi
    {
        [Post("/api/auth/login")]
        Task<ApiResponse<AuthSuccessResponse>> Login(UserAccountAuthenticationRequest request);

        [Post("/api/auth/register")]
        Task<ApiResponse<AuthSuccessResponse>> Register(UserAccountRegistrationRequest request);
        
        [Post("/api/auth/refresh")]
        Task<ApiResponse<AuthSuccessResponse>> Refresh(RefreshTokenRequest request);

        [Get("/api/user/@me")]
        Task<ApiResponse<UserResponse>> GetMeAsync();

        [Get("/api/user")]
        Task<ApiResponse<PagedResponse<UserResponse>>> GetUsersAsync(UserSearchRequest request = default, PaginationQuery pagination = default);
        
        [Get("/api/user/{id}")]
        Task<ApiResponse<UserResponse>> GetUserByIdAsync(int id);

        [Post("/api/user/{id}")]
        Task<ApiResponse<UserResponse>> UpdateUserAsync(int id, UserUpdateRequest request);

        [Put("/api/user/{id}")]
        Task<ApiResponse<UserResponse>> InsertUserAsync(int id, UserInsertRequest request);

        [Delete("/api/user/{id}")]
        Task<ApiResponse<bool>> DeleteUserAsync(int id);

        [Get("/api/role")]
        Task<ApiResponse<PagedResponse<RoleResponse>>> GetRolesAsync();
        
        [Get("/api/role/{id}")]
        Task<ApiResponse<PagedResponse<RoleResponse>>> GetRolesByIdAsync(int id);
    }
}
