using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pizzeriaApiRest.DTOs;
using pizzeriaApiRest.Models;
using pizzeriaApiRest.Respositories;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;

namespace pizzeriaApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private PizzaRespository _pizzaRespository;
        private IngrediantRespository _ingrediantRespository;

        public PizzaController(PizzaRespository pizzaRepository, IngrediantRespository ingrediantRespository)
        {
            _pizzaRespository = pizzaRepository;
            _ingrediantRespository = ingrediantRespository;
        }

        [Authorize(Policy = "admin")]
        [HttpPost("add")]
        public IActionResult Save([FromBody] PizzaRequestDTO pizzaRequestDTO)
        {
            Pizza pizza = new Pizza()
            {
               Name = pizzaRequestDTO.Name,
               Description = pizzaRequestDTO.Description,
               Price = pizzaRequestDTO.Price,
               Image = pizzaRequestDTO.Image,   
               Vegan = pizzaRequestDTO.Vegan,
               Spicy = pizzaRequestDTO.Spicy,
            };

            if (_pizzaRespository.Save(pizza))
            {

                return Ok(new PizzaResponseDTO() { Id = pizza.Id, Name = pizza.Name, Description = pizza.Description, Image = pizza.Image, Price = pizza.Price , Spicy = pizza.Spicy, Vegan = pizza.Vegan });
            }
            return StatusCode(500, new { Message = "Erreur server product" });
        }

        [Authorize(Policy = "client")]
        [HttpGet("menu")]
        public IActionResult Reade()
        {
            List<PizzaResponseDTO> response = new List<PizzaResponseDTO>();
            _pizzaRespository.FindAll().ForEach(pizza =>
            {
                PizzaResponseDTO responseDTO =  new PizzaResponseDTO();
                responseDTO.Name = pizza.Name;
                responseDTO.Description = pizza.Description;
                responseDTO.Image = pizza.Image;
                responseDTO.Price = pizza.Price;
                responseDTO.Spicy = pizza.Spicy;
                responseDTO.Vegan = pizza.Vegan;
                pizza.PizzaIngrediants.ForEach(p =>
                {
                    responseDTO.PizzaIngrediants.Add(new PizzaIngrediantResponceDTO() { Name = p.Ingrediants.Name, Descrisption = p.Ingrediants.Description });
                });
                response.Add(responseDTO);
            });
           return Ok(response);
        }

        [Authorize(Policy = "admin")]
        [HttpPost("add-topping/{pizzaId}/{toppingId}")]
        public IActionResult UpdateTopping(int pizzaId, int toppingId)
        {
            Pizza pizza = _pizzaRespository.FindById(pizzaId);
            Ingrediants ingrediant = _ingrediantRespository.FindById(toppingId);
            if (pizza != null && ingrediant != null)
            {
                pizza.PizzaIngrediants.Add(new PizzasIngrediants() { Pizza = pizza , Ingrediants = ingrediant });

                    if (_pizzaRespository.Update())
                        return Ok(pizza);
                return StatusCode(500, new { Message = " Erreur serveur update topping  " });
            }
            return NotFound();
        }

        [Authorize(Policy = "admin")]
        [HttpDelete("remove-topping/{pizzaId}/{toppingId}")]
        public IActionResult DeleteTopping(int pizzaId, int toppingId)
        {
            Pizza pizza = _pizzaRespository.FindById(pizzaId);
            Ingrediants ingrediant = _ingrediantRespository.FindById(toppingId);
            if (pizza != null && ingrediant != null)
            {
                pizza.PizzaIngrediants.RemoveAll(p => p.Ingrediants.Id == ingrediant.Id);

                if (_pizzaRespository.Update())
                    return Ok(pizza);
                return StatusCode(500, new { Message = " Erreur serveur update topping  " });
            }
            return NotFound();
        }

        [Authorize(Policy = "admin")]
        [HttpDelete("delete/{id}")]
        public IActionResult DeletePizza(int id )
        {
            Pizza pizza = _pizzaRespository.FindById(id);
            if (pizza != null)
            {
                _pizzaRespository.Delete(pizza);
                return StatusCode(200, new { Message = "Delete pizza Ok " });
            }
            return NotFound();
        }

        [Authorize(Policy = "admin")]
        [HttpPut("update/{pizzaId}")]
        public IActionResult UpdatePizza(int pizzaId , [FromBody] PizzaRequestDTO pizzaRequestDTO)
        {
            Pizza pizza = _pizzaRespository.FindById(pizzaId);

            if (pizza != null)
            {
                pizza.Name = pizzaRequestDTO.Name;
                pizza.Description = pizzaRequestDTO.Description;
                pizza.Price = pizzaRequestDTO.Price;
                pizza.Image= pizzaRequestDTO.Image;
                pizza.Spicy = pizzaRequestDTO.Spicy;  
                pizza.Vegan =  pizzaRequestDTO.Vegan;

                if (_pizzaRespository.Update())
                    return Ok(pizza);
                return StatusCode(500, new { Message = " Erreur serveur update " });
            }
            return NotFound();
        }
    }
}
