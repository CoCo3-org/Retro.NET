using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ANDR_Register : Instruction, IRegister 
	{
		public ANDR_Register(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x34; } }

		public override string Mnemonic { get { return "ANDR"; } }
	}
}
