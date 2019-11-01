namespace SocialNetworkAnaylser.Core
{
    using System;

    public class DomainEntity
    {
        public long Id { get; protected set; }
        public DateTime CreationTime { get; protected set; }
        public DateTime? DeletionTime { get; protected set; }
        public bool IsDeleted { get; protected set; }
    }
}
