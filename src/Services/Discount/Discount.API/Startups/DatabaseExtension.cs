using System;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.API.Startups
{
	public static class DatabaseExtension
	{
		public static void MigrateDatabase(this IServiceProvider serviceProvider, int retry = 1)
		{

            int retryForAvailability = retry;

            var logger = serviceProvider.GetRequiredService<ILogger<IStartup>>();

			var configuration = serviceProvider.GetRequiredService<IConfiguration>();
			string ConnectionString = configuration.GetValue<string>("DatabaseSettingss:ConnectionString")
                    ?? throw new ArgumentNullException(nameof(ConnectionString));


			try
			{
				logger.LogInformation("Migrating postgresql database.");

                using var connection = new NpgsqlConnection(ConnectionString);

				connection.Open();

				using var command = new NpgsqlCommand()
				{
					Connection = connection
				};

                command.CommandText = "DROP TABLE IF EXISTS Coupon";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
                command.ExecuteNonQuery();


                command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
			{
                if (retryForAvailability < 50)
                {
                    logger.LogError("Migration postgres failed, try again: " + retryForAvailability);
                    logger.LogError(ex.Message);

                    retryForAvailability++;
                    MigrateDatabase(serviceProvider, retryForAvailability);
                    Thread.Sleep(2000);
                }
            }
        }

	}
}

