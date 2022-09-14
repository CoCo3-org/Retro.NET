using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LSLD_Inherent : Instruction, IInherent 
	{
		public LSLD_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x48; } }

		public override string Mnemonic { get { return "LSLD"; } }
	}
}
