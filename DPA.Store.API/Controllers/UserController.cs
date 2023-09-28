using DPA.Store.DOMAIN.Core.Entities;
using DPA.Store.DOMAIN.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DPA.Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAll();
            return Ok(users);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("GetByEmail/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(User user)
        {
            // TODO: Implementar validación y hashing de contraseña

            var existingUser = await _userRepository.GetByEmail(user.Email);
            if (existingUser != null) return BadRequest("El correo electrónico ya está registrado.");

            var result = await _userRepository.Insert(user);
            if (!result) return BadRequest(result);

            return Ok(new { userId = user.Id });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            // TODO: Implementar un método seguro de verificación de contraseña.

            var user = await _userRepository.GetByEmailAndPassword(email, password); // ¡Este es solo un ejemplo simplificado!

            if (user == null)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            return Ok(new { userId = user.Id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userRepository.Delete(id);
            if (!result) return BadRequest(result);

            return Ok(result);
        }
    }
}
