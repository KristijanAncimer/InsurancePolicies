using DbUp;

namespace InsurancePoliciesWebApp.Util;

public static class DatabaseMigrator
{
    public static void Migrate(string connectionString)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        Console.WriteLine($"Current Working Directory: {currentDirectory}");

        var migrationsPath = Path.Combine(currentDirectory, @"..\InsurancePolicies.Infrastructure\Migrations");

        if (!Directory.Exists(migrationsPath))
        {
            Console.WriteLine($"Migrations folder not found at: {migrationsPath}");
            Environment.Exit(-1);
        }

        var upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsFromFileSystem(migrationsPath)
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.WriteLine("Migration failed");
            Console.WriteLine(result.Error);
            Environment.Exit(-1);
        }

        Console.WriteLine("Migration succeeded!");
    }
}
