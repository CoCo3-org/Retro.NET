using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class MUL_Inherent : Instruction, IInherent 
	{
		public MUL_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x3D; } }

		public override string Mnemonic { get { return "MUL"; } }
	}
}
