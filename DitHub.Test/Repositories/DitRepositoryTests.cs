using DitHub.Core.Models;
using DitHub.Persistence.Repositories;
using DitHub.Test.Extentions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace DitHub.Test.Repositories
{
    [TestClass]
    public class DitRepositoryTests
    {
        private DitR ditR;
        private Mock<DbSet<Dit>> mockSet;

        [TestInitialize]
        public void TestInitialize()
        {
            var mockContext = new Mock<IApplicationDbContext>();

            mockSet = new Mock<DbSet<Dit>>();

            mockContext.SetupGet(c => c.Dits).Returns(() => mockSet.Object);

            ditR = new DitR(mockContext.Object);

        }

        [TestMethod]
        public void GetDitWithGenra_DitIsOld_NotReturned()
        {
            var dit = new Dit("1") { Date = DateTime.Now.AddDays(-1) };

            mockSet.PopulateDbset(new[] { dit });

            var dits = ditR.GetDitWithGenra("1");

            dits.Should().BeEmpty();
        }

        [TestMethod]
        public void GetDitWithGenra_AnotherUserDit_NotReturned()
        {
            var dit = new Dit("1") { Date = DateTime.Now.AddDays(1) };

            mockSet.PopulateDbset(new[] { dit });

            var dits = ditR.GetDitWithGenra(dit.AppUserId + "-");

            dits.Should().BeEmpty();
        }

        [TestMethod]
        public void GetDitWithGenra_DitIsForUser_Returned()
        {
            var dit = new Dit("1") { Date = DateTime.Now.AddDays(1) };

            mockSet.PopulateDbset(new[] { dit });

            var dits = ditR.GetDitWithGenra(dit.AppUserId);

            dits.Should().Contain(dit);
        }

        [TestMethod]
        public void GetUserFave_DitIsNotFave_NotReturned()
        {
            var dit = new Dit();

            dit.FaveDits.Add(new("1", dit.Id));

            mockSet.PopulateDbset(new[] { dit });

            var dits = ditR.GetUserFave("2");

            dits.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUserFave_DitIsRemoved_NotReturned()
        {
            var user = new AppUser() { Id = "1" };

            var dit = new Dit();

            var fave = new FaveDit(user.Id, dit.Id);

            fave.AppUserSet(user);

            dit.FaveDits.Add(fave);

            dit.Remove();

            mockSet.PopulateDbset(new[] { dit });

            var dits = ditR.GetUserFave("1");

            dits.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUserFave_DitIsInFave_Returned()
        {
            var dit = new Dit();

            dit.FaveDits.Add(new("1", dit.Id));

            mockSet.PopulateDbset(new[] { dit });

            var dits = ditR.GetUserFave("1");

            dits.Should().Equal(new[] { dit });
        }

    }
}
