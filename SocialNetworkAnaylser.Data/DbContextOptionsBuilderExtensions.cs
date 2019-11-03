namespace SocialNetworkAnaylser.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;

    public static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder UseDatabase(
            this DbContextOptionsBuilder options,
            SupportedDatabase db,
            string connectionString)
        {
            switch (db)
            {
                case SupportedDatabase.SqlServer:
                    return options.UseSqlServer(connectionString);

                case SupportedDatabase.Postgres:
                    return options.UseNpgsql(connectionString);

                default:
                    throw new InvalidOperationException($"Unkown database: {db}");
            }
        }
    }
}
