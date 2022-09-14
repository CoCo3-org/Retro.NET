using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LEAX_Indexed : Instruction, IIndexed 
	{
		public LEAX_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x30; } }

		public override string Mnemonic { get { return "LEAX"; } }
	}
}
