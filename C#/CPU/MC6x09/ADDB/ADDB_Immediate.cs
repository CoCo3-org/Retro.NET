using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ADDB_Immediate : ADDB_Instruction, IImmediate 
	{
		public ADDB_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xCB; } }
	}
}
