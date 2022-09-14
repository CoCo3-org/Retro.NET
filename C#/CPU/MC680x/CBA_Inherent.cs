using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class CBA_Inherent : Instruction, IInherent
	{
		public CBA_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x11; } }

		public override string Mnemonic { get { return "CBA"; } }
	}
}
