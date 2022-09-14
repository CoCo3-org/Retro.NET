using System;
using System.Collections.Generic;
using System.Text;

namespace CPU
{
	public class EQU_Directive : Directive
	{
		public override string Mnemonic { get { return "EQU"; } }

		public EQU_Directive(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}
	}
}
