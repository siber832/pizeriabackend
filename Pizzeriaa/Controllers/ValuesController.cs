using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Facades;
using Pizzeria.Enums;
using Microsoft.AspNetCore.Cors;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Globalization;

namespace Pizzeria.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("Pizzas/")]
    public class ValuesController : Controller
    {
        
        private FacadePizza FaPizza = new FacadePizza() { Context = Startup.db };
        private FacadeUsers FaUser = new FacadeUsers() { Context = Startup.db };

        [HttpPost("login")]
        public IActionResult Login(String user, String password)
        {
            Errors err = FaUser.Loguin(user, password);
            if (err.Equals(Errors.NO_ERROR))
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, user) };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                HttpContext.Authentication.SignInAsync("Cookies", principal);
                return Ok(FaUser.GetId(user));
            }
            if (err.Equals(Errors.LOGIN_DONT_EXISTS)) 
                return StatusCode(403, "Login dont exists.");

            return StatusCode(403, "Password do not match.");
        }
        
        [HttpPost("register")]
        public IActionResult RegisterUser(String user, String password, String name, String surname, String email)
        {
            Errors err = FaUser.Insert(user,password,name,surname,email);
            if(err.Equals(Errors.NO_ERROR))
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Authentication.SignOutAsync("Cookies");
            return NoContent();
        }

        [HttpGet("pizzas")]
        public IActionResult GetPizzas()
        {
            //if (!IsLogued()) return StatusCode(403);
            return Ok(FaPizza.GetPizzas());
        }

        [HttpGet("pizza")]
        public IActionResult GetPizza(String id)
        {
            //if (!IsLogued()) return StatusCode(403);
            return Ok(FaPizza.GetPizza(id));
        }

        [HttpPost("insert_pizza")]
        public string AddPizza(String name, IFormFile f)
        {
            if (f == null) return "Error";
            var filePath = "img/" + f.FileName;
            if (f.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    f.CopyTo(stream);
                }
            }
            String id = FaPizza.AddPizza(name, filePath);
            return id;
        }

        [HttpPost("ingredient")]
        public IActionResult AddIngredient(String pizzaId, String nombre, string precio)
        {
            //if (!IsLogued()) return StatusCode(403);
            decimal decimalPrice = Decimal.Parse(precio, CultureInfo.InvariantCulture);
            Errors err = FaPizza.AddIngredient(pizzaId, nombre, decimalPrice);
            if (err.Equals(Errors.NO_ERROR))
                return NoContent();
            else
                return StatusCode(500);
        }

        [HttpPost("ingredients_end")]
        public IActionResult EndAddIngredient(String pizzaId, String nombre, String precio)
        {
            //if (!IsLogued()) return StatusCode(403);
            FaPizza.EndAddIngredients();
            return NoContent();
        }

        [HttpPost("comment")]
        public IActionResult AddComment(String pizzaId, String userId, String comentario, String puntuacion)
        {
            //if (!IsLogued()) return StatusCode(403);
            int puntuacionInt = int.Parse(puntuacion);
            Errors err = FaPizza.AddComment(pizzaId, userId, comentario, puntuacionInt);
            if (err.Equals(Errors.NO_ERROR)) return NoContent();
            else return StatusCode(500);
        }

        [HttpPost("logued")]
        public IActionResult Logued()
        {
            if (!IsLogued()) return StatusCode(403);
            return NoContent();
        }

        private bool IsLogued()
        {
            var loggedInUser = HttpContext.User;
            if (loggedInUser != null)
            {
                var loggedInUserName = loggedInUser.Identity.Name;
                if (loggedInUserName != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
