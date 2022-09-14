using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class SWI3_Inherent : Instruction, IInherent 
	{
		public SWI3_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x3F; } }

		public override string Mnemonic { get { return "SWI3"; } }
	}
}
