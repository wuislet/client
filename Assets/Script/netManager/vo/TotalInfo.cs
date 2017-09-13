using System;

namespace AssemblyCSharp
{
	[Serializable]
	public class TotalInfo
	{
		public string gang;
		public string peng;
		public string chi;
		public string hu;
		public string genzhuang;
		public TotalInfo ()
		{

		}
        public String toString()
        {
            return " [class]TotalInfo " + " gang: " + gang + " peng " + peng + " chi " + chi + " hu " + hu + " genzhuang " + genzhuang;
        }
	}
}

