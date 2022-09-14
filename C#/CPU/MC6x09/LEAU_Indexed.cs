using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LEAU_Indexed : Instruction, IIndexed 
	{
		public LEAU_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x33; } }

		public override string Mnemonic { get { return "LEAU"; } }
	}
}
