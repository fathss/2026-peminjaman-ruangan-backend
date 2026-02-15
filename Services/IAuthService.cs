using PeminjamanRuanganAPI.DTO;
using PeminjamanRuanganAPI.Models;

namespace PeminjamanRuanganAPI.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(RegisterDto dto);
        Task<string?> LoginAsync(LoginDto dto);
    }
}