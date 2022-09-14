using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class COM_Instruction : Instruction 
	{
		public COM_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "COM"; } }
	}
}
