using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brady.Services.Factory
{
	public class JsonProcessorFactory : DataProcessorFactory
	{
		public override DataProcessor GetDataProcessor()
		{
			return new JsonProcessor();
		}
	}
}
