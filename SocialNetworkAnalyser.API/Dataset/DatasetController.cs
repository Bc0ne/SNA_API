namespace SocialNetworkAnalyser.API.Dataset
{
    using System.IO;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Routing;
    using SocialNetworkAnaylser.Core.Dataset;
    using SocialNetworkAnalyser.Web.Models.Dataset;
    using SocialNetworkAnaylser.Core.Friendship;
    using AutoMapper;
    using SocialNetworkAnalyser.Web.Models;

    [Route("api/datasets")]
    [ApiController]
    public class DatasetController : Controller
    {

        private readonly IDatasetRepository _datasetRepository;
        private readonly IFriendshipRepository _friendShipRepository;

        public DatasetController(IFriendshipRepository friendshipRepository, IDatasetRepository datasetRepository)
        {
            _datasetRepository = datasetRepository;
            _friendShipRepository = friendshipRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDatasetsAsync()
        {
            var datasets = await _datasetRepository.GetAllDatasetsAsync();

            var result = Mapper.Map<List<DatasetOutputModel>>(datasets);

            return Ok(ResponseResult.SucceededWithData(result));
        }

        [HttpGet("{id}/users")]
        public async Task<IActionResult> GetAllUsersByDatasetIdAsync(long id)
        {
            var dataset = await _datasetRepository.GetDatasetByIdAsync(id);

            if (dataset == null)
            {
                return NotFound(ResponseResult.Failed(ErrorCode.Error, "Dataset isn't found."));
            }


            var users = await _friendShipRepository.GetAllUniqueUsersWithFriendsCountByDatasetIdAsync(id);

            var result = new DatasetUsersOutputModel
            {
                Dataset = Mapper.Map<DatasetOutputModel>(dataset),
                Users = Mapper.Map<List<UserOutputModel>>(users)
            };

            return Ok(ResponseResult.SucceededWithData(result));
        }

        [HttpGet("{id}/friendships")]
        public async Task<IActionResult> GetAllFriendshipsByDatasetIdAsync(long id)
        {
            var dataset = await _datasetRepository.GetDatasetByIdAsync(id);

            if (dataset == null)
            {
                return NotFound(ResponseResult.Failed(ErrorCode.Error, "Dataset isn't found."));
            }

            var friendships = await _friendShipRepository.GetAllFriendshipsByDatasetIdAsync(id);

            var result = Mapper.Map<List<FriendshipOutputModel>>(friendships);

            return Ok(ResponseResult.SucceededWithData(result));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDatasetAsync(DatasetInputModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return BadRequest(ResponseResult.Failed(ErrorCode.ValidationError, "Dataset name can't be empty."));
            }

            var dataset = Dataset.New(model.Name);

            await _datasetRepository.AddDatasetAsync(dataset);

            var result = Mapper.Map<DatasetOutputModel>(dataset);

            return Ok(ResponseResult.SucceededWithData(result));
        }

        [HttpPost("{id}/data")]
        public async Task<IActionResult> UploadDataByDatasetIdAsync(long id, IFormFile file)
        {
            if (file == null)
            {
                return BadRequest(ResponseResult.Failed(ErrorCode.ValidationError, "File can't be empty."));
            }

            var dataset = await _datasetRepository.GetDatasetByIdAsync(id);

            if (dataset == null)
            {
                return NotFound(ResponseResult.Failed(ErrorCode.Error, "Dataset isn't found."));
            }

            if (dataset.IsImported)
            {
                return BadRequest(ResponseResult.Failed(ErrorCode.Error, "Dataset can't be imported more than once."));
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

            dataset.Update();

            await _datasetRepository.UpdateDatasetAsync(dataset);

            return Ok(ResponseResult.Succeeded());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDatasetByIdAsync(long id)
        {
            var dataset = await _datasetRepository.GetDatasetByIdAsync(id);

            if (dataset == null)
            {
                return NotFound(ResponseResult.Failed(ErrorCode.Error, "Dataset isn't found."));
            }

            var friendships = await _friendShipRepository.GetAllFriendshipsByDatasetIdAsync(id);

            foreach(var fs in friendships)
            {
                fs.Delete();
            }

            dataset.Delete();

            await _datasetRepository.UpdateDatasetAsync(dataset);
            await _friendShipRepository.UpdateListOfFriendshipsAsync(friendships);

            return Ok();
        }
    }
}