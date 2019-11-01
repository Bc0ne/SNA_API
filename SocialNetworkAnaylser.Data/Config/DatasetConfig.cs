namespace SocialNetworkAnaylser.Data.Config
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SocialNetworkAnaylser.Core.Dataset;

    public class DatasetConfig : IEntityTypeConfiguration<Dataset>
    {
        public void Configure(EntityTypeBuilder<Dataset> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Name)
                .IsRequired();
        }
    }
}
