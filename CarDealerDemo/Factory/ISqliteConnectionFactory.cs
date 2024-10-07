using Microsoft.Data.Sqlite;

namespace CarDealerDemo.Factory;

public interface ISqliteConnectionFactory
{
    SqliteConnection CreateConnection();
}

