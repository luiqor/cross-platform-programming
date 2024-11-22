namespace Lab6.Data;

public class DbConfiguration
{
    public required string DatabaseProvider { get; set; }  // MS-SQL, PostgreSQL, SQLite, In-Memory
    public required string ConnectionString { get; set; }
}
