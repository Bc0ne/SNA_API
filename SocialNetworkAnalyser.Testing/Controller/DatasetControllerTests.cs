namespace SocialNetworkAnalyser.Testing.Controller
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Xunit;
    using Moq;
    using SocialNetworkAnalyser.API.Dataset;
    using SocialNetworkAnalyser.Web.Models;
    using SocialNetworkAnaylser.Core.Dataset;
    using SocialNetworkAnaylser.Core.Friendship;
    using SocialNetworkAnalyser.Web.Models.Dataset;
    using SocialNetworkAnalyser.API.Bootstraper;

    public class DatasetControllerTests
    {

        private readonly Mock<IDatasetRepository> _datasetMockRepository;
        private readonly Mock<IFriendshipRepository> _friendshipMockRepository;
        private readonly DatasetController _datasetController;

        public DatasetControllerTests()
        {
            AutoMapperConfig.Initialize();
            _datasetMockRepository = new Mock<IDatasetRepository>();
            _friendshipMockRepository = new Mock<IFriendshipRepository>();
            _datasetController = new DatasetController(_friendshipMockRepository.Object, _datasetMockRepository.Object);
        }

        [Fact]
        public async Task GetAllDatasetsAsync_ActionExecutes_ReturnsOk()
        {
            var result = await _datasetController.GetAllDatasetsAsync();
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task CreateDatasetAsync_InvalidModel_ResturnsBadRequest()
        {
            var datasetInputModel = new DatasetInputModel
            {
                Name = ""
            };

            var result = await _datasetController.CreateDatasetAsync(datasetInputModel);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var responseResult = Assert.IsType<ResponseResult>(badRequestResult.Value);
        }

        [Fact]
        public async Task CreateDatasetAsync_InvalidModel_AddDatasetAsyncNeverExecutes()
        {
            var datasetInputModel = new DatasetInputModel
            {
                Name = ""
            };

            var result = await _datasetController.CreateDatasetAsync(datasetInputModel);

            _datasetMockRepository.Verify(x => x.AddDatasetAsync(It.IsAny<Dataset>()), Times.Never);
        }

        [Fact]
        public async Task CreateDatasetAsync_ValidModel_AddDatasetAsyncCalledOnce()
        {
            Dataset dataset = null;

            _datasetMockRepository.Setup(r => r.AddDatasetAsync(It.IsAny<Dataset>()))
                .Callback<Dataset>(x => dataset = x);

            var datasetInputModel = new DatasetInputModel { Name = "Test Dataset" };

            await _datasetController.CreateDatasetAsync(datasetInputModel);

            _datasetMockRepository.Verify(x => x.AddDatasetAsync(It.IsAny<Dataset>()), Times.Once);
        }

        [Fact]
        public async Task GetAllUsersByDatasetIdAsync_DatasetIdNotExist_ReturnsNotFound()
        {
            _datasetMockRepository.Setup(repo => repo.GetAllDatasetsAsync())
               .ReturnsAsync(new List<Dataset>() { Dataset.New("First"), Dataset.New("Second") });

            var result = await _datasetController.GetAllUsersByDatasetIdAsync(0);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
