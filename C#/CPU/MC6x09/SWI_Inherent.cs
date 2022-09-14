using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class SWI_Inherent : Instruction, IInherent 
	{
		public SWI_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x3F; } }

		public override string Mnemonic { get { return "SWI"; } }
	}
}
