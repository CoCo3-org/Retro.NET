using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class CLRW_Inherent : Instruction, IInherent 
	{
		public CLRW_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x5F; } }

		public override string Mnemonic { get { return "CLRW"; } }
	}
}
