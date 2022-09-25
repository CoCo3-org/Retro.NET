﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ASLB_Inherent : Instruction, IInherent
	{
		public ASLB_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x58; } }

		public override string Mnemonic { get { return "ASLB"; } }
	}
}