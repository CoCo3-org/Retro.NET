using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class PULX_Inherent : Instruction, IInherent
	{
		public PULX_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x38; } }

		public override string Mnemonic { get { return "PULX"; } }
	}
}
