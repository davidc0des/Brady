using Brady.Data;
using System.Xml;
using System.Xml.Serialization;

namespace Brady.Services
{
	internal sealed class XmlProcessor : DataProcessor
	{
		public override GeneratorData ParseFileContents(string filePath)
		{
			var generatorData = new GeneratorData();
			var doc = new XmlDocument();
			doc.Load(filePath);

			using (var stream = GenerateStream(doc.OuterXml))
			{
				using var reader = new StreamReader(stream);
				var fileContents = reader.ReadToEnd();

				var serializer = new XmlSerializer(typeof(GeneratorData), new XmlRootAttribute("GenerationReport"));
				var stringReader = new StringReader(fileContents);

				generatorData = (GeneratorData)serializer.Deserialize(stringReader);
			}

			return generatorData;
		}

		private static Stream GenerateStream(string contents)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);

			writer.Write(contents);
			writer.Flush();
			stream.Position = 0;

			return stream;
		}
	}
}
