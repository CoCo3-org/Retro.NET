﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ASRB_Inherent : Instruction, IInherent
	{
		public ASRB_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x57; } }

		public override string Mnemonic { get { return "ASRB"; } }
	}
}