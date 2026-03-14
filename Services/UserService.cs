using AutoMapper;
using TMIApi.DTOs;
using TMIApi.Models;
using TMIApi.Repositories;

namespace TMIApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserResponseDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserResponseDTO>>(users);
        }

        public async Task<UserResponseDTO> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("User nahi mila");
            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task CreateAsync(CreateUserDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            user.CreatedAt = DateTime.Now;
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateAsync(int id, UpdateUserDTO dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("User nahi mila");
            _mapper.Map(dto, user);
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("User nahi mila");
            await _userRepository.DeleteAsync(user);
        }
        public async Task<UserResponseDTO> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                throw new Exception("User nahi mila");
            return _mapper.Map<UserResponseDTO>(user);
        }
    }
}