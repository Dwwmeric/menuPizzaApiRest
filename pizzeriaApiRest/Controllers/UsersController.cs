using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pizzeriaApiRest.DTOs;
using pizzeriaApiRest.Models;
using pizzeriaApiRest.Respositories;
using pizzeriaApiRest.Services;

namespace pizzeriaApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UsersRespository _usersRespository;
        private JWTService _jwtService;

        public UsersController (UsersRespository usersResponce, JWTService jwtService)
        {
            _usersRespository = usersResponce;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Save([FromBody] UsersRequestDTO usersRequestDTO)
        {
            Users user = new Users()
            {
               FirstName =  usersRequestDTO.FirstName,
               LastName = usersRequestDTO.LastName,
               Email = usersRequestDTO.Email,
               Adresse = usersRequestDTO.Adresse,
               Password = usersRequestDTO.Password,
               Phone = usersRequestDTO.Phone,
               Role = usersRequestDTO.Role,
            };

            if (_usersRespository.Save(user))
            {

                return Ok(new UsersResponseDTO() { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, Adresse = user.Adresse, Phone = user.Phone,  Role = user.Role });
            }
            return StatusCode(500, new { Message = "Erreur server users" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] string email, [FromForm] string password)
        {
            string token = _jwtService.Login(email, password);
            if (token != null)
            {
                return Ok(token);
            }
            return StatusCode(404);
        }

        [Authorize(Policy = "admin")]
        [HttpGet("Reade")]
        public IActionResult reade()
        {
            List<Users> users = _usersRespository.FindAll();
            List<UsersResponseDTO> reponseDTO = new List<UsersResponseDTO>();
            users.ForEach(u =>
            {
                reponseDTO.Add(new UsersResponseDTO()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Phone = u.Phone,
                    Adresse = u.Adresse,
                    Role = u.Role,
                });
            });
            return Ok(reponseDTO);
        }

        [Authorize(Policy = "admin")]
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] UsersRequestDTO usersRequestDTO)
        {
            Users user = _usersRespository.FindById(id);

            if (user != null)
            {
                user.FirstName = usersRequestDTO.FirstName;
                user.LastName = usersRequestDTO.LastName;
                user.Email = usersRequestDTO.Email;
                user.Password = usersRequestDTO.Password;
                user.Adresse = usersRequestDTO.Adresse;
                user.Role = usersRequestDTO.Role;
                user.Phone = usersRequestDTO.Phone;

                if (_usersRespository.Update())
                    return Ok(user);
                return StatusCode(500, new { Message = " Erreur serveur update " });
            }
            return NotFound();
        }

        [Authorize(Policy = "client")]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            Users user = _usersRespository.FindById(id);
            if (user != null)
            {
                _usersRespository.Delete(user);
                return StatusCode(200, new { Message = "Delete user Ok " });
            }
            return NotFound();
        }

        [Authorize(Policy = "admin")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Users user = _usersRespository.FindById(id);
            if (user != null)
                return Ok(user);
            return NotFound();
        }


    }
}
