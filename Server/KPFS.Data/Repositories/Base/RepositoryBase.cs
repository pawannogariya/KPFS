using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace KPFS.Data.Repositories.Base
{
    public abstract class RepositoryBase
    {
        protected KpfsDbContext Context { get; }
        private readonly IConfiguration configuration;
        protected RepositoryBase(KpfsDbContext context, IConfiguration configuration)
        {
            Context = context;
            this.configuration = configuration;
        }

        protected IDbConnection BuildConnection()
        {
            return new MySqlConnection(configuration.GetConnectionString("Default"));
        }
    }
}
