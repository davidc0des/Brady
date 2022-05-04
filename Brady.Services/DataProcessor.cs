using Brady.Data;

namespace Brady.Services
{
	public abstract class DataProcessor
	{
		public abstract GeneratorData ParseFileContents(string filePath);
	}
}
