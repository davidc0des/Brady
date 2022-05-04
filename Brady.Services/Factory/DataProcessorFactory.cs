using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brady.Services.Factory
{
	public abstract class DataProcessorFactory
	{
		public abstract DataProcessor GetDataProcessor();
	}
}
