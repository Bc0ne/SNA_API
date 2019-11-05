namespace SocialNetworkAnalyser.Web.Models.Dataset
{
    using System;

    public class FriendshipOutputModel
    {
        public long Id { get; set; }
        public long PersonId1 { get; set; }
        public long PersonId2 { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
