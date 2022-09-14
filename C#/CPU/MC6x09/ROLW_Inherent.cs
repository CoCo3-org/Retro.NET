using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ROLW_Inherent : Instruction, IInherent 
	{
		public ROLW_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x59; } }

		public override string Mnemonic { get { return "ROLW"; } }
	}
}
