namespace SocialNetworkAnaylser.Core.Friendship
{
    using System;
    using Core.Dataset;

    public class Friendship : DomainEntity
    {
        public long PersonId1 { get; private set; }
        public long PersonId2 { get; private set; }
        public long DatasetId { get; private set; }
        public virtual Dataset Dataset { get; private set; }

        public static Friendship New(long personId1, long personId2, Dataset dataset)
        {
            return new Friendship
            {
                PersonId1 = personId1,
                PersonId2 = personId2,
                Dataset = dataset,
                CreationTime = DateTime.Now
            };
        }

        public void Delete()
        {
            DeletionTime = DateTime.Now;
            IsDeleted = true;
        }
    }
}
