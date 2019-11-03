namespace SocialNetworkAnalyser.Web.Models.Dataset
{
    using System;
    using System.Collections.Generic;

    public class DatasetUsersOutputModel
    {
        public DatasetOutputModel Dataset { get; set; }
        = new DatasetOutputModel();
        public List<UserOutputModel> Users { get; set; }
    }
}
