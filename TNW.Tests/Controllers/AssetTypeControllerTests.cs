using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TNW.Interfaces;
using TNW.Controllers;
using TestStack.FluentMVCTesting;
using TNW.ViewModels;
using TNW.Models;
using System.Linq.Expressions;
using AutoMapper;
using TNW.App_Start;
using System.Web.Mvc;
using System.Security.Principal;
using System.Security.Claims;

namespace TNW.Tests.Controllers
{
    [TestFixture]
    public class AssetTypeControllerTests
    {
        [Test]
        public void IndexViewShouldReturnListOfAssetTypes()
        {
            // Arrange
            IEnumerable<AssetType> assettypes = new List<AssetType>()
            {
                new AssetType { Id = 1, Comments = "test", OwnerId = "123", TypeName= "type1"},
                new AssetType { Id = 1, Comments = "test", OwnerId = "123", TypeName= "type2"},
            };
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());
            var fakeUOW = new Mock<IUnitOfWork>();
            var fakeAssetRepo = new Mock<IGenericRepository<AssetType>>();
            fakeAssetRepo.Setup(x =>
                x.GetAll(It.IsAny<Expression<Func<AssetType, bool>>>(), It.IsAny<Expression<Func<AssetType, object>>[]>()))
                .Returns(assettypes);
            fakeUOW.Setup(x => x.AssetTypes).Returns(fakeAssetRepo.Object);

            var sut = new AssetTypesController(fakeUOW.Object);

            // act and assert
            sut.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<AssetTypeViewModel>>(x => x.Count() == 2);
        }

        [Test]
        public void CreateReturnsDefaultView()
        {
            // Arrange
            var fakeUOW = new Mock<IUnitOfWork>();

            var sut = new AssetTypesController(fakeUOW.Object);

            // act and assert
            sut.WithCallTo(x => x.Create())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void CreateWithValidModelShouldCreateAssetTypeAndRedirectToIndex()
        {
            // Arrange
            AssetTypeViewModel assetType = new AssetTypeViewModel
            {
                Comments = "test",
                TypeName = "debt"
            };

            string username = "username";
            string userid = Guid.NewGuid().ToString("N"); //could be a constant
            List<Claim> claims = new List<Claim> {
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userid)
            };
            var genericIdentity = new GenericIdentity("");
            genericIdentity.AddClaims(claims);
            var genericPrincipal = new GenericPrincipal(genericIdentity, new string[] { "Asegurado" });
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(genericPrincipal);

            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());

            var fakeUOW = new Mock<IUnitOfWork>();
            var fakeAssetRepo = new Mock<IGenericRepository<AssetType>>();
            fakeUOW.Setup(x => x.AssetTypes).Returns(fakeAssetRepo.Object);
            var sut = new AssetTypesController(fakeUOW.Object);
            sut.ControllerContext = controllerContext.Object;

            // act and assert
            sut.WithCallTo(x => x.Create(assetType))
                .ShouldRedirectTo(x => x.Index);
            fakeAssetRepo.Verify(x => x.Add(It.IsAny<AssetType>()), Times.Once());
        }
    }
}
