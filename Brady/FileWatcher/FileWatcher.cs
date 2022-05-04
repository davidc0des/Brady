using Brady.Data;
using Microsoft.Extensions.Options;
using Brady.Services.Calculators;
using Brady.Services.Factory;
using Brady.Data.Consts;

namespace Brady.FileWatcher
{
	internal sealed class FileWatcher : IFileWatcher
	{
		private readonly IOptions<FileWatcherSettings> _settings;
		private readonly IDataHelper _dataHelper;

		public FileWatcher(IOptions<FileWatcherSettings> configuration, IDataHelper dataHelper)
		{
			_settings = configuration;
			_dataHelper = dataHelper;
		}

		public void Setup()
		{
			var filePath = _settings.Value.FilePath;
			var fileName = _settings.Value.FileName;
			var fileExtension = _settings.Value.FileExtension;

			var watcher = new FileSystemWatcher(filePath)
			{
				NotifyFilter = NotifyFilters.Attributes
										 | NotifyFilters.CreationTime
										 | NotifyFilters.FileName
										 | NotifyFilters.LastWrite
			};

			watcher.Changed += OnChanged;
			watcher.Created += OnCreated;

			watcher.Filter = "*.xml";
			watcher.IncludeSubdirectories = true;
			watcher.EnableRaisingEvents = true;
		}

		private static void OnChanged(object sender, FileSystemEventArgs e)
		{
			if (e.ChangeType != WatcherChangeTypes.Changed)
			{
				return;
			}

			Console.WriteLine($"Changed: {e.FullPath}");
		}

		private void OnCreated(object sender, FileSystemEventArgs e)
		{
			Console.WriteLine($"New file detected at {e.FullPath}");

			try
			{
				var fileInfo = new FileInfo(e.FullPath);

				var generatorData = GetDataProcessorFactory(fileInfo.Extension)
					.GetDataProcessor()
					.ParseFileContents(e.FullPath);

				var resultData = GenerateResultData(generatorData);

				// Extend and call dataProcessor to generate result output file.
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred during processing of file: {ex.Message}");
			}
		}

		private (string highestDailyEmissionsResult, string actualHeatRateResult, string totalGenerationValueResult) GenerateResultData(GeneratorData inputData)
		{
			var calculator = new CalculatorContext(new HighestDailyEmissionsOutput(_dataHelper));
			var highestDailyEmissionsResult = calculator.Calculate(inputData);

			calculator.SetCalculator(new ActualHeatRateOutput());
			var actualHeatRateResult = calculator.Calculate(inputData);

			calculator.SetCalculator(new TotalGenerationValueOutput(_dataHelper));
			var totalGenerationValueResult = calculator.Calculate(inputData);

			return (highestDailyEmissionsResult, actualHeatRateResult, totalGenerationValueResult);
		}

		private static DataProcessorFactory GetDataProcessorFactory(string fileExtension)
		{
			switch (fileExtension)
			{
				case FileExtension.XML:
					return new XmlProcessorFactory();
				case FileExtension.JSON:
					return new JsonProcessorFactory();
				default:
					Console.Error.WriteLine("File extension not recognised. This file will not be processed.");
					break;
			}

			throw new NotSupportedException("File extension is not currently supported.");
		}
	}
}
