using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDE_Indexed : LDE_Instruction, IIndexed 
	{
		public LDE_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0xA6; } }
	}
}
