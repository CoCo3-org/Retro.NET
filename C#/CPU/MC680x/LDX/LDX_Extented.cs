using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class LDX_Extended : LDX_Instruction, IExtended
	{
		public LDX_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xFE; } }
	}
}
