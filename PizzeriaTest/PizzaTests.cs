using System;
using Xunit;
using PizzeriaTest;
using Pizzeria.Facades;
using Pizzeria.Enums;
using Pizzeria.Database;
using Microsoft.EntityFrameworkCore;

namespace PizzeriaTest
{
    public class PizzaTests
    {

        private FacadePizza Facade;
        private FacadeUsers Facadeuser;
        private DbContextOptions<PizzeriaContext> options;
       
        public PizzaTests()
        {
            options = new DbContextOptionsBuilder<PizzeriaContext>()
                 .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                 .Options;
        }

        [Fact]
        public void InsertPizzaNoError()
        {
            Facade = new FacadePizza() { Context = new PizzeriaContext(options) { Test = true } };
            String id = Facade.AddPizza("Loguin", "Path");
            Assert.NotEqual("Error", id);
        }
        [Fact]
        public void AddIngredient() {
           
            Facade = new FacadePizza() { Context = new PizzeriaContext(options) { Test = true } };
            Facade.AddPizza("name","photo");  
        }
        [Fact]
        public void AddComment()
        {
          
            Facade = new FacadePizza() { Context = new PizzeriaContext(options) { Test = true } };
            Facadeuser = new FacadeUsers() { Context = new PizzeriaContext(options) { Test = true } };
            Errors err = Facadeuser.Insert("Loguin", "Password", "Name", "Surname", "Email");
            User user = Facadeuser.GetUser("Loguin");
            Assert.Equal(user.Loguin, "Loguin");
            String pizzaId = Facade.AddPizza("name","photo");
            
            Assert.NotEqual(pizzaId, "Error");
            Facade.AddComment(pizzaId, user.UserId, "blablabñlablabla");
            
        }
    }
}
