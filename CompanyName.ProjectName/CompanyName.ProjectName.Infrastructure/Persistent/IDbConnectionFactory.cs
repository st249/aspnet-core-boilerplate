using Microsoft.Data.SqlClient;
using System.Data;
using Npgsql;

namespace CompanyName.ProjectName.Infrastructure.Persistent;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection(string connectionString);
}

public interface ISqlServerDbConnectionFactory : IDbConnectionFactory
{

}

public interface IPostgresDbConnectionFactory : IDbConnectionFactory
{

}

public class SqlServerDbConnectionFactory : ISqlServerDbConnectionFactory
{

    public IDbConnection CreateConnection(string connectionString)
    {
        return new SqlConnection(connectionString);
    }
}

public class PostgresDbConnectionFactory : IPostgresDbConnectionFactory
{
    public IDbConnection CreateConnection(string connectionString)
    {
        return new NpgsqlConnection(connectionString);
    }
}


