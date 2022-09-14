using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class PSHUW_Register : Instruction, IRegister 
	{
		public PSHUW_Register(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x3A; } }

		public override string Mnemonic { get { return "PSHUW"; } }
	}
}
