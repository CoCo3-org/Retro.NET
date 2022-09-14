using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ROLB_Inherent : Instruction, IInherent 
	{
		public ROLB_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x59; } }

		public override string Mnemonic { get { return "ROLB"; } }
	}
}
