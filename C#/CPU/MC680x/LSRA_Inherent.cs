using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class LSRA_Inherent : Instruction, IInherent
	{
		public LSRA_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x44; } }

		public override string Mnemonic { get { return "LSRA"; } }
	}
}
