using System;
using Xunit;
using Pizzeria.Facades;
using Pizzeria.Enums;
using Pizzeria.Database;
using Microsoft.EntityFrameworkCore;

namespace PizzeriaTests
{
    public class UserTests
    {
        private FacadeUsers Facade;
        private DbContextOptions<PizzeriaContext> options;

        public UserTests()
        {
            options = new DbContextOptionsBuilder<PizzeriaContext>()
                 .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                 .Options;
        }

        [Fact]
        public void UserInsertNoErrorTest()
        {
            InsertUser();
        }

        [Fact]
        public void UsertInsertLoguinExists()
        {
            InsertUser();
            Errors err = Facade.Insert("Loguin", "Password", "Name", "Surname", "Email");
            Assert.Equal(Errors.INSERTION_ERROR, err);
        }

        [Fact]
        public void UserUpdateLoguinNoError()
        {
            UserUpdateFieldNoError("Log", "", "", "");
        }

        [Fact]
        public void UserUpdatePasswordNoError()
        {
            UserUpdateFieldNoError("", "8656", "", "");
        }

        [Fact]
        public void UserUpdateLoguinError()
        {
            InsertUser();
            Errors err = Facade.Insert("Log", "Password", "Name", "Surname", "Email");
            Assert.Equal(Errors.NO_ERROR, err);
            err = Facade.Update("Loguin", "Log", "", "", "");
            

            Assert.Equal(Errors.UPDATE_ERROR, err);
        }
        
        private void InsertUser()
        {
            Facade = new FacadeUsers() { Context = new PizzeriaContext(options) { Test = true } };
            Errors err = Facade.Insert("Loguin", "Password", "Name", "Surname", "Email");
            Assert.Equal(Errors.NO_ERROR, err);
        }

        private void UserUpdateFieldNoError(String loguin, String password, String name, String surname)
        {
            InsertUser();
            Errors err = Facade.Update("Loguin", loguin, password, name, surname);
            Assert.Equal(Errors.NO_ERROR, err);
        }
       
    }
}
