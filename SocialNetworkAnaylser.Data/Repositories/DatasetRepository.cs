namespace SocialNetworkAnaylser.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SocialNetworkAnaylser.Core.Dataset;
    using SocialNetworkAnaylser.Data.Context;

    public class DatasetRepository: IDatasetRepository
    {
        private readonly SocialNetworkAnalyserContext _context;

        public DatasetRepository(SocialNetworkAnalyserContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddDatasetAsync(Dataset dataset)
        {
            await _context.Datasets.AddAsync(dataset);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Dataset>> GetAllDatasetsAsync()
        {
            return await _context.Datasets
                .Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<Dataset> GetDatasetByIdAsync(long id)
        {
            return await _context.Datasets
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task UpdateDatasetAsync(Dataset dataset)
        {
            _context.Datasets.Update(dataset);
            await _context.SaveChangesAsync();
        }
    }
}
