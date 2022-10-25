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
    public class IngrediantsController : ControllerBase
    {
        private IngrediantRespository _ingrediantRespository;

        public IngrediantsController(IngrediantRespository ingrediiantResponce)
        {
            _ingrediantRespository = ingrediiantResponce;
        }

        [Authorize(Policy = "admin")]
        [HttpPost("add")]
        public IActionResult Save([FromBody] IngrediantResquestDTO ingrediantRequestDTO)
        {
            Ingrediants ingrediant = new Ingrediants()
            {
                Name = ingrediantRequestDTO.Name,
                Description = ingrediantRequestDTO.Description,
            };

            if (_ingrediantRespository.Save(ingrediant))
            {

                return Ok(new IngrediantResponseDTO() { Id = ingrediant.Id , Name = ingrediant.Name , Description = ingrediant.Description});
            }
            return StatusCode(500, new { Message = "Erreur server ingrediant" });
        }

        [Authorize(Policy = "admin")]
        [HttpGet("Reade")]
        public IActionResult reade()
        {
            List<Ingrediants> ingrediant = _ingrediantRespository.FindAll();
            List<IngrediantResponseDTO> reponseDTO = new List<IngrediantResponseDTO>();
            ingrediant.ForEach(i =>
            {
                reponseDTO.Add(new IngrediantResponseDTO()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                });
            });
            return Ok(reponseDTO);
        }

        [Authorize(Policy = "admin")]
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] IngrediantResquestDTO ingrediantRequestDTO)
        {
            Ingrediants ingrediant = _ingrediantRespository.FindById(id);

            if (ingrediant != null)
            {
                ingrediant.Name = ingrediantRequestDTO.Name;
                ingrediant.Description = ingrediantRequestDTO.Description;

                if (_ingrediantRespository.Update())
                    return Ok(ingrediant);
                return StatusCode(500, new { Message = " Erreur serveur update " });
            }
            return NotFound();
        }

        [Authorize(Policy = "admin")]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            Ingrediants ingrediant = _ingrediantRespository.FindById(id);
            if (ingrediant != null)
            {
                _ingrediantRespository.Delete(ingrediant);
                return StatusCode(200, new { Message = "Delete Ingrediant Ok " });
            }
            return NotFound();
        }

        [Authorize(Policy = "admin")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Ingrediants ingrediant = _ingrediantRespository.FindById(id);
            if (ingrediant != null)
                return Ok(ingrediant);
            return NotFound();
        }
    }
}
