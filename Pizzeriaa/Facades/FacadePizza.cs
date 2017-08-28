using Pizzeria.Database;
using System;
using System.Linq;
using Pizzeria.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pizzeria.Facades
{
    public class FacadePizza
    {
        public PizzeriaContext Context { set; private get; }
        
        public Pizza GetPizzaName(String name)
        {
            Pizza pizza = Context.Pizzas
                    .Where(p => p.Name == name)
                    .FirstOrDefault();
            return pizza;
        }

        public Errors AddComment(String pizzaId, String userID, String text, int rating) {
            var pizza = Context.Pizzas.Find(pizzaId);
            User user = Context.Users.Find(userID);
            var comment = new Comment { Date = DateTime.Now, Text = text, User = user.UserId, Pizza = pizzaId, Rating = rating };
            try
            {
                Context.Add(comment);
                pizza.Coment.Add(comment);
                Context.Pizzas.Update(pizza);
                Context.SaveChanges();
                return Errors.NO_ERROR;
            } catch(Exception e)
            {
                pizza.Coment.Remove(comment);
                Context.Remove(comment);
                return Errors.INSERTION_ERROR;
            }
        }
        public String GetPizzas(){
            var pizzas = Context.Pizzas.Where(p => true);

            return JsonConvert.SerializeObject(pizzas);
        }

        public String GetPizza(String id)
        {
            var pizzas = Context.Pizzas.Find(id);

            return JsonConvert.SerializeObject(pizzas);
        }

        public String AddPizza(String name, String photo){
            Pizza pizza = new Pizza() { Price = 5, Name = name, Photo = photo };
            try
            {
                Context.Pizzas.Add(pizza);
                Context.SaveChanges();
            }
            catch {
                Context.Pizzas.Remove(pizza);
                return "Error";
            }

            return pizza.PizzaId;
        }
        public Errors AddIngredient(String PizzaId,String name, decimal price ) {
            var pizza = Context.Pizzas.Find(PizzaId);
            var ing = new Ingredients { Name = name, Price = price };

            try
            {
                Context.Add(ing);
                pizza.Ingredient.Add(ing);
                pizza.AddIngredientPrice(ing.Price);
                Context.Update(pizza);
            } catch(Exception e)
            {
                pizza.Ingredient.Remove(ing);
                Context.Remove(ing);
                return Errors.INSERTION_ERROR;
            }
            return Errors.NO_ERROR;
        }

        public void EndAddIngredients()
        {
            Context.SaveChanges();
        }
        
    }
}
