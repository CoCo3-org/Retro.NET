using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class TSTF_Inherent : Instruction, IInherent 
	{
		public TSTF_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x5D; } }

		public override string Mnemonic { get { return "TSTF"; } }
	}
}
