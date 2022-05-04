namespace Brady.Services.Factory
{
	public class XmlProcessorFactory : DataProcessorFactory
	{
		public override DataProcessor GetDataProcessor()
		{
			return new XmlProcessor();
		}
	}
}
