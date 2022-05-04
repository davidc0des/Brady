using Brady.Data;

namespace Brady.Services.Calculators
{
	public class CalculatorContext
	{
		private IOutputStrategy _calculationStrategy;

		public CalculatorContext(IOutputStrategy calculationStrategy)
		{
			_calculationStrategy = calculationStrategy;
		}

		public void SetCalculator(IOutputStrategy calculationStrategy) => _calculationStrategy = calculationStrategy;

		public string Calculate(GeneratorData inputData)
		{
			return _calculationStrategy.GenerateOutput(inputData);
		}
	}
}
