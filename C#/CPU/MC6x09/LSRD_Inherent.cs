using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LSRD_Inherent : Instruction, IInherent 
	{
		public LSRD_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x44; } }

		public override string Mnemonic { get { return "LSRD"; } }
	}
}
