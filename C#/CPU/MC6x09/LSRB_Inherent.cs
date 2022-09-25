﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LSRB_Inherent : Instruction, IInherent 
	{
		public LSRB_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x54; } }

		public override string Mnemonic { get { return "LSRB"; } }
	}
}