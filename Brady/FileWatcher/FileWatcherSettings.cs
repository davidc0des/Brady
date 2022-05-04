namespace Brady.FileWatcher
{
	public class FileWatcherSettings
	{
		public const string SectionName = "FileWatcher";

		public string FilePath { get; set; } = "";

		public string FileName { get; set; } = "";

		public string FileExtension { get; set; } = "";
	}
}
