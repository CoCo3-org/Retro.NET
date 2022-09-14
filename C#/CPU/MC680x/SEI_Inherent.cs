using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SEI_Inherent : Instruction, IInherent
	{
		public SEI_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x0F; } }

		public override string Mnemonic { get { return "SEI"; } }
	}
}
