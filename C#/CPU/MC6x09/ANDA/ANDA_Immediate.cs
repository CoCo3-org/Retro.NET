﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ANDA_Immediate : ANDA_Instruction, IImmediate 
	{
		public ANDA_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x84; } }
	}
}