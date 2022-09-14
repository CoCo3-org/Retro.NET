using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class INCE_Inherent : Instruction, IInherent 
	{
		public INCE_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x4C; } }

		public override string Mnemonic { get { return "INCE"; } }
	}
}
