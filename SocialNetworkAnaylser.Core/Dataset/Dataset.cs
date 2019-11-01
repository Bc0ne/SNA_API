namespace SocialNetworkAnaylser.Core.Dataset
{
    using System;
    using System.Collections.Generic;
    using Core.Friendship;

    public class Dataset : DomainEntity
    {
        public string Name { get; private set; }
        public bool IsImported { get; private set; }
        public virtual IEnumerable<Friendship> Friendships { get; set; }

        public static Dataset New (string name)
        {
            return new Dataset
            {
                Name = name,
                CreationTime = DateTime.Now
            };
        }

        public void UpdateDateset()
        {
            IsImported = true;
        }
    }
}
