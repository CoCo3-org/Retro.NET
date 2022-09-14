using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class CLRF_Inherent : Instruction, IInherent 
	{
		public CLRF_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x5F; } }

		public override string Mnemonic { get { return "CLRF"; } }
	}
}
