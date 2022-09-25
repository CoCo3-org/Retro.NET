﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ASLA_Inherent : Instruction, IInherent
	{
		public ASLA_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x48; } }

		public override string Mnemonic { get { return "ASLA"; } }
	}
}