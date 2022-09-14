using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ASRA_Inherent : Instruction, IInherent
	{
		public ASRA_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x47; } }

		public override string Mnemonic { get { return "ASRA"; } }
	}
}
