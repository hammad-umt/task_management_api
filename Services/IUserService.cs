using TMIApi.DTOs;
using TMIApi.Models;

namespace TMIApi.Services
{
    public interface IUserService
{
    Task<List<UserResponseDTO>> GetAllAsync();
    Task<UserResponseDTO> GetByIdAsync(int id);
    Task CreateAsync(CreateUserDTO dto);
    Task UpdateAsync(int id, UpdateUserDTO dto);
    Task DeleteAsync(int id);
}
}