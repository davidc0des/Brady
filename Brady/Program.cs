using Brady.Data;
using Brady.FileWatcher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

class Program
{
	public static void Main(string[] args)
	{
		var configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.Build();

		// Setup logging e.g use ILogger to log out to text file.

		var serviceProvider = new ServiceCollection()
			.Configure<FileWatcherSettings>(configuration.GetSection(FileWatcherSettings.SectionName))
			.AddSingleton<IFileWatcher, FileWatcher>()
			.AddTransient<IDataHelper, DataHelper>()
			.BuildServiceProvider();

		var fileWatcher = serviceProvider.GetRequiredService<IFileWatcher>();
		fileWatcher?.Setup();

		Console.WriteLine("Waiting for new file...");
		Console.ReadLine();
	}
}