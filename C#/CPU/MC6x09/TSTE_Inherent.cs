using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class TSTE_Inherent : Instruction, IInherent 
	{
		public TSTE_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x4D; } }

		public override string Mnemonic { get { return "TSTE"; } }
	}
}
