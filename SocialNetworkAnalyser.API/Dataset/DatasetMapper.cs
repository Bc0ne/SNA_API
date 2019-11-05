namespace SocialNetworkAnalyser.API.Dataset
{
    using AutoMapper;
    using SocialNetworkAnalyser.Web.Models.Dataset;
    using SocialNetworkAnaylser.Core;
    using SocialNetworkAnaylser.Core.Dataset;
    using SocialNetworkAnaylser.Core.Friendship;

    public class DatasetMapper: Profile
    {
        public DatasetMapper()
        {
            CreateMap<Dataset, DatasetOutputModel>();
            CreateMap<UserAnalyze, UserOutputModel>();
            CreateMap<Friendship, FriendshipOutputModel>();
            
        }
    }
}
