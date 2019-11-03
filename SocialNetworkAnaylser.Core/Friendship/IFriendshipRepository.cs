namespace SocialNetworkAnaylser.Core.Friendship
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFriendshipRepository
    {
        Task AddDataAsync(List<Friendship> friendships);
        Task<IEnumerable<UserAnalyze>> GetAllUsersByDatasetIdAsync(long datasetId);
    }
}
