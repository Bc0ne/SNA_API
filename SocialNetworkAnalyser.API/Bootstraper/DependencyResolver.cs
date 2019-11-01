namespace SocialNetworkAnalyser.API.Bootstraper
{
    using System;
    using Autofac;
    using Microsoft.AspNetCore.Hosting;
    using SocialNetworkAnaylser.Data.Context;
    using SocialNetworkAnaylser.Data.Repositories;

    public class DependencyResolver : Module
    {
        private readonly IHostingEnvironment _env;

        public DependencyResolver(IHostingEnvironment env)
        {
            _env = env;
        }

        protected override void Load(ContainerBuilder builder)
        {
            LoadModules(builder);
        }

        private void LoadModules(ContainerBuilder builder)
        {
            builder.RegisterType<SocialNetworkAnalyserContext>().InstancePerLifetimeScope();
            builder.RegisterType<FriendshipRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<DatasetRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
