namespace SocialNetworkAnalyser.API.Friendship
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.IO;
    using SocialNetworkAnaylser.Core.Friendship;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Routing;
    using SocialNetworkAnaylser.Core.Dataset;
    using SocialNetworkAnalyser.Web.Models.Dataset;

    [ApiController]
    [Route("api/datasets")]
    public class DatasetController : Controller
    {
        private readonly IDatasetRepository _datasetRepository;
        private readonly IFriendshipRepository _friendShipRepository;

        public DatasetController(IFriendshipRepository friendshipRepository, IDatasetRepository datasetRepository)
        {
            _datasetRepository = datasetRepository;
            _friendShipRepository = friendshipRepository;
        }

        public async Task<IActionResult> GetAllDatasetsAsync()
        {
            var datasets = await _datasetRepository.GetAllDatasetsAsync();

            var result = new List<DatasetCreationOutputModel>();

            foreach(var dataset in datasets)
            {
                result.Add(new DatasetCreationOutputModel
                {
                    Id = dataset.Id,
                    Name = dataset.Name,
                    CreationTime = dataset.CreationTime,
                    IsImported = dataset.IsImported
                });
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllUsersByDatasetIdAsync(long id)
        {
            var dataset = await _datasetRepository.GetDatasetByIdAsync(id);

            if (dataset == null)
            {
                return NotFound($"There is no dataset by id: {id}");
            }

            var result = await _friendShipRepository.GetUsersCountByDatasetIdAsync(id);

            var datasetUsersOutputModel = new List<DatasetUsersOutputModel>();

            foreach(var user in result)
            {
                datasetUsersOutputModel.Add(new DatasetUsersOutputModel
                {
                    UserId = user.Key,
                    NumOfFriends = user.Value
                });
            }

            return Ok(datasetUsersOutputModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDatasetAsync(DatasetInputModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return BadRequest("Dataset name can't be empty.");
            }

            var dataset = Dataset.New(model.Name);

            await _datasetRepository.AddDatasetAsync(dataset);

            var datasetCreationOutputModel = new DatasetCreationOutputModel
            {
                Id = dataset.Id,
                Name = dataset.Name,
                IsImported = dataset.IsImported,
                CreationTime = dataset.CreationTime
            };

            return Ok(datasetCreationOutputModel);
        }

        [HttpPost("{id}/upload")]
        public async Task<IActionResult> UploadFriendshipDatasetAsync(long id, IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("File can't be null.");
            }

            var dataset = await _datasetRepository.GetDatasetByIdAsync(id);

            if (dataset == null)
            {
                return NotFound($"There is no dataset by id: {id}");
            }

            if (dataset.IsImported)
            {
                return BadRequest($"Dataset can't be imported more than once.");
            }

            var friendships = new List<Friendship>();

            using (StreamReader streamReader = new StreamReader(file.OpenReadStream()))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var fields = line.Split(' ');
                    friendships.Add(Friendship.New(long.Parse(fields[0]), long.Parse(fields[1]), dataset));
                }
            }

            await _friendShipRepository.AddDataAsync(friendships);

            dataset.UpdateDateset();

            await _datasetRepository.UpdateDatasetAsync(dataset);

            return Ok();
        }
    }
}