namespace SocialNetworkAnaylser.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using SocialNetworkAnaylser.Core;
    using SocialNetworkAnaylser.Core.Friendship;
    using SocialNetworkAnaylser.Data.Context;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly SocialNetworkAnalyserContext _context;

        public FriendshipRepository(SocialNetworkAnalyserContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddDataAsync(List<Friendship> friendships)
        {
            await _context.Friendships.AddRangeAsync(friendships);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserAnalyze>> GetAllUsersByDatasetIdAsync(long datasetId)
        {
            var users = await (from UserTbl in (
                   ((
                       from Friendships in _context.Friendships
                       where
                           Friendships.DatasetId == datasetId
                           && !Friendships.IsDeleted
                       select new
                       {
                           Friendships.PersonId1
                       }
                   ).Concat
                   (
                       from Friendships in _context.Friendships
                       where
                           Friendships.DatasetId == datasetId
                           && !Friendships.IsDeleted
                       select new
                       {
                           PersonId1 = Friendships.PersonId2
                       }
                     )))
                         select UserTbl).ToListAsync();

            var result = from user in users
                         group user by new
                         {
                             user.PersonId1
                         } into g
                         orderby
                         g.Key.PersonId1
                         select new UserAnalyze
                         {
                             UserId = g.Key.PersonId1,
                             NumOfFriends = g.Count()
                         };

            return result;
        }
    }
}
