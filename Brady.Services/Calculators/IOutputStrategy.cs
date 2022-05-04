using Brady.Data;

namespace Brady.Services.Calculators
{
	public interface IOutputStrategy
	{
		string GenerateOutput(GeneratorData inputData);
	}
}
