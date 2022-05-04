using Brady.Data;

namespace Brady.Services.Calculators
{
	public class TotalGenerationValueOutput : IOutputStrategy
	{
		private readonly IDataHelper _dataHelper;

		public TotalGenerationValueOutput(IDataHelper dataHelper)
		{
			_dataHelper = dataHelper;
		}

		public string GenerateOutput(GeneratorData inputData)
		{
			// Daily Generation Value = Energy x Price x ValueFactor
			var refData = _dataHelper.GetReferenceData();

			foreach(var generator in inputData.Wind.WindGenerator)
			{
				foreach (var day in generator.Generation.Day)
				{
					var valueFactor = generator.Name.Equals("Wind[Offshore]") 
						? refData.Factors.ValueFactor.Low 
						: refData.Factors.ValueFactor.High;

					if(Double.TryParse(day.Energy, out var energyDouble)
						&& Double.TryParse(day.Price, out var priceDouble))
					{
						var dailyGenerationValue = energyDouble * priceDouble * valueFactor;
					}
				}
			}

			// Extend XmlProcessor to handle the generation of XmlDocument with results in correct format.
			// Extend this class to handle multiple generators e.g. Coal, Gas etc.


			return string.Empty;
		}
	}
}
