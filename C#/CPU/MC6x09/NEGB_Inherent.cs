using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class NEGB_Inherent : Instruction, IInherent 
	{
		public NEGB_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x50; } }

		public override string Mnemonic { get { return "NEGB"; } }
	}
}
