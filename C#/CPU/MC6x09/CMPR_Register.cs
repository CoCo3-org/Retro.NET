using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class CMPR_Register : Instruction, IRegister 
	{
		public CMPR_Register(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x37; } }

		public override string Mnemonic { get { return "CMPR"; } }
	}
}
