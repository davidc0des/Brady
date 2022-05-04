using System.Xml.Serialization;

namespace Brady.Data
{
	[XmlRoot(ElementName = "ReferenceData")]
	public class ReferenceData
	{
		public Factors Factors { get; set; }
	}

	[XmlRoot(ElementName = "Factors")]
	public class Factors
	{
		public ValueFactor ValueFactor { get; set; }

		public EmissionsFactor EmissionsFactor { get; set; }
	}

	[XmlRoot(ElementName = "EmissionsFactor")]
	public class EmissionsFactor
	{
		[XmlElement(ElementName = "High")]
		public float High { get; set; }

		[XmlElement(ElementName = "Medium")]
		public float Medium { get; set; }

		[XmlElement(ElementName = "Low")]
		public float Low { get; set; }
	}

	[XmlRoot(ElementName = "ValueFactor")]
	public class ValueFactor
	{
		[XmlElement(ElementName = "High")]
		public float High { get; set; }

		[XmlElement(ElementName = "Medium")]
		public float Medium { get; set; }

		[XmlElement(ElementName = "Low")]
		public float Low { get; set; }
	}
}
