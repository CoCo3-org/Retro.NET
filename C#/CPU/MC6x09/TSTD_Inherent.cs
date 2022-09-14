using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class TSTD_Inherent : Instruction, IInherent 
	{
		public TSTD_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x4D; } }

		public override string Mnemonic { get { return "TSTD"; } }
	}
}
