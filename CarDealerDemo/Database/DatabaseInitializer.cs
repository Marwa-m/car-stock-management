using CarDealerDemo.Factory;
using CarDealerDemo.Helper;
using Dapper;

namespace CarDealerDemo.Database;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly ISqliteConnectionFactory _connectionFactory;
    private readonly IPasswordHasher _passwordHasher;

    public DatabaseInitializer(ISqliteConnectionFactory connectionFactory,
        IPasswordHasher passwordHasher)
    {
        _connectionFactory = connectionFactory;
        _passwordHasher = passwordHasher;
    }

    public void Initialize()
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        // Check if the Dealers table exists
        var dealersTableExists = connection.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type='table' AND name='Dealers'");

        if (dealersTableExists == 0)
        {
            // Create Dealers table if it doesn't exist
            var createDealersTableQuery = @"
        CREATE TABLE Dealers (
            ID INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL UNIQUE,
            Email TEXT NOT NULL UNIQUE,
            PasswordHash TEXT NOT NULL,
            Token TEXT NOT NULL
        );";
            connection.Execute(createDealersTableQuery);

            // Insert seed data into Dealers table
          var hashedPassowrd=  _passwordHasher.Hash("password");
            var insertDealersQuery = @"
        INSERT INTO Dealers (Name, Email, PasswordHash, Token) VALUES 
        ('Marwa', 'marwa@example.com', @PasswordHash, ''),
        ('Sari', 'sari@example.com', @PasswordHash, '');
        ";
            connection.Execute(insertDealersQuery, new { @PasswordHash=hashedPassowrd});

        }

        // Check if the Cars table exists
        var carsTableExists = connection.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type='table' AND name='Cars'");

        if (carsTableExists == 0)
        {
            // Create Cars table if it doesn't exist
            var createCarsTableQuery = @"
        CREATE TABLE Cars (
            ID INTEGER PRIMARY KEY AUTOINCREMENT,
            Make TEXT NOT NULL,
            Model TEXT NOT NULL,
            Year INTEGER NOT NULL,
            StockLevel INTEGER NOT NULL,
            DealerId INTEGER NOT NULL,
            FOREIGN KEY (DealerId) REFERENCES Dealers (ID),
            UNIQUE(Make, Model, Year, DealerId)
        );";
            connection.Execute(createCarsTableQuery);
            var marwaID = connection.ExecuteScalar<int>("SELECT ID FROM Dealers WHERE Name='Marwa'");
            var sariID = connection.ExecuteScalar<int>("SELECT ID FROM Dealers WHERE Name='Sari'");

            // Insert seed data into Cars table
            var insertCarsQuery = @$"
        INSERT INTO Cars (Make, Model, Year, StockLevel, DealerId) VALUES
        ('Toyota', 'Corolla', 2021, 10, {marwaID}),
        ('Honda', 'Civic', 2020, 8, {marwaID}),
        ('Ford', 'Focus', 2019, 5, {marwaID}),
        ('Tesla', 'Model 3', 2022, 7, {marwaID}),
        ('BMW', 'X5', 2021, 6, {sariID}),
        ('Audi', 'A4', 2020, 4, {sariID}),
        ('Mercedes', 'C-Class', 2021, 3, {sariID}),
        ('Hyundai', 'Elantra', 2019, 9, {sariID});
        ";
            connection.Execute(insertCarsQuery);
        }
    }

}
