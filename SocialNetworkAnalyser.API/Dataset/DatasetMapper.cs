namespace SocialNetworkAnalyser.API.Dataset
{
    using AutoMapper;
    using SocialNetworkAnalyser.Web.Models.Dataset;
    using SocialNetworkAnaylser.Core;
    using SocialNetworkAnaylser.Core.Dataset;

    public class DatasetMapper: Profile
    {
        public DatasetMapper()
        {
            CreateMap<Dataset, DatasetOutputModel>();
            CreateMap<UserAnalyze, UserOutputModel>();
            
        }
    }
}
