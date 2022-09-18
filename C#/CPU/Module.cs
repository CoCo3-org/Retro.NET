using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPU
{
	public class Module : CodeBlock
	{
		public override string XmlTag => "Module";
		
		public Module() 
			: base(null)
		{
		}
	}
}
