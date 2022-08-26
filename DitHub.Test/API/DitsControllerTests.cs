using DitHub.API;
using DitHub.Core.IRepositories;
using DitHub.Core.Models;
using DitHub.Test.Extentions;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Security.Claims;

namespace DitHub.Test.API
{
    [TestClass]
    public class DitsControllerTests
    {
        private readonly string Id = "1";
        private DitsController controller;
        private Mock<UserManager<AppUser>> userManager;
        private Mock<IDitR> ditR;

        [TestInitialize]
        public void TestInitialize()
        {
            var userStoreMock = new Mock<IUserStore<AppUser>>();
            userManager = new Mock<UserManager<AppUser>>(
                                    userStoreMock.Object, null, null, null, null, null, null, null, null);
            ditR = new Mock<IDitR>();
            var unit = new Mock<IUnitOfWork>();
            unit.Setup(u => u.Dits).Returns(ditR.Object);
            controller = new DitsController(userManager.Object, unit.Object);
            controller.MockUser("tuser@domaim.com", Id);
            userManager.Setup(u => u.GetUserId(controller.User)).Returns(controller.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [TestMethod]
        public void Delete_DitIsNull_NotFound()
        {
            var result = controller.Delete(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Delete_DitIsCanceled_NotFound()
        {
            var dit = new Dit();
            dit.Remove();

            ditR.Setup(d => d.GetDitWithFaves(1)).Returns(dit);

            var result = controller.Delete(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Delete_UserIsNotTheOwner_Unauthorized()
        {
            //the controller.User.UserId = Id , the dit.UserId = Id + "-"
            var dit = new Dit(Id + "-");

            ditR.Setup(d => d.GetDitWithFaves(1)).Returns(dit);

            var result = controller.Delete(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Delete_Valid_OK()
        {
            var appUser = new AppUser() { Id = Id + "-" };

            var dit = new Dit(Id);

            var favedit = new FaveDit(appUser.Id, dit.Id);

            favedit.AppUserSet(appUser);

            dit.FaveDits.Add(favedit);

            ditR.Setup(d => d.GetDitWithFaves(1)).Returns(dit);

            var result = controller.Delete(1);

            result.Should().BeOfType<OkResult>();
        }
    }
}
