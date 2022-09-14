using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class INCA_Inherent : Instruction, IInherent 
	{
		public INCA_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x4C; } }

		public override string Mnemonic { get { return "INCA"; } }
	}
}
