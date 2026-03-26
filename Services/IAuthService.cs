using PeminjamanRuanganAPI.DTO;
using PeminjamanRuanganAPI.Models;

namespace PeminjamanRuanganAPI.Services
{
    public interface IAuthService
    {
        Task<(User?, string?)> RegisterAsync(RegisterDto dto);
        Task<(User?, string?, string?)> LoginAsync(LoginDto dto);
    }
}