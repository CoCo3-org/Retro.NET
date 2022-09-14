using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ASRD_Inherent : Instruction, IInherent 
	{
		public ASRD_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x47; } }

		public override string Mnemonic { get { return "ASRD"; } }
	}
}
