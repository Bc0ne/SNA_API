namespace SocialNetworkAnaylser.Core.Friendship
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFriendshipRepository
    {
        Task AddDataAsync(List<Friendship> friendships);
        Task<IEnumerable<UserAnalyze>> GetAllUniqueUsersWithFriendsCountByDatasetIdAsync(long datasetId);
        Task<IEnumerable<Friendship>> GetAllFriendshipsByDatasetIdAsync(long datasetId);
        Task UpdateListOfFriendshipsAsync(IEnumerable<Friendship> friendships);
    }
}
