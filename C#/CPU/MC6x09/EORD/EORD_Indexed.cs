using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class EORD_Indexed : EORD_Instruction, IIndexed 
	{
		public EORD_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0xA8; } }
	}
}
