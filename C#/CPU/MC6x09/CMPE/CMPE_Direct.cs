﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class CMPE_Direct : CMPE_Instruction, IDirect 
	{
		public CMPE_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x91; } }
	}
}