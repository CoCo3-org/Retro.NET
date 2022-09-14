using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class DAA_Inherent : Instruction, IInherent 
	{
		public DAA_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x19; } }

		public override string Mnemonic { get { return "DAA"; } }
	}
}
