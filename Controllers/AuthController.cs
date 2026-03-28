using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PeminjamanRuanganAPI.Constants;
using PeminjamanRuanganAPI.Data;
using PeminjamanRuanganAPI.DTO;
using PeminjamanRuanganAPI.Services;

namespace PeminjamanRuanganAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly AppDbContext _context;

        public AuthController(IAuthService authService, AppDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var (user, error) = await _authService.RegisterAsync(dto);

            if (user == null)
            {
                return BadRequest(new { message = error });
            }

            return Ok(new { message = "Registrasi berhasil." });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
        {
            var (user, token, error) = await _authService.LoginAsync(dto);

            if (token == null)
            {
                return Unauthorized(new { message = error });
            }

            return Ok(new AuthResponseDto
            {
                Username = dto.Username,
                Token = token,
                Role = user!.Role
            });
        }
    }
}