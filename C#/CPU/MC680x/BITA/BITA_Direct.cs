﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BITA_Direct : BITA_Instruction, IDirect
	{
		public BITA_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x95; } }
	}
}