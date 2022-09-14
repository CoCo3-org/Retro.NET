using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SUBD_Indexed : SUBD_Instruction, IIndexed
	{
		public SUBD_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xA3; } }
	}
}
