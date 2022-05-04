using Brady.Data;

namespace Brady.Services.Calculators
{
	public class HighestDailyEmissionsOutput : IOutputStrategy
	{
		private readonly IDataHelper _dataHelper;

		public HighestDailyEmissionsOutput(IDataHelper dataHelper)
		{
			_dataHelper = dataHelper;
		}

		public string GenerateOutput(GeneratorData inputData)
		{
			// Daily emissions = Energy x EmissionRating x EmissionFactor
			var referenceData = _dataHelper.GetReferenceData();
			var valuFactorHigh = referenceData.Factors.ValueFactor.High;

			//TODO: Calculate the highest daily emissions for each day using the referenceData and Factor data 


			throw new NotImplementedException();
		}
	}
}
