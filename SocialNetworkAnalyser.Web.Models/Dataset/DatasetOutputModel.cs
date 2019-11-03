namespace SocialNetworkAnalyser.Web.Models.Dataset
{
    using System;

    public class DatasetOutputModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsImported { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
