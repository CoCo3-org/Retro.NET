using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LBRA_Relative : Instruction, IRelative 
	{
		public LBRA_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x16; } }

		public override string Mnemonic { get { return "LBRA"; } }
	}
}
