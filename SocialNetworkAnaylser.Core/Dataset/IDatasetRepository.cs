namespace SocialNetworkAnaylser.Core.Dataset
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDatasetRepository
    {
        Task<Dataset> GetDatasetByIdAsync(long id);
        Task UpdateDatasetAsync(Dataset dataset);
        Task AddDatasetAsync(Dataset dataset);
        Task<List<Dataset>> GetAllDatasetsAsync();
    }
}
