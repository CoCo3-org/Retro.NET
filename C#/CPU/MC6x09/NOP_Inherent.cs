using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class NOP_Inherent : Instruction, IInherent 
	{
		public NOP_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x12; } }

		public override string Mnemonic { get { return "NOP"; } }
	}
}
