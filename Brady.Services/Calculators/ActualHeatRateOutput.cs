using Brady.Data;
using System.Xml;

namespace Brady.Services.Calculators
{
	public class ActualHeatRateOutput : IOutputStrategy
	{
		public string GenerateOutput(GeneratorData inputData)
		{
			// Actual Heat Rate = TotalHeatInput / ActualNetGeneration
			var document = new XmlDocument();
			var root = document.DocumentElement;
			var actualHeatRatesElement = document.CreateElement("ActualHeatRates");
			document.AppendChild(actualHeatRatesElement);

			foreach (var generator in inputData.Coal.CoalGenerator)
			{
				var generatorActualHeatRate = 0.0;

				if (Double.TryParse(generator.ActualNetGeneration, out var actualNet)
					&& Double.TryParse(generator.TotalHeatInput, out var totalHeat))
				{
					generatorActualHeatRate = totalHeat / actualNet;
				}

				// Move to XmlProcessor to handle XML document creation, passing calculated results.

				var actualHeatRateElement = document.CreateElement("ActualHeatRate");
				actualHeatRatesElement.AppendChild(actualHeatRateElement);

				var nameElement = document.CreateElement("Name");
				var nameVal = document.CreateTextNode(generator.Name);
				nameElement.AppendChild(nameVal);
				actualHeatRateElement.AppendChild(nameElement);

				var heatRateElement = document.CreateElement("HeatRate");
				var heatRateVal = document.CreateTextNode(generatorActualHeatRate.ToString());
				heatRateElement.AppendChild(heatRateVal);
				actualHeatRateElement.AppendChild(heatRateElement);
			}

			return document.OuterXml;
		}
	}
}
