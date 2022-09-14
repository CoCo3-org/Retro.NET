using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LBMI_Relative : Instruction, IRelative 
	{
		public LBMI_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x2B; } }

		public override string Mnemonic { get { return "LBMI"; } }
	}
}
