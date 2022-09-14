using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class DES_Inherent : Instruction, IInherent
	{
		public DES_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x34; } }

		public override string Mnemonic { get { return "DES"; } }
	}
}
