namespace SocialNetworkAnaylser.Data.Context
{
    using Microsoft.EntityFrameworkCore;
    using SocialNetworkAnaylser.Core.Dataset;
    using SocialNetworkAnaylser.Core.Friendship;
    using SocialNetworkAnaylser.Data.Config;

    public class SocialNetworkAnalyserContext: DbContext
    {
        public SocialNetworkAnalyserContext(DbContextOptions<SocialNetworkAnalyserContext> options)
            :base(options)
        {
        }

        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Dataset> Datasets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FriendshipConfig())
                .ApplyConfiguration(new DatasetConfig());
        }
    }
}
