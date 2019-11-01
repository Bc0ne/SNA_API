namespace SocialNetworkAnaylser.Data.Config
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SocialNetworkAnaylser.Core.Friendship;

    class FriendshipConfig : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(x => x.Dataset)
                .WithMany(m => m.Friendships)
                .HasForeignKey(x => x.DatasetId);
        }
    }
}
