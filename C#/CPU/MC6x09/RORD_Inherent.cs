using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class RORD_Inherent : Instruction, IInherent 
	{
		public RORD_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x46; } }

		public override string Mnemonic { get { return "RORD"; } }
	}
}
