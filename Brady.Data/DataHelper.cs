using System.Reflection;
using System.Xml.Serialization;

namespace Brady.Data
{
	public class DataHelper : IDataHelper
	{
		public ReferenceData GetReferenceData()
		{
			var referenceData = new ReferenceData();
			var resource = Assembly.LoadFrom("Brady").GetManifestResourceStream("Brady.ReferenceData.xml");

			using (var stream = resource)
			{
				using var reader = new StreamReader(stream);
				var fileContents = reader.ReadToEnd();


				var serializer = new XmlSerializer(typeof(ReferenceData), new XmlRootAttribute("ReferenceData"));

				var stringReader = new StringReader(fileContents);

				referenceData = (ReferenceData)serializer.Deserialize(stringReader);
			}

			return referenceData;
		}
	}
}
