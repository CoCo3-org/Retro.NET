﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ANDD_Instruction : Instruction 
	{
		public ANDD_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "ANDD"; } }
	}
}