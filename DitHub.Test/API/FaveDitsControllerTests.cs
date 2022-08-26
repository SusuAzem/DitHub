using DitHub.API;
using DitHub.Core.DTO;
using DitHub.Core.IRepositories;
using DitHub.Core.Models;
using DitHub.Test.Extentions;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace DitHub.Test.API
{
    [TestClass]
    public class FaveDitsControllerTests
    {
        private Mock<UserManager<AppUser>> userManager;
        private Mock<IFaveDitR> faveDitR;
        private Mock<IDitR> ditR;
        private FaveDitsController controller;
        private string userId;
        private readonly string Id = "1";

        [TestInitialize]
        public void TestInitialize()
        {
            var userStoreMock = new Mock<IUserStore<AppUser>>();
            userManager = new Mock<UserManager<AppUser>>(
                                    userStoreMock.Object, null, null, null, null, null, null, null, null);
            faveDitR = new Mock<IFaveDitR>();
            ditR = new Mock<IDitR>();

            var unit = new Mock<IUnitOfWork>();
            unit.Setup(u => u.Favedits).Returns(faveDitR.Object);
            unit.Setup(u => u.Dits).Returns(ditR.Object);

            controller = new FaveDitsController(userManager.Object, unit.Object);
            controller.MockUser("tuser@domaim.com", Id);
            userId = userManager.Object.GetUserId(controller.User);
        }

        [TestMethod]
        public void Post_DitNotExist_NotFound()
        {
            var dto = new FDTO()
            {
                Ditid = 1
            };
            var result = controller.Post(dto);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Post_ItIsInFaveD_BadRequest()
        {
            var dit = new Dit();
            var appUser = new Mock<AppUser>();
            appUser.SetupGet(a => a.Id).Returns(userId);
            var faved = new FaveDit(appUser.Object.Id, dit.Id);
            appUser.SetupGet(a => a.FaveDits).Returns(new List<FaveDit> { faved });

            faveDitR.Setup(f => f.IsInFaveD(1, userId)).Returns(appUser.Object.FaveDits.Contains(faved));

            var dto = new FDTO { Ditid = 1 };
            var result = controller.Post(dto);
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [TestMethod]
        public void Post_Valid_Ok()
        {
            var dit = new Mock<Dit>(Id + "-");
            dit.SetupGet(d => d.Id).Returns(int.Parse(Id));
            ditR.Setup(d => d.GetDit(dit.Object.Id)).Returns(dit.Object);

            var appUser = new Mock<AppUser>();
            appUser.SetupGet(a => a.Id).Returns(Id);
            appUser.SetupGet(a => a.FaveDits).Returns(new List<FaveDit>());

            var faved = new FaveDit(appUser.Object.Id, dit.Object.Id);
            faveDitR.Setup(f => f.IsInFaveD(dit.Object.Id, appUser.Object.Id)).
                Returns(appUser.Object.FaveDits.Contains(faved));
            faveDitR.Setup(f => f.AddFavedit(faved));

            var dto = new FDTO { Ditid = int.Parse(Id) };
            var result = controller.Post(dto);
            result.Should().BeOfType<OkObjectResult>();
        }

    }
}
