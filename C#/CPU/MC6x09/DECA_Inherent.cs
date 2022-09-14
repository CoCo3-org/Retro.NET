using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class DECA_Inherent : Instruction, IInherent 
	{
		public DECA_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x4A; } }

		public override string Mnemonic { get { return "DECA"; } }
	}
}
