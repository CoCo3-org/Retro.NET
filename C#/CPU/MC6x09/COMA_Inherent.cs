using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class COMA_Inherent : Instruction, IInherent 
	{
		public COMA_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x43; } }

		public override string Mnemonic { get { return "COMA"; } }
	}
}
