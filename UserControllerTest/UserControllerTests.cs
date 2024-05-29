using NUnit.Framework;
using System.Web.Mvc;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Collections.Generic;
using System.Linq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CRUD_application_2.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTest
    {
        private UserController _controller;
        private List<User> _users;

        [SetUp]
        public void Setup()
        {
            _users = new List<User>
            {
                new User { Id = 1, Name = "Test User 1", Email = "test1@example.com" },
                new User { Id = 2, Name = "Test User 2", Email = "test2@example.com" },
                new User { Id = 3, Name = "Test User 3", Email = "test3@example.com" },
            };

            UserController.userlist = _users;
            _controller = new UserController();
        }

        [Test]
        public void Index_ReturnsCorrectView_WithAllUsers()
        {
            var result = _controller.Index() as ViewResult;
            var model = result.Model as List<User>;

            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual(3, model.Count);
        }

        [Test]
        public void Details_ReturnsCorrectView_WithUser()
        {
            var result = _controller.Details(1) as ViewResult;
            var model = result.Model as User;

            Assert.AreEqual("Details", result.ViewName);
            Assert.AreEqual("Test User 1", model.Name);
        }

        [Test]
        public void Create_Post_AddsNewUser()
        {
            var newUser = new User { Id = 4, Name = "Test User 4", Email = "test4@example.com" };
            var result = _controller.Create(newUser) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(4, UserController.userlist.Count);
        }

        [Test]
        public void Edit_Post_UpdatesUser()
        {
            var updatedUser = new User { Id = 1, Name = "Updated User", Email = "updated@example.com" };
            var result = _controller.Edit(1, updatedUser) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Updated User", UserController.userlist.First(u => u.Id == 1).Name);
        }

        [Test]
        public void Delete_Post_RemovesUser()
        {
            var result = _controller.Delete(1, new FormCollection()) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(2, UserController.userlist.Count);
        }
    }
}
