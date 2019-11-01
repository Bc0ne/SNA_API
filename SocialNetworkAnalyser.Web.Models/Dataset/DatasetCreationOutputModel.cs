using System;

namespace SocialNetworkAnalyser.Web.Models.Dataset
{
    public class DatasetCreationOutputModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsImported { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
