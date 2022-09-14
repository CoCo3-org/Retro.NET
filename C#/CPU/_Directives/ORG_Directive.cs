using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPU
{
	public class ORG_Directive : Directive
	{
		public override string Mnemonic { get { return "ORG"; } }

		public ORG_Directive(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}
	}
}
