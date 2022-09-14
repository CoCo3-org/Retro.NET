using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LEAS_Indexed : Instruction, IIndexed 
	{
		public LEAS_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x32; } }

		public override string Mnemonic { get { return "LEAS"; } }
	}
}
